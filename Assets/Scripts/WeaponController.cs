using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]
[RequireComponent (typeof (Light))]

public class WeaponController : MonoBehaviour {

	public float maximumDamage = 120f;
	public float minimumDamage = 45f;
	public AudioClip shotClip;
	public float flashIntensity = 3f;
	public float fadeSpeed = 10f;
	public float laserDuration = 0.25f;
	public float fireRate = 0.5f;
	public float effectiveRange = 50.0f;

	private LineRenderer laserShotLine;
	private Light laserShotLight;
	private bool firing;
	private float scaledDamage;
	private float lastFire;
	private float nextFire;

	void Awake(){
		this.laserShotLine = GetComponentInChildren<LineRenderer>();
		this.laserShotLight = laserShotLine.gameObject.GetComponent<Light>();
	}

	// Use this for initialization
	void Start(){
		laserShotLine.enabled = false;
		laserShotLight.intensity = 0f;
		scaledDamage = maximumDamage - minimumDamage;
	}
	
	// Update is called once per frame
	void Update(){
		// Hide the laser line if it has met the duration
		if(Time.time > lastFire + laserDuration){
			laserShotLine.enabled = false;
		}
		if(firing && Time.time >= nextFire){
			// Done firing this round, cleanup the laser line
			firing = false;
		}
		// Fade the light each update until it's off
		laserShotLight.intensity = Mathf.Lerp(laserShotLight.intensity, 0f, fadeSpeed * Time.deltaTime);
	}

	public void Fire(GameObject target){
		if(firing || Time.time < nextFire){ return ; } // No double firing and respect the fire rate.

		firing = true;
		lastFire = Time.time;
		nextFire = Time.time + fireRate;

		ShowFiringEffects(target);

		float effectiveness = Vector3.Distance(transform.position, target.transform.position) / effectiveRange;
		float damage = maximumDamage - scaledDamage * effectiveness;
		
		// The player takes damage.
		target.GetComponent<HitpointController>().Damage(damage);
	}

	private void ShowFiringEffects(GameObject target){
		// Draw laser line from gun to the target
		laserShotLine.SetPosition(0, laserShotLine.transform.position);
		laserShotLine.SetPosition(1, target.transform.position + Vector3.up * 1.5f);
		laserShotLine.enabled = true;
		
		// Flash the light
		laserShotLight.intensity = flashIntensity;
		
		// Play the gun shot clip at the position of the muzzle flare.
		//AudioSource.PlayClipAtPoint(shotClip, laserShotLight.transform.position);
	}
}
