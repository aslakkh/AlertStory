using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismissWarninig : MonoBehaviour {

	public GameObject WarningPannel;

	public void HideWarningPanel() {
		WarningPannel.SetActive(false);
	}
}
