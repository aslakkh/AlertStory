using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPackageController : MonoBehaviour {

	public void AddPersonToInformationPackage (string message) {
		Debug.Log("Added Person info to information package" + message);
	}

	public void AddPostToInformationMackage () {
		Debug.Log("Added post to information package");
	}
}
