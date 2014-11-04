using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Geom

{
	public class Extrusion {

		public Mesh mesh;

		public Extrusion(Vector3[] vertices, int[] triangles) {
			mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.triangles = triangles;
		}
		
		public static Extrusion ExtrudeY(Vector2[] shape, float height) {

			// First double the vertex array with extruded y values
			// This deals with CARTESIAN COORDINATES
			Vector3[] V = new Vector3[shape.Length * 2];
			int s = shape.Length;
			for (int i = 0; i<s; i++) {
				V[i] = new Vector3(shape[i].x, 0, shape[i].y);
				V[i+s] = new Vector3(shape[i].x, height, shape[i].y);
			}
			

			// Next create the sides, an array of triangle vertecies.  
			// Every 3 indecies in the array is a triangle.
			// This deals with INDECIES.
			List<int> _T = new List<int>();

			for (int i = 0; i < s; i++) {
				int v0 = i;
				int v1;
				int v2;
				int v3;
				
				if ((i + 1) < s) 
				{
					v1 = i+1;
				} else {
					v1 = 0;
				}
				v2 = v1 + s;
				v3 = v0 + s;
				

				_T.Add(v0);
				_T.Add(v1);
				_T.Add(v2);
				
				_T.Add(v0);
				_T.Add(v2);
				_T.Add(v3);
			}
			
			// next triangulate the orignal poly to get end cap indecies
			Triangulator tr = new Triangulator(shape);
			int[] _baseIndecies = tr.Triangulate();

			foreach (int i in _baseIndecies)   //top cap
				_T.Add (s + i);

			Array.Reverse (_baseIndecies);

			foreach (int i in _baseIndecies)// bottom cap
			{
				_T.Add(i);
			}


			int[] T = _T.ToArray ();
			return new Extrusion(V, T);
			
		}
		
		public void RecalculateNormals(){
			mesh.Clear();
			mesh.RecalculateNormals();
		}
		
	}
}
