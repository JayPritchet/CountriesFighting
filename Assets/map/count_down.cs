using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class count_down : MonoBehaviour
{
    private DateTime time;
    public Text count_down_text;
    public int total_seconds;
    // Start is called before the first frame update
    void Start()
    {
       count_down_text = this.GetComponent<Text>();
       time=DateTime.Now;
       count_down_text.text = total_seconds.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        count_down_text.text = (total_seconds-(int)(DateTime.Now - time).TotalSeconds).ToString();
        if (count_down_text.text == "0")
        {
            //game over
            SceneManager.LoadScene(0);
        }
    }
}
