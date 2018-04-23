using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsbookPostsController : MonoBehaviour {

    public GameObject postPrefab;
    private List<FriendsbookPost> posts;

    private void Start()
    {
        if(posts != null)
        {
            DisplayPosts();
        }
    }

    public void SetPosts(List<FriendsbookPost> posts)
    {
        this.posts = posts;
    }

    public void DisplayPosts()
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
