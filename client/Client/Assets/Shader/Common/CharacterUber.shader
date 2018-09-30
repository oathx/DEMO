Shader "Character/Uber"
{
	Properties
	{
		_MainTex("Diffuse Tex", 2D) = "black" {}
		//[MaterialToggle] EBG_DIFFUSE ("Disable diffuse", Float) = 0
		
		_NDotLWrap("N.L Wrap", Float) = 1
		
		[Header(Second Diffuse)]

		_NDotLWrap1("N.L Wrap 2", Float) = 0.4
		
		[Header(SH Probes)]
		
		[MaterialToggle] EBG_SH_PROBES ("Enable", Float) = 0
		
		[Header(Normal Map)]
		
		[MaterialToggle] EBG_NORMAL_MAP ("Enable", Float) = 0
		_NormalMap ("Normal Map", 2D) = "bump" {}
		
		[Header(Specular)]
		
		[MaterialToggle] EBG_SPEC ("Enable", Float) = 0
		_SpecTex("Spec Map (A - Gloss)", 2D) = "black" {}
		_SpecularIntensity("Intensity", Float) = 1.5	
		_SpecularGlossModulation("Gloss Modulation", Float) = 5
		[MaterialToggle] EBG_ANISOTROPIC ("Use Anistropic Specular", Float) = 0
		_AnisotropicTex("Anisotropic Shift", 2D) = "black" {}
		_AnisoMix("Anistropic Specular Mix", Float) = 0.5
		
		[Header(Emissive)]
		
		[MaterialToggle] EBG_EMISSIVE ("Enable", Float) = 0
		_EmissiveTex("Emissive Map", 2D) = "black" {}
		_EmissiveColor("Color", Color) = (0, 0, 0, 1)
		_EmissiveIntensity("Emissive Intensity", Float) = 1.0
		
		[Header(Reflections)]
		
		[MaterialToggle] EBG_REFLECTIONS ("Enable", Float) = 0
		[MaterialToggle] EBG_BLURRY_REFLECTIONS ("Use Blurry", Float) = 0
		_ReflectionColor("Color", Color) = (0, 0, 0, 1)
		_ReflectionHDR("HDR", Float) = 0
		_ReflectionFresnelIntensity("Reflection Fresnel Boost", Float) = 0
		_ReflectionFresnelPower("Reflection Fresnel Power", Float) = 1

		[Header(Fresnel)]
		[MaterialToggle] EBG_FRESNEL ("Enable", Float) = 0
		_FresnelPower("Fresnel Power",Float) = 0 
		_FresnelColorIntensity("Fresnel Color", Color) = (0,0,0,0)
		
		[MaterialToggle] EBG_HOTSPOT_DEBUG ("Hot Spot Debug", Float) = 0

	} 

	Category 
	{		
		Tags {
			"Queue"="Geometry" 
			"RenderType"="Character"
			"LightMode"="ForwardBase"
		} 	
		Cull Back
		ZWrite On
		ZTest Lequal
		Blend Off
		Fog { Mode Off }
		
		Stencil
	    {
	    	Ref 2
	    	Comp Always
	    	Pass Replace
	    	ZFail Keep
	    }
	    
		Subshader
		{
			LOD 100
		 
			Pass
			{
				CGPROGRAM  
				
	            #pragma exclude_renderers xbox360 ps3 flash 
				
				#define EBG_FOG_ON
				#define EBG_POINT_LIGHT
				#define EBG_RIM_ON
				#undef EBG_DIFFUSE_ON
		
				#pragma multi_compile EBG_NORMAL_MAP_OFF EBG_NORMAL_MAP_ON
				#define EBG_DETAIL_OFF
				#pragma multi_compile EBG_SPEC_OFF EBG_SPEC_ON
				#pragma multi_compile EBG_FRESNEL_OFF EBG_FRESNEL_ON
				#pragma multi_compile EBG_EMISSIVE_OFF EBG_EMISSIVE_ON
				#pragma multi_compile EBG_ANISOTROPIC_OFF EBG_ANISOTROPIC_ON
				#pragma multi_compile EBG_REFLECTIONS_OFF EBG_REFLECTIONS_ON
				#pragma multi_compile EBG_BLURRY_REFLECTIONS_OFF EBG_BLURRY_REFLECTIONS_ON
				#pragma multi_compile EBG_HOTSPOT_DEBUG_OFF EBG_HOTSPOT_DEBUG_ON
				
				//fresnel is reserved for our rarity/class glow effect
				#pragma multi_compile EBG_SH_PROBES_OFF EBG_SH_PROBES_ON
				
				#pragma target 3.0
				
				#include "UnityCG.cginc"	
				#include "Assets/Shader/Common/BaseShaderGlobals.cginc"
				#include "Assets/Shader/Common/CharUber.cginc"
				
				#pragma vertex vertex_shader
				#pragma fragment fragment_shader
		
				ENDCG
			}
		}
	}
}

