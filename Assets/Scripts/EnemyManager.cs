using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject[] enemyPrefabs;
	
	private void Awake(){
		// Register events
		EventManager.SpawnEnemy += SpawnEnemy;
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Jump")) {
			EventManager.TriggerSpawnEnemy();
		}
	}

	private void SpawnEnemy(){
		Vector3 center = new Vector3(100, 100, 0);
		Instantiate(enemyPrefabs [0], center, Quaternion.identity);
	}
}
