Shader "Custom/EmberGlow"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1, 0.4, 0, 1)
        _ColorVariance ("Color Variance", Range(0, 1)) = 0.2
        _EmissionStrength ("Emission Strength", Float) = 5.0
        [HDR]_EmissionColor ("Emission Color", Color) = (1, 0.4, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend One One
        ZWrite Off
        Cull Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _BaseColor;
            float _ColorVariance;
            float _EmissionStrength;
            float4 _EmissionColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                // Add color randomness using UV as seed
                float random = frac(sin(dot(v.uv, float2(12.9898, 78.233))) * 43758.5453);
                float4 variance = float4(random, random * 0.5, random * 0.3, 0) * _ColorVariance;
                o.color = _BaseColor + variance;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 texColor = tex2D(_MainTex, i.uv);
                float4 finalColor = i.color * texColor;

                // Emission
                float4 emission = finalColor * _EmissionColor * _EmissionStrength;
                return emission;
            }
            ENDCG
        }
    }
}
