Shader "Unlit/Billboard"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EmissionMap ("Emission Map", 2D) = "black" {}
        [HDR] _EmissionColor ("Emission Color", Color) = (0,0,0)
        _SinSpeed("SinSpeed",Float) = 0
    }
    SubShader
    {
        Tags
        { 
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True" 
            "DisableBatching" = "True"
        }

        Pass
        {
            // 透過PNGを使えるようになるやつ
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile _BILLBOARD_Y_AXIS

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;     // テクスチャ
            float4 _MainTex_ST;     // テクスチャのスケールとオフセット
            sampler2D _EmissionMap; // 光らせたいところを色くしたテクスチャ
            float4 _EmissionColor;  // 光の色と強さ
            float _SinSpeed;
            
            v2f vert (appdata v)
            {
                v2f o;
                // Meshの原点をModelView変換
                float3 viewPos = UnityObjectToViewPos(float3(0, 0, 0));
                
                // スケールと回転（平行移動なし）だけModel変換して、View変換はスキップ
                float3 scaleRotatePos = mul((float3x3)unity_ObjectToWorld, v.vertex);                
                
                // View行列から回転行列のY軸成分のみを抽出
                float3x3 ViewRotateY = float3x3(
                    1, UNITY_MATRIX_V._m01, 0,
                    0, UNITY_MATRIX_V._m11, 0,
                    0, UNITY_MATRIX_V._m21, -1// Zの符号を反転して右手系に変換
                );
                viewPos += mul(ViewRotateY, scaleRotatePos);
                
                o.vertex = mul(UNITY_MATRIX_P, float4(viewPos, 1));

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 albedo = (tex2D(_MainTex, i.uv)) ;
                
                float4 emission = tex2D(_EmissionMap,i.uv) * _EmissionColor;
                // emission += (2*_SinTime) + 1.0;

                UNITY_APPLY_FOG(i.fogCoord, albedo);

                return albedo * emission;
            }
            ENDCG
        }
    }
}