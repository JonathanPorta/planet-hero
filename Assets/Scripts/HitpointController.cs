using UnityEngine;
using System.Collections;

public class HitpointController : MonoBehaviour {

	public delegate void DeathHandler();
	public DeathHandler OnDeath;

	public float startingHitpoints = 1000.0f;
	public float maximumHitpoints = 1000.0f;
	public float minimumHitpoints = 0.0f;

	private float hitpoints;

	void Awake(){
		
	}

	// Use this for initialization
	void Start(){
		hitpoints = startingHitpoints;
	}
	
	// Update is called once per frame
	void Update(){

	}

	public void Repair(float amount){
		Debug.Log(string.Format("Repair will try to add {0} hitpoints to existing {1}...", amount, hitpoints));
		SetHitpoints(hitpoints + amount);
	}

	public void Damage(float amount){
		Debug.Log(string.Format("Damage will try to remove {0} hitpoints from existing {1}...", amount, hitpoints));
		SetHitpoints(hitpoints - amount);
	}

	private void SetHitpoints(float value){
		if(value < minimumHitpoints){
			hitpoints = minimumHitpoints;
			if(OnDeath != null){
				OnDeath();
			}
		}
		else if(value > maximumHitpoints){
			hitpoints = maximumHitpoints;
		}
		else {
			hitpoints = value;
		}
		Debug.Log(string.Format("Attempted to set hitpoints to {0} hitpoints. Set hitpoints to {1}", value, hitpoints));
	}
}
