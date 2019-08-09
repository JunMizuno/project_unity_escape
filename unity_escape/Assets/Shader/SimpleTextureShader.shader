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
                float3 normal : NORMAL;
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
                
                // @memo. 頂点をコントロール
                /*
                float x = v.vertex.x + frac(_Time.y) * v.normal.x;
                float y = v.vertex.y + frac(_Time.y) * v.normal.y;
                float z = v.vertex.z + frac(_Time.y) * v.normal.z;
                float4 vertexPos = float4(float3(x, y, z), v.vertex.w);
                o.vertex = UnityObjectToClipPos(vertexPos);
                */

                // @memo. _MainTexの描画をスクロールする処理
                //o.uv1 = TRANSFORM_TEX(float2(v.uv1.x + clamp(frac(_Time.y), 0.0, 1.0), v.uv1.y), _MainTex);
                // @memo. _SubTexを利用して描画範囲をスクロールする処理
                // @memo. フラグメントのマスク処理と併用することで効果が出る
                //o.uv2 = TRANSFORM_TEX(float2(v.uv2.x + clamp(frac(_Time.y / 4), 0.0, 1.0), v.uv2.y), _SubTex);

                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // @memo. 引数にfloat4 sp:WPOSを持つこともできる(セマンティック注意)
                // @memo. _ScreenParams.xyで割ることで、スクリーン左下(0, 0)、右上(1, 1)になる
                //float2 convertPos = i.vertex.xy / _ScreenParams.xy;

                fixed4 mainTexCol = tex2D(_MainTex, i.uv1);
                fixed4 subTexCol = tex2D(_SubTex, i.uv2);
                // @memo. 頂点カラーに対してテクスチャのカラーを乗算
                // @memo. もし乗算が意図したものにならない場合はテクスチャ自体の設定が原因
                //fixed4 o = fixed4(i.color, 1) * mainTexCol * subTexCol;

                // @memo. _SubTexを利用して描画にマスクを掛ける処理
                return subTexCol.r > 0.1 ? fixed4(0, 0, 0, 0) : fixed4(i.color, 1) * mainTexCol;
                
                //return fixed4(i.color, 1) * mainTexCol;
                //return fixed4(i.color, 1) * subTexCol;
            }
            
            ENDCG
        }
    }
}
