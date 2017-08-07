using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings{



	public delegate void VolumeChange(float Volume);
	static public event VolumeChange MusicVolumeChanged;


	static float _musicVolume;

	static public float MusicVolume
	{
		get{ return PlayerPrefs.GetFloat("MusicVolume",0.8f); }
		set
		{ 
			PlayerPrefs.SetFloat("MusicVolume", value);

			if (MusicVolumeChanged != null)
				MusicVolumeChanged(value);
		}
	}



	static public event VolumeChange SfxVolumeChanged;
	static public float sfxVolume
	{
		get{ return PlayerPrefs.GetFloat("SFxVolume",0.4f); }
		set
		{ 
			PlayerPrefs.SetFloat("SFxVolume", value);

			if (SfxVolumeChanged != null)
				SfxVolumeChanged(value);
		}
	}

}
