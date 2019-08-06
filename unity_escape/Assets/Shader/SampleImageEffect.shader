// @memo. ImageEffectとはレンダリング後の１枚画像に対して効果を付けること
// @memo. カメラと同じGameObjectにアタッチする(OnRenderImageで描画)

// フェードアウト
Shader "ImageEffect/SampleImageEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
    }

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // @memo. _Timeはビルトイン変数
                // @memo. _Time.yには純粋な経過時間が格納されている
                // @memo. ここでは_Time.yyyとすることで3要素(r,g,b)に対してそれを使用している
                fixed4 black = 1 - fixed4(_Time.yyy /2, 1);
                col *= black;
                return col;
            }

            ENDCG
        }
    }
}
