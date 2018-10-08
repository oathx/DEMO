using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XWorldDemoApp : MonoBehaviour {
	public Transform 			center;
	public int 					radius;

	// Use this for initialization
	void Start () {
		XWorld.GetSingleton ().LoadWorld ("Terrain/generator", center, radius, delegate(XVec2I v) {
			
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
