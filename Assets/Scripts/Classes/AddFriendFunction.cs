using UnityEngine;

[CreateAssetMenu (menuName = "Event/AddFriendFunction")]
public class AddFriendFunction : FunctionCall {

    public Character selfCharacter;
    public Character newFriend;

    public override void triggerFunction() {
        selfCharacter.friendsbookProfile.AddFriend(newFriend);
    }
}