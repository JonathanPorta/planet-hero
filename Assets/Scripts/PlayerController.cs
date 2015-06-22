using UnityEngine;
using System.Collections;

public static class RendererExtensions
{
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}


public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public GameObject characterPrefab;

	private WeaponController weaponController;
	private GameObject character;
	private GameObject planet;
	private Orbit playerOrbit;

	void Awake(){
		// Register events
		EventManager.SelectPlanet += SetPlanet;
	}

	void Start(){

	}

	void Update(){
		// If we aren't on a planet or don't have a character, bail.
		if (planet == null || character == null) { return ;}

		playerOrbit.center = CalculateCenterOfGravity();
		float startingAngle = playerOrbit.GetAngleInDegrees();
		float nextAngle = playerOrbit.GetAngleInDegrees() + speed * -Input.GetAxis("Horizontal");
		playerOrbit.SetAngleInDegrees(nextAngle);

		if(!OnScreen(playerOrbit.CalculatePosition())){
			// reset the angle
			Debug.Log("NEW ANGLE OFF SCREEN");
			playerOrbit.SetAngleInDegrees(startingAngle);
		}

		// Set the new position
		transform.position = playerOrbit.CalculatePosition();
		transform.LookAt(playerOrbit.center);
			 
		if(Input.GetButton("Jump")){
			Firing();
		}
	}

	private bool OnScreen(Vector3 position){
		Debug.Log("testing pos:");Debug.Log(position);
		float buffer = 1.0f;
		Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(Camera.main.transform.position.y)));
		Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Mathf.Abs(Camera.main.transform.position.y)));
		Debug.Log("bottomLeft: ");Debug.Log(bottomLeft);Debug.Log("topRight: ");Debug.Log(topRight);

		return position.x-buffer > bottomLeft.x && position.x+buffer < topRight.x && position.z+buffer < topRight.z && position.z-buffer > bottomLeft.z;
	}

	private void Firing(){
		weaponController.Fire();
	}

	private void SetPlanet(GameObject selection){
		planet = selection;
		transform.parent = planet.transform;
		Debug.Log("SETTING NEW PLANET!");

		// Setup orbit based on selected planet
		float orbitHeight = planet.GetComponent<SphereCollider>().radius * planet.transform.localScale.x + characterPrefab.transform.localScale.x;
		playerOrbit = new Orbit(CalculateCenterOfGravity(), orbitHeight, 90.0f, 10.0f);
		
		if(character == null){
			//this.character = Instantiate(characterPrefab, playerOrbit.CalculatePosition(), Quaternion.identity) as GameObject;
			character = Instantiate(characterPrefab, transform.position, transform.rotation) as GameObject;
		}
		character.transform.parent = transform;
		weaponController = transform.GetComponentInChildren<WeaponController>();
	}

	private Vector3 CalculateCenterOfGravity(){
		return planet.transform.position;
	}
}