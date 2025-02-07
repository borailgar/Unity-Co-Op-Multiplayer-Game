﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapons{

		None,
		UMP45,
		StovRifle,
		DefenderShotgun,
	}


public class WeaponDegis : MonoBehaviour {

	public static WeaponDegis instance;
	//public Weapons currentOne = Weapons.UMP45;
	//private int currentWeaponIndex = 0;

	/*
	private Weapons[] WeaponArray = {
									 Weapons.UMP45,
									 Weapons.StovRifle,
									 Weapons.DefenderShotgun}; 
	
	 */

	 private Weapons primaryWeapon = Weapons.None;
	 private Weapons currentWeapon;
	 private Weapons secondaryWeapon = Weapons.DefenderShotgun;
	 private Weapons thirdWeapon = Weapons.UMP45;


	 private GameObject primaryWeaponObj;
	 private GameObject secondaryWeaponObj;
	 private GameObject currentWeaponObj;
	 private GameObject thirdWeaponObj;

	 
	void Awake(){
		if(instance == null){
			instance = this;
		}
		else{
			Destroy(gameObject);
		}
	}
	void Start(){
		currentWeapon = secondaryWeapon;
	//	primaryWeaponObj = FindWeaponObject(primaryWeapon);
		secondaryWeaponObj = FindWeaponObject(secondaryWeapon);
		//thirdWeaponObj = FindWeaponObject(thirdWeapon);
		
		currentWeaponObj = secondaryWeaponObj;
		//Switch();
		SelectCurrentWeapon();
	}

	public void SetPrimaryWeapon(Weapons weapon){

		currentWeaponObj.SetActive(false);

		primaryWeapon = weapon;
		primaryWeaponObj = FindWeaponObject(weapon);

		currentWeapon = primaryWeapon;
		currentWeaponObj = primaryWeaponObj;
	
		SelectCurrentWeapon();

	}

	public void SetSecondaryWeapon(Weapons weapon){
		currentWeaponObj.SetActive(false);
		

		secondaryWeapon = weapon;
		secondaryWeaponObj = FindWeaponObject(weapon);

		currentWeapon = secondaryWeapon;
		currentWeaponObj = secondaryWeaponObj;

		SelectCurrentWeapon();
	}

	public void SetThirdWeapon(Weapons weapon){
		currentWeaponObj.SetActive(false);
		

		thirdWeapon = weapon;
		thirdWeaponObj = FindWeaponObject(weapon);

		currentWeapon = thirdWeapon;
		currentWeaponObj = thirdWeaponObj;

		SelectCurrentWeapon();
	}


	public void ReplaceCurrentWeapon(Weapons weapon){
		if(currentWeapon == primaryWeapon){
			SetPrimaryWeapon(weapon);
		}else{
			SetSecondaryWeapon(weapon);
		}
		
	}
	public bool hasPrimaryWeapon(){
		return primaryWeapon != Weapons.None;
	}

	public bool hasWeapon(Weapons weapon){
		return primaryWeapon == weapon || secondaryWeapon == weapon;
	}

	


	GameObject FindWeaponObject(Weapons weapon){
		return transform.Find(weapon.ToString()).gameObject;
	}

	public GameObject GetCurrentWeaponObject(){
		return currentWeaponObj;
	}
	void SelectCurrentWeapon(){
		currentWeaponObj.SetActive(true);
		currentWeaponObj.GetComponent<WeaponBase>().Select();

		Player.instance.SetWeapon(currentWeapon);

	}

	public Weapons GetCurrentWeapon(){
		return currentWeapon;
	}
	void Update(){
		//CheckWeaponSwitch();
		if( primaryWeapon != Weapons.None && currentWeapon != primaryWeapon && Input.GetKeyDown(KeyCode.Alpha1)){
				currentWeapon = primaryWeapon;
				currentWeaponObj = primaryWeaponObj;
				//primaryWeaponObj.SetActive(true);
				secondaryWeaponObj.SetActive(false);

				SelectCurrentWeapon();
		}
		else if(secondaryWeapon != Weapons.None && currentWeapon != secondaryWeapon && Input.GetKeyDown(KeyCode.Alpha2)){
				currentWeapon = secondaryWeapon;
				currentWeaponObj = secondaryWeaponObj;
				primaryWeaponObj.SetActive(false);

				SelectCurrentWeapon();

		}
	
	}

	











































/*

void Switch(){
		for(int i = 0; i<transform.childCount; i++){
		transform.GetChild(i).gameObject.SetActive(false);	
		}

		GameObject currWeapon =  transform.Find(WeaponArray[currentWeaponIndex].ToString()).gameObject;
		currWeapon.SetActive(true);

		currWeapon.GetComponent<WeaponBase>().Select();
	}


	void CheckWeaponSwitch(){
		float mouse = Input.GetAxis("Mouse ScrollWheel");

		if(mouse > 0){
			SelectPrev();
		}
		else if(mouse < 0){
			SelectNext();
		}

	}

	void SelectPrev(){
		if(currentWeaponIndex == 0){
			currentWeaponIndex = WeaponArray.Length - 1;
		}
		else{
			currentWeaponIndex--;
		}
		Switch();
	}
		void SelectNext(){
		if(currentWeaponIndex >= (WeaponArray.Length - 1)){
			currentWeaponIndex = 0;
		}
		else{
			currentWeaponIndex++;
		}
			Switch();
	}

 */
}