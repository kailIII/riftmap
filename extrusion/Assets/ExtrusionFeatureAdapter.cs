using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExtrusionFeatureAdapter {

	// I get that it is pretty brain dead to use all of this memory
	// just to pad out the vector and to transform it into an array
	// rather than a list.  In the spirit of avoiding premature optimization
	// though ...
	public static Vector3[] PolygonToShape(List<Vector2> polygon) {
		List<Vector3> l = new List<Vector3>();
		foreach (Vector2 point in polygon) {
			l.Add (new Vector3(point.x, 0, point.y));
		}
		return l.ToArray ();
	}	      
}
