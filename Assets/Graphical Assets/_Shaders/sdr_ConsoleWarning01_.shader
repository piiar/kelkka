// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-5112-OUT;n:type:ShaderForge.SFN_Tex2d,id:7071,x:32302,y:33017,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_7071,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:227f08531c42f0f429d73d4422a245a4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5112,x:32514,y:33117,varname:node_5112,prsc:2|A-7071-RGB,B-3022-OUT;n:type:ShaderForge.SFN_Time,id:7491,x:31255,y:33111,varname:node_7491,prsc:2;n:type:ShaderForge.SFN_Frac,id:28,x:31636,y:33204,varname:node_28,prsc:2|IN-3644-OUT;n:type:ShaderForge.SFN_RemapRange,id:6910,x:31891,y:33204,varname:node_6910,prsc:2,frmn:0,frmx:1,tomn:1,tomx:-1|IN-28-OUT;n:type:ShaderForge.SFN_Multiply,id:3644,x:31416,y:33217,varname:node_3644,prsc:2|A-7491-TSL,B-7391-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7391,x:31255,y:33261,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_7391,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Abs,id:5584,x:32091,y:33204,varname:node_5584,prsc:2|IN-6910-OUT;n:type:ShaderForge.SFN_RemapRange,id:3022,x:32302,y:33204,varname:node_3022,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1.7|IN-5584-OUT;proporder:7071-7391;pass:END;sub:END;*/

Shader "Shader Forge/sdr_ConsoleWarning01_" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float ) = 10
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
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Speed;
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
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float4 node_7491 = _Time;
                float3 emissive = (_Texture_var.rgb*(abs((frac((node_7491.r*_Speed))*-2.0+1.0))*1.2+0.5));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
