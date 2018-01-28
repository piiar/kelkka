// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32870,y:32729,varname:node_3138,prsc:2|emission-4838-OUT,alpha-4915-A;n:type:ShaderForge.SFN_TexCoord,id:9801,x:31278,y:32561,varname:node_9801,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:4915,x:32218,y:32828,ptovrint:False,ptlb:MainTex_,ptin:_MainTex_,varname:node_4915,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:61353d94f75a53845b2887418144e8c6,ntxv:0,isnm:False|UVIN-3150-UVOUT;n:type:ShaderForge.SFN_Panner,id:3150,x:32031,y:32828,varname:node_3150,prsc:2,spu:0,spv:0.1|UVIN-9801-UVOUT,DIST-4067-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5763,x:31116,y:32848,varname:node_5763,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3117-UVOUT;n:type:ShaderForge.SFN_Multiply,id:884,x:31279,y:32848,varname:node_884,prsc:2|A-5763-OUT,B-5955-OUT;n:type:ShaderForge.SFN_Sin,id:5269,x:31439,y:32848,varname:node_5269,prsc:2|IN-884-OUT;n:type:ShaderForge.SFN_Slider,id:5955,x:30959,y:33046,ptovrint:False,ptlb:Frequency,ptin:_Frequency,varname:node_5955,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:18.25948,max:100;n:type:ShaderForge.SFN_RemapRange,id:6172,x:31596,y:32848,varname:node_6172,prsc:2,frmn:-1,frmx:1,tomn:0.5,tomx:0.9|IN-5269-OUT;n:type:ShaderForge.SFN_Panner,id:3117,x:30959,y:32848,varname:node_3117,prsc:2,spu:-5,spv:-1|UVIN-9801-UVOUT;n:type:ShaderForge.SFN_Power,id:4067,x:31752,y:32848,varname:node_4067,prsc:2|VAL-6172-OUT,EXP-5649-OUT;n:type:ShaderForge.SFN_Slider,id:5649,x:31439,y:33045,ptovrint:False,ptlb:Power,ptin:_Power,varname:node_5649,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5229529,max:10;n:type:ShaderForge.SFN_Multiply,id:4838,x:32467,y:32686,varname:node_4838,prsc:2|A-7800-OUT,B-7784-RGB,C-4915-RGB;n:type:ShaderForge.SFN_Color,id:7784,x:32218,y:32662,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7784,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Slider,id:7800,x:32061,y:32562,ptovrint:False,ptlb:Multiply,ptin:_Multiply,varname:node_7800,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;proporder:4915-7784-7800-5955-5649;pass:END;sub:END;*/

Shader "Shader Forge/sdr_AnimatedDistortion" {
    Properties {
        _MainTex_ ("MainTex_", 2D) = "white" {}
        _Color ("Color", Color) = (1,0,0,1)
        _Multiply ("Multiply", Range(0, 2)) = 1
        _Frequency ("Frequency", Range(0, 100)) = 18.25948
        _Power ("Power", Range(0, 10)) = 0.5229529
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _MainTex_; uniform float4 _MainTex__ST;
            uniform float _Frequency;
            uniform float _Power;
            uniform float4 _Color;
            uniform float _Multiply;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_761 = _Time;
                float2 node_3150 = (i.uv0+pow((sin(((i.uv0+node_761.g*float2(-5,-1)).r*_Frequency))*0.2+0.7),_Power)*float2(0,0.1));
                float4 _MainTex__var = tex2D(_MainTex_,TRANSFORM_TEX(node_3150, _MainTex_));
                float3 emissive = (_Multiply*_Color.rgb*_MainTex__var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,_MainTex__var.a);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
