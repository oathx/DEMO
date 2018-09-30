// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Character/CharReflectionDissolve"
{
	Properties
	{
		_MainTex("Diffuse Tex", 2D) = "black" {}
		_NDotLWrap("N.L Wrap", Float) = 0
		_NDotLWrap1("N.L Wrap 2", Float) = 0
		_SHIntensity("Intensity", Float) = 0
		_NormalMap("Normal Map", 2D) = "bump" {}
		_DetailNormalMap("Detail Normal Map", 2D) = "bump" {}
		_DetailNormalMapTile("Detail Normal Map Tile (X/Y)", Vector) = (1, 1, 1, 1)
		_SpecTex("Spec Map (A - Gloss)", 2D) = "black" {}
		_SpecularIntensity("Specular Intensity", Float) = 1
		_SpecularGlossModulation("Specular Gloss Modulation", Float) = 1
		_AnisoMix("Anistropic Specular Mix", Float) = 0
		_EmissiveTex("Emissive Map", 2D) = "black" {}
		_EmissiveColor("Emissive Color", Color) = (0, 0, 0, 0)
		_EmissiveIntensity("Emissive Intensity", Float) = 1
		_ReflectionColor("Reflection Color", Color) = (0, 0, 0, 0)
		_ReflectionHDR("Reflection HDR", Float) = 0
		_ReflectionFresnelIntensity("Reflection Fresnel Intensity", Float) = 0
		_ReflectionFresnelPower("Reflection Fresnel Power", Float) = 1
		_FresnelPower("Fresnel Power", Float) = 1
		_FresnelColorIntensity("Fresnel Color", Color) = (0, 0, 0, 0)
		//dissolve
		_DissolveSrc("DissolveTex",2D) = "white" {}
		_DissolveScale ("DissolveScale", Range (0.1, 1)) = 0.5
		_Tile("Tile", float) = 1
		_Amount ("Amount", Range (0, 1)) = 0.5
		_DissColor ("DissColor", Color) = (1,1,1,1)
		_ColorAnimate ("ColorAnimate", vector) = (1,1,1,1)
		_StartAmount("StartAmount", float) = 0.1
	}
	Category
	{
		Tags
		{
			"Queue"="Geometry"
			"RenderType"="Character"
			"LightMode"="ForwardBase"
		}
		Lighting Off
		Fog { Mode Off }
		Cull Back
		ZWrite On
		ZTest LEqual
		Blend Off
		Stencil
		{
		Ref 2
		Comp Always
		Pass Replace
		ZFail Keep
		}
		Subshader
		{
			LOD 800
			Pass
			{
				CGPROGRAM
				#include "UnityCG.cginc"
				#include "UnityShaderVariables.cginc"
				#include "Assets/Shader/Common/BaseShaderGlobals.cginc"
				#pragma target 3.0
				#pragma vertex vertex_shader
				#pragma fragment fragment_shader
				#pragma multi_compile EBG_FLIP_PLAYER_AXIS_ON EBG_FLIP_PLAYER_AXIS_OFF
				//EBG_POINT_LIGHT EBG_RIM_ON EBG_FOG_ON EBG_FRESNEL_ON EBG_DETAIL_OFF EBG_HOTSPOT_DEBUG_OFF EBG_BLURRY_REFLECTIONS_ON EBG_NORMAL_MAP_ON EBG_REFLECTIONS_ON EBG_SH_PROBES_ON EBG_SPEC_ON
				sampler2D _MainTex;	
				half _NDotLWrap;
				half _NDotLWrap1;
				sampler2D _NormalMap; 
				sampler2D _SpecTex;	
				half _SpecularIntensity;
				half _SpecularGlossModulation;
				half _FresnelPower;
				fixed4 _FresnelColorIntensity;
				fixed4 _ReflectionColor;
				half _ReflectionHDR;
				half _ReflectionFresnelIntensity;
				half _ReflectionFresnelPower;
				//dissolve
				sampler2D _DissolveSrc;
				float _DissolveScale;
				float _Tile;
				float _Amount;
				half4 _DissColor;
				half4 _ColorAnimate;
				float _StartAmount;
				struct Input 
				{
				    float4 vertex : POSITION;
				    float3 normal : NORMAL;
					float4 tangent : TANGENT;
					float2 texcoord0 : TEXCOORD0; 
				};
				struct VtoS
				{
					float4 position : SV_POSITION;	
					float3 uv_main_fog : TEXCOORD0;
				  	float3 localSurface2World0	: TEXCOORD1;
				  	float3 localSurface2World1	: TEXCOORD2;
				  	float3 localSurface2World2	: TEXCOORD3;
					float3 viewDir : TEXCOORD4;
					fixed3 pointLight : TEXCOORD6;
					fixed3 color : TEXCOORD7;
				};
				float4 _MainTex_ST;
				VtoS vertex_shader(Input v)
				{
					VtoS data;
					data.position = UnityObjectToClipPos(v.vertex);
					data.uv_main_fog.xy = v.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
					float3 norm = v.normal;
					data.uv_main_fog.z = EBGFogVertex(v.vertex);
					float3 worldN = normalize(mul((float3x3)unity_ObjectToWorld, norm));
					float3 local0 = normalize(mul((float3x3)unity_ObjectToWorld, v.tangent.xyz));
					float3 local2 = normalize(mul(norm, (float3x3)unity_WorldToObject));
					float3 local1 = normalize(cross(local2, local0) * (v.tangent.w * unity_WorldTransformParams.w));
					data.localSurface2World0 = float3(local0.x, local1.x, local2.x);
					data.localSurface2World1 = float3(local0.y, local1.y, local2.y);
					data.localSurface2World2 = float3(local0.z, local1.z, local2.z);
					data.viewDir = WorldSpaceViewDir(v.vertex);
					float3 viewDirNorm = normalize(WorldSpaceViewDir(v.vertex));
					float f = saturate(1.0f - dot(viewDirNorm, worldN));
					data.pointLight = EBGPointLight(v.vertex, worldN); 
					data.pointLight += pow(f, _FresnelPower) * _FresnelColorIntensity.rgb;
					data.color = ShadeSH9(float4(worldN, 1)) * _EBGCharLightProbeScale;
					return data;  
				}
				fixed4 fragment_shader(VtoS IN) : COLOR0
				{				
					fixed3 mainTex = tex2D(_MainTex, IN.uv_main_fog.xy).rgb;
					fixed3 normalMapTex = UnpackNormal(tex2D(_NormalMap, IN.uv_main_fog.xy));
#if defined(EBG_FLIP_PLAYER_AXIS_ON)
normalMapTex.y *= -1;
#endif
					float3 n;
					n.x = dot(normalMapTex, IN.localSurface2World0);
					n.y = dot(normalMapTex, IN.localSurface2World1);
					n.z = dot(normalMapTex, IN.localSurface2World2);
					n = normalize(n);
					half nDotL = max(0, (dot(n, _EBGCharDirectionToLight0.xyz) + _NDotLWrap) / (1 + _NDotLWrap));
					float3 viewDirNorm = normalize(IN.viewDir);
				  	fixed3 res = nDotL * _EBGCharLightDiffuseColor0.rgb + IN.color;
					res *= mainTex;
					half nDotL1 = max(0, (dot(mul((half3x3)UNITY_MATRIX_V, n), _EBGCharDirectionToLight1.xyz) + _NDotLWrap1) / (1 + _NDotLWrap1)); 
					res += nDotL1 * _EBGCharLightDiffuseColor1.rgb;
					res += IN.pointLight;
					half f = min(1, 1.0f - dot(viewDirNorm, n));
					fixed3 fresnel = pow(f, _FresnelPower) * _FresnelColorIntensity.rgb;
						res += fresnel;
					fixed3 specTex = tex2D(_SpecTex, IN.uv_main_fog.xy).rgb;
					fixed3 specularDir = reflect(_EBGCharLightDirection0.xyz, n);
					half s = max(0.0, dot(viewDirNorm, specularDir));
					fixed3 spec = max (0, _SpecularIntensity * pow(s, _SpecularGlossModulation) * specTex * _EBGCharLightSpecularColor0);
					res += spec;
					float3 reflectionDir = reflect(-viewDirNorm, n);
					fixed4 reflectionTex = texCUBE(_EBGCubemapBlurry, reflectionDir);
					fixed3 reflection = _ReflectionColor.rgb * reflectionTex.rgb * (1 + reflectionTex.a * _ReflectionHDR);
					reflection *= specTex;
					reflection *=_ReflectionFresnelIntensity * pow(f, _ReflectionFresnelPower) + 1.0f;
					res += reflection;
					res = EBGFogFragment(res, IN.uv_main_fog.z);
					//dissolve
					fixed4 ClipTex = tex2D (_DissolveSrc, IN.uv_main_fog.xy/_Tile);
					fixed ClipAmount = Luminance(ClipTex.rgb) - _Amount;
					float Clip = 0;
					float ani_color = 1 / (ClipAmount * 100) * _DissolveScale;
					float3 albedo2 = _DissColor + float4( ani_color,ani_color,ani_color,1) * _ColorAnimate;
					clip(ClipAmount);
					float scale_ = step(0,ClipAmount * 0.7 - _StartAmount);
					res = res* scale_ + albedo2 * (1 - scale_);
					return fixed4(res, 1); 
				}
				
				ENDCG
			}
		}
		Subshader
		{
			LOD 700
			Pass
			{
				CGPROGRAM
				#include "UnityCG.cginc"
				#include "UnityShaderVariables.cginc"
				#include "Assets/Shader/Common/BaseShaderGlobals.cginc"
				#pragma target 3.0
				#pragma vertex vertex_shader
				#pragma fragment fragment_shader
				//EBG_POINT_LIGHT EBG_RIM_ON EBG_FOG_ON EBG_FRESNEL_ON EBG_NORMAL_MAP_OFF EBG_DETAIL_OFF EBG_HOTSPOT_DEBUG_OFF EBG_BLURRY_REFLECTIONS_ON EBG_REFLECTIONS_ON EBG_SH_PROBES_ON EBG_SPEC_ON
				sampler2D _MainTex;	
				half _NDotLWrap;
				half _NDotLWrap1;
				sampler2D _SpecTex;	
				half _SpecularIntensity;
				half _SpecularGlossModulation;
				half _FresnelPower;
				fixed4 _FresnelColorIntensity;
				fixed4 _ReflectionColor;
				half _ReflectionHDR;
				half _ReflectionFresnelIntensity;
				half _ReflectionFresnelPower;
				//dissolve
				sampler2D _DissolveSrc;
				float _DissolveScale;
				float _Tile;
				float _Amount;
				half4 _DissColor;
				half4 _ColorAnimate;
				float _StartAmount;
				struct Input 
				{
				    float4 vertex : POSITION;
				    float3 normal : NORMAL;
					float2 texcoord0 : TEXCOORD0; 
				};
				struct VtoS
				{
					float4 position : SV_POSITION;	
					float3 uv_main_fog : TEXCOORD0;
					float2 nDotL : TEXCOORD1;
					float4 reflectionDir_Fresnel : TEXCOORD3;
					half spec : TEXCOORD4;
					fixed3 pointLight : TEXCOORD6;
					fixed3 color : TEXCOORD7;
				};
				float4 _MainTex_ST;
				VtoS vertex_shader(Input v)
				{
					VtoS data;
					data.position = UnityObjectToClipPos(v.vertex);
					data.uv_main_fog.xy = v.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
					float3 norm = v.normal;
					data.uv_main_fog.z = EBGFogVertex(v.vertex);
					float3 worldN = normalize(mul((float3x3)unity_ObjectToWorld, norm));
					data.nDotL.x = max(0, (dot(worldN, _EBGCharDirectionToLight0.xyz) + _NDotLWrap) / (1 + _NDotLWrap));
					data.nDotL.y = max(0, (dot(mul((half3x3)UNITY_MATRIX_V, worldN), _EBGCharDirectionToLight1.xyz) + _NDotLWrap1) / (1 + _NDotLWrap1)); 
					float3 viewDirNorm = normalize(WorldSpaceViewDir(v.vertex));
					float f = saturate(1.0f - dot(viewDirNorm, worldN));
					data.pointLight = EBGPointLight(v.vertex, worldN); 
					data.pointLight += pow(f, _FresnelPower) * _FresnelColorIntensity.rgb;
					data.reflectionDir_Fresnel.xyz = reflect(-viewDirNorm, worldN);
					data.reflectionDir_Fresnel.w = _ReflectionFresnelIntensity * pow(f, _ReflectionFresnelPower) + 1.0f;
					float3 specularDir = reflect(_EBGCharLightDirection0.xyz, worldN);
					data.spec = max(0.0, dot(viewDirNorm, specularDir));
					data.color = ShadeSH9(float4(worldN, 1)) * _EBGCharLightProbeScale;
					return data;  
				}
				fixed4 fragment_shader(VtoS IN) : COLOR0
				{				
					fixed3 mainTex = tex2D(_MainTex, IN.uv_main_fog.xy).rgb;
					half nDotL = IN.nDotL.x; 
				  	fixed3 res = nDotL * _EBGCharLightDiffuseColor0.rgb + IN.color;
					res *= mainTex;
					res += IN.nDotL.y * _EBGCharLightDiffuseColor1.rgb;
					res += IN.pointLight;
					fixed3 specTex = tex2D(_SpecTex, IN.uv_main_fog.xy).rgb;
					half s = IN.spec;
					fixed3 spec = max (0, _SpecularIntensity * pow(s, _SpecularGlossModulation) * specTex * _EBGCharLightSpecularColor0);
					res += spec;
					float3 reflectionDir = normalize(IN.reflectionDir_Fresnel.xyz);
					fixed4 reflectionTex = texCUBE(_EBGCubemapBlurry, reflectionDir);
					fixed3 reflection = _ReflectionColor.rgb * reflectionTex.rgb * (1 + reflectionTex.a * _ReflectionHDR);
					reflection *= specTex;
					reflection *= IN.reflectionDir_Fresnel.w;
					res += reflection;
					res = EBGFogFragment(res, IN.uv_main_fog.z);
					//dissolve
					fixed4 ClipTex = tex2D (_DissolveSrc, IN.uv_main_fog.xy/_Tile);
					fixed ClipAmount = Luminance(ClipTex.rgb) - _Amount;
					float Clip = 0;
					float ani_color = 1 / (ClipAmount * 100) * _DissolveScale;
					float3 albedo2 = _DissColor + float4( ani_color,ani_color,ani_color,1) * _ColorAnimate;
					clip(ClipAmount);
					float scale_ = step(0,ClipAmount * 0.7 - _StartAmount);
					res = res* scale_ + albedo2 * (1 - scale_);
					return fixed4(res, 1); 
				}
				
				ENDCG
			}
		}
		Subshader
		{
			LOD 600
			Pass
			{
				CGPROGRAM
				#include "UnityCG.cginc"
				#include "UnityShaderVariables.cginc"
				#include "Assets/Shader/Common/BaseShaderGlobals.cginc"
				#pragma target 3.0
				#pragma vertex vertex_shader
				#pragma fragment fragment_shader
				//EBG_POINT_LIGHT EBG_RIM_ON EBG_FOG_ON EBG_FRESNEL_ON EBG_REFLECTIONS_OFF EBG_BLURRY_REFLECTIONS_OFF EBG_NORMAL_MAP_OFF EBG_DETAIL_OFF EBG_HOTSPOT_DEBUG_OFF EBG_SH_PROBES_ON EBG_SPEC_ON
				sampler2D _MainTex;	
				half _NDotLWrap;
				half _NDotLWrap1;
				sampler2D _SpecTex;	
				half _SpecularIntensity;
				half _SpecularGlossModulation;
				half _FresnelPower;
				fixed4 _FresnelColorIntensity;
				//dissolve
				sampler2D _DissolveSrc;
				float _DissolveScale;
				float _Tile;
				float _Amount;
				half4 _DissColor;
				half4 _ColorAnimate;
				float _StartAmount;
				struct Input 
				{
				    float4 vertex : POSITION;
				    float3 normal : NORMAL;
					float2 texcoord0 : TEXCOORD0; 
				};
				struct VtoS
				{
					float4 position : SV_POSITION;	
					float3 uv_main_fog : TEXCOORD0;
					float2 nDotL : TEXCOORD1;
					half spec : TEXCOORD4;
					fixed3 pointLight : TEXCOORD6;
					fixed3 color : TEXCOORD7;
				};
				float4 _MainTex_ST;
				VtoS vertex_shader(Input v)
				{
					VtoS data;
					data.position = UnityObjectToClipPos(v.vertex);
					data.uv_main_fog.xy = v.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
					float3 norm = v.normal;
					data.uv_main_fog.z = EBGFogVertex(v.vertex);
					float3 worldN = normalize(mul((float3x3)unity_ObjectToWorld, norm));
					data.nDotL.x = max(0, (dot(worldN, _EBGCharDirectionToLight0.xyz) + _NDotLWrap) / (1 + _NDotLWrap));
					data.nDotL.y = max(0, (dot(mul((half3x3)UNITY_MATRIX_V, worldN), _EBGCharDirectionToLight1.xyz) + _NDotLWrap1) / (1 + _NDotLWrap1)); 
					float3 viewDirNorm = normalize(WorldSpaceViewDir(v.vertex));
					float f = saturate(1.0f - dot(viewDirNorm, worldN));
					data.pointLight = EBGPointLight(v.vertex, worldN); 
					data.pointLight += pow(f, _FresnelPower) * _FresnelColorIntensity.rgb;
					float3 specularDir = reflect(_EBGCharLightDirection0.xyz, worldN);
					data.spec = max(0.0, dot(viewDirNorm, specularDir));
					data.color = ShadeSH9(float4(worldN, 1)) * _EBGCharLightProbeScale;
					return data;  
				}
				fixed4 fragment_shader(VtoS IN) : COLOR0
				{				
					fixed3 mainTex = tex2D(_MainTex, IN.uv_main_fog.xy).rgb;
					half nDotL = IN.nDotL.x; 
				  	fixed3 res = nDotL * _EBGCharLightDiffuseColor0.rgb + IN.color;
					res *= mainTex;
					res += IN.nDotL.y * _EBGCharLightDiffuseColor1.rgb;
					res += IN.pointLight;
					fixed3 specTex = tex2D(_SpecTex, IN.uv_main_fog.xy).rgb;
					half s = IN.spec;
					fixed3 spec = max (0, _SpecularIntensity * pow(s, _SpecularGlossModulation) * specTex * _EBGCharLightSpecularColor0);
					res += spec;
					res = EBGFogFragment(res, IN.uv_main_fog.z);
					//dissolve
					fixed4 ClipTex = tex2D (_DissolveSrc, IN.uv_main_fog.xy/_Tile);
					fixed ClipAmount = Luminance(ClipTex.rgb) - _Amount;
					float Clip = 0;
					float ani_color = 1 / (ClipAmount * 100) * _DissolveScale;
					float3 albedo2 = _DissColor + float4( ani_color,ani_color,ani_color,1) * _ColorAnimate;
					clip(ClipAmount);
					float scale_ = step(0,ClipAmount * 0.7 - _StartAmount);
					res = res* scale_ + albedo2 * (1 - scale_);
					return fixed4(res, 1); 
				}
				
				ENDCG
			}
		}
		Subshader
		{
			LOD 500
			Pass
			{
				CGPROGRAM
				#include "UnityCG.cginc"
				#include "UnityShaderVariables.cginc"
				#include "Assets/Shader/Common/BaseShaderGlobals.cginc"
				#pragma target 3.0
				#pragma vertex vertex_shader
				#pragma fragment fragment_shader
				//EBG_POINT_LIGHT EBG_RIM_ON EBG_FOG_ON EBG_FRESNEL_OFF EBG_SPEC_OFF EBG_ANISOTROPIC_OFF EBG_EMISSIVE_OFF EBG_REFLECTIONS_OFF EBG_BLURRY_REFLECTIONS_OFF EBG_NORMAL_MAP_OFF EBG_DETAIL_OFF EBG_HOTSPOT_DEBUG_OFF EBG_SH_PROBES_ON
				sampler2D _MainTex;	
				half _NDotLWrap;
				half _NDotLWrap1;
				//dissolve
				sampler2D _DissolveSrc;
				float _DissolveScale;
				float _Tile;
				float _Amount;
				half4 _DissColor;
				half4 _ColorAnimate;
				float _StartAmount;
				struct Input 
				{
				    float4 vertex : POSITION;
				    float3 normal : NORMAL;
					float2 texcoord0 : TEXCOORD0; 
				};
				struct VtoS
				{
					float4 position : SV_POSITION;	
					float3 uv_main_fog : TEXCOORD0;
					float2 nDotL : TEXCOORD1;
					fixed3 pointLight : TEXCOORD6;
					fixed3 color : TEXCOORD7;
				};
				float4 _MainTex_ST;
				VtoS vertex_shader(Input v)
				{
					VtoS data;
					data.position = UnityObjectToClipPos(v.vertex);
					data.uv_main_fog.xy = v.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
					float3 norm = v.normal;
					data.uv_main_fog.z = EBGFogVertex(v.vertex);
					float3 worldN = normalize(mul((float3x3)unity_ObjectToWorld, norm));
					data.nDotL.x = max(0, (dot(worldN, _EBGCharDirectionToLight0.xyz) + _NDotLWrap) / (1 + _NDotLWrap));
					data.nDotL.y = max(0, (dot(mul((half3x3)UNITY_MATRIX_V, worldN), _EBGCharDirectionToLight1.xyz) + _NDotLWrap1) / (1 + _NDotLWrap1)); 
					data.pointLight = EBGPointLight(v.vertex, worldN); 
					data.color = ShadeSH9(float4(worldN, 1)) * _EBGCharLightProbeScale;
					return data;  
				}
				fixed4 fragment_shader(VtoS IN) : COLOR0
				{				
					fixed3 mainTex = tex2D(_MainTex, IN.uv_main_fog.xy).rgb;
					half nDotL = IN.nDotL.x; 
				  	fixed3 res = nDotL * _EBGCharLightDiffuseColor0.rgb + IN.color;
					res *= mainTex;
					res += IN.nDotL.y * _EBGCharLightDiffuseColor1.rgb;
					res += IN.pointLight;
					res = EBGFogFragment(res, IN.uv_main_fog.z);
					//dissolve
					fixed4 ClipTex = tex2D (_DissolveSrc, IN.uv_main_fog.xy/_Tile);
					fixed ClipAmount = Luminance(ClipTex.rgb) - _Amount;
					float Clip = 0;
					float ani_color = 1 / (ClipAmount * 100) * _DissolveScale;
					float3 albedo2 = _DissColor + float4( ani_color,ani_color,ani_color,1) * _ColorAnimate;
					clip(ClipAmount);
					float scale_ = step(0,ClipAmount * 0.7 - _StartAmount);
					res = res* scale_ + albedo2 * (1 - scale_);
					return fixed4(res, 1); 
				}
				
				ENDCG
			}
		}
		Subshader
		{
			LOD 300
			Pass
			{
				CGPROGRAM
				#include "UnityCG.cginc"
				#include "UnityShaderVariables.cginc"
				#include "Assets/Shader/Common/BaseShaderGlobals.cginc"
				#pragma target 3.0
				#pragma vertex vertex_shader
				#pragma fragment fragment_shader
				//EBG_POINT_LIGHT EBG_RIM_ON EBG_FRESNEL_OFF EBG_FOG_OFF EBG_SPEC_OFF EBG_ANISOTROPIC_OFF EBG_EMISSIVE_OFF EBG_REFLECTIONS_OFF EBG_BLURRY_REFLECTIONS_OFF EBG_NORMAL_MAP_OFF EBG_DETAIL_OFF EBG_HOTSPOT_DEBUG_OFF EBG_SH_PROBES_ON
				sampler2D _MainTex;	
				half _NDotLWrap;
				half _NDotLWrap1;
				//dissolve
				sampler2D _DissolveSrc;
				float _DissolveScale;
				float _Tile;
				float _Amount;
				half4 _DissColor;
				half4 _ColorAnimate;
				float _StartAmount;
				struct Input 
				{
				    float4 vertex : POSITION;
				    float3 normal : NORMAL;
					float2 texcoord0 : TEXCOORD0; 
				};
				struct VtoS
				{
					float4 position : SV_POSITION;	
					float2 uv_main_fog : TEXCOORD0;
					float2 nDotL : TEXCOORD1;
					fixed3 pointLight : TEXCOORD6;
					fixed3 color : TEXCOORD7;
				};
				float4 _MainTex_ST;
				VtoS vertex_shader(Input v)
				{
					VtoS data;
					data.position = UnityObjectToClipPos(v.vertex);
					data.uv_main_fog.xy = v.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
					float3 norm = v.normal;
					float3 worldN = normalize(mul((float3x3)unity_ObjectToWorld, norm));
					data.nDotL.x = max(0, (dot(worldN, _EBGCharDirectionToLight0.xyz) + _NDotLWrap) / (1 + _NDotLWrap));
					data.nDotL.y = max(0, (dot(mul((half3x3)UNITY_MATRIX_V, worldN), _EBGCharDirectionToLight1.xyz) + _NDotLWrap1) / (1 + _NDotLWrap1)); 
					data.pointLight = EBGPointLight(v.vertex, worldN); 
					data.color = ShadeSH9(float4(worldN, 1)) * _EBGCharLightProbeScale;
					return data;  
				}
				fixed4 fragment_shader(VtoS IN) : COLOR0
				{				
					fixed3 mainTex = tex2D(_MainTex, IN.uv_main_fog.xy).rgb;
					half nDotL = IN.nDotL.x; 
				  	fixed3 res = nDotL * _EBGCharLightDiffuseColor0.rgb + IN.color;
					res *= mainTex;
					res += IN.nDotL.y * _EBGCharLightDiffuseColor1.rgb;
					res += IN.pointLight;
					//dissolve
					fixed4 ClipTex = tex2D (_DissolveSrc, IN.uv_main_fog.xy/_Tile);
					fixed ClipAmount = Luminance(ClipTex.rgb) - _Amount;
					float Clip = 0;
					float ani_color = 1 / (ClipAmount * 100) * _DissolveScale;
					float3 albedo2 = _DissColor + float4( ani_color,ani_color,ani_color,1) * _ColorAnimate;
					clip(ClipAmount);
					float scale_ = step(0,ClipAmount * 0.7 - _StartAmount);
					res = res* scale_ + albedo2 * (1 - scale_);
					return fixed4(res, 1); 
				}
				
				ENDCG
			}
		}
		Subshader
		{
			LOD 150
			Pass
			{
				CGPROGRAM
				#include "UnityCG.cginc"
				#include "UnityShaderVariables.cginc"
				#include "Assets/Shader/Common/BaseShaderGlobals.cginc"
				#pragma target 3.0
				#pragma vertex vertex_shader
				#pragma fragment fragment_shader
				//EBG_POINT_LIGHT EBG_FRESNEL_OFF EBG_RIM_OFF EBG_FOG_OFF EBG_SPEC_OFF EBG_ANISOTROPIC_OFF EBG_EMISSIVE_OFF EBG_REFLECTIONS_OFF EBG_BLURRY_REFLECTIONS_OFF EBG_NORMAL_MAP_OFF EBG_DETAIL_OFF EBG_HOTSPOT_DEBUG_OFF EBG_SH_PROBES_ON
				sampler2D _MainTex;	
				half _NDotLWrap;
				struct Input 
				{
				    float4 vertex : POSITION;
				    float3 normal : NORMAL;
					float2 texcoord0 : TEXCOORD0; 
				};
				struct VtoS
				{
					float4 position : SV_POSITION;	
					float2 uv_main_fog : TEXCOORD0;
					float nDotL : TEXCOORD1;
					fixed3 pointLight : TEXCOORD6;
					fixed3 color : TEXCOORD7;
				};
				float4 _MainTex_ST;
				VtoS vertex_shader(Input v)
				{
					VtoS data;
					data.position = UnityObjectToClipPos(v.vertex);
					data.uv_main_fog.xy = v.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
					float3 norm = v.normal;
					float3 worldN = normalize(mul((float3x3)unity_ObjectToWorld, norm));
					data.nDotL.x = max(0, (dot(worldN, _EBGCharDirectionToLight0.xyz) + _NDotLWrap) / (1 + _NDotLWrap));
					data.pointLight = EBGPointLight(v.vertex, worldN); 
					data.color = ShadeSH9(float4(worldN, 1)) * _EBGCharLightProbeScale;
					return data;  
				}
				fixed4 fragment_shader(VtoS IN) : COLOR0
				{				
					fixed3 mainTex = tex2D(_MainTex, IN.uv_main_fog.xy).rgb;
					half nDotL = IN.nDotL.x; 
				  	fixed3 res = nDotL * _EBGCharLightDiffuseColor0.rgb + IN.color;
					res *= mainTex;
					res += IN.pointLight;
					return fixed4(res, 1); 
				}
				
				ENDCG
			}
		}
		Subshader
		{
			LOD 100
			Pass
			{
				CGPROGRAM
				#include "UnityCG.cginc"
				#include "UnityShaderVariables.cginc"
				#include "Assets/Shader/Common/BaseShaderGlobals.cginc"
				#pragma target 3.0
				#pragma vertex vertex_shader
				#pragma fragment fragment_shader
				//EBG_FRESNEL_OFF EBG_RIM_OFF EBG_FOG_OFF EBG_SPEC_OFF EBG_ANISOTROPIC_OFF EBG_EMISSIVE_OFF EBG_REFLECTIONS_OFF EBG_BLURRY_REFLECTIONS_OFF EBG_NORMAL_MAP_OFF EBG_DETAIL_OFF EBG_HOTSPOT_DEBUG_OFF EBG_SH_PROBES_ON
				sampler2D _MainTex;	
				half _NDotLWrap;
				struct Input 
				{
				    float4 vertex : POSITION;
				    float3 normal : NORMAL;
					float2 texcoord0 : TEXCOORD0; 
				};
				struct VtoS
				{
					float4 position : SV_POSITION;	
					float2 uv_main_fog : TEXCOORD0;
					float nDotL : TEXCOORD1;
					fixed3 color : TEXCOORD7;
				};
				float4 _MainTex_ST;
				VtoS vertex_shader(Input v)
				{
					VtoS data;
					data.position = UnityObjectToClipPos(v.vertex);
					data.uv_main_fog.xy = v.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
					float3 norm = v.normal;
					float3 worldN = normalize(mul((float3x3)unity_ObjectToWorld, norm));
					data.nDotL.x = max(0, (dot(worldN, _EBGCharDirectionToLight0.xyz) + _NDotLWrap) / (1 + _NDotLWrap));
					data.color = ShadeSH9(float4(worldN, 1)) * _EBGCharLightProbeScale;
					return data;  
				}
				fixed4 fragment_shader(VtoS IN) : COLOR0
				{				
					fixed3 mainTex = tex2D(_MainTex, IN.uv_main_fog.xy).rgb;
					half nDotL = IN.nDotL.x; 
				  	fixed3 res = nDotL * _EBGCharLightDiffuseColor0.rgb + IN.color;
					res *= mainTex;
					return fixed4(res, 1); 
				}
				
				ENDCG
			}
		}
	}
}
