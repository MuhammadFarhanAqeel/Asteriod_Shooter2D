using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[Header("SPAWN")]
	public GameObject refrence;


	[Header("Spawning")]
	[Range(0.001f,100)]	public float minRate = 1.0f;
	[Range(0.001f,100)]	public float maxRate = 1.0f;

	//	float _timeStamp;	
	public int number = 5;
	public	bool infinite;
	int _remaining;


	[Header("Locations")]

	public GameArea area;


	void Start(){
		//_timeStamp = Time.time;
		_remaining = number;
		StartCoroutine(Spawn());
	}




	IEnumerator Spawn(){
	
		while (infinite || _remaining > 0)
		{


			Vector3 _position = area ? area.GetRandomPosition() : transform.position;

			Instantiate(refrence, _position, transform.rotation);
			_remaining--;
			yield return new WaitForSeconds(1 / Random.Range(minRate,maxRate));
		}
	}


	/*
	void Update(){
		if (Time.time <= _timeStamp + rate)
			return;
		_timeStamp = Time.time;


		if (_remaining > 0)
		{
			Instantiate(refrence, transform.position, transform.rotation);
			_remaining --;
			if ((_remaining <= 0))
				enabled = false;
		}
	}
	*/
}
