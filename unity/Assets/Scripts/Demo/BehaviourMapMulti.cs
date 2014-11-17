using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Geom;
using GIS;

public class BehaviourMapMulti : MonoBehaviour {
	
	public TextAsset GEO_JSON;
	public TextAsset[] DATA;
	public string[] DATALABELS;
	public Material renderMaterial;

	private GameObject[] G;
	private string[] Labels;

	private int curr = 0;


	void Start () {

		List<Feature> F = GeoJSONParser.ParseGeoJSON(GEO_JSON.text);

		int nTimeSlices = DATA.Length;
		Utils.Assert (nTimeSlices != 0);
		Utils.Assert (nTimeSlices == DATALABELS.Length);
		G = new GameObject[nTimeSlices];
		Labels = new string[nTimeSlices];
	
		for (int i =0; i<nTimeSlices; i++) {
			Attributes a = AttributeCSVParser.ParseCSV(DATA[i].text, DATALABELS[i]);
			//TODO FIXME - hard coded the height scale, it should be a function of the max of all a's,
			// actually probably a function of the extents too... that's a bigger pandoras box than I want
			// to open right now.
			G[i] = Modeler.BuildGameObjectsFromFeaturesAndAttributes(renderMaterial,F,a);
			G[i].transform.RotateAround(Vector3.zero, Vector3.up, -90f);
			G[i].transform.localScale = new Vector3(.1f,.1f,.1f);
			G[i].SetActive (false);
			Labels[i] = a.Name;
		}
		G[0].SetActive (true);

	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			foreach (GameObject g in G) {
				g.SetActive (false);
			}
			curr = (curr + 1) % G.Length;
			G[curr].SetActive (true);
		}
	}

	void OnGUI(){
		GUI.Label(new Rect(30, 30, 300, 20), Labels[curr] + " (TAB key cycles)");
	}
}

