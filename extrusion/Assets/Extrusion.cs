using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Geom

{
	public class Extrusion {
		private Vector3[] vertecies;
		private int[] triangles;
		
		public Extrusion(Vector3[] vertecies, int[] triangles) {
			this.vertecies = vertecies;
			this.triangles = triangles;
		}
		
		public static Extrusion ExtrudeY(Vector3[] shape, float height) {
			// First double the vertex array with extruded y values
			Vector3[] V = new Vector3[shape.Length * 2];
			int s = shape.Length;
			for (int i = 0; i<s; i++) {
				V[i] = shape[i];
				V[i+s] = shape[i];
				V[i+s].y = V[i].y + height;
			}
			

			// Next create the sides, an array of triangle vertecies.  Every 3 indecies in the array
			// is a triangle.
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
			Vector2[] _base = new Vector2[s];
			for (var i = 0; i<s; i++) 
				_base[i] = new Vector2(shape[i].x, shape[i].z);
			Triangulator tr = new Triangulator(_base);
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
		
		public void LoadMesh(Mesh mesh) {
			mesh.Clear();
			mesh.vertices = this.vertecies;
			mesh.triangles = this.triangles;
			mesh.RecalculateNormals();
		}
		
	}
}
