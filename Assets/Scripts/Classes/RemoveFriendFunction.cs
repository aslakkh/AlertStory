using UnityEngine;

[CreateAssetMenu (menuName = "Event/Remove Friend Function")]
public class RemoveFriendFunction : FunctionCall {
        
    public Character selfCharacter;
    public Character newFriend;

    public override void triggerFunction() {
        //selfCharacter.friendsbookProfile.friends.Remove(newFriend);
        GameManager gm = GameObject.FindObjectOfType<GameManager>();

        //remove reference to selfcharacter in runtime copy of newfriend
        foreach(Character c in gm.characterList)
        {
            if(c.fullName == newFriend.fullName)
            {
                c.friendsbookProfile.RemoveFriend(selfCharacter);
            }
        }

        //remove reference to newfriend in runtime copy of playerCharacter
        gm.playerCharacter.friendsbookProfile.RemoveFriend(newFriend);

    }
}