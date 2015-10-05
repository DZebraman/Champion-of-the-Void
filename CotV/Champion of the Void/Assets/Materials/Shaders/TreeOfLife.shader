// Shader created with Shader Forge v1.20 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.20;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4013,x:33127,y:32714,varname:node_4013,prsc:2|emission-8032-OUT,alpha-8547-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32156,y:32648,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:1827,x:32145,y:32835,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_1827,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:69fa957f00c96cf48ab14aa0ee4b6440,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8032,x:32386,y:32735,varname:node_8032,prsc:2|A-1304-RGB,B-1827-RGB;n:type:ShaderForge.SFN_Multiply,id:2090,x:32890,y:32505,varname:node_2090,prsc:2|A-5627-OUT,B-8032-OUT;n:type:ShaderForge.SFN_Sin,id:3534,x:32332,y:32471,varname:node_3534,prsc:2|IN-6102-OUT;n:type:ShaderForge.SFN_Time,id:8057,x:31860,y:32514,varname:node_8057,prsc:2;n:type:ShaderForge.SFN_RemapRange,id:5627,x:32556,y:32382,varname:node_5627,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-3534-OUT;n:type:ShaderForge.SFN_Multiply,id:1860,x:32630,y:32877,varname:node_1860,prsc:2|A-5627-OUT,B-5236-OUT;n:type:ShaderForge.SFN_Slider,id:2159,x:31739,y:32370,ptovrint:False,ptlb:FadeSpeed,ptin:_FadeSpeed,varname:node_2159,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:6102,x:32127,y:32402,varname:node_6102,prsc:2|A-2159-OUT,B-8057-T;n:type:ShaderForge.SFN_Multiply,id:8547,x:32847,y:32954,varname:node_8547,prsc:2|A-1860-OUT,B-6390-OUT;n:type:ShaderForge.SFN_Slider,id:6390,x:32527,y:33146,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_6390,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7695031,max:1;n:type:ShaderForge.SFN_Tex2d,id:1299,x:32145,y:33068,ptovrint:False,ptlb:Gradient,ptin:_Gradient,varname:node_1299,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a238a14178cc6454e86564464783d4ac,ntxv:0,isnm:False|UVIN-881-UVOUT;n:type:ShaderForge.SFN_Panner,id:881,x:31881,y:33070,varname:node_881,prsc:2,spu:0,spv:0.25;n:type:ShaderForge.SFN_Multiply,id:5236,x:32387,y:32895,varname:node_5236,prsc:2|A-1827-R,B-1299-R;proporder:1304-1827-2159-6390-1299;pass:END;sub:END;*/

Shader "Shader Forge/TreeOfLife" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Diffuse ("Diffuse", 2D) = "white" {}
        _FadeSpeed ("FadeSpeed", Range(0, 1)) = 1
        _Opacity ("Opacity", Range(0, 1)) = 0.7695031
        _Gradient ("Gradient", 2D) = "white" {}
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _FadeSpeed;
            uniform float _Opacity;
            uniform sampler2D _Gradient; uniform float4 _Gradient_ST;
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
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 node_8032 = (_Color.rgb*_Diffuse_var.rgb);
                float3 emissive = node_8032;
                float3 finalColor = emissive;
                float4 node_8057 = _Time + _TimeEditor;
                float node_5627 = (sin((_FadeSpeed*node_8057.g))*0.5+0.5);
                float4 node_9645 = _Time + _TimeEditor;
                float2 node_881 = (i.uv0+node_9645.g*float2(0,0.25));
                float4 _Gradient_var = tex2D(_Gradient,TRANSFORM_TEX(node_881, _Gradient));
                fixed4 finalRGBA = fixed4(finalColor,((node_5627*(_Diffuse_var.r*_Gradient_var.r))*_Opacity));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
