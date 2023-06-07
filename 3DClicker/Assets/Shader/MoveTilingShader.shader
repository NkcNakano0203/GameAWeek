Shader "Gradation/Tiling&GradationShader" 
{
    Properties
    {
        _MainTex("Texture",2D)="white"{}
        // スクロール方向と速度
        _MoveVector("DirectionAndSpeed",vector)= (1,1,0,0)

        _Radian("Rotate", float) = 0.0
    }

    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque" 
            "IgnoreProjector" = "True"
            "Queue" = "Transparent"
        }

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _MoveVector;

            float _Radian;

            struct appdata
            {
                half4 vertex : POSITION;
                half2 uv : TEXCOORD0;
            };

            struct v2f
            {
                half4 vertex : POSITION;
                half2 uv : TEXCOORD0;
            };

            v2f vert(appdata v) 
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            
            float2x2 rot(float rad)
            {
                return float2x2(cos(rad), sin(rad), -sin(rad), cos(rad));
            }

            fixed4 frag(v2f i) : SV_Target
            {

                // スクロール計算
                i.uv.x +=_MoveVector.x*_Time;
                i.uv.y +=_MoveVector.y*_Time;
                i.uv.x *= 16./9.;

                float1x2 v = float1x2(i.uv.x, i.uv.y);
                float2 uv = mul(v, rot(_Radian));

                // i.uvの適用
                fixed4 col = tex2D(_MainTex, uv);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;            
            }
            ENDCG
        }
    }
}