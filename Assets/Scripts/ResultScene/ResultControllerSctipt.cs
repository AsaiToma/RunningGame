using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultControllerSctipt : MonoBehaviour
{
    public Text finalScore;
    private float s;
    public Text playTime;
    private float p;

    private void Start()
    {
        s = Data.Instance.finalScore;
        p = Data.Instance.totalRunningTime;

        finalScore.text = "Score : " + (s).ToString("F2");
        playTime.text = "PlayTime : " + (p).ToString("F2");
    }

    public void pushRetryButtom()
    {
        SceneManager.LoadScene("startScene");
        Data.Instance.finalScore = 0.0f;
        Data.Instance.totalRunningTime = 0.0f;
    }
}
