using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTransforms : MonoBehaviour {


	private Vector3 _position;
	private Quaternion _rotation;

	public void Save(){
		_position = transform.position;
		_rotation = transform.rotation;
	}


	public void Reload(){
	
		transform.rotation = _rotation;
		transform.position = _position;

	}

	void Start()
	{
		Save();
	}


}
