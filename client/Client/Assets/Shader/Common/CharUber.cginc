// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

sampler2D _MainTex;	

half _NDotLWrap;
#if defined(EBG_RIM_ON)	
half _NDotLWrap1;
#endif

#if defined(EBG_NORMAL_MAP_ON)
sampler2D _NormalMap; 
#endif
#if defined(EBG_NORMAL_MAP_ON) && defined(EBG_DETAIL_ON)
sampler2D _DetailNormalMap;
half2 _DetailNormalMapTile;
#endif

#if defined(EBG_EMISSIVE_ON)
sampler2D _EmissiveTex;
fixed4 _EmissiveColor;
half _EmissiveIntensity;
#endif

#if defined(EBG_SPEC_ON)
sampler2D _SpecTex;	
half _SpecularIntensity;
half _SpecularGlossModulation;
#endif
#if defined(EBG_SPEC_ON) && defined(EBG_ANISOTROPIC_ON)
sampler2D _AnisotropicTex;	
float _AnisoMix;
#endif

#if defined(EBG_FRESNEL_ON) && !defined(EBG_BLUEPRINT) && !defined(EBG_INVERT_FRESNEL)
half _FresnelPower;
#define EBG_VAL_FRESNEL_POWER _FresnelPower
fixed4 _FresnelColorIntensity;
#define EBG_VAL_FRESNEL_COLOR_INTENSITY _FresnelColorIntensity.rgb
#endif

#if defined(EBG_FRESNEL_ON) && defined(EBG_INVERT_FRESNEL)
#define EBG_VAL_FRESNEL_POWER (1.0)
#define EBG_VAL_FRESNEL_COLOR_INTENSITY fixed3(1, 1, 1)
#endif

#if defined(EBG_REFLECTIONS_ON)
fixed4 _ReflectionColor;
half _ReflectionHDR;
half _ReflectionFresnelIntensity;
half _ReflectionFresnelPower;
#endif

#if defined(EBG_TINT_ON)
fixed3 _DiffuseTint;
fixed3 _SpecTint;
#endif

//BLUEPRINT

#if defined(EBG_BLUEPRINT)
sampler2D _ScanlineTex;
#define EBG_VAL_FRESNEL_POWER (5.0)
#define EBG_VAL_FRESNEL_COLOR_INTENSITY fixed3(0, 4.7645, 5)
#endif
				
struct Input 
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
#if defined(EBG_NORMAL_MAP_ON)
	float4 tangent : TANGENT;
#endif
	float2 texcoord0 : TEXCOORD0; 
};

struct VtoS
{
	float4 position : SV_POSITION;	
	
#if defined(EBG_BLUEPRINT) || defined(EBG_FOG_ON)
	float3 uv_main_fog : TEXCOORD0;
#else
	float2 uv_main_fog : TEXCOORD0;
#endif

#if defined(EBG_NORMAL_MAP_ON)
	// WITH NORMAL MAPS
  	float3 localSurface2World0	: TEXCOORD1;
  	float3 localSurface2World1	: TEXCOORD2;
  	float3 localSurface2World2	: TEXCOORD3;
#endif

#if defined(EBG_NORMAL_MAP_ON) && (defined(EBG_SPEC_ON) || defined(EBG_FRESNEL_ON) || defined(EBG_REFLECTIONS_ON))
	float3 viewDir : TEXCOORD4;
#endif

#if !defined(EBG_NORMAL_MAP_ON) && defined(EBG_RIM_ON)
	// WITHOUT NORMAL MAPS
	float2 nDotL : TEXCOORD1;
#elif !defined(EBG_NORMAL_MAP_ON)
	// WITHOUT NORMAL MAPS
	float nDotL : TEXCOORD1;
#endif

#if defined(EBG_FRESNEL_ON) && (!defined(EBG_POINT_LIGHT) || defined(EBG_INVERT_FRESNEL))
	fixed3 fresnel : TEXCOORD2;
#endif

#if !defined(EBG_NORMAL_MAP_ON) && defined(EBG_REFLECTIONS_ON)
	float4 reflectionDir_Fresnel : TEXCOORD3;
#endif

#if !defined(EBG_NORMAL_MAP_ON) && defined(EBG_SPEC_ON) && defined(EBG_ANISOTROPIC_ON)
	fixed3 spec : TEXCOORD4;
	float3 anisoN : TEXCOORD5;
#elif !defined(EBG_NORMAL_MAP_ON) && defined(EBG_SPEC_ON)
	half spec : TEXCOORD4;
#endif

#if defined(EBG_POINT_LIGHT)
	fixed3 pointLight : TEXCOORD6;
#endif

#if defined(EBG_SH_PROBES_ON)
	fixed3 color : TEXCOORD7;
#endif
};

float4 _MainTex_ST;

VtoS vertex_shader(Input v)
{
	VtoS data;
	
	data.position = UnityObjectToClipPos(v.vertex);
	 
	data.uv_main_fog.xy = v.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
	
	#if defined(EBG_DOUBLE_SIDED)
	float3 worldSpaceNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
	float3 worldSpaceViewDir = normalize(WorldSpaceViewDir(v.vertex));
	float frontFacing = step(0, dot(worldSpaceNormal, worldSpaceViewDir));
	float3 norm = lerp(-v.normal, v.normal, frontFacing);
	#else
	float3 norm = v.normal;
	#endif
	
#if defined(EBG_BLUEPRINT)
	float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
	data.uv_main_fog.z = worldPos.y/3.0; 
#elif defined(EBG_FOG_ON)
	data.uv_main_fog.z = EBGFogVertex(v.vertex);
#endif
	
#if !defined(EBG_NORMAL_MAP_ON) || defined(EBG_SH_PROBES_ON) || defined(EBG_POINT_LIGHT)
	float3 worldN = normalize(mul((float3x3)unity_ObjectToWorld, norm));
#endif
	
#if defined(EBG_NORMAL_MAP_ON)
	float3 local0 = normalize(mul((float3x3)unity_ObjectToWorld, v.tangent.xyz));
	float3 local2 = normalize(mul(norm, (float3x3)unity_WorldToObject));
	float3 local1 = normalize(cross(local2, local0) * (v.tangent.w * unity_WorldTransformParams.w));
	data.localSurface2World0 = float3(local0.x, local1.x, local2.x);
	data.localSurface2World1 = float3(local0.y, local1.y, local2.y);
	data.localSurface2World2 = float3(local0.z, local1.z, local2.z);
#endif

#if defined(EBG_NORMAL_MAP_ON) && (defined(EBG_SPEC_ON) || defined(EBG_FRESNEL_ON) || defined(EBG_REFLECTIONS_ON))
	data.viewDir = WorldSpaceViewDir(v.vertex);
#endif

#if !defined(EBG_NORMAL_MAP_ON)
	data.nDotL.x = max(0, (dot(worldN, _EBGCharDirectionToLight0.xyz) + _NDotLWrap) / (1 + _NDotLWrap));
#endif

#if !defined(EBG_NORMAL_MAP_ON) && defined(EBG_RIM_ON)
	data.nDotL.y = max(0, (dot(mul((half3x3)UNITY_MATRIX_V, worldN), _EBGCharDirectionToLight1.xyz) + _NDotLWrap1) / (1 + _NDotLWrap1)); 
#endif

#if (defined(EBG_FRESNEL_ON) || defined(EBG_REFLECTIONS_ON) || defined(EBG_SPEC_ON))
	float3 viewDirNorm = normalize(WorldSpaceViewDir(v.vertex));
#endif

#if (defined(EBG_FRESNEL_ON) || defined(EBG_REFLECTIONS_ON))
	float f = saturate(1.0f - dot(viewDirNorm, worldN));
#endif

#if defined(EBG_POINT_LIGHT)
	data.pointLight = EBGPointLight(v.vertex, worldN); 
#endif

#if defined(EBG_FRESNEL_ON) && defined(EBG_POINT_LIGHT) && !defined(EBG_INVERT_FRESNEL)
	data.pointLight += pow(f, EBG_VAL_FRESNEL_POWER) * EBG_VAL_FRESNEL_COLOR_INTENSITY;
#elif defined(EBG_FRESNEL_ON)
	data.fresnel.rgb = pow(f, EBG_VAL_FRESNEL_POWER) * EBG_VAL_FRESNEL_COLOR_INTENSITY;
#endif

#if !defined(EBG_NORMAL_MAP_ON) && defined(EBG_REFLECTIONS_ON)
	data.reflectionDir_Fresnel.xyz = reflect(-viewDirNorm, worldN);
	data.reflectionDir_Fresnel.w = _ReflectionFresnelIntensity * pow(f, _ReflectionFresnelPower) + 1.0f;
#endif

#if !defined(EBG_NORMAL_MAP_ON) && defined(EBG_SPEC_ON) && defined(EBG_ANISOTROPIC_ON)
	//aniso specular half angle
	data.spec = normalize(_EBGCharLightDirection0.xyz + viewDirNorm);
	data.anisoN = worldN * _AnisoMix;
#elif !defined(EBG_NORMAL_MAP_ON) && defined(EBG_SPEC_ON)
	//spec calculation
	float3 specularDir = reflect(_EBGCharLightDirection0.xyz, worldN);
	data.spec = max(0.0, dot(viewDirNorm, specularDir));
#endif
	
#if defined(EBG_SH_PROBES_ON)
	data.color = ShadeSH9(float4(worldN, 1)) * _EBGCharLightProbeScale;
#endif
	 
	return data;  
}

fixed4 fragment_shader(VtoS IN) : COLOR0
{				
//#if defined(EBG_DIFFUSE_ON)
//	fixed3 mainTex = 0.5;
//#else
	fixed3 mainTex = tex2D(_MainTex, IN.uv_main_fog.xy).rgb;
//#endif

#if defined(EBG_TINT_ON)
	mainTex *= _DiffuseTint;
#endif
	
#if defined(EBG_NORMAL_MAP_ON)
	//normal map
	fixed3 normalMapTex = UnpackNormal(tex2D(_NormalMap, IN.uv_main_fog.xy));
#endif
#if defined(EBG_NORMAL_MAP_ON) && defined(EBG_DETAIL_ON)
	//detail normal map
	fixed3 detailNormalMapTex = UnpackNormal(tex2D(_NormalMap, IN.uv_main_fog.xy * _DetailNormalMapTile.xy));
	normalMapTex += detailNormalMapTex;
#endif
#if defined(EBG_NORMAL_MAP_ON)
	float3 n;
	n.x = dot(normalMapTex, IN.localSurface2World0);
	n.y = dot(normalMapTex, IN.localSurface2World1);
	n.z = dot(normalMapTex, IN.localSurface2World2);
	n = normalize(n);
	
	half nDotL = max(0, (dot(n, _EBGCharDirectionToLight0.xyz) + _NDotLWrap) / (1 + _NDotLWrap));
#else
	half nDotL = IN.nDotL.x; 
#endif

#if defined(EBG_NORMAL_MAP_ON) && (defined(EBG_SPEC_ON) || defined(EBG_FRESNEL_ON) || defined(EBG_REFLECTIONS_ON))
	float3 viewDirNorm = normalize(IN.viewDir);
#endif
  
#if defined(EBG_EMISSIVE_ON)
  	//emissive
	fixed4 emissiveTex = tex2D(_EmissiveTex, IN.uv_main_fog.xy);
	fixed3 emissive = emissiveTex * _EmissiveColor.rgb;
	#if defined(EBG_REFLECTIONS_ON)
		fixed emissiveLuminance = EBGLuminance(emissive);
	#endif
#endif
	 
	//diffuse
#if defined(EBG_SH_PROBES_ON)
  	fixed3 res = nDotL * _EBGCharLightDiffuseColor0.rgb + IN.color;
#else
  	fixed3 res = nDotL * _EBGCharLightDiffuseColor0.rgb; 
#endif

	res *= mainTex;

#if defined(EBG_RIM_ON) && defined(EBG_NORMAL_MAP_ON)
	//rim, purposely additive
	half nDotL1 = max(0, (dot(mul((half3x3)UNITY_MATRIX_V, n), _EBGCharDirectionToLight1.xyz) + _NDotLWrap1) / (1 + _NDotLWrap1)); 
	res += nDotL1 * _EBGCharLightDiffuseColor1.rgb;
#elif defined(EBG_RIM_ON)
	res += IN.nDotL.y * _EBGCharLightDiffuseColor1.rgb;
#endif
	  
#if defined(EBG_POINT_LIGHT)
	//point light
	res += IN.pointLight;
#endif 

#if defined(EBG_NORMAL_MAP_ON) && (defined(EBG_FRESNEL_ON) || defined(EBG_REFLECTIONS_ON))
	half f = min(1, 1.0f - dot(viewDirNorm, n));
#endif

#if defined(EBG_FRESNEL_ON) && defined(EBG_NORMAL_MAP_ON)
	fixed3 fresnel = pow(f, EBG_VAL_FRESNEL_POWER) * EBG_VAL_FRESNEL_COLOR_INTENSITY;
	#if defined(EBG_INVERT_FRESNEL)
		res -= (1 - saturate(fresnel));
	#else
		res += fresnel;
	#endif
#elif defined(EBG_FRESNEL_ON) && (!defined(EBG_POINT_LIGHT) || defined(EBG_INVERT_FRESNEL))
	fixed3 fresnel = IN.fresnel;
	#if defined(EBG_INVERT_FRESNEL)
		res -= (1 - saturate(fresnel));
	#else
		res += fresnel;
	#endif
#endif
	  
#if defined(EBG_SPEC_ON)
	//spec
	fixed3 specTex = tex2D(_SpecTex, IN.uv_main_fog.xy).rgb;
#endif
#if defined(EBG_SPEC_ON) && defined(EBG_TINT_ON)
	specTex *= _SpecTint;
#endif
#if defined(EBG_SPEC_ON) && defined(EBG_ANISOTROPIC_ON)
	fixed3 anisotropicTex = tex2D(_AnisotropicTex, IN.uv_main_fog.xy).rgb;
	anisotropicTex = normalize(mul((float3x3)unity_WorldToObject, anisotropicTex));
	#if defined(EBG_NORMAL_MAP_ON)
	fixed3 h = normalize(_EBGCharLightDirection0.xyz + viewDirNorm);
	fixed HdotA = dot(normalize(anisotropicTex + n * _AnisoMix), h);
	#else
	fixed3 h = IN.spec;
	float3 anisoN = IN.anisoN;
	fixed HdotA = dot(normalize(anisotropicTex + anisoN), h);
	#endif
	half s = abs(sin(HdotA * 3.14159));
#elif defined(EBG_SPEC_ON) && defined(EBG_NORMAL_MAP_ON)
	fixed3 specularDir = reflect(_EBGCharLightDirection0.xyz, n);
	half s = max(0.0, dot(viewDirNorm, specularDir));
#elif defined(EBG_SPEC_ON)
	half s = IN.spec;
#endif
#if defined(EBG_SPEC_ON)
	fixed3 spec = max (0, _SpecularIntensity * pow(s, _SpecularGlossModulation) * specTex * _EBGCharLightSpecularColor0);
	#if defined(EBG_FRESNEL_ON) && defined(EBG_INVERT_FRESNEL)
		spec *= saturate(fresnel);
	#endif
	res += spec;
#endif

#if defined(EBG_REFLECTIONS_ON) && defined(EBG_NORMAL_MAP_ON)
	//reflections
	float3 reflectionDir = reflect(-viewDirNorm, n);
#elif defined(EBG_REFLECTIONS_ON)
	//reflections
	float3 reflectionDir = normalize(IN.reflectionDir_Fresnel.xyz);
#endif
#if defined(EBG_REFLECTIONS_ON) && defined(EBG_BLURRY_REFLECTIONS_ON)
	//blurry reflections
	fixed4 reflectionTex = texCUBE(_EBGCubemapBlurry, reflectionDir);
#elif defined(EBG_REFLECTIONS_ON)
	//sharp reflections
	fixed4 reflectionTex = texCUBE(_EBGCubemap, reflectionDir);
#endif
#if defined(EBG_REFLECTIONS_ON)
	fixed3 reflection = _ReflectionColor.rgb * reflectionTex.rgb * (1 + reflectionTex.a * _ReflectionHDR);
#endif
#if defined(EBG_REFLECTIONS_ON) && defined(EBG_SPEC_ON)
	//spec map masks out reflection
	reflection *= specTex;
#endif
#if defined(EBG_REFLECTIONS_ON) && defined(EBG_EMISSIVE_ON)
	//emission masks out reflection
	reflection *= (1 - emissiveLuminance);
#endif
#if defined(EBG_REFLECTIONS_ON) && defined(EBG_NORMAL_MAP_ON)
	reflection *=_ReflectionFresnelIntensity * pow(f, _ReflectionFresnelPower) + 1.0f;
#elif defined(EBG_REFLECTIONS_ON)
	reflection *= IN.reflectionDir_Fresnel.w;
#endif

#if defined(EBG_REFLECTIONS_ON) && defined(EBG_FRESNEL_ON) && defined(EBG_INVERT_FRESNEL)
	reflection *= saturate(fresnel);
#endif

#if defined(EBG_REFLECTIONS_ON)
	res += reflection;
#endif
	
#if defined(EBG_EMISSIVE_ON)
	//enhance emissive NOT pick the strong one
	//res = max(emissive, res);
	res = emissive*_EmissiveIntensity + res;
#endif

#if defined(EBG_BLUEPRINT)
	res += tex2D(_ScanlineTex, (IN.uv_main_fog.xy * 1.5) + (_Time.xx * 0.5)).rgb;
	res *= saturate(IN.uv_main_fog.z);
#elif defined(EBG_FOG_ON)
	res = EBGFogFragment(res, IN.uv_main_fog.z);
#endif
	
	return fixed4(res, 1); 
}
