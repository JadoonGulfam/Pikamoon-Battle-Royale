Shader "Orbio/UltraProFXMaster_FINAL_V4"
{
    Properties
    {
        // Base
        _MainTex ("Main Texture", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _Transparency ("Transparency", Range(0,1)) = 1
        _Brightness ("Final Brightness", Range(0,5)) = 1

        // ✂️ Alpha Cutout
        [Toggle] _EnableCutout ("Enable Alpha Cutout", Float) = 0
        _AlphaThreshold ("Alpha Cut Threshold", Range(0,1)) = 0.5
        _AlphaEdgeExpand ("Edge Expand/Invert (-1=shrink,+1=expand)", Range(-1,1)) = 0.0

        // 🌟 Glow / Emission (EXTREME)
        [Toggle] _EnableGlow ("Enable Glow", Float) = 1
        _GlowColor ("Glow Color", Color) = (1,0.6,0.2,1)
        _GlowIntensity ("Glow Intensity", Range(0,1000)) = 3
        _GlowPulseSpeed ("Glow Pulse Speed", Range(0,10)) = 3
        [Toggle] _AdditiveBlend ("Additive Mode", Float) = 0

        // UV / Wave Motion
        [Toggle] _EnableWaves ("Enable Wave Motion", Float) = 1
        _WaveAmplitude ("Wave Amplitude", Range(0,1)) = 0.1
        _WaveFrequency ("Wave Frequency", Range(0,20)) = 5
        _WaveSpeed ("Wave Speed", Range(0,10)) = 2
        _UVScroll ("UV Scroll (X,Y)", Vector) = (0.1,0.1,0,0)
        _UVRotation ("UV Rotation Speed", Range(-10,10)) = 0.5

        // Noise
        [Toggle] _EnableNoise ("Enable Noise Flow", Float) = 1
        _NoiseScale ("Noise Scale", Range(0.1,10)) = 3
        _NoiseSpeed ("Noise Speed", Range(0,10)) = 2
        _NoiseIntensity ("Noise Intensity", Range(0,2)) = 0.4

        // Dissolve
        [Toggle] _EnableDissolve ("Enable Dissolve", Float) = 0
        _DissolveTex ("Dissolve Mask", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0,1)) = 0.2
        _DissolveEdgeColor ("Edge Color", Color) = (1,0.3,0,1)
        _DissolveEdgeWidth ("Edge Width", Range(0.01,0.5)) = 0.1

        // Gradient
        [Toggle] _EnableGradient ("Enable Color Gradient", Float) = 1
        _GradientColor ("Secondary Color", Color) = (0.3,0.7,1,1)
        _GradientBlend ("Gradient Blend", Range(0,1)) = 0.5
        _GradientSpeed ("Gradient Speed", Range(0,10)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 400

        Pass
        {
            Name "UltraProFX"
            ZWrite Off
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _DissolveTex;

            float4 _BaseColor;
            float _Transparency, _Brightness;

            float _EnableCutout, _AlphaThreshold, _AlphaEdgeExpand;

            float _EnableGlow, _GlowIntensity, _GlowPulseSpeed, _AdditiveBlend;
            float4 _GlowColor;

            float _EnableWaves, _WaveAmplitude, _WaveFrequency, _WaveSpeed;
            float4 _UVScroll;
            float _UVRotation;

            float _EnableNoise, _NoiseScale, _NoiseSpeed, _NoiseIntensity;

            float _EnableDissolve, _DissolveAmount, _DissolveEdgeWidth;
            float4 _DissolveEdgeColor;

            float _EnableGradient, _GradientBlend, _GradientSpeed;
            float4 _GradientColor;

            // --- Simple noise for distortion ---
            float hash(float2 p){ return frac(sin(dot(p,float2(127.1,311.7)))*43758.5453); }
            float noise(float2 p){
                float2 i=floor(p); float2 f=frac(p);
                float a=hash(i),b=hash(i+float2(1,0)),c=hash(i+float2(0,1)),d=hash(i+float2(1,1));
                float2 u=f*f*(3-2*f);
                return lerp(a,b,u.x)+(c-a)*u.y*(1-u.x)+(d-b)*u.x*u.y;
            }

            struct appdata { float4 vertex:POSITION; float2 uv:TEXCOORD0; };
            struct v2f { float4 pos:SV_POSITION; float2 uv:TEXCOORD0; };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float time = _Time.y + _SinTime.w * 2.0 + _CosTime.w * 3.0;
                float2 uv = i.uv;

                // ---- UV Animation ----
                uv += _UVScroll.xy * time;
                float s = sin(_UVRotation * time);
                float c = cos(_UVRotation * time);
                uv = mul(float2x2(c, -s, s, c), uv - 0.5) + 0.5;

                if (_EnableWaves > 0.5)
                    uv += sin(uv.y * _WaveFrequency + time * _WaveSpeed) * _WaveAmplitude;

                if (_EnableNoise > 0.5)
                {
                    float n = noise(uv * _NoiseScale + time * _NoiseSpeed);
                    uv += (n - 0.5) * _NoiseIntensity;
                }

                float4 col = tex2D(_MainTex, uv) * _BaseColor;
                float alpha = col.a * _Transparency;

                // ✂️ REAL UNITY-STYLE ALPHA CUTOUT
                if (_EnableCutout > 0.5)
                {
                    float texAlpha = col.a;
                    float vertexAlpha = 1.0; // you can pass vertex color alpha later if needed
                    float combinedAlpha = texAlpha * vertexAlpha * _Transparency;
                    combinedAlpha += _AlphaEdgeExpand * 0.5;

                    // discard like Unity particle shaders
                    if (combinedAlpha < _AlphaThreshold)
                        discard;

                    alpha = combinedAlpha;
                }

                // ---- Gradient Color ----
                if (_EnableGradient > 0.5)
                {
                    float g = sin(time * _GradientSpeed) * 0.5 + 0.5;
                    col.rgb = lerp(col.rgb, _GradientColor.rgb, g * _GradientBlend);
                }

                // ---- Glow ----
                if (_EnableGlow > 0.5)
                {
                    float pulse = 0.5 + 0.5 * sin(time * _GlowPulseSpeed);
                    col.rgb += _GlowColor.rgb * (_GlowIntensity * pulse);
                }

                // ---- Dissolve ----
                if (_EnableDissolve > 0.5)
                {
                    float mask = tex2D(_DissolveTex, uv).r;
                    float edge = smoothstep(_DissolveAmount - _DissolveEdgeWidth, _DissolveAmount, mask);
                    float dissolveAlpha = step(_DissolveAmount, mask);
                    float edgeGlow = (1 - edge) * step(mask, _DissolveAmount + _DissolveEdgeWidth);
                    col.rgb = lerp(col.rgb, _DissolveEdgeColor.rgb, edgeGlow);
                    alpha *= dissolveAlpha;
                }

                // ---- Additive Blend ----
                if (_AdditiveBlend > 0.5)
                    col.rgb = col.rgb * alpha + _GlowColor.rgb * _GlowIntensity;

                col.rgb *= _Brightness;
                col.a = alpha;
                return col;
            }
            ENDHLSL
        }
    }
    FallBack Off
}
