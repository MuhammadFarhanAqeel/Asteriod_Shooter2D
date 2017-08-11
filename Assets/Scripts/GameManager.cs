using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

static public class GameManager 
{

	public enum STATE{
		Running,
		Paused,
		Over
	}


	public delegate void ScoreChange(int score);
	public delegate void LivesChange(int lives);
	public delegate void DamageChange(float damage);

	public delegate void StateChange(STATE state);



	public static event StateChange StateChanged;
	static STATE _state;

	static public STATE State{
		get{ return _state;}
		set{ 
			if (value != _state)
				_state = value;

			switch (_state)
			{
				case STATE.Running:
					Time.timeScale = 1;
					Controls = true;
					break;
				case STATE.Paused:
					Time.timeScale = 0;
					Controls = false;
					break;
				case STATE.Over:
					Time.timeScale = 0;
					Controls = false;
					break;
				default:
					break;
			}
			if (StateChanged != null)
				StateChanged(_state);
		}
	}




	static ControlsBase[] _controls = new ControlsBase[0];
	public static bool Controls
	{
		set
		{
			if (_controls.Length == 0)
			{
				_controls = (from item in GameObject.FindObjectsOfType<ControlsBase>()
				             where item.enabled = true
				             select item).ToArray();
			}	
			foreach (ControlsBase control in _controls)
			{
				control.enabled = value;
			}
		}
	}


	public const float maxDamage = 100;
	public static event ScoreChange HighScoreChanged;

	static public int HighScore
	{
		get { return PlayerPrefs.GetInt("HighScore"); }
		set
		{
			PlayerPrefs.SetInt("HighScore", value); 
			if (HighScoreChanged != null)
				HighScoreChanged(value);
		}
	}

	static public event ScoreChange ScoreChanged;
	static int _score;
	static public int Score
	{
		get { return _score; }
		set
		{ 
			if (_score != value)
			{
				_score = value;
				if (ScoreChanged != null)
					ScoreChanged(_score);
			
				if (_score > HighScore)
					HighScore = _score;

			}
		}
	}

	public static event LivesChange LivesChanged;
	static int _lives = 5;
	static public int Lives
	{
		get
			{ return _lives; }
		set
		{ 
			if (_lives != value)
			{

				if (value < _lives)
					Component.FindObjectOfType<ShipDamage>().Die();


				_lives = value;

				if (LivesChanged != null)
					LivesChanged(_lives);

				if (_lives <= 0)
				{
					State = STATE.Over;
				}
			}
		}
	}



	public static event DamageChange DamageChanged;
	static float _damage = 90f;		
	static	public float Damage
	{
		get
		{
			return _damage;
		}set
		{ 
			if (_damage != value)
			{
				_damage = value;
				if (DamageChanged != null)
					DamageChanged(_damage);

				if (_damage >= maxDamage)
				{
					Lives--;
					_damage = 0;
				}
			}
		}
	}


	public static void RestartGame(){

		ObjectPool[] pools = GameObject.FindObjectsOfType<ObjectPool>();
		foreach (ObjectPool pool in pools)
			pool.Clear();

		Spawner[] spawmers = GameObject.FindObjectsOfType<Spawner>();
		foreach (Spawner spawner in spawmers)
			spawner.Restart();


		GameObject.FindObjectOfType<ShipControllerBase>().ResetShip();


		SaveTransforms[] saveTransforms = GameObject.FindObjectsOfType<SaveTransforms>();
		foreach (SaveTransforms s in saveTransforms)
			s.Reload();

		Lives = 5;
		Damage = 0;
		Score = 0;



		State = STATE.Running;
	}

}
