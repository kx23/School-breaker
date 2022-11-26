using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Threading.Tasks;
public class LeaderBoard : MonoBehaviour
{
    string authCode;
    private const string leaderBoard = "CgkI2JLB5J0NEAIQAw";
    // Start is called before the first frame update
    void Start()
    {

        #if UNITY_ANDROID
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
           .RequestServerAuthCode(false /* Don't force refresh */)
           .Build();


        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success => {
                if (success)
                {
                    authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                    Debug.LogError("Authenticate.");
                }
                else
                {
                    Debug.LogError("Error Authenticate.");
                }
            });
        #endif
    }

    public void ShowLeaderBoard ()
    {
        Social.ReportScore(1, leaderBoard, (bool success) => { });
        Social.ShowLeaderboardUI();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
