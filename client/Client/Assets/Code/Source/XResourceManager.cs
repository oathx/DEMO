using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SLua;
using UnityEngine.SceneManagement;

[CustomLuaClass]
public class XResourceManager : MonoBehaviour 
{
	/// <summary>
	/// The instance.
	/// </summary>
    static XResourceManager instance;

	/// <summary>
	/// Gets the singleton.
	/// </summary>
	/// <returns>The singleton.</returns>
    public static XResourceManager GetSingleton()
	{
		if (!instance) {
            GameObject singleton = new GameObject(typeof(XResourceManager).Name);
			if (!singleton)
				throw new System.NullReferenceException ();

            instance = singleton.AddComponent<XResourceManager>();

			GameObject.DontDestroyOnLoad (singleton);
		}

		return instance;
	}

	/// <summary>
	/// Loads the async.
	/// </summary>
	/// <returns><c>true</c>, if async was loaded, <c>false</c> otherwise.</returns>
	/// <param name="url">URL.</param>
	/// <param name="type">Type.</param>
	/// <param name="complete">Complete.</param>
	public Coroutine		LoadAsync(string url, System.Type type, System.Action<Object> complete)
	{
		if (string.IsNullOrEmpty (url))
			throw new System.NullReferenceException (url);

		return StartCoroutine (OnLoadAsync (url, type, complete));
	}

	/// <summary>
	/// Raises the load async event.
	/// </summary>
	/// <param name="url">URL.</param>
	/// <param name="type">Type.</param>
	/// <param name="complete">Complete.</param>
	IEnumerator				OnLoadAsync(string url, System.Type type,
		System.Action<Object> complete)
	{
		ResourceRequest req = Resources.LoadAsync (url, type);
		yield return req;

		if (complete != null) {
			complete (req.asset);
		}
	}

	/// <summary>
	/// Loads the multi async.
	/// </summary>
	/// <returns><c>true</c>, if multi async was loaded, <c>false</c> otherwise.</returns>
	/// <param name="aryUrl">Ary URL.</param>
	/// <param name="type">Type.</param>
	/// <param name="complete">Complete.</param>
	public Coroutine		LoadMultiAsync(string[] aryUrl, System.Type type, System.Action<Object[]> complete)
	{
		return StartCoroutine (OnLoadMultiAsync (aryUrl, type, complete));
	}

	/// <summary>
	/// Raises the load multi async event.
	/// </summary>
	/// <param name="aryUrl">Ary URL.</param>
	/// <param name="type">Type.</param>
	/// <param name="complete">Complete.</param>
	IEnumerator				OnLoadMultiAsync(string[] aryUrl, System.Type type, System.Action<Object[]> complete)
	{
		Object[] aryLoaded = new Object[aryUrl.Length];

		for (int idx = 0; idx < aryUrl.Length; idx++) {
			yield return LoadAsync(aryUrl[idx], type, delegate(Object obj) {
				aryLoaded[idx] = obj;
			});
		}

		complete(aryLoaded);
	}

	/// <summary>
	/// Loads the scene.
	/// </summary>
	/// <returns><c>true</c>, if scene was loaded, <c>false</c> otherwise.</returns>
	/// <param name="szName">Size name.</param>
	/// <param name="complete">Complete.</param>
	public bool				LoadScene(string url, System.Action<string> complete)
	{
		if (string.IsNullOrEmpty (url))
			throw new System.NullReferenceException (url);

		StartCoroutine (OnLoadScene (url, complete));

		return true;
	}

	/// <summary>
	/// Raises the load scene event.
	/// </summary>
	/// <param name="szName">Size name.</param>
	/// <param name="complete">Complete.</param>
	IEnumerator				OnLoadScene(string szName, 
		System.Action<string> complete)
	{
		AsyncOperation ao = SceneManager.LoadSceneAsync (szName);
		yield return ao;

		if (ao.isDone) {
			complete (szName);
		}
	}
}
