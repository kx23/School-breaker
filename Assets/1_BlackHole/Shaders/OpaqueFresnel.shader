Shader "Custom/OpaqueFresnel" {
    Properties {
        _MainTexture ("MainTexture", 2D) = "white" {}
        _FresnelValue ("FresnelValue", Float ) = 1
        _FresnelPower ("FresnelPower", Float ) = 1
        _Color ("Color", Color) = (0,0,0,1)
        _FresnelIntensity ("FresnelIntensity", Float ) = 1.5

        _QOffset("Offset", Vector) = (-10,-10,0,0)
        _Dist("Distance", Float) = 100.0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _FresnelValue;
            uniform float _FresnelPower;
            uniform float4 _Color;
            uniform float _FresnelIntensity;
            float4 _QOffset;
            float _Dist;
            float fogCoord;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                float4 vPos = mul(UNITY_MATRIX_MV, v.vertex);
                float zOff = vPos.z / _Dist;
                vPos += _QOffset * zOff * zOff;

                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_P, vPos);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 emissive = (_Color.rgb+((i.vertexColor.rgb*pow(pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelValue),_FresnelPower))*_FresnelIntensity));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Standard"
    CustomEditor "ShaderForgeMaterialInspector"
}
