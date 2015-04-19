using UnityEngine;
using System.Collections;

public static class EventManager {

	// Events that make the game world go 'round
	public delegate void GameEvent();	
	public static event GameEvent GameStart, GameOver, SpawnEnemy;

	// In game events due to user interaction
	public delegate void InterfaceEvent(GameObject target);
	public static event InterfaceEvent SelectPlanet;

	public static void TriggerGameStart(){
		if(GameStart != null){
			GameStart();
		}
	}
	
	public static void TriggerGameOver(){
		if(GameOver != null){
			GameOver();
		}
	}

	public static void TriggerSelectPlanet(GameObject selection){
		if(SelectPlanet != null){
			SelectPlanet(selection);
		}
	}

	public static void TriggerSpawnEnemy(){
		if(SpawnEnemy != null){
			SpawnEnemy();
		}
	}
}