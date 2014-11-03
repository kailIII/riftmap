using UnityEngine;
using System.Collections;
using SimpleJSON;

public class JSONParseTestBehaviour : MonoBehaviour {		
	public TextAsset GEO_JSON;
	
	private string m_InGameLog = "";
	private Vector2 m_Position = Vector2.zero;
	void P(string aText)
	{
		m_InGameLog += aText + "\n";
	}
	
	void Test()
	{
		/*var N = JSONNode.Parse("{\"name\":\"test\", \"array\":[1,{\"data\":\"value\"}]}");
		P("'nice formatted' string representation of the JSON tree:");
		P(N.ToString(""));
		P("");*/

		var N = JSONNode.Parse (GEO_JSON.text);
		P("'nice formatted' string representation of the JSON tree:");
		P(N.ToString(""));
		P("");




		
		/*P("'normal' string representation of the JSON tree:");
		P(N.ToString());
		P("");
		
		P("content of member 'name':");
		P(N["name"]);
		P("");
		
		P("content of member 'array':");
		P(N["array"].ToString(""));
		P("");
		
		P("first element of member 'array': " + N["array"][0]);
		P("");
		
		N["array"][0].AsInt = 10;
		P("value of the first element set to: " + N["array"][0]);
		P("The value of the first element as integer: " + N["array"][0].AsInt);
		P("");
		
		P("N[\"array\"][1][\"data\"] == " + N["array"][1]["data"]);
		P("");
		*/
	}
	
	void Start()
	{
		Test();
		Debug.Log("Test results:\n" + m_InGameLog);
	}
	void OnGUI()
	{
		m_Position = GUILayout.BeginScrollView(m_Position);
		GUILayout.Label(m_InGameLog);
		GUILayout.EndScrollView();
	}
}