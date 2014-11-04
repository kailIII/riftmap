using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Geom;

public class FeatureExtruder  {

	public static List<Extrusion> BuildExtrusions (Feature f, float height) {
		List<Extrusion> E = new List<Extrusion>();
		foreach (List<Vector2> polygon in f.polygons) {
			E.Add(Extrusion.ExtrudeY (polygon.ToArray (), height));
		}
		return E;
	}
}
