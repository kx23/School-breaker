using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Score : MonoBehaviour
{
    private const string leaderBoard = "CgkI2JLB5J0NEAIQAw";
    private float score = 0f;
    public float GetScore 
    {
        get => score;
    }
    private void Start()
    {
        GameManager.OnGetCollectable.AddListener(() => GetCollectable());
    }
    private void FixedUpdate()
    {
        ScoreUpdate();
    }
    private void ScoreUpdate()
    {

        if (!PlatformMovement.isMove) return;
        if (Tutorial.isTutorial)
            return;
        score += PlatformMovement.speed / 20;
    }
    private void GetCollectable()
    {
        score += 150;
    }
    public void SetBestScore()
    {
#if UNITY_ANDROID
        if (Social.localUser.authenticated)
            Social.ReportScore((int)score, leaderBoard, (bool success) => { });
#endif
            PlayerPrefs.SetInt("Score", (int)score);

    }

}
