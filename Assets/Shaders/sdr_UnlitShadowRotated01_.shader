// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-8982-OUT;n:type:ShaderForge.SFN_Color,id:209,x:31427,y:32556,ptovrint:False,ptlb:Color_copy,ptin:_Color_copy,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_LightAttenuation,id:6960,x:31839,y:32679,varname:node_6960,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:6146,x:31427,y:32724,ptovrint:False,ptlb:Multiplier,ptin:_Multiplier,varname:node_514,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:5459,x:31427,y:32360,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_2880,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dfe41b1d2342d754f895c42db7ab70cd,ntxv:0,isnm:False|UVIN-729-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3477,x:31680,y:32428,varname:node_3477,prsc:2|A-5459-RGB,B-209-RGB;n:type:ShaderForge.SFN_Multiply,id:8996,x:31906,y:32463,varname:node_8996,prsc:2|A-3477-OUT,B-6146-OUT;n:type:ShaderForge.SFN_Multiply,id:8982,x:32258,y:32701,varname:node_8982,prsc:2|A-8996-OUT,B-6960-OUT;n:type:ShaderForge.SFN_Rotator,id:729,x:31124,y:32250,varname:node_729,prsc:2|UVIN-1307-UVOUT,SPD-1965-OUT;n:type:ShaderForge.SFN_TexCoord,id:1307,x:30813,y:32224,varname:node_1307,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:1965,x:30958,y:32518,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_1965,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-2;proporder:209-6146-5459-1965;pass:END;sub:END;*/

Shader "Shader Forge/sdr_UnlitShadowRotated01_" {
    Properties {
        _Color_copy ("Color_copy", Color) = (1,1,1,1)
        _Multiplier ("Multiplier", Float ) = 1
        _Texture ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float ) = -2
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
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color_copy;
            uniform float _Multiplier;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                LIGHTING_COORDS(1,2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 node_4126 = _Time;
                float node_729_ang = node_4126.g;
                float node_729_spd = _Speed;
                float node_729_cos = cos(node_729_spd*node_729_ang);
                float node_729_sin = sin(node_729_spd*node_729_ang);
                float2 node_729_piv = float2(0.5,0.5);
                float2 node_729 = (mul(i.uv0-node_729_piv,float2x2( node_729_cos, -node_729_sin, node_729_sin, node_729_cos))+node_729_piv);
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_729, _Texture));
                float3 emissive = (((_Texture_var.rgb*_Color_copy.rgb)*_Multiplier)*attenuation);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color_copy;
            uniform float _Multiplier;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                LIGHTING_COORDS(1,2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 finalColor = 0;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
