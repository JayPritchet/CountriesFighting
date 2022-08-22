using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class teamScore : MonoBehaviour
{
    public int score = 0;
    public int teamNumber;
    private String[] TeamColor = { "红", "红", "黄", "蓝", "绿" };
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = score.ToString();
    }
}
