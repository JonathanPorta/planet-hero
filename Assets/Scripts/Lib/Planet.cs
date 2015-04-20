using UnityEngine;
using System.Collections;

public class Planet : Gridable {
	public GameObject prefab;
	public Orbit orbit = null;
	public GameObject gameObject = null;
	
	public Planet(GameObject prefab){
		this.prefab = prefab;
	}

	public Planet(GameObject prefab, Orbit orbit){
		this.prefab = prefab;
		this.orbit = orbit;
	}
	
	override public GameObject GetPrefab(){
		return this.prefab;
	}

	public void DrawOrbit(){
		//TODO: This needs some serious work. It is probably best to do this with a texture, or a vector line plugin.
		// Shamelessly ctrl+c'd, ctrl+v'd from http://stackoverflow.com/questions/13708395/unity-3d-draw-circle
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		if(lineRenderer == null){
			lineRenderer = gameObject.AddComponent<LineRenderer>();
			//float radius = orbit.distance;
			//float thetaScale = 1.0f;             //Set lower to add more points
			//int size = Mathf.RoundToInt(radius * Mathf.PI / thetaScale); //Total number of points in circle.

			lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
			//lineRenderer.SetColors(c1, c2);
			lineRenderer.SetWidth(0.2F, 0.2F);
			//lineRenderer.SetVertexCount(360);

			
			//int i = 0;
			//for(float theta = 0; theta < radius * Mathf.PI; theta += thetaScale) {
			//	float x = radius * Mathf.Cos(theta);
			//	float y = radius * Mathf.Sin(theta);
			//	
			//	Vector3 pos = new Vector3(x, y, 0);
			//	lineRenderer.SetPosition(i, pos);
			//	i+=1;
			//}

			float degreesPerSegment = 0.1f;
			float currentAngle = 0.0f;
			lineRenderer.SetVertexCount(Mathf.RoundToInt(360 / degreesPerSegment + 1));

			float y = 0.0f;
			for (int i = 0; i < 360 / degreesPerSegment + 1; i++)
			{
				float currentAngleInRadians = currentAngle * Mathf.PI / 180;
				float x =  orbit.center.x + orbit.distance * Mathf.Cos(currentAngleInRadians);
				float z =  orbit.center.z + orbit.distance * Mathf.Sin(currentAngleInRadians);

				lineRenderer.SetPosition(i, new Vector3(x, y, z));
				currentAngle += degreesPerSegment;
			}
			// We don't want things bumping into our orbit lines. They're just for decoration, bro!
			//lineRenderer.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		}
	}
}