// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/BasicBumpMap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalTex ("Texture", 2D) = "white" {}
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

            uniform sampler2D _MainTex;
            uniform sampler2D _NormalTex;

            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float3 tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 light : COLOR1;
            };
            
            float4x4 InvTangentMatrix(float3 tangent, float3 binormal, float3 normal)
            {
                // 接空間行列
                float4x4 mat = float4x4
                (
                    float4(tangent, 0.0f),
                    float4(binormal, 0.0f),
                    float4(normal, 0.0f),
                    float4(0.0f, 0.0f, 0.0f, 1.0f)
                );

                // 転置
                return transpose(mat);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                float3 normal = normalize(v.normal);
                float3 tangent = normalize(v.tangent);
                float3 binormal = cross(normal, tangent);

                o.light = mul(mul(unity_WorldToObject, _WorldSpaceLightPos0), InvTangentMatrix(tangent, binormal, normal));

                return o;
            }

            fixed4 frag (v2f i) : COLOR
            {
                float3 normal = float4(UnpackNormal(tex2D(_NormalTex, i.uv)), 1);
                float3 lightvec = normalize(i.light.xyz);
                float diffuse = max(0, dot(normal, lightvec));
                return diffuse * tex2D(_MainTex, i.uv);
            }

            ENDCG
        }
    }
}
