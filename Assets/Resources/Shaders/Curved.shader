Shader "Custom/Curved" {
	Properties {
		_Color("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo", 2D) = "white" {}
		_QOffset ("Offset", Vector) = (-10,-10,0,0)
		_Dist ("Distance", Float) = 100.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog  
			#include "UnityCG.cginc"

            sampler2D _MainTex;
			float4 _QOffset;
			float _Dist;
			float fogCoord;
			
			struct v2f {

			    float4 pos : SV_POSITION;
			    float4 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
			};

			v2f vert (appdata_base v)
			{
			    v2f o;
			    float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
			    float zOff = vPos.z/_Dist;
			    vPos += _QOffset*zOff*zOff;
			    o.pos = mul (UNITY_MATRIX_P, vPos);
			    o.uv = v.texcoord;
				UNITY_TRANSFER_FOG(o, o.pos);
			    return o;
			}

			half4 frag (v2f i) : COLOR
			{
			    half4 col = tex2D(_MainTex, i.uv.xy);
				UNITY_APPLY_FOG(i.fogCoord, col);
			    return col;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}