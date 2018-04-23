using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPackage : MonoBehaviour {

	public List<FriendsbookPost> postList = new List<FriendsbookPost>();
	public List<Character> characterList = new List<Character>();

	public void addPost (FriendsbookPost post) {
		if (checkLength(postList, characterList)) {
			postList.Add(new FriendsbookPost());
		} else {
			Debug.Log("The Information Package is full. Can't add a post.");
		}
	}

	public void addPerson (Character person) {
		if (checkLength(postList, characterList)) {
			characterList.Add(new Character());
		} else {
			Debug.Log("The Information Package is full. Can't add a person.");
		}
	}

	public void addRelation () {
		// Do something to add relation...
	}

	public bool checkLength (List<FriendsbookPost> postList, List<Character> characterList) {
		// Check if there is space in the information package to add another item of information
		if (postList.Count + characterList.Count <= 4) {
			return true;
		}
		return false;
	}

}
