using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public enum EGameState{
	Running
}

public class GameView : MonoBehaviour
{
	
	public int VCInput_Axis;
	public int VCInput_Ver_Axis;
	private int _vcInputBtnA;
	public int VCInputBtnA{
		get{
			return _vcInputBtnA;
		}
		set{
			_vcInputBtnA = value;
		}
	}
	private int _vcInputBtnB;
	public int VCInputBtnB{
		get{
			return _vcInputBtnB;
		}
		set{
			_vcInputBtnA = value;
		}
	}
	
	public EGameState gameState;
	
	public Camera main_camera;
	
	private Hero hero;
	
	public GameObject cubeParent;
	
	bool initFinish = false;
	
	void Start ()
	{
		// init hero
		StartCoroutine(InitWorld());
	}
	
	// Update is called once per frame
	void Update ()
	{
		// keyboard controll
		/// for test.when build， close it
		#if UNITY_EDITOR||UNITY_STANDALONE_WIN
		if(Input.GetKey(KeyCode.A)){
			VCInput_Axis = -1;
		}else if(Input.GetKey(KeyCode.D)){
			VCInput_Axis = 1;
		}else{
			VCInput_Axis = 0;
		}
		
		if(Input.GetKey(KeyCode.W)){
			VCInput_Ver_Axis = 1;
		}else if(Input.GetKey(KeyCode.S)){
			VCInput_Ver_Axis = -1;
		}else{
			VCInput_Ver_Axis = 0;
		}
		
		if(Input.GetKeyDown(KeyCode.Space)){
			VCInputBtnA = 1;
		}else{
			VCInputBtnA = 0;
		}
		
		if(Input.GetKeyDown(KeyCode.X)){
			VCInputBtnB  = 1;
		}else{
			VCInputBtnB = 0;
		}
		#endif
		if(hero != null){
			hero.DoUpdate();
		}
	}
	
	void LateUpdate(){
//		VCInput_BtnA = 0;
//		VCInput_BtnB = 0;
//		VCInput_Ver_Axis = 0;
	}
	
	
	void OnGUI(){

	}
	//×××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××
	
	public void OnBtnPress(string btnname, bool isDown){
		if("btn_down".Equals(btnname)){
			if(isDown){
				VCInput_Ver_Axis = -1;
			}else{
				VCInput_Ver_Axis = 0;
			}
		}
		if("btn_up".Equals(btnname)){
			if(isDown){
				VCInput_Ver_Axis = 1;
			}else{
				VCInput_Ver_Axis = 0;
			}
		}
		if("btn_left".Equals(btnname)){
			if(isDown){
				VCInput_Axis = -1;
			}else{
				VCInput_Axis = 0;
			}
		}
		if("btn_right".Equals(btnname)){
			if(isDown){
				VCInput_Axis = 1;
			}else{
				VCInput_Axis = 0;
			}
		}
		
		if("btn_A".Equals(btnname)){
			if(isDown){
				VCInputBtnA = 1;
			}else{
				VCInputBtnA = 0;
			}
		}
		if("btn_B".Equals(btnname)){
			if(isDown){
				VCInputBtnB  = 1;
			}else{
				VCInputBtnB = 0;
			}
		}
	}
	
	public bool IsInGameState(EGameState gameState){
		return this.gameState == gameState;
	}
	

}