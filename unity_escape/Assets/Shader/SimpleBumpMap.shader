Shader "Unlit/SimpleBumpMap"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _BumpMap ("BumpMap", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _BumpMap;
            fixed4 _Color;

            // 接空間へ変換する行列を求める
            // 接空間からローカル空間への変換の逆行列で、ローカル空間のライトを接空間に変換する
            float4x4 InvTangentMatrix(float3 tangent, float3 bin, float3 nor)
            {
                float4x4 mat = float4x4
                (
                    float4(tangent, 0),
                    float4(bin, 0),
                    float4(nor, 0),
                    float4(0, 0, 0, 1)
                );

                // @memo. 正規直交系行列なので、逆行列は転置行列で求まるとのこと
                return transpose(mat);
            }

            struct appdata
            {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 lightDir : TEXCOORD2;
            };

            v2f vert (appdata_full v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                // @memo. UnityCG.cgincをインクルードしてappdata_fullを使用する場合はv.texcoordでOK
                // @memo. TRANSFORM_TEX(v.uv, _MainTex)とする必要がなくなる
                o.uv = v.texcoord;

                // @todo. 以下詳細は後ほど調べる
                // ローカル空間上での接空間ベクトルの方向を求める
                float3 n = normalize(v.normal);
                float3 t = v.tangent;
                float3 b = cross(n, t);

                // ワールド位置にあるライトをローカル空間へ変換する
                float3 localLight = mul(unity_WorldToObject, _WorldSpaceLightPos0);

                // ローカルライトを接空間へ変換する(行列の掛ける順番に注意しなければならない)
                o.lightDir = mul(localLight, InvTangentMatrix(t, b, n));

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = float4(UnpackNormal(tex2D(_BumpMap, i.uv)), 1);
                float3 light = normalize(i.lightDir);
                float diff = max(0, dot(normal, light));
                return diff * _Color;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
