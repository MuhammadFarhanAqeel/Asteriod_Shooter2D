using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTransforms : MonoBehaviour {


	public Transform refrence;
	public float speed = 1f;
	public float angularSpeed = 45f;


	void Update () {
		transform.rotation = Quaternion.RotateTowards(transform.rotation, refrence.transform.rotation, angularSpeed * Time.deltaTime);
		transform.position =	Vector3.MoveTowards(transform.position, refrence.transform.position, speed * Time.deltaTime);	
	}
}
