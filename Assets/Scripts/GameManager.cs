using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GameManager 
{
	public const float maxDamage = 100;



	static public int HighScore
	{
		get { return PlayerPrefs.GetInt("HighScore"); }
		set{ PlayerPrefs.SetInt("HighScore", value); }
	}


	static int _score;
	static public int Score
	{
		get { return _score; }
		set
		{ 
			if (_score != value)
			{
				_score = value;
				if (_score > HighScore)
					HighScore = _score;

			}
		}
	}




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
				if (_lives <= 0)
				{
				//TODO : Handle GameOver

				}

			}
		}
	}


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

				if (_damage >= maxDamage)
				{
					Lives--;
					_damage = 0;
				}
			}
		}
	}

}
