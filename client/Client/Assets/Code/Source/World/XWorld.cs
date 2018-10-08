using System;
using System.Collections;
using UnityEngine;
using SLua;
/// <summary>
/// X world.
/// </summary>
[CustomLuaClass]
public class XWorld : MonoBehaviour {	
	/// <summary>
	/// The instance.
	/// </summary>
	private static XWorld 		instance;

	/// <summary>
	/// Gets the singleton.
	/// </summary>
	/// <returns>The singleton.</returns>
	public static XWorld GetSingleton(){
		if (!instance) {
			GameObject singleton = new GameObject (typeof(XWorld).Name);
			if (!singleton)
				throw new System.NullReferenceException ();

			instance = singleton.AddComponent<XWorld> ();

			GameObject.DontDestroyOnLoad (singleton);
		}

		return instance;
	}

	/// <summary>
	/// The can activate character.
	/// </summary>
	private bool 				CanActivateCharacter;

	/// <summary>
	/// The generator.
	/// </summary>
	public XTerrainGenerator 	Generator;
	public int 					Radius;
	public XVec2I 				CenterChunkPosition;

	/// <summary>
	/// The center.
	/// </summary>
	public Transform 			Center;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {
		CanActivateCharacter 	= false;
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start(){
#if UNITY_EDITOR
		LoadWorld (Center, Radius, delegate(XVec2I centerChunkPosition) {
			
		});
#endif
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update(){
		if (Center.gameObject.activeSelf && CanActivateCharacter) {
			XVec2I curChunkPosition = Generator.GetChunkPosition(Center.position);
			if (!curChunkPosition.Equals(CenterChunkPosition))
			{
				Generator.UpdateTerrain(Center.position, Radius);
				CenterChunkPosition = curChunkPosition;
			}	
		}
	}

	/// <summary>
	/// Loads the world.
	/// </summary>
	/// <param name="center">Center.</param>
	/// <param name="radius">Radius.</param>
	/// <param name="complate">Complate.</param>
	public void LoadWorld(Transform center, int radius, System.Action<XVec2I> complate){
		StartCoroutine (OnLoadWorld (center, radius, complate));
	}

	/// <summary>
	/// Enumerators the load world.
	/// </summary>
	/// <returns>The load world.</returns>
	/// <param name="center">Center.</param>
	/// <param name="radius">Radius.</param>
	/// <param name="complate">Complate.</param>
	IEnumerator OnLoadWorld(Transform center, 
		int radius, System.Action<XVec2I> complate) {

		Center = center;
		Radius = radius;

		Generator.InitGenerate ();
		Generator.UpdateTerrain(Center.position, Radius);

		do
		{
			var exists = Generator.IsTerrainAvailable(Center.position);
			if (exists)
				CanActivateCharacter = true;
			yield return null;

		} while (!CanActivateCharacter);

		CenterChunkPosition = Generator.GetChunkPosition(Center.position);
		Center.position = new Vector3(Center.position.x, 
			Generator.GetTerrainHeight(Center.position) + 0.5f, Center.position.z);

		Center.gameObject.SetActive(true);

		if (complate != null) {
			complate (CenterChunkPosition);
		}
	}
}
