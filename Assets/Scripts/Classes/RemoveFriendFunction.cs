using UnityEngine;

[CreateAssetMenu (menuName = "Event/Remove Friend Function")]
public class RemoveFriendFunction : FunctionCall {
        
    public Character selfCharacter;
    public Character newFriend;

    public override void triggerFunction() {
        //selfCharacter.friendsbookProfile.friends.Remove(newFriend);
        GameManager gm = GameObject.FindObjectOfType<GameManager>();

        //match character in temporary runtime list with newFriend. If it exists, remove it as friend in gm.playercharacter
        foreach(Character c in gm.characterList)
        {
            if(c.fullName == newFriend.fullName) //match on name
            {
                gm.playerCharacter.friendsbookProfile.RemoveFriendInBoth(c);
            }
        }
    }
}