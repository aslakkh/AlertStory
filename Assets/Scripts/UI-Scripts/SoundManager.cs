using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource sfxSource;
	public AudioSource musicSource;

	public AudioClip[] backgroundPlaylist;
	public AudioClip standardSFXSound;
	
	public static SoundManager instance = null;

	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	public void RandomizeClick() {
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		sfxSource.pitch = randomPitch;
		sfxSource.clip = standardSFXSound;
		sfxSource.Play();
	}
	
	
	private void PlayRandomBakcgroundMusic(params AudioClip[] playlist) {
		int randIndex = Random.Range(0, playlist.Length);

		musicSource.clip = playlist[randIndex];
		musicSource.Play();
	}

	private void FixedUpdate() {
		if (!musicSource.isPlaying) {
			PlayRandomBakcgroundMusic(backgroundPlaylist);
		}
	}
}
