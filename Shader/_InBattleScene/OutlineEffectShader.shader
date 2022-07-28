Shader "Unit/Selection"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline color", Color) = (1,1,1,1)
        _OutlineWidth ("Outline width", float) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off 
        //ZWrite Off 
        //ZTest Always
        tags{"RenderType"="Transparent" "Queue"="Transparent"}
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            fixed4 _OutlineColor;
            float _OutlineWidth;
            sampler2D _MainTex;

            static float d = 0.71;
            static float2 _dirs[8] = { float2(1,0), float2(-1,0), float2(0,1), float2(0,-1),
                float2(d,d), float2(-d,d), float2(d,-d), float2(-d,-d) };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            float GetMaxAlpha(float2 uv) 
            {
                float result = 0;
                for (uint i = 0; i < 8; i++)
                {
                    float2 sUV = uv +_dirs[i] * _OutlineWidth;
                    result = max(result, tex2D(_MainTex, sUV).a);
                }
                return result;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= i.color;
                col.rgb = lerp(_OutlineColor, col.rgb, col.a);
                col.a = GetMaxAlpha(i.uv);

                return col;
            }
            ENDCG
        }
    }
}
