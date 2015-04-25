using UnityEngine;
using System.Collections;

[RequireComponent (typeof (HitpointController))]
[RequireComponent (typeof (WeaponController))]
[RequireComponent (typeof (NavMeshAgent))]

public class EnemyController : MonoBehaviour {

	public EnemyManager enemyManager;
	public HitpointController hitpointController;
	public float speed = 1.00f;
	public GameObject target;

	private WeaponController[] weapons;
	private NavMeshAgent nav;
	private bool targetWithinRange;
	
	private void Awake(){
		this.hitpointController = transform.GetComponent<HitpointController>();
		this.weapons = transform.GetComponentsInChildren<WeaponController>();
		this.nav = transform.GetComponent<NavMeshAgent>();
		// Set handlers for the hitpoint related events that we care about.
		hitpointController.OnDeath += HandleDeath;
		// Register events
		EventManager.SelectPlanet += SetTarget;
	}
	
	// Use this for initialization
	void Start(){
		this.targetWithinRange = false;
	}
	
	// Update is called once per frame
	void Update(){
		// No target? Bail.
		if(target == null){return ;}

		//Targeting()
		if(targetWithinRange){
			Attacking();
		}
		else {
			Advancing();
		}
	}

	void OnTriggerEnter(Collider other){
		//Debug.DrawLine(transform.position, other.gameObject.transform.position);
		targetWithinRange = true;
	}

	void OnTriggerStay(Collider other){
		//Debug.DrawLine(transform.position, other.gameObject.transform.position);
	}

	void OnTriggerExit(Collider other){
		targetWithinRange = false;
	}

	public void SetTarget(GameObject target){
		this.target = target;
	}

	private void Attacking(){
		nav.Stop();
		transform.LookAt(target.transform.position);
		foreach(WeaponController weapon in weapons){
			weapon.Fire();
		}
	}

	private void Advancing(){
		transform.LookAt(target.transform.position);
		nav.SetDestination(target.transform.position);
	}
	
	private void HandleDeath(){
		Debug.Log("EnemyController.HandleDeath()");
		enemyManager.DestroyEnemy(this.gameObject);
	}
}
