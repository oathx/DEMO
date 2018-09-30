using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SLua;

[CustomLuaClass]
public enum DissolveType{
	DT_NORMAL = 0
}

/// <summary>
/// Shader effect.
/// </summary>
[CustomLuaClass]
public class ShaderEffect
{
	/// <summary>
	/// Dissolve the specified mats, go, fDuration, bIn and complete.
	/// </summary>
	/// <param name="mats">Mats.</param>
	/// <param name="go">Go.</param>
	/// <param name="fDuration">F duration.</param>
	/// <param name="bIn">If set to <c>true</c> b in.</param>
	/// <param name="complete">Complete.</param>
	public static bool Dissolve(DissolveType type, Material[] mats, GameObject go, float fDuration, bool bIn, System.Action complete)
	{
		switch (type) {
		case DissolveType.DT_NORMAL:
			return NormalDissolve (mats, go, fDuration, bIn, complete);
		}

		return false;
	}

	/// <summary>
	/// Normals the dissolve.
	/// </summary>
	/// <param name="mats">Mats.</param>
	/// <param name="go">Go.</param>
	/// <param name="fDuration">F duration.</param>
	/// <param name="bIn">If set to <c>true</c> b in.</param>
	/// <param name="complete">Complete.</param>
	public static bool NormalDissolve(Material[] dissovleMats, GameObject go, float fDuration, bool bIn, System.Action complete)
	{
		if (dissovleMats.Length < 2) return false;

		Material dissolve = dissovleMats[0];
		Material dissolveReflection = dissovleMats[1];

		Renderer[] skins = go.GetComponentsInChildren<Renderer>();
		List<Material[]> oldMatList = new List<Material[]>();
		for (int i = 0; i < skins.Length; i++)
		{
			List<Material> tempMatList = new List<Material>();
			Material[] skinMats = skins[i].materials;
			for (int j = 0; j < skinMats.Length; j++)
			{
				Material tempMat;
				if (skinMats[j].shader.name.IndexOf("REFLECTIONS_ON") != -1)
				{
					tempMat = Object.Instantiate(dissolveReflection);
				}
				else
				{
					tempMat = Object.Instantiate(dissolve);
				}

				Color color;
				ColorUtility.TryParseHtmlString("#61B2FF00", out color);
				if (skinMats[j].shader.renderQueue < 2450 && skinMats[j].shader.name != "Standard")
				{
					tempMat.SetTexture("_MainTex", skinMats[j].GetTexture("_MainTex"));
					tempMat.SetFloat("_NDotLWrap", skinMats[j].GetFloat("_NDotLWrap"));
					if (skinMats[j].HasProperty("_ShadowAdjust"))
					{
						tempMat.SetFloat("_ShadowAdjust", skinMats[j].GetFloat("_ShadowAdjust"));
					}
					if (skinMats[j].HasProperty("_PointLightsIntensity"))
					{
						tempMat.SetFloat("_PointLightsIntensity", skinMats[j].GetFloat("_PointLightsIntensity"));
					}
					if (skinMats[j].HasProperty("_AnisoMix"))
					{
						tempMat.SetFloat("_AnisoMix", skinMats[j].GetFloat("_AnisoMix"));
					}
					tempMat.SetFloat("_NDotLWrap1", skinMats[j].GetFloat("_NDotLWrap1"));
					tempMat.SetFloat("_SHIntensity", skinMats[j].GetFloat("_SHIntensity"));
					tempMat.SetTexture("_NormalMap", skinMats[j].GetTexture("_NormalMap"));
					tempMat.SetTexture("_DetailNormalMap", skinMats[j].GetTexture("_DetailNormalMap"));
					tempMat.SetVector("_DetailNormalMapTile", skinMats[j].GetVector("_DetailNormalMapTile"));
					tempMat.SetTexture("_SpecTex", skinMats[j].GetTexture("_SpecTex"));
					if (bIn)
					{
						tempMat.SetFloat("_SpecularIntensity", skinMats[j].GetFloat("_SpecularIntensity") / (2 / 0.75f));

					}
					else
					{
						tempMat.SetFloat("_SpecularIntensity", skinMats[j].GetFloat("_SpecularIntensity"));
					}
					tempMat.SetFloat("_SpecularGlossModulation", skinMats[j].GetFloat("_SpecularGlossModulation"));
					tempMat.SetTexture("_EmissiveTex", skinMats[j].GetTexture("_EmissiveTex"));
					tempMat.SetVector("_EmissiveColor", skinMats[j].GetVector("_EmissiveColor"));
					tempMat.SetFloat("_EmissiveIntensity", skinMats[j].GetFloat("_EmissiveIntensity"));
					tempMat.SetVector("_ReflectionColor", skinMats[j].GetVector("_ReflectionColor"));
					tempMat.SetFloat("_ReflectionHDR", skinMats[j].GetFloat("_ReflectionHDR"));
					tempMat.SetFloat("_ReflectionFresnelIntensity", skinMats[j].GetFloat("_ReflectionFresnelIntensity"));
					tempMat.SetFloat("_ReflectionFresnelPower", skinMats[j].GetFloat("_ReflectionFresnelPower"));
					tempMat.SetFloat("_FresnelPower", 4.5f);
					tempMat.SetVector("_FresnelColorIntensity", Color.black);
					if (bIn)
					{
						tempMat.SetFloat("_Amount", 1);
					}
					else
					{
						tempMat.SetFloat("_Amount", 0);
					}
					tempMatList.Add(tempMat);
				}
				else
				{
					if (!bIn)
					{

					}
					tempMatList.Add(skinMats[j]);
				}
			}
			oldMatList.Add(skins[i].materials);
			skins[i].materials = tempMatList.ToArray();
			skins[i].materials = skins[i].materials;
			skinMats = skins[i].materials;
			for (int j = 0; j < skinMats.Length; j++)
			{
				Tweener t = null;
				if (skinMats[j].renderQueue >= 2450)
				{
					string proColorName = string.Empty;
					if (skinMats[j].HasProperty("_Color"))
					{
						proColorName = "_Color";
					}
					else if (skinMats[j].HasProperty("_TintColor"))
					{
						proColorName = "_TintColor";
					}
					if (!string.IsNullOrEmpty(proColorName))
					{
						if (bIn)
						{
							Color color = skinMats[j].GetColor(proColorName);
							Color fakeColor = color;
							fakeColor.a = 0;
							skinMats[j].SetColor(proColorName, fakeColor);
							t = skinMats[j].DOColor(color, proColorName, fDuration).SetEase(Ease.InCubic);
						}
						else
						{
							Color color = skinMats[j].GetColor(proColorName);
							color.a = 0;
							t = skinMats[j].DOColor(color, proColorName, fDuration).SetEase(Ease.OutCubic);
						}
					}
				}
				else
				{
					if (bIn) 
					{
						t = skinMats[j].DOFloat(0, "_Amount", fDuration).SetEase(Ease.OutCubic);
					}
					else 
					{
						t = skinMats[j].DOFloat(1, "_Amount", fDuration).SetEase(Ease.InCubic);
					}
				}

				//if (i == skins.Length - 1 && j == skinMats.Length - 1) {
				if (i == 1 && j == 1 && t != null){
					t.OnComplete(delegate {
						for (int _i = 0; _i < skins.Length; _i++)
						{
							skins[_i].materials = oldMatList[_i];
						}
						if (complete != null) complete();
					});
				}
			}
		}
		return true;
	}
}


