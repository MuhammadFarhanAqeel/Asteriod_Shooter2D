using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Animator))]
public abstract class ShipControllerBase : MonoBehaviour,IControllable {



	public AnimationCurve steeringCurve;
	public AnimationCurve glideCurve;

	float rearPowerLimit = 0.2f;
	protected Animator _animator;
	protected int _steeringHashID;
	protected int _thrustXHashID;
	protected int _thrustYHashID;
	protected int _shieldHashID;

	List<Weapon> _weapons;


	Weapon _weapon;

	public Weapon Weapon
	{
		get
		{ 
			if (!_weapon)
				_weapon = GetComponentInChildren<Weapon>();
			
			return _weapon; 
		
		}
		set
		{ 
			if (_weapon != value)
			{
				_weapon = value;
				if (!_weapons.Contains(_weapon))
				{
					_weapons.Add(_weapon);
				}
			}
		}
	}




	//bool _firing;
	protected bool Firing
	{
		get
		{ return Weapon.Firing; }
		set
		{ 
			if (value != _weapon.Firing)
			{
				Weapon.Firing = Shield ? false : value;
			}
		}
	}

	Vector2 _thrust;

	protected Vector2 Thrust
	{
		get{ return _thrust; }
		set
		{ 
			if (_thrust != value)
			{
				_thrust = value;
				_animator.SetFloat(_thrustXHashID,_thrust.x);
				_animator.SetFloat(_thrustYHashID,_thrust.y);
			}
		}
	}

	float _steering;

	protected float Steering
	{
		get{ return _steering; }
		set
		{ 
			if (_steering != value)
			{
				_steering = value;
				_animator.SetFloat(_steeringHashID,_steering);
			}
		}
	}

	Vector2 _thrust_Tmp;


	public bool Attacking
	{
		get{ return Firing; }
		set{ Firing = value; }
	}
	bool _shield;

	public bool Shield
	{
		get{ return _shield; }
		set
		{ 
			if (_shield != value)
			{
				_shield = value;
				_animator.SetLayerWeight(_shieldHashID, _shield ? 1 : 0f);
			}
			if (_shield)
				Firing = false;
			
		}
	}

	public bool Protecting
	{
		get{ return Shield; }
		set{ Shield = value; }
	}


	protected virtual void Awake(){
		_animator = GetComponent<Animator>();
		_steeringHashID = Animator.StringToHash("Steering");
		_thrustXHashID = Animator.StringToHash("ThrustX");
		_thrustYHashID = Animator.StringToHash("ThrustY");
		_shieldHashID = _animator.GetLayerIndex("Shield");

		_weapons = new List<Weapon>();
		_weapons.Add(Weapon);
	}


	public virtual void Move(Vector2 movement)
	{
	
		movement = Vector2.ClampMagnitude(movement,1.0f);
		movement.y = Mathf.Clamp(movement.y,-rearPowerLimit,1);

		Steering = steeringCurve.Evaluate(movement.y) * movement.x;


		_thrust_Tmp.x = glideCurve.Evaluate(movement.y) * movement.x;
		_thrust_Tmp.y = movement.y;

		Thrust = _thrust_Tmp;

		enabled = movement != Vector2.zero;
	}


	public virtual void ResetShip(){

		Steering = 0;
		Thrust = Vector2.zero;
		Firing = Shield = false;

		transform.rotation = Quaternion.identity;
		transform.position = Vector3.zero;

		gameObject.SendMessage("Repair");
		Weapon = _weapons[0];  

	}


	public void SwitchWeapon(Weapon newWeapon){

		if (Weapon.name == newWeapon.name)
			return;

		var existingWeapon = (from item in _weapons
		                      where item.name == newWeapon.name
		                      select item).FirstOrDefault();

		bool wasFiring = Firing;
		Firing = false;
		Weapon.gameObject.SetActive(false);

		if (existingWeapon != null)
		{
			existingWeapon.gameObject.SetActive(true);
			Weapon = existingWeapon;
		}
		else
		{
			GameObject newWeaponGO = Instantiate(newWeapon.gameObject,transform);
			newWeaponGO.transform.localPosition = Vector3.zero;
			newWeaponGO.transform.localRotation = Quaternion.identity;
			Weapon = newWeaponGO.GetComponent<Weapon>();

		}

		Firing = wasFiring;
	}

}
