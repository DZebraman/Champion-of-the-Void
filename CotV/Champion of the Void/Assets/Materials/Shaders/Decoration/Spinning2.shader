// Shader created with Shader Forge v1.20 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.20;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|emission-3053-OUT,clip-7919-OUT;n:type:ShaderForge.SFN_Color,id:6190,x:32195,y:32634,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.641,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:3053,x:32494,y:32814,varname:node_3053,prsc:2|A-6190-RGB,B-2979-RGB,C-9160-RGB;n:type:ShaderForge.SFN_Tex2d,id:2979,x:32195,y:32836,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_2648,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1ed829c8abab6e24ca54a6ab3d7aef10,ntxv:0,isnm:False|UVIN-8372-UVOUT;n:type:ShaderForge.SFN_Rotator,id:8372,x:31978,y:32836,varname:node_8372,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:9160,x:32195,y:33058,ptovrint:False,ptlb:noise,ptin:_noise,varname:node_3542,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:73c71777eb58ec540958c8a6cd3ec56a,ntxv:0,isnm:False|UVIN-438-UVOUT;n:type:ShaderForge.SFN_Rotator,id:438,x:31978,y:33058,varname:node_438,prsc:2|UVIN-187-OUT,SPD-3639-OUT;n:type:ShaderForge.SFN_Time,id:6477,x:31615,y:33194,varname:node_6477,prsc:2;n:type:ShaderForge.SFN_Negate,id:3639,x:31781,y:33194,varname:node_3639,prsc:2|IN-6477-T;n:type:ShaderForge.SFN_Multiply,id:7919,x:32482,y:32974,varname:node_7919,prsc:2|A-2979-R,B-9160-R;n:type:ShaderForge.SFN_Slider,id:1209,x:31467,y:32983,ptovrint:False,ptlb:UV,ptin:_UV,varname:node_1209,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4,max:4;n:type:ShaderForge.SFN_TexCoord,id:526,x:31624,y:33056,varname:node_526,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:187,x:31808,y:33028,varname:node_187,prsc:2|A-1209-OUT,B-526-UVOUT;proporder:6190-2979-9160-1209;pass:END;sub:END;*/

Shader "Shader Forge/Spinning2" {
    Properties {
        _Color ("Color", Color) = (0.641,1,1,1)
        _Emission ("Emission", 2D) = "white" {}
        _noise ("noise", 2D) = "white" {}
        _UV ("UV", Range(0, 4)) = 4
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _UV;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
/////// Vectors:
                float4 node_5489 = _Time + _TimeEditor;
                float node_8372_ang = node_5489.g;
                float node_8372_spd = 1.0;
                float node_8372_cos = cos(node_8372_spd*node_8372_ang);
                float node_8372_sin = sin(node_8372_spd*node_8372_ang);
                float2 node_8372_piv = float2(0.5,0.5);
                float2 node_8372 = (mul(i.uv0-node_8372_piv,float2x2( node_8372_cos, -node_8372_sin, node_8372_sin, node_8372_cos))+node_8372_piv);
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(node_8372, _Emission));
                float4 node_6477 = _Time + _TimeEditor;
                float node_438_ang = node_5489.g;
                float node_438_spd = (-1*node_6477.g);
                float node_438_cos = cos(node_438_spd*node_438_ang);
                float node_438_sin = sin(node_438_spd*node_438_ang);
                float2 node_438_piv = float2(0.5,0.5);
                float2 node_438 = (mul((_UV*i.uv0)-node_438_piv,float2x2( node_438_cos, -node_438_sin, node_438_sin, node_438_cos))+node_438_piv);
                float4 _noise_var = tex2D(_noise,TRANSFORM_TEX(node_438, _noise));
                clip((_Emission_var.r*_noise_var.r) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (_Color.rgb*_Emission_var.rgb*_noise_var.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _UV;
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
                float4 node_1294 = _Time + _TimeEditor;
                float node_8372_ang = node_1294.g;
                float node_8372_spd = 1.0;
                float node_8372_cos = cos(node_8372_spd*node_8372_ang);
                float node_8372_sin = sin(node_8372_spd*node_8372_ang);
                float2 node_8372_piv = float2(0.5,0.5);
                float2 node_8372 = (mul(i.uv0-node_8372_piv,float2x2( node_8372_cos, -node_8372_sin, node_8372_sin, node_8372_cos))+node_8372_piv);
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(node_8372, _Emission));
                float4 node_6477 = _Time + _TimeEditor;
                float node_438_ang = node_1294.g;
                float node_438_spd = (-1*node_6477.g);
                float node_438_cos = cos(node_438_spd*node_438_ang);
                float node_438_sin = sin(node_438_spd*node_438_ang);
                float2 node_438_piv = float2(0.5,0.5);
                float2 node_438 = (mul((_UV*i.uv0)-node_438_piv,float2x2( node_438_cos, -node_438_sin, node_438_sin, node_438_cos))+node_438_piv);
                float4 _noise_var = tex2D(_noise,TRANSFORM_TEX(node_438, _noise));
                clip((_Emission_var.r*_noise_var.r) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
