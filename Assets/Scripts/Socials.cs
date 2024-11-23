using UnityEngine;

public class Socials : MonoBehaviour
{
    public void JoinCommunity()
    {
        Application.OpenURL("https://t.me/Space_Kombat");
    }
    public void LatestUpdates()
    {
        Application.OpenURL("");
    }
    public void FollowTwitter()
    {
        Application.OpenURL("https://x.com/Space_Kombat");
    }
    public void ShareToStatus()
    {
        //Share to telegram link implementation 
    }
    public void FollowInstagram()
    {
        Application.OpenURL("//Put Instagram profile link");
    }
    public void FollowTiktok()
    {
        Application.OpenURL("//Put Tiktok profile link");
    }
    public void SubscribeToYoutube()
    {
        Application.OpenURL("//Put Youtube profile link");
    }
}
