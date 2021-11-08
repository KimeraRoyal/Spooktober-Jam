Shader "Screen/Static"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Static1 ("Static Layer 1", 2D) = "white" {}
        _Static2 ("Static Layer 2", 2D) = "white" {}
        _Static3 ("Static Layer 3", 2D) = "white" {}
        _Static4 ("Static Layer 4", 2D) = "white" {}
        _Slope ("Colour Slope", 2D) = "white" {}
        _StaticSpeedU ("Static U Speed", Vector) = (1, 1, 1, 1)
        _StaticSpeedV ("Static V Speed", Vector) = (1, 1, 1, 1)
        _Amount ("Amount", Float) = 1.0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            sampler2D _MainTex;

            sampler2D _Static1;
            sampler2D _Static2;
            sampler2D _Static3;
            sampler2D _Static4;

            sampler2D _Slope;

            fixed4 _StaticSpeedU;
            fixed4 _StaticSpeedV;

            float _Amount;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                const float2 timeUV = float2(_Time.y, _Time.y);
                const fixed4 static1 = tex2D(_Static1, i.uv + timeUV * float2(_StaticSpeedU.x, _StaticSpeedV.x) * col.r + timeUV * float2(_StaticSpeedU.x, _StaticSpeedV.x));
                const fixed4 static2 = tex2D(_Static2, i.uv + timeUV * float2(_StaticSpeedU.y, _StaticSpeedV.y) * col.g * static1.r + timeUV * float2(_StaticSpeedU.y, _StaticSpeedV.y));
                const fixed4 static3 = tex2D(_Static3, i.uv + timeUV * float2(_StaticSpeedU.z, _StaticSpeedV.z) * col.b * static2.r + timeUV * float2(_StaticSpeedU.z, _StaticSpeedV.z));
                const fixed4 static4 = tex2D(_Static4, i.uv + timeUV * float2(_StaticSpeedU.w, _StaticSpeedV.w) * col.r * static3.r + timeUV * float2(_StaticSpeedU.w, _StaticSpeedV.w));

                fixed4 staticCol = (static1 + static2 + static3 + static4) * 0.25;
                const fixed4 slopedStatic = tex2D(_Slope, float2(staticCol.r, 0.5));
                
                return col * (1 - _Amount) + slopedStatic * _Amount;
            }
            ENDCG
        }
    }
}
