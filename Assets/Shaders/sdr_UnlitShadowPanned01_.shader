// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-8067-OUT;n:type:ShaderForge.SFN_Color,id:8994,x:31457,y:32563,ptovrint:False,ptlb:Color_copy,ptin:_Color_copy,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_LightAttenuation,id:7220,x:31869,y:32686,varname:node_7220,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8678,x:31457,y:32731,ptovrint:False,ptlb:Multiplier,ptin:_Multiplier,varname:node_514,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:9541,x:31457,y:32371,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_2880,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c0ba76b9e3c57ec4e9c90f6239ef6490,ntxv:0,isnm:False|UVIN-5984-OUT;n:type:ShaderForge.SFN_Multiply,id:8900,x:31710,y:32435,varname:node_8900,prsc:2|A-9541-RGB,B-8994-RGB;n:type:ShaderForge.SFN_Multiply,id:8960,x:31936,y:32470,varname:node_8960,prsc:2|A-8900-OUT,B-8678-OUT;n:type:ShaderForge.SFN_Multiply,id:8067,x:32288,y:32708,varname:node_8067,prsc:2|A-8960-OUT,B-7220-OUT;n:type:ShaderForge.SFN_TexCoord,id:4093,x:31056,y:32386,varname:node_4093,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ToggleProperty,id:9847,x:31233,y:32758,ptovrint:False,ptlb:OnOff,ptin:_OnOff,varname:node_9847,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_ValueProperty,id:2176,x:30773,y:32741,ptovrint:False,ptlb:U Speed,ptin:_USpeed,varname:node_2176,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:8143,x:31001,y:32758,varname:node_8143,prsc:2|A-2176-OUT,B-7860-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7860,x:30773,y:32833,ptovrint:False,ptlb:V Speed,ptin:_VSpeed,varname:node_7860,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:6977,x:31056,y:32590,varname:node_6977,prsc:2|A-8387-T,B-8143-OUT;n:type:ShaderForge.SFN_Time,id:8387,x:30773,y:32572,varname:node_8387,prsc:2;n:type:ShaderForge.SFN_Add,id:5984,x:31257,y:32563,varname:node_5984,prsc:2|A-4093-UVOUT,B-6977-OUT;proporder:8994-8678-9541-9847-2176-7860;pass:END;sub:END;*/

Shader "Shader Forge/sdr_UnlitShadowPanned01_" {
    Properties {
        _Color_copy ("Color_copy", Color) = (1,1,1,1)
        _Multiplier ("Multiplier", Float ) = 1
        _Texture ("Texture", 2D) = "white" {}
        [MaterialToggle] _OnOff ("OnOff", Float ) = 0
        _USpeed ("U Speed", Float ) = 0
        _VSpeed ("V Speed", Float ) = 1
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
            uniform float _USpeed;
            uniform float _VSpeed;
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
                float4 node_8387 = _Time;
                float2 node_5984 = (i.uv0+(node_8387.g*float2(_USpeed,_VSpeed)));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_5984, _Texture));
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
            uniform float _USpeed;
            uniform float _VSpeed;
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
