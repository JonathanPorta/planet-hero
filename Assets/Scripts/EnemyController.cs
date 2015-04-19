using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed = 0.01f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Advancing();
	}

	private void Advancing() {
		Vector3 target = new Vector3(0, 0, 0);
		transform.LookAt(target);
		transform.position = Vector3.Lerp(transform.position, target, speed);
	}
}
