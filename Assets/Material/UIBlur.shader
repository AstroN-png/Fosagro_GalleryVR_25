Shader "UI/BlurEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlurSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 texelSize = _MainTex_TexelSize.xy * _BlurSize;

                fixed4 col = fixed4(0,0,0,0);
                col += tex2D(_MainTex, uv + texelSize * float2(-1.0, -1.0)) * 0.0625;
                col += tex2D(_MainTex, uv + texelSize * float2( 0.0, -1.0)) * 0.125;
                col += tex2D(_MainTex, uv + texelSize * float2( 1.0, -1.0)) * 0.0625;

                col += tex2D(_MainTex, uv + texelSize * float2(-1.0,  0.0)) * 0.125;
                col += tex2D(_MainTex, uv + texelSize * float2( 0.0,  0.0)) * 0.25;
                col += tex2D(_MainTex, uv + texelSize * float2( 1.0,  0.0)) * 0.125;

                col += tex2D(_MainTex, uv + texelSize * float2(-1.0,  1.0)) * 0.0625;
                col += tex2D(_MainTex, uv + texelSize * float2( 0.0,  1.0)) * 0.125;
                col += tex2D(_MainTex, uv + texelSize * float2( 1.0,  1.0)) * 0.0625;

                return col;
            }
            ENDCG
        }
    }
}

