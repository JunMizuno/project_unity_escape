// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SimpleTextureShader"
{
    // @memo. インスペクター上から設定できるようにする
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SubTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        // @memo. 描画順などの設定
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            // @memo. アルファブレンディングする為には記述が必要
            Blend SrcAlpha OneMinusSrcAlpha
            //Blend Off

            // @memo. 頂点シェーダーとフラグメントシェーダーの宣言
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // @memo. プロパティでセットされたテクスチャが格納される(変数名を合わせること必須)
            sampler2D _MainTex;
            sampler2D _SubTex;

            // @memo. 参照している記述は無いが、UnityCG.cgincで使用されている
            float4 _MainTex_ST;
            float4 _SubTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                fixed3 color : COLOR0;
                // @memo. テクスチャのUV座標(複数テクスチャにより末尾が連番になることもある)
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed3 color : COLOR0;
                // @memo. 同上
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv1 = TRANSFORM_TEX(v.uv1, _MainTex);

                o.uv2 = TRANSFORM_TEX(v.uv2, _SubTex);

                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainTexCol = tex2D(_MainTex, i.uv1);
                fixed4 subTexCol = tex2D(_SubTex, i.uv2);
                // @memo. 頂点カラーに対してテクスチャのカラーを乗算
                // @memo. もし乗算が意図したものにならない場合はテクスチャ自体の設定が原因
                //fixed4 o = fixed4(i.color, 1) * mainTexCol * subTexCol;

                if (subTexCol.r > 0.1) {
                    return fixed4(0, 0, 0, 0);
                }
                               
                fixed4 o = fixed4(i.color, 1) * mainTexCol;
                return o;
            }
            
            ENDCG
        }
    }
}
