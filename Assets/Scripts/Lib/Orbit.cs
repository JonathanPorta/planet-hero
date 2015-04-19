using UnityEngine;
using System.Collections;

public class Orbit {
	public Vector3 center;
	public float distance;
	public float velocityInDegrees;
	
	private float angle;
	
	public Orbit(Vector3 center, float distance, float angleInDegrees, float velocityInDegrees){
		this.center = center;
		this.distance = distance;
		this.SetAngleInDegrees(angleInDegrees);
		this.velocityInDegrees = velocityInDegrees;
	}
	
	public Vector3 CalculatePosition(){
		float x = center.x + distance * Mathf.Cos(angle);
		float y = center.y + distance * Mathf.Sin(angle);
		return new Vector3(x, y, this.center.z);
	}
	
	public void UpdateAngle(){
		this.SetAngleInDegrees(this.GetAngleInDegrees() + this.velocityInDegrees * Time.deltaTime);
	}
	
	public void SetAngle(float angle){
		this.angle = angle;
	}
	
	public float GetAngle(){
		return this.angle;
	}
	
	public void SetAngleInDegrees(float angleInDegrees){
		this.angle = this.toRadians(angleInDegrees);
	}
	
	public float GetAngleInDegrees(){
		return this.toDegrees(this.angle);
	}
	
	private float toRadians(float degrees){
		return degrees * Mathf.PI / 180;
	}
	
	private float toDegrees(float radians){
		return radians * 180 / Mathf.PI;
	}
}