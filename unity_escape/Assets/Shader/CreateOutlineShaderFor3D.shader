// @memo. 3Dモデル全体に対しての縁取り
// @todo. 現時点で四角形の物体には使えないので要対応
Shader "Unlit/CreateOutlineShaderFor3D"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineSize ("OutlineSize", Range(0, 0.08)) = 0.0
        _OutlineColor ("Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Pass
        {
            Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _OutlineSize;
            float4 _OutlineColor;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex += float4(v.normal * _OutlineSize, 0);   
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }

            ENDCG
        }

        Pass
        {
            Cull Back

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;

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
                return col;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
