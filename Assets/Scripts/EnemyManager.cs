using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	public GameObject[] enemyPrefabs;
	public GameObject[] spawnPoints;
	public float spawnRate = 0.1f; // Number of enemies to spawn per second.

	private List<GameObject> enemies;
  private float lastSpawnTime;
	private GameObject currentTarget; // TODO: Remove after enemies can target on their own.
	
	private void Awake(){
		// Register events
		EventManager.SpawnEnemy += OnSpawnEnemy;
		// TODO: Don't listen on this. Remove after enemies can target on their own.
		EventManager.SelectPlanet += SetTarget;
	}
	
	private void Start(){
		this.enemies = new List<GameObject>();
	}

	// Update is called once per frame
	void Update(){
		int enemiesToSpawn = Mathf.FloorToInt((Time.time - lastSpawnTime) * spawnRate);
    for(int i = 0; i< enemiesToSpawn; i++){
      lastSpawnTime = Time.time;
      EventManager.TriggerSpawnEnemy();
    }
		if(Input.GetButtonUp("Fire2")){
			EventManager.TriggerSpawnEnemy();
		}
	}

	private void OnSpawnEnemy(){
		Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
		GameObject enemy = Instantiate(enemyPrefabs[0], spawnPosition, Quaternion.identity) as GameObject;
		// Inject ref to the manager instance.
		enemy.GetComponent<EnemyController>().enemyManager = this;
		enemy.transform.parent = transform;
		this.enemies.Add(enemy);

		// TODO: Temporary until enemies can find their own targets.
		enemy.GetComponent<EnemyController>().target = currentTarget;
	}
	
	public void DestroyEnemy(GameObject enemy){
		Debug.Log("EnemyManager.DestroyEnemy()");
		this.enemies.Remove(enemy);
		Destroy(enemy);
	}

	public void SetTarget(GameObject target){ // TODO: Remove after enemies can target on their own.
		this.currentTarget = target; // TODO: Remove after enemies can target on their own.
		foreach(GameObject e in this.enemies){ // TODO: Remove after enemies can target on their own.
			e.GetComponent<EnemyController>().target = this.currentTarget; // TODO: Remove after enemies can target on their own.
		} // TODO: Remove after enemies can target on their own.
	} // TODO: Remove after enemies can target on their own.
}
