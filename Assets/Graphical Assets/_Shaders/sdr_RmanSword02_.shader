// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT,alpha-2478-A;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-797-RGB,B-9758-OUT;n:type:ShaderForge.SFN_Color,id:797,x:32232,y:32719,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.9172413,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:6593,x:31460,y:33226,varname:node_6593,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:1269,x:31677,y:33226,varname:node_1269,prsc:2,spu:-10,spv:0|UVIN-6593-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2478,x:32217,y:33141,ptovrint:False,ptlb:node_2478,ptin:_node_2478,varname:node_2478,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:45d9e013b9c8869459fc6d6f878af6e4,ntxv:0,isnm:False|UVIN-1269-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:9758,x:32232,y:32921,ptovrint:False,ptlb:Multiplier,ptin:_Multiplier,varname:node_9758,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;proporder:797-2478-9758;pass:END;sub:END;*/

Shader "Shader Forge/sdr_RmanSword02_" {
    Properties {
        _TintColor ("Color", Color) = (0,0.9172413,1,1)
        _node_2478 ("node_2478", 2D) = "white" {}
        _Multiplier ("Multiplier", Float ) = 2
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TintColor;
            uniform sampler2D _node_2478; uniform float4 _node_2478_ST;
            uniform float _Multiplier;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float3 emissive = (_TintColor.rgb*_Multiplier);
                float3 finalColor = emissive;
                float4 node_1535 = _Time;
                float2 node_1269 = (i.uv0+node_1535.g*float2(-10,0));
                float4 _node_2478_var = tex2D(_node_2478,TRANSFORM_TEX(node_1269, _node_2478));
                fixed4 finalRGBA = fixed4(finalColor,_node_2478_var.a);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
