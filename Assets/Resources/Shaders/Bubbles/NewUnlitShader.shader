// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/NewUnlitShader"
{
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Color("Color (RGBA)", Color) = (1, 1, 1, 1)     
            
           _Opacity("Opacity", Range(0,1)) = 0.5
           _AnimSpeedX("Anim Speed (X)", Range(0,4)) = 1.3
           _AnimSpeedY("Anim Speed (Y)", Range(0,4)) = 2.7
           _AnimScale("Anim Scale", Range(0,1)) = 0.03
           _AnimTiling("Anim Tiling", Range(0,20)) = 8
         
    }


    SubShader{
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull front   
        LOD 100



        Pass {
            CGPROGRAM
                #pragma vertex vert alpha
                #pragma fragment frag alpha

                #include "UnityCG.cginc"
             

                // Structs (types)

                struct appdata
                {
                   float4 vertex : POSITION;
                   float2 uv : TEXCOORD0;
                   float3 normal : NORMAL;
                };

                struct v2f
                {
                   float2 uv : TEXCOORD0;
                   UNITY_FOG_COORDS(1)
                   float4 vertex : SV_POSITION;
                };

              

                // Registry variables

                sampler2D _MainTex;
                sampler2D _NoiseTex;

                float4 _MainTex_ST;
                float4 _Color;
                half _Opacity;
                float _AnimSpeedX;
                float _AnimSpeedY;
                float _AnimScale;
                float _AnimTiling;


                // Functions

                   

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
           
                    v.uv.x = 1 - v.uv.x;
                    
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    

                    i.uv.x += sin((i.uv.x + i.uv.y) * _AnimTiling + _Time.y * _AnimSpeedX) * _AnimScale;
                    i.uv.y += cos((i.uv.x - i.uv.y) * _AnimTiling + _Time.y * _AnimSpeedY) * _AnimScale;

                    fixed4 col = tex2D(_MainTex, i.uv) * _Color; 
                    col.a = _Opacity;

                    return col;
                }
            ENDCG
        }
    }
}
