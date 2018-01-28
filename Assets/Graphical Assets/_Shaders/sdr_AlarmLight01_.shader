// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-8411-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32105,y:32411,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:3449,x:31746,y:32867,varname:node_3449,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:6029,x:32028,y:32892,varname:node_6029,prsc:2,spu:1,spv:0|UVIN-3449-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9605,x:32105,y:32217,ptovrint:False,ptlb:node_9605,ptin:_node_9605,varname:node_9605,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f1b536ddf0d797a4a8296e71c053d2f5,ntxv:0,isnm:False|UVIN-6029-UVOUT;n:type:ShaderForge.SFN_Multiply,id:8411,x:32348,y:32345,varname:node_8411,prsc:2|A-9605-RGB,B-7241-RGB,C-8661-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8661,x:32105,y:32610,ptovrint:False,ptlb:node_8661,ptin:_node_8661,varname:node_8661,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.2;proporder:7241-9605-8661;pass:END;sub:END;*/

Shader "Shader Forge/sdr_AlarmLight01_" {
    Properties {
        _Color ("Color", Color) = (1,0,0,1)
        _node_9605 ("node_9605", 2D) = "white" {}
        _node_8661 ("node_8661", Float ) = 1.2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _node_9605; uniform float4 _node_9605_ST;
            uniform float _node_8661;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_81 = _Time;
                float2 node_6029 = (i.uv0+node_81.g*float2(1,0));
                float4 _node_9605_var = tex2D(_node_9605,TRANSFORM_TEX(node_6029, _node_9605));
                float3 emissive = (_node_9605_var.rgb*_Color.rgb*_node_8661);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
