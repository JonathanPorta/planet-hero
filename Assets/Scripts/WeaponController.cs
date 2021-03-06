﻿using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (LineRenderer))]
//[RequireComponent (typeof (Light))]

public class WeaponController : MonoBehaviour {

	public float maximumDamage = 120f;
	public float minimumDamage = 45f;
	public AudioClip shotClip;
	public float flashIntensity = 10f;
	public float fadeSpeed = 10f;
	public float fireRate = 0.1f;
	public float effectiveRange = 50.0f;
  public GameObject laserBoltPrefab;
  public Material laserBoltMaterial;

	private Light laserShotLight;
	private bool firing;
	private float scaledDamage;
	private float lastFire;
	private float nextFire;

	void Awake(){
		this.laserShotLight = GetComponentInChildren<Light>();
	}

	// Use this for initialization
	void Start(){
		laserShotLight.intensity = 0f;
		scaledDamage = maximumDamage - minimumDamage;
	}
	
	// Update is called once per frame
	void Update(){
		if(firing && Time.time >= nextFire){
			// Done firing this round, cleanup the laser line
			firing = false;
		}
		// Fade the light each update until it's off
		laserShotLight.intensity = Mathf.Lerp(laserShotLight.intensity, 0f, fadeSpeed * Time.deltaTime);
	}

	//public void Fire(Vector3 direction){
	//	if(firing || Time.time < nextFire){ return ; } // No double firing and respect the fire rate.
	//	
	//	firing = true;
	//	lastFire = Time.time;
	//	nextFire = Time.time + fireRate;

	//	RaycastHit hit;
	//	Vector3 laserEndPoint;

		// Cast a ray as far as the weapon is effective and see if we hit something
	//	if(Physics.Raycast(transform.position, direction, out hit, effectiveRange)){
	//		GameObject recipient = hit.transform.gameObject;
	//		laserEndPoint = recipient.transform.position;

      //TODO: Move damage into the laserbolt script
	//		HitpointController hitpointController = recipient.GetComponent<HitpointController>();
	//		if(hitpointController != null){
	//			float damage = ComputeDamage(recipient.transform.position);
	//			hitpointController.Damage(damage);
	//		}
	//		else {
	//			Debug.Log("GameObject did not have a HitpointController. Not applying damages.");
	//		}
	//	}
	//	else { // We still need a point to draw our laser, even if we aren't hitting anything.
  //			Ray ray = new Ray(transform.position, direction);
	//		laserEndPoint = ray.GetPoint(effectiveRange);
	//	}
	//	ShowFiringEffects(laserEndPoint);
	//}

	public void Fire(){
		if(firing || Time.time < nextFire){ return ; } // No double firing and respect the fire rate.

		firing = true;
		lastFire = Time.time;
		nextFire = Time.time + fireRate;

		//float damage = ComputeDamage(target.transform.position);
		
		// Damage the target
		//target.GetComponent<HitpointController>().Damage(damage);

		ShowFiringEffects();
	}

	//private float ComputeDamage(Vector3 target){
    //TODO: Move damage into the laserbolt script
		//float distance = Vector3.Distance(transform.position, target);
		//if(distance > effectiveRange) // Weapon is not effective past its effective range
		//	return 0.0f;
		//float effectiveness = Mathf.Clamp01((effectiveRange - distance) / effectiveRange);
		//float damage = minimumDamage + scaledDamage * effectiveness;
		//return damage;
	//}

	private void ShowFiringEffects(){
    GameObject bolt = Instantiate(laserBoltPrefab, transform.position, transform.rotation) as GameObject;
    bolt.GetComponentInChildren<Renderer>().material = laserBoltMaterial;
    bolt.GetComponentInChildren<LaserBolt>().effectiveRange = effectiveRange;
 
		// Flash the light
		laserShotLight.intensity = flashIntensity;
		
		// Play the gun shot clip at the position of the muzzle flare.
		AudioSource.PlayClipAtPoint(shotClip, laserShotLight.transform.position);
	}
}
