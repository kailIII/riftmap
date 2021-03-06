﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GIS {
	/*
	 * A feature corresponds to an entry in a GeoJSON file,
	 * and consists of one or more polygons.
	 */
	public class Feature {

		public List<List<Vector2>> polygons;
		public string name;
		public string id;
			
		public Feature(string name, string id, List<List<Vector2>> polygons) {
			this.name = name;
			this.id = id;
			this.polygons = polygons;
		}

		public override string  ToString() {
			StringBuilder sb = new StringBuilder();
			sb.Append ("name   : " + name + "\n" );
			sb.Append ("id     : " + id + "\n");
			sb.Append ("npolys : " + polygons.Count.ToString () + "\n");
			foreach (List<Vector2> polygon in polygons) {
				sb.Append ("\tnvertex : " + polygon.Count.ToString () + "\n");
			}
			return sb.ToString ();
		} 



	}
}
