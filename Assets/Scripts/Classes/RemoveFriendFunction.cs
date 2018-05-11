using UnityEngine;

[CreateAssetMenu (menuName = "Event/Remove Friend Function")]
public class RemoveFriendFunction : FunctionCall {
        
    public Character selfCharacter;
    public Character newFriend;

    public override void triggerFunction() {
        selfCharacter.friendsbookProfile.friends.Remove(newFriend);
    }
}