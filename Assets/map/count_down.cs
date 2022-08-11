using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState
{
    Playing,
    End,
}

public class count_down : MonoBehaviour
{
    private DateTime time;
    public Text count_down_text;
    public int totalSeconds=10;
    private GameState m_state;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        count_down_text = this.GetComponent<Text>();
       time=DateTime.Now;
        count_down_text.text = totalSeconds.ToString() + "秒后本局结束";
        m_state = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_state == GameState.End)
            return;


        if ((int)(DateTime.Now - time).TotalSeconds < 1)
            return;
        time = DateTime.Now;
        count_down_text.text = totalSeconds.ToString()+"秒后本局结束";
        totalSeconds--;

        if (totalSeconds == 0)
        {
            Time.timeScale = 0f;

            //game over
            m_state = GameState.End;
            var canvas = GameObject.Find("Canvas");
            var prefab = Resources.Load("GameOverPanel");
            var panel = GameObject.Instantiate(prefab) as GameObject;
            panel.transform.SetParent(canvas.transform, false);

           // SceneManager.LoadScene(0);
        }
    }
}
