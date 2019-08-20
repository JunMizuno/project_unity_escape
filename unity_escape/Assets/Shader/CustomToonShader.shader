Shader "Unlit/CustomToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                half nl = max(0, dot(i.normal, _WorldSpaceLightPos0.xyz));
                if (nl <= 0.01f)
                {
                    nl = 0.1f;
                }
                else if (nl <= 0.3f)
                {
                    nl = 0.3f;
                }
                else
                {
                    nl = 1.0f;
                }
                fixed4 diffCol = fixed4(nl, nl, nl, 1);
                return col * diffCol;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
