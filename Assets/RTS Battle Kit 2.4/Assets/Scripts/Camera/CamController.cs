﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CamController : MonoBehaviour {
	
	//variables not visible in the inspector
	public static float movespeed;
	public static float zoomSpeed;
	public static float mouseSensitivity;
    public static float clampAngle;
    public FModDemo fmodDemo;
	
    float rotationY = 0;
    float rotationX = 0;
	
	bool canRotate;
 
    void Start(){
		//get start rotation
		Vector3 rot = transform.eulerAngles;
		rotationY = rot.y;
		rotationX = rot.x;
    }
	
	void Update(){
		//if the mobile prefab is added to the scene, use mobile controls. Else use pc controls
		if(GameObject.Find("Mobile") == null && GameObject.Find("Mobile multiplayer") == null){
			PcCamera();
		}
		#if PHOTON_MULTIPLAYER
		else if((GameObject.Find("Mobile") && Mobile.camEnabled) || (GameObject.Find("Mobile multiplayer") && MobileMultiplayer.camEnabled)){
			MobileCamera();
		}
		#else
		else if(GameObject.Find("Mobile") && Mobile.camEnabled){
			MobileCamera();
		}
		#endif
	}
	
	void PcCamera(){
		//if key gets pressed move left/right
		if(Input.GetKey("a")){
			transform.Translate(Vector3.right * Time.deltaTime * -movespeed);
		}
		if(Input.GetKey("d")){
			transform.Translate(Vector3.right * Time.deltaTime * movespeed);
		}
	
		//if key gets pressed move up/down
		if(Input.GetKey("w")){
			transform.Translate(Vector3.up * Time.deltaTime * movespeed);
		}
		if(Input.GetKey("s")){
			transform.Translate(Vector3.up * Time.deltaTime * -movespeed);
		}
	
		//if scrollwheel is down rotate camera
		if(Input.GetMouseButton(2)){
			float mouseX = Input.GetAxis("Mouse X");
			float mouseY = -Input.GetAxis("Mouse Y");
			RotateCamera(mouseX, mouseY);
		}

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");
            transform.parent.Rotate(new Vector3(0.0f, mouseY * mouseSensitivity * Time.deltaTime, 0.0f));
            //rotationX += mouseY * mouseSensitivity * Time.deltaTime;
        }
	
		//move camera when you scroll
		transform.Translate(new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel")) * Time.deltaTime * zoomSpeed);
        float distance = transform.transform.position.magnitude;
        float percent = Mathf.Clamp(distance - 150.0f, 0, 50) / 50.0f;
        fmodDemo.setZoomOut(percent);
	}
	
	
	void MobileCamera(){
		//check if exactly one finger is touching the screen
		if(Input.touchCount == 1){
			//rotate camera based on the touch position
			Touch touch = Input.GetTouch(0);
			
			if(touch.phase == TouchPhase.Began){
				if(EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)){
					canRotate = false;
				}
				else{
					canRotate = true;
				}
			}
			
			if(!canRotate)
				return;
				
			float mouseX = touch.deltaPosition.x;
			float mouseY = -touch.deltaPosition.y;
			
			RotateCamera(mouseX, mouseY);
		}
		//check for two touches
		else if(Input.touchCount == 2){
            //store two touches
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            //find the position in the previous frame of each touch
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            //find the magnitude of the vector (the distance) between the touches in each frame
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            //find the difference in the distances between each frame
            float z = (prevTouchDeltaMag - touchDeltaMag) * 0.001f * zoomSpeed;
			
			//zoom camera by moving it forward/backward
			transform.Translate(new Vector3(0, 0, -z));
		}
	}
	
	
	void RotateCamera(float mouseX, float mouseY){
		//check if mobile controls are enabled to adjust sensitivity
		if(GameObject.Find("Mobile") == null && GameObject.Find("Mobile multiplayer") == null){
			rotationY += mouseX * mouseSensitivity * Time.deltaTime;
			rotationX += mouseY * mouseSensitivity * Time.deltaTime;
		}
		else{
			rotationY += mouseX * mouseSensitivity * Time.deltaTime * 0.02f;
			rotationX += mouseY * mouseSensitivity * Time.deltaTime * 0.02f;	
		}
	
		//clamp x rotation to limit it
		rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);
	
		//apply rotation
		transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
	}
}
