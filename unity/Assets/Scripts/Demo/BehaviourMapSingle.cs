using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Geom;
using GIS;

public class BehaviourMapSingle: MonoBehaviour {

	public TextAsset GEO_JSON;
	public TextAsset DATA;
	public Material renderMaterial;
	
	void Start () {
		List<Feature> F = GeoJSONParser.ParseGeoJSON(GEO_JSON.text);
		Attributes A = AttributeCSVParser.ParseCSV(DATA.text);
		GameObject g = Modeler.BuildGameObjectsFromFeaturesAndAttributes (renderMaterial, F, A);
		g.transform.RotateAround(Vector3.zero, Vector3.up, -90f);
		g.transform.localScale = new Vector3(.1f,.1f,.1f);
	}
}
