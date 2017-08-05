using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Fit area to camera.
/// </summary>
[AddComponentMenu("Farhan/Fit Area To Camera Size")]
[RequireComponent(typeof(GameArea))]

public class FitAreaToCamera : MonoBehaviour {

	GameArea Area
	{
		get{ return GetComponent<GameArea>(); }
	}


	void FitToCamera(Camera cam){
	//	Area.SetArea(new Vector2(cam.aspect * cam.orthographicSize*2,cam.orthographicSize * 2)); 
		Area.Size = new Vector2(cam.aspect * cam.orthographicSize*2,cam.orthographicSize * 2); 

		transform.position = (Vector2)cam.transform.position;
		transform.rotation = cam.transform.rotation;
	}

	void FitToMainCamera(){
		FitToCamera(Camera.main);
	}


	void OnValidate(){
		FitToMainCamera();
	}


	void Awake(){
		FitToMainCamera();
	}
}
