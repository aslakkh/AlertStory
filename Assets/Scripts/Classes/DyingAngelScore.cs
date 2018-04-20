using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Text;

public class DyingAngelScore : MonoBehaviour {

	private Animation animation;

	void Awake () {
		gameObject.SetActive(false);

		animation = gameObject.GetComponent<Animation>();
	}

	public void fireDyingAngelAnimation (int score) {
		gameObject.GetComponent<UnityEngine.UI.Text>().text = score.toString();
		gameObject.SetActive(true);
		animation.Play();
	}

}
