using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Geom;

public class BehaviourExtrusionTest : MonoBehaviour {
	private static Vector2[] TEST_POLY1 = {
		new Vector3(4,4),
		new Vector3(4, 5),
		new Vector3(5, 5),
		new Vector3(5,4)
	};
	private static Vector2[] TEST_POLY2 = {
		new Vector3(0,0),
		new Vector3(0,2),
		new Vector3(2,2),
		new Vector3(2,1),
		new Vector3(1,1),
		new Vector3(1,0),
	};


	// Use this for initialization
	void Start () {

		Vector2[][] shapes = {TEST_POLY1, TEST_POLY2};

		foreach (Vector2[] shape in shapes) 
		{
			GameObject o = new GameObject();
			o.AddComponent("MeshFilter");
			o.AddComponent("MeshRenderer");
			Extrusion e = Extrusion.ExtrudeY(shape, 1.0f);
			Mesh omesh = o.GetComponent<MeshFilter>().mesh;
			// Seems like I should just be able to assign omesh=e.mesh
			// but that doesn't render.  Doesn't this way use twice the memory?
			omesh.vertices = e.mesh.vertices;
			omesh.triangles = e.mesh.triangles;
			omesh.RecalculateNormals();

		}
	}
}
