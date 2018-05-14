using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundContainer : MonoBehaviour {
	private SoundManager soundManager;

	// Use this for initialization
	void Start() {
		soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		var buttons = GetComponentsInChildren<Button>(true);
		AddListeners(buttons);
	}

	private void AddListeners(Button[] buttons) {
		foreach (var button in buttons) {
			button.onClick.AddListener(PlayClick);
		}
	}

	public void PlayClick() {
		soundManager.sfxSource.PlayOneShot(soundManager.sfxSource.clip);
	}
}	
