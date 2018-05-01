using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//view for posts (subview of character profile page)
public class FriendsbookPostsView : MonoBehaviour {

    public GameObject postPrefab;
    private List<FriendsbookPost> posts;

    //update view with posts in posts
    public void DisplayPosts(List<FriendsbookPost> posts)
    {
        foreach(FriendsbookPost p in posts)
        {
            GameObject post = Instantiate(postPrefab);
            post.transform.SetParent(transform, false);
            var postView = post.GetComponent<PostView>();
            if(p.from == p.to)
            {
                postView.SetTitle(p.to.character.fullName);
            }
            else
            {
                postView.SetTitle(string.Format("{0} -> {1}", p.from.character.fullName, p.to.character.fullName));
            }
            
            postView.SetContent(p.content);
            postView.SetDate(p.date.ToString());
        }
    }
}
