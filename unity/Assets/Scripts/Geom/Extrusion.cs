using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using LibTessDotNet;

/* An extrusion is a utility wrapper around a Unity Mesh object */
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
			
			// TRIANGULATION - SEMI WORKING VERSION
			// next triangulate the orignal poly to get end cap indecies

			Triangulator tr = new Triangulator(shape);
			int[] _baseIndecies = tr.Triangulate();

			// Uhh -  cap is being drawin with the wrong handedness..
			// ***for the data set I am using ***
			Array.Reverse(_baseIndecies);
			foreach (int i in _baseIndecies)   //top cap
				_T.Add (s + i);



			/*
			NEW TRIANGULATION
			
			Vector2[] capVertecies;
			int[] capIndecies;

			var tess = new LibTessDotNet.Tess();
			var contour = new LibTessDotNet.ContourVertex[shape.Length];
			for (int i = 0; i < shape.Length; i++)
			{
				// NOTE : Z is here for convenience if you want to keep a 3D vertex position throughout the tessellation process but only X and Y are important.
				// 0, x = 0, y=1
				// 1, x = 2, y = 3
				contour[i].Position = new LibTessDotNet.Vec3 { X = shape[i*2].x , Y = shape[i*2 + 1].y, Z = 0.0f };

				// Data can contain any per-vertex data, here a constant color.
				contour[i].Data = Color.Azure;
			}
			tess.AddContour(contour, LibTessDotNet.ContourOrientation.Clockwise);

			// Tessellate!
			// The winding rule determines how the different contours are combined together.
			// See http://www.glprogramming.com/red/chapter11.html (section "Winding Numbers and Winding Rules") for more information.
			// If you want triangles as output, you need to use "Polygons" type as output and 3 vertices per polygon.
			tess.Tessellate(LibTessDotNet.WindingRule.EvenOdd, LibTessDotNet.ElementType.Polygons, 3, VertexCombine);


			/// LEAVING OFF HERE.  I think what I need to do is add the verecies in tess.Elements to
			/// my mesh's vertecies, and add the indecies in the loop below to the meshe's indexes.
			// Output triangles
			int numTriangles = tess.ElementCount;
			for (int i = 0; i < numTriangles; i++)
			{
				var v0 = tess.Vertices[tess.Elements[i * 3]].Position;
				var v1 = tess.Vertices[tess.Elements[i * 3 + 1]].Position;
				var v2 = tess.Vertices[tess.Elements[i * 3 + 2]].Position;
				//Console.WriteLine("#{0} ({1:F1},{2:F1}) ({3:F1},{4:F1}) ({5:F1},{6:F1})", i, v0.X, v0.Y, v1.X, v1.Y, v2.X, v2.Y);
				Vector2
			}
			//Console.ReadLine();
				*/

			int[] T = _T.ToArray ();
			return new Extrusion(V, T);
			
		}
		
		public void RecalculateNormals(){
			mesh.Clear();
			mesh.RecalculateNormals();
		}
		
	}
}
