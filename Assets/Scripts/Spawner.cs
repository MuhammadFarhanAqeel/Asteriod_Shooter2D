using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


public class Spawner : MonoBehaviour {

	[Header("SPAWN")]
	public List<GameObject> refrences;
	private List<int> _refrencesIDs;
	public bool random;

	[Header("SPAWNING")]
	[Range(0.001f,100)]	public float minRate = 1.0f;
	[Range(0.001f,100)]	public float maxRate = 1.0f;

	public int number = 5;
	public	bool infinite;


	[Header("LOCATIONS")]
	public GameArea area;
	Transform player;
	public float minDistanceFromPlayer;



	[Header("VELOCITY")]
	[Range(-180,180)] public float angle;
	[Range(0,360)] public float spread = 30f;
	[Range(0,10)] public float minStrength = 1;
	[Range(0,10)] public float maxStrength = 10;

	int _remaining;

	[Header("ANIMATOR")]
	public string animatorSpawningParameterName = "Spawning";
	public float animatorDelayIN = 1;
	public float animatorDelayOUT = 1;
	Animator _animator;
	int _spawningHashID;


	int _index;

	public int Index
	{
		get
		{ 
			_index = _index < refrences.Count - 1 ? _index + 1 : 0;
			return _index; 
		}
	}


	private int RandomInt{
		get{ 
			return Random.Range(0,refrences.Count);
		}
	}

	int Next
	{
		get
		{ 
			return random ? RandomInt : Index;
		}
	}



	void Awake(){

		_refrencesIDs = new List<int>();
		foreach (GameObject g in refrences)
		{
			ObjectPool.InitPool(g);
			_refrencesIDs.Add(g.GetInstanceID());
		}

		_animator = GetComponent<Animator>();
		if (_animator)
			_spawningHashID = Animator.StringToHash(animatorSpawningParameterName);
	}


	public void Restart(){
	
		StopCoroutine(Start());

		StartCoroutine(Start());

	}



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

		if(_animator){
			_animator.SetBool(_spawningHashID, true);
			yield return new WaitForSeconds(animatorDelayIN);
			}

		while (infinite || _remaining > 0)
		{
			Vector3 _position = area ? area.GetRandomPosition() : transform.position;

			if(player && Vector3.Distance(_position,player.position) < minDistanceFromPlayer)
			{
//				Debug.Log(_position);
				Vector2 debugPos = _position;
				Debug.DrawLine(transform.position, debugPos);
				_position = (_position - player.position).normalized * minDistanceFromPlayer;
				Debug.DrawLine(debugPos, _position);
			}

		//	GameObject obj = (GameObject)Instantiate(refrence, _position, transform.rotation);
			GameObject obj = ObjectPool.GetInstance(_refrencesIDs[Next], _position, transform.rotation);

			Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
			if (rb2d)
			{
				float angleDelta = Random.Range(-spread * 0.5f, spread * 0.5f);
				float angle_ = angle + angleDelta;
				Vector2 direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle_), Mathf.Cos(Mathf.Deg2Rad * angle_));
				direction *= Random.Range(minStrength, maxStrength);
				rb2d.velocity = direction;
			}
			_remaining--;
			yield return new WaitForSeconds(1 / Random.Range(minRate,maxRate));
		}
		if(_animator){
			_animator.SetBool(_spawningHashID, false);
			yield return new WaitForSeconds(animatorDelayOUT);
		}

		gameObject.SetActive(false);

	}



}
