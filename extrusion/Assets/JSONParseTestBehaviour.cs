using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class JSONParseTestBehaviour : MonoBehaviour {		
	public TextAsset GEO_JSON;
	
	private string m_InGameLog = "";
	private Vector2 m_Position = Vector2.zero;
	private List<Feature> F;

	void P(string aText)
	{
		m_InGameLog += aText + "\n";
	}
	
	void Parse()
	{
		F = new List<Feature>();

		var N = JSONNode.Parse (GEO_JSON.text);
		//P("");
		JSONArray features = (JSONArray) N["features"];
		P (features.Count.ToString ());
		foreach (JSONNode feature in features) {
			JSONNode geometry;
			string name;
			string id;
			string type;
			List<List<Vector2>> V = new List<List<Vector2>>();

			geometry = feature["geometry"];
			name = feature["properties"]["NAME"].Value;
			id = feature["properties"]["GEO_ID"].Value;
		//	P(name + " => " + id);
			type = geometry["type"].Value;

		
			/*
			 if (type == "OLDPolygon") {
				JSONArray coordinatesArray = (JSONArray) geometry["coordinates"];
				foreach (JSONArray coordinates in coordinatesArray) {
					List<Vector2> lcoordinates = new List<Vector2>();
					foreach(JSONArray point in coordinates) {
						float x = point[0].AsFloat;
						float y = point[1].AsFloat;
						lcoordinates.Add (new Vector2(x, y));
					}
					V.Add(lcoordinates);
				}
			}
			*/
			if (type=="Polygon") {
				List<Vector2> lcoordinates = new List<Vector2>();
				foreach(JSONArray point in (JSONArray) geometry["coordinates"][0]) {
					float x = point[0].AsFloat;
					float y = point[1].AsFloat;
					lcoordinates.Add (new Vector2(x, y));
				}
				V.Add(lcoordinates);
			}
			else if (type == "MultiPolygon") {
				foreach (JSONArray c in (JSONArray) geometry["coordinates"]) {
					List<Vector2> lcoordinates = new List<Vector2>();
					foreach(JSONArray point in (JSONArray) c[0]) {
						float x = point[0].AsFloat;
						float y = point[1].AsFloat;
						lcoordinates.Add (new Vector2(x, y));
					}
					V.Add(lcoordinates);
				}
			} else {
				throw new System.SystemException("Not a Polygon and not a MultiPolygon");
			}

			F.Add (new Feature(name, id, V));

		} 

		foreach (Feature f in F) {
			P (f.ToString ());
			P ("");
		}

	}
	
	void Start()
	{
		Parse();
		Debug.Log("Test results:\n" + m_InGameLog);
	}
	void OnGUI()
	{
		m_Position = GUILayout.BeginScrollView(m_Position);
		GUILayout.Label(m_InGameLog);
		GUILayout.EndScrollView();
	}
}