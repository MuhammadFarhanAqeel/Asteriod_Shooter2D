using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[Header("SPAWN")]
	public GameObject refrence;


	[Header("Spawning")]
	[Range(0.001f,100)]	public float minRate = 1.0f;
	[Range(0.001f,100)]	public float maxRate = 1.0f;

	public int number = 5;
	public	bool infinite;
	int _remaining;


	[Header("Locations")]

	public GameArea area;
	Transform player;
	public float minDistanceFromPlayer;


	IEnumerator Start(){
		_remaining = number;

		if (minDistanceFromPlayer > 0)
		{
			GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
			if (playerGO)
				player = playerGO.transform;
			else
				Debug.LogWarning("No Player Found, Please assign player tag to player object! ");
		}

		while (infinite || _remaining > 0)
		{
			Vector3 _position = area ? area.GetRandomPosition() : transform.position;

			if(player && Vector3.Distance(_position,player.position) < minDistanceFromPlayer)
			{
				Debug.Log(_position);
				Vector2 debugPos = _position;
				Debug.DrawLine(transform.position, debugPos);
				_position = (_position - player.position).normalized * minDistanceFromPlayer;
				Debug.DrawLine(debugPos, _position);
			}
			Instantiate(refrence, _position, transform.rotation);
			_remaining--;
			yield return new WaitForSeconds(1 / Random.Range(minRate,maxRate));
		}
	}



}
