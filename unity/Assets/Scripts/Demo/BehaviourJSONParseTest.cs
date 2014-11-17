using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GIS;

public class BehaviourJSONParseTest : MonoBehaviour {		
	public TextAsset GEO_JSON;
	
	private string m_InGameLog = "";
	private Vector2 m_Position = Vector2.zero;
	private List<Feature> F;

	void P(string aText)
	{
		m_InGameLog += aText + "\n";
	}
	
	void Start()
	{
		List<Feature> F = GeoJSONParser.ParseGeoJSON(GEO_JSON.text);
		foreach (Feature f in F) {
			P (f.ToString ());
			P ("");
		}
	}
	void OnGUI()
	{
		m_Position = GUILayout.BeginScrollView(m_Position);
		GUILayout.Label(m_InGameLog);
		GUILayout.EndScrollView();
	}
}