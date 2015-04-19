using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed = 1.00f;
	public GameObject target;

	private NavMeshAgent nav;
	private bool targetWithinRange;
	
	private void Awake(){
		// Register events
		EventManager.SelectPlanet += SetTarget;
	}
	
	// Use this for initialization
	void Start(){
		nav = transform.GetComponent<NavMeshAgent>();
		targetWithinRange = false;
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
		Debug.DrawLine(transform.position, other.gameObject.transform.position);
		targetWithinRange = true;
	}

	void OnTriggerStay(Collider other){
		Debug.DrawLine(transform.position, other.gameObject.transform.position);
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
	}

	private void Advancing(){
		transform.LookAt(target.transform.position);
		nav.SetDestination(target.transform.position);
	}
}
