using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class GeoJSONParser{

	public static void P(string msg) {
		Debug.Log(msg);
	}

	public static List<Feature> ParseGeoJSON(string json)
	{
		List<Feature> F = new List<Feature>();
		
		var N = JSONNode.Parse (json);
		//P("");
		JSONArray features = (JSONArray) N["features"];
		//P (features.Count.ToString ());
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

		return F;
		
	}

}
