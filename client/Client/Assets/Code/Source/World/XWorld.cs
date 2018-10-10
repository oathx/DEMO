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
    private XVec2I              centerChunkPosition;

	/// <summary>
	/// The generator.
	/// </summary>
	public XTerrainGenerator 	generator;
    public int                  radius;
	public Transform 			center;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {
		CanActivateCharacter 	= false;
		radius 					= 2;

        if (!center)
        {
            GameObject defaultCenterTrans = new GameObject();
            defaultCenterTrans.transform.position = Vector3.zero;
            center = defaultCenterTrans.transform;
        }
	}

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        StartCoroutine(OnLoadWorld(delegate(XVec2I v){

        }));
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update(){
		if (CanActivateCharacter && center.gameObject.activeSelf) {
			XVec2I curChunkPosition = generator.GetChunkPosition(center.position);
			if (!curChunkPosition.Equals(centerChunkPosition))
			{
				generator.UpdateTerrain(center.position, radius);
				centerChunkPosition = curChunkPosition;
			}	
		}
	}

	/// <summary>
	/// Enumerators the load world.
	/// </summary>
	/// <returns>The load world.</returns>
	/// <param name="center">Center.</param>
	/// <param name="radius">Radius.</param>
	/// <param name="complate">Complate.</param>
	IEnumerator OnLoadWorld(System.Action<XVec2I> complate) {
		generator.InitGenerate ();
		generator.UpdateTerrain(center.position, radius);

		do
		{
			var exists = generator.IsTerrainAvailable(center.position);
			if (exists)
				CanActivateCharacter = true;
			yield return null;

		} while (!CanActivateCharacter);

		centerChunkPosition = generator.GetChunkPosition(center.position);
		center.position = new Vector3(center.position.x, 
			generator.GetTerrainHeight(center.position) + 0.5f, center.position.z);

		center.gameObject.SetActive(true);

		if (complate != null) {
			complate (centerChunkPosition);
		}
	}
}
