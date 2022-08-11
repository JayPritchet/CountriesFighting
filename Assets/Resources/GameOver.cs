using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public Text winnerText;
    public Text countDownText;
    public int totalSeconds=10;

    private DateTime time;

    private String[] TeamColor={ "红", "红", " 黄", " 蓝", " 绿" };
    // Start is called before the first frame update
    void Start()
    {
        
        int score1 = GameObject.Find("team1_score").GetComponent<teamScore>().score;
        int score2 = GameObject.Find("team2_score").GetComponent<teamScore>().score;
        int score3 = GameObject.Find("team3_score").GetComponent<teamScore>().score;
        int score4 = GameObject.Find("team4_score").GetComponent<teamScore>().score;

        int max_score = Math.Max(Math.Max(score1, score2), Math.Max(score3, score4));
        int winner = 0;
        if (max_score == score1)
            winner = 1;
        else if (max_score == score2)
            winner = 2;
        else if (max_score == score3)
            winner = 3;
        else if (max_score == score4)
            winner = 4;

        winnerText.text = "恭喜"+TeamColor[winner]+"队取得胜利";

        time = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)(DateTime.Now - time).TotalSeconds < 1)
            return;
        time = DateTime.Now;
        countDownText.text = totalSeconds.ToString()+ "秒后游戏重新开始";
        totalSeconds--;
        if (totalSeconds==0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
