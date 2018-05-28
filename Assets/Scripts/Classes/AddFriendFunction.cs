using UnityEngine;

[CreateAssetMenu (menuName = "Event/AddFriendFunction")]
public class AddFriendFunction : FunctionCall {

    public Character selfCharacter;
    public Character newFriend;

    public override void triggerFunction()
    {
        //selfCharacter.friendsbookProfile.friends.Add(newFriend);
        GameManager gm = GameObject.FindObjectOfType<GameManager>();

        //add reference to selfcharacter in runtime copy of newfriend
        foreach (Character c in gm.characterList)
        {
            if (c.fullName == newFriend.fullName)
            {
                c.friendsbookProfile.AddFriend(selfCharacter);
            }
        }

        //add reference to newfriend in runtime copy of playercharacter
        gm.playerCharacter.friendsbookProfile.AddFriend(newFriend);

    }
}