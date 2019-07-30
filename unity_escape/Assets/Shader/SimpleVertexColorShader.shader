// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SimpleVertexColorShader"
{
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

            struct appdata
            {
                float4 vertex : POSITION;
                fixed3 color : COLOR0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed3 color : COLOR0;
            };

            float _ColorControlValue;
            
            v2f vert (appdata v)
            {
                v2f o;
                // @memo. mul(UNITY_MATRIX_MVP, v.vertex); で記述していましたが、一度処置通すと以下のように変換されます
                o.vertex = UnityObjectToClipPos(v.vertex);

                // @memo. colorは0.0-1.0の間で指定
                o.color.r = v.color.r * _ColorControlValue;
                o.color.g = v.color.g * _ColorControlValue;
                o.color.b = v.color.b * _ColorControlValue;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.color, 1);
            }
            ENDCG
        }
    }
}
