using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed = 0.5f;
	AudioSource _audio;
	float _distance ;


	[System.NonSerialized]
	[HideInInspector]
	public float range = 3;



	void Awake(){
		_audio = GetComponent<AudioSource>();
	}

	void Start(){
		_audio.Play();
	}

	void FixedUpdate(){
		transform.Translate(0, speed * Time.fixedDeltaTime, 0,Space.Self);
		_distance += speed * Time.fixedDeltaTime;
		if (_distance > range)
		{
			Destroy(gameObject);  // TODO : Use Object Pooling
		}
	}
}
