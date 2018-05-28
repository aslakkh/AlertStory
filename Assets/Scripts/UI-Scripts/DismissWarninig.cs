using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismissWarninig : MonoBehaviour {

	public GameObject WarningPannel;
	private SoundManager soundManager;

	private void Awake() {
		soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

	public void HideWarningPanel() {
		soundManager.sfxSource.Play();
        GameObject.Destroy(WarningPannel);
	}
}
