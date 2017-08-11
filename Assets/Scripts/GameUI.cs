using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour {

	[Header("HUD")]
	public Text livesText;
	public Text scoreText;
	public Text highScoreText;
	public Slider damageSlider;
	 Image _damageFillArea;
	Color damageSliderColorMin = Color.yellow;
	Color damageSliderColorMax = Color.red;
	public Text gameStateText;

	[Header("NAV")]

	public Button pauseButton;
	public Button resumeButton;
	public Image pauseMenu;

	[Header ("SETTINGS")]
	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;

	public float MusicVolume
	{
		get{ return PlayerSettings.MusicVolume; }
		set	{ 	PlayerSettings.MusicVolume = value; }
	}

	public float SfxVolume
	{
		get{ return PlayerSettings.sfxVolume; }
		set	{ 	PlayerSettings.sfxVolume = value; }
	}


	void Awake(){
		_damageFillArea = damageSlider.fillRect.GetComponent<Image>();
		damageSlider.maxValue = GameManager.maxDamage;
	}

	void Start(){

		damageSlider.value = GameManager.Damage;
		UpdateUI(GameManager.State);

		musicVolumeSlider.value = PlayerSettings.MusicVolume;
		sfxVolumeSlider.value = PlayerSettings.sfxVolume;

		_damageFillArea.color = Color.Lerp(damageSliderColorMin,damageSliderColorMax,GameManager.Damage/damageSlider.maxValue);

		livesText.text = string.Format("{0} {1}", GameManager.Lives.ToString(), GameManager.Lives > 1 ? "lives" : "life");
		scoreText.text = string.Format("Score : {0}", GameManager.Score.ToString());
		highScoreText.text = string.Format("High Score : {0}", GameManager.HighScore.ToString());

		GameManager.ScoreChanged += delegate(int score)
			{
				scoreText.text = string.Format("Score : {0}", score.ToString());
			};
	
		GameManager.HighScoreChanged += delegate(int score)
			{
				highScoreText.text = string.Format("High Score : {0}", score.ToString());
			};

		GameManager.LivesChanged += delegate(int lives)
			{
					livesText.text = string.Format("{0} {1}", lives.ToString(), lives > 1 ? "lives" : "life");

			};

		GameManager.DamageChanged += delegate(float damage)
			{
					damageSlider.value = damage;
					_damageFillArea.color = Color.Lerp(damageSliderColorMin,damageSliderColorMax,damage/damageSlider.maxValue);
			};

		GameManager.StateChanged += UpdateUI;

	}


//	void OnDestroy(){
//		GameManager.StateChanged -= UpdateUI;
//	}


	public void UpdateUI(GameManager.STATE state){
		pauseButton.gameObject.SetActive(state==GameManager.STATE.Running);
		pauseMenu.gameObject.SetActive(state != GameManager.STATE.Running);
		resumeButton.gameObject.SetActive(state != GameManager.STATE.Over);
		gameStateText.text = string.Format("Game {0}", state.ToString().ToUpper());
	}

	public void PauseGame(){
		GameManager.State = GameManager.STATE.Paused;
	}
	public void ResumeGame(){
		GameManager.State = GameManager.STATE.Running;
	}
	public void RestartGame(){
		
		GameManager.RestartGame();
	}


}
