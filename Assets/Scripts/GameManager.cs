﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
	Hazir,
	Oyunda,

	OyunSonu
}

public class GameManager : Photon.PunBehaviour {
	
	public static GameManager instance;
	public bool spawnZombies = true;
	[HideInInspector] public GameState gameState = GameState.Hazir;
	public ViewBase startView; 
	public GameObject playerPrefab;
	public Transform playerSpawnPoint;

	public Transform[] spawnPoints;
	 public EnemySpawner enemySpawner;

	//Zaman araliklari
	 public float spawnDuration = 5f;
	 public int maxZombies = 20;
	 public int zombiesSpawned = 0;

	 public float upgradeDuration = 20f;
	 public int baseZombieHP = 100;
	 public float baseZombieSpeed = 1.0f;

	 public int baseKillReward = 10;

	 public float maxZombieSpeed = 5.0f;

	[SerializeField]private int zombieHP;
	[SerializeField]private float zombieSpeed;
	[SerializeField]private int killReward;

	private IEnumerator CoSpawnEnemies_;
	private IEnumerator CoEnhanceZombieStatus_;

	[HideInInspector]public Player [] players;

	void Awake(){
		instance = this;
	}


	 public void StartGame(){
		zombieHP = baseZombieHP;
		zombieSpeed = baseZombieSpeed;
		killReward = baseKillReward;

		//Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
		PhotonNetwork.Instantiate("Player_", playerSpawnPoint.position, playerSpawnPoint.rotation, 0);
		gameState = GameState.Oyunda;

		if(spawnZombies == false){
			return;
		}
		if(!PhotonNetwork.isMasterClient){
			return; 
		}

		CoSpawnEnemies_ = CoSpawnEnemies();
		CoEnhanceZombieStatus_ = CoEnhanceZombieStatus();

		 StartCoroutine(CoSpawnEnemies_);
		 StartCoroutine(CoEnhanceZombieStatus_);



	 }
	 public void GameOver(){
		 StopCoroutine(CoSpawnEnemies_);
		 StopCoroutine(CoEnhanceZombieStatus_);
		 gameState = GameState.OyunSonu;
	 }

	 public void ResetGame(){
		 //Clean up game
		ZombieFollow[] zombies = GameObject.FindObjectsOfType<ZombieFollow>();
		for(int i = 0; i <zombies.Length; i++ ){
			Destroy(zombies[i].gameObject);
		}

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
 
		 gameState = GameState.Hazir;
		 startView.Show();
	 }

	public void RefreshCurrentPlayers(){
		players = GameObject.FindObjectsOfType<Player>(); 

	 }
	 IEnumerator CoSpawnEnemies(){
		 yield return new WaitForSeconds(5);

		RefreshCurrentPlayers();
		while(true){
			for(int i = 0; i< spawnPoints.Length; i++){
				if(zombiesSpawned >= maxZombies ) continue;

				GameObject enemyObj = enemySpawner.SpawnAt(spawnPoints[i].position, spawnPoints[i].rotation);
				ZombieFollow enemyZombie = enemyObj.GetComponent<ZombieFollow>();
				Health enemyHealth  = enemyObj.GetComponent<Health>();
				KillZombiOdul enemyKillReward = enemyObj.GetComponent<KillZombiOdul>();

				enemyZombie.speed = zombieSpeed;
				enemyHealth.val = zombieHP;
				enemyKillReward.odul_miktar = killReward;

				enemyZombie.onDead.AddListener(() => {
			//		Debug.Log("Zombie DEAD!");
					zombiesSpawned--;
				});
				//enemyZombie.players = players;
				zombiesSpawned++;
			}
			yield return new WaitForSeconds(spawnDuration);
		}
	 }

	 IEnumerator CoEnhanceZombieStatus(){

		 yield return new WaitForSeconds(5);

		 	while(true){
				 yield return new WaitForSeconds(upgradeDuration);

				 Debug.Log("Upgrading Enemy!");

				 zombieHP += 20;
				 zombieSpeed += 0.25f;
				 killReward += 20; 

				 if(zombieSpeed > maxZombieSpeed){
					 zombieSpeed = maxZombieSpeed;
				 }
			 }
	 }

}
