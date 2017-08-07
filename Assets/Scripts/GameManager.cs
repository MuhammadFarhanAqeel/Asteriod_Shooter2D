using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
					break;
				case STATE.Paused:
					Time.timeScale = 0;
					break;
				case STATE.Over:
					Time.timeScale = 0;
					break;
				default:
					break;
			}
			if (StateChanged != null)
				StateChanged(_state);
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
				_lives = value;

				if (LivesChanged != null)
					LivesChanged(_lives);

				if (_lives <= 0)
				{
				//TODO : Handle GameOver
					State = STATE.Over;
				}
			}
		}
	}



	public static event DamageChange DamageChanged;
	static float _damage;		
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
		Lives = 5;
		Damage = 0;
		Score = 0;
		DamageChanged = null;
		LivesChanged = null;
		ScoreChanged = null;
		HighScoreChanged = null;
		StateChanged = null;
		SceneManager.LoadScene(0);	
		State = STATE.Running;
	}

}
