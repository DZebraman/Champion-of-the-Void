// Shader created with Shader Forge v1.20 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.20;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:33147,y:32742,varname:node_3138,prsc:2|custl-2652-OUT,clip-2648-R;n:type:ShaderForge.SFN_Color,id:7241,x:32433,y:32580,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.641,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:2652,x:32732,y:32760,varname:node_2652,prsc:2|A-7241-RGB,B-2648-RGB,C-3542-RGB;n:type:ShaderForge.SFN_Tex2d,id:2648,x:32433,y:32782,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_2648,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1ed829c8abab6e24ca54a6ab3d7aef10,ntxv:0,isnm:False|UVIN-9444-UVOUT;n:type:ShaderForge.SFN_Rotator,id:9444,x:32216,y:32782,varname:node_9444,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:3542,x:32433,y:33004,ptovrint:False,ptlb:noise,ptin:_noise,varname:node_3542,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:73c71777eb58ec540958c8a6cd3ec56a,ntxv:0,isnm:False|UVIN-2431-UVOUT;n:type:ShaderForge.SFN_Rotator,id:2431,x:32216,y:33004,varname:node_2431,prsc:2|SPD-3974-OUT;n:type:ShaderForge.SFN_Time,id:1561,x:31879,y:33002,varname:node_1561,prsc:2;n:type:ShaderForge.SFN_Negate,id:3974,x:32045,y:33002,varname:node_3974,prsc:2|IN-1561-T;proporder:7241-2648-3542;pass:END;sub:END;*/

Shader "Shader Forge/LightPillarsSpinning" {
    Properties {
        _Color ("Color", Color) = (0.641,1,1,1)
        _Emission ("Emission", 2D) = "white" {}
        _noise ("noise", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform sampler2D _noise; uniform float4 _noise_ST;
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
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
/////// Vectors:
                float4 node_2774 = _Time + _TimeEditor;
                float node_9444_ang = node_2774.g;
                float node_9444_spd = 1.0;
                float node_9444_cos = cos(node_9444_spd*node_9444_ang);
                float node_9444_sin = sin(node_9444_spd*node_9444_ang);
                float2 node_9444_piv = float2(0.5,0.5);
                float2 node_9444 = (mul(i.uv0-node_9444_piv,float2x2( node_9444_cos, -node_9444_sin, node_9444_sin, node_9444_cos))+node_9444_piv);
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(node_9444, _Emission));
                clip(_Emission_var.r - 0.5);
////// Lighting:
                float4 node_1561 = _Time + _TimeEditor;
                float node_2431_ang = node_2774.g;
                float node_2431_spd = (-1*node_1561.g);
                float node_2431_cos = cos(node_2431_spd*node_2431_ang);
                float node_2431_sin = sin(node_2431_spd*node_2431_ang);
                float2 node_2431_piv = float2(0.5,0.5);
                float2 node_2431 = (mul(i.uv0-node_2431_piv,float2x2( node_2431_cos, -node_2431_sin, node_2431_sin, node_2431_cos))+node_2431_piv);
                float4 _noise_var = tex2D(_noise,TRANSFORM_TEX(node_2431, _noise));
                float3 finalColor = (_Color.rgb*_Emission_var.rgb*_noise_var.rgb);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
/////// Vectors:
                float4 node_4638 = _Time + _TimeEditor;
                float node_9444_ang = node_4638.g;
                float node_9444_spd = 1.0;
                float node_9444_cos = cos(node_9444_spd*node_9444_ang);
                float node_9444_sin = sin(node_9444_spd*node_9444_ang);
                float2 node_9444_piv = float2(0.5,0.5);
                float2 node_9444 = (mul(i.uv0-node_9444_piv,float2x2( node_9444_cos, -node_9444_sin, node_9444_sin, node_9444_cos))+node_9444_piv);
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(node_9444, _Emission));
                clip(_Emission_var.r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
