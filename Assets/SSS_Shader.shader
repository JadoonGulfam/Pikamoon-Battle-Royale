Shader "Custom/URP_SubsurfaceScattering"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _SubsurfaceColor ("Subsurface Color", Color) = (1, 0.5, 0.4, 1)
        _FresnelPower ("Fresnel Power", Float) = 3.0
        _SubsurfaceIntensity ("Subsurface Intensity", Float) = 0.5
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _NormalMap("Normal Map", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float2 uv : TEXCOORD2;
                float3 viewDir : TEXCOORD3;
            };

            sampler2D _BaseMap;
            sampler2D _NormalMap;

            float4 _BaseColor;
            float4 _SubsurfaceColor;
            float _FresnelPower;
            float _SubsurfaceIntensity;
            float _Smoothness;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;
                o.viewDir = normalize(UnityWorldSpaceViewDir(o.worldPos));
                return o;
            }

            float3 CalculateSubsurface(float3 worldNormal, float3 viewDir)
            {
                // Fresnel effect for rim lighting (simulates edge lighting)
                float fresnel = pow(1.0 - saturate(dot(viewDir, worldNormal)), _FresnelPower);
                
                // Calculate subsurface color using Fresnel and subsurface intensity
                float3 subsurface = _SubsurfaceColor.rgb * fresnel * _SubsurfaceIntensity;
                
                return subsurface;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Sample base color and normal map
                float3 baseColor = _BaseColor.rgb;
                float3 normal = normalize(UnpackNormal(tex2D(_NormalMap, i.uv)).rgb);
                
                // Calculate the world normal with tangent-space normal map
                float3 worldNormal = normalize(i.worldNormal + normal * 0.5);

                // Calculate subsurface effect
                float3 subsurface = CalculateSubsurface(worldNormal, i.viewDir);

                // Calculate lighting using URP's main light
                Light mainLight = GetMainLight(); // Use URP lighting functions
                float3 lightDir = normalize(mainLight.direction);
                float diff = saturate(dot(worldNormal, lightDir));

                float3 diffuse = baseColor * diff * mainLight.color;
                
                // Combine lighting with subsurface scattering
                float3 finalColor = diffuse + subsurface;

                return half4(finalColor, 1.0);
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
