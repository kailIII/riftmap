using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Geom;
using GIS;

public class Modeler  {

	public static GameObject BuildGameObjectsFromFeaturesAndAttributes(Material m, List<Feature> F, Attributes A, float heightScale = .25f) 
	{

		GameObject rootNode = new GameObject();
		foreach (Feature f in F) {
			float height = A.Get(f.id) * heightScale;
			List<Extrusion> E = BuildExtrusions(f, height);
			foreach (Extrusion e in E) {
				GameObject o = new GameObject();
				o.AddComponent("MeshFilter");
				o.AddComponent("MeshRenderer");
				Mesh omesh = o.GetComponent<MeshFilter>().mesh;
				o.renderer.material = m;
				
				// not sure why omesh = e.mesh doesnt work here.
				omesh.vertices = e.mesh.vertices;
				omesh.triangles = e.mesh.triangles;

				// This is slightly voodoo to me, comes from
				// http://docs.unity3d.com/ScriptReference/Mesh-uv.html
				// I understand that u,v aligns texture to model.
				// It adds a fair amount of time to startup, and I'm not
				// actually using any textures, so?
				Vector2[] uvs = new Vector2[omesh.vertices.Length];
				int i = 0;
				while (i < uvs.Length) {
					uvs[i] = new Vector2(omesh.vertices[i].x, omesh.vertices[i].z);
					i++;
				}
				omesh.uv = uvs;


				omesh.RecalculateNormals();
				o.transform.parent = rootNode.transform;
			}
		}
		return rootNode;
	}

	private static List<Extrusion> BuildExtrusions (Feature f, float height) {
		List<Extrusion> E = new List<Extrusion>();
		foreach (List<Vector2> polygon in f.polygons) {
			E.Add(Extrusion.ExtrudeY (polygon.ToArray (), height));
		}
		return E;
	}



}
