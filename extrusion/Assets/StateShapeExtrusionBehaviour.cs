using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Geom;

public class StateShapeExtrusionBehaviour : MonoBehaviour {

	public TextAsset GEO_JSON;
	
	void Start () {
		GameObject rootNode = new GameObject();

		List<Feature> F = GeoJSONParser.ParseGeoJSON(GEO_JSON.text);
		foreach (Feature f in F) {
//			if (f.name != "Florida") {
//				continue;
//			}
			float height = 1.0f;
			List<Extrusion> E = FeatureExtruder.BuildExtrusions(f, height);
			foreach (Extrusion e in E) {
				GameObject o = new GameObject();
				o.AddComponent("MeshFilter");
				o.AddComponent("MeshRenderer");
				Mesh omesh = o.GetComponent<MeshFilter>().mesh;
			 	
				// not sure why omesh = e.mesh doesnt work here.
				omesh.vertices = e.mesh.vertices;
				omesh.triangles = e.mesh.triangles;
				omesh.RecalculateNormals();
				o.transform.parent = rootNode.transform;
			}
		}
		rootNode.transform.RotateAround(Vector3.zero, Vector3.up, -90f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
