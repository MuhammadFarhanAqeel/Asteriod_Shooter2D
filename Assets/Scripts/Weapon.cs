using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {




	new public string name;
	public GameObject projectile;
	int _projectileID;

	public Transform[] emitters;
	int _current;
	public bool cycling = true; // Used to do a burst fire from all emitters!

	public float firingRange = 5;
	Collider2D _shipCollider2D;
	[Range(0.01f,100f)] public float firingRate = 1f;

	public bool automatic = true; // if false we have to press the button again and again to fire! 


	bool _firing;
	public bool Firing
	{
		get
		{ return _firing; }
		set
		{ 
			if (_firing != value)
			{
				_firing = value;

				if (_firing)
				{
					if (automatic)
						InvokeRepeating("Fire", 1 / firingRate, 1 / firingRate);
					else
						Fire();
				}
				else
				{
					CancelInvoke();
				}
			}
		}
	}


	void Awake(){
		if (name == string.Empty)
			name = gameObject.name;
		ObjectPool.InitPool(projectile);
		_projectileID = projectile.GetInstanceID();

		_shipCollider2D = transform.parent.GetComponent<Collider2D>();

	}

	void Fire(){
		if (cycling)
		{
			FireOnce();
		}
		else
		{
			for (int i = 0; i < emitters.Length; i++)
				FireOnce();

			}
	}
	void FireOnce(){

		_current = (_current >= emitters.Length - 1) ? 0 : _current + 1; 

		Vector3 position = emitters[_current].TransformPoint(Vector3.up * 0.5f);
	//	GameObject projetileInstance = (GameObject)Instantiate(projectile, emitters[_current].position, emitters[_current].rotation);
		GameObject projetileInstance = ObjectPool.GetInstance(_projectileID, emitters[_current].position, emitters[_current].rotation);

		projetileInstance.GetComponent<Projectile>().range = firingRange;
		Physics2D.IgnoreCollision(_shipCollider2D, projetileInstance.GetComponent<Collider2D>());
	}

}
