using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Geom;

public class ExtrusionTest : MonoBehaviour {
	private static Vector3[] TEST_POLY1 = {
		new Vector3(4,0,4),
		new Vector3(4,0,5),
		new Vector3(5,0,5),
		new Vector3(5,0,4)
	};
	private static Vector3[] TEST_POLY2 = {
		new Vector3(0,0,0),
		new Vector3(1,0,1),
		new Vector3(2,0,0)
	};
	private static Vector3[] TEST_POLY3 = {
		new Vector3(0,0,0),
		new Vector3(0,0,2),
		new Vector3(2,0,2),
		new Vector3(2,0,1),
		new Vector3(1,0,1),
		new Vector3(1,0,0),
	};


	// Use this for initialization
	void Start () {

		Vector3[][] shapes = {TEST_POLY1, TEST_POLY3};

		foreach (Vector3[] shape in shapes) 
		{
			GameObject o = new GameObject();
			o.AddComponent("MeshFilter");
			o.AddComponent("MeshRenderer");
			Extrusion e = Extrusion.ExtrudeY(shape, 1.0f);
			e.LoadMesh(o.GetComponent<MeshFilter>().mesh);
		
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
