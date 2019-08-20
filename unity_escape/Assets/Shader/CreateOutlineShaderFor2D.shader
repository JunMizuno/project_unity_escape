// @memo. 画像に対しての縁取り(特に2D)
Shader "Unlit/CreateOutlineShaderFor2D"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("MainColor", Color) = (1, 1, 1, 0.5)
        _Width ("TextureWidth", Float) = 200.0
        _Height ("TextureHeight", Float) = 200.0
        _Thick ("LineThickness", Range(0, 20)) = 10
    }

    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="False"
        }
        
        Pass
        {
            // @memo. 種類はUnityのマニュアル参照
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _Color;
            float _Width;
            float _Height;
            int _Thick;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // その時のピクセルの周囲の透明度の最大値を調べる
                float alphaMax = 0.0f;
                for (int x = -_Thick; x <= _Thick; x++)
                {
                    for (int y = -_Thick; y <= _Thick; y++)
                    {
                        float alpha = tex2D(_MainTex, i.uv + float2(x / _Width, y / _Height)).a;
                        if (alpha > alphaMax)
                        {
                            alphaMax = alpha;
                        }
                    }
                }

                // その時のピクセルが透明の場合、テクスチャの縁に当たると判断する
                if (col.a < 0.5)
                {
                    return float4(1, 1, 1, alphaMax) * _Color;
                }
                
                return col;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
