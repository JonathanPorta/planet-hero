﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	public GameObject[] enemyPrefabs;
	public GameObject[] spawnPoints;

	private List<GameObject> enemies;

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
		if(Input.GetButtonUp("Jump")){
			EventManager.TriggerSpawnEnemy();
		}
	}

	private void OnSpawnEnemy(){
		Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
		GameObject enemy = Instantiate(enemyPrefabs[0], spawnPosition, Quaternion.identity) as GameObject;
		// Inject ref to the manager instance.
		enemy.GetComponent<EnemyController>().enemyManager = this;
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
