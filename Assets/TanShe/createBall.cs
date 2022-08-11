using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class createBall : MonoBehaviour
{
    //子弹
    public GameObject team1_ball;
    public GameObject team2_ball;
    public GameObject team3_ball;
    public GameObject team4_ball;

    //子弹容器

    public Transform[] team_root=new Transform[5];


    public GameObject tilemap;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rd = new System.Random();
        tilemap = GameObject.Find("bound");
        for (int i = 1; i <= 4; i++)
        {
            int n = rd.Next(2,5);

            for (int j = 1; j <= n; j++)
            {
                CreateBullet(i);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        //可能有延迟 可能是bug 待修复
        if (Input.GetKeyUp("1"))
        {
            CreateBullet(1);
        }
        if (Input.GetKeyUp("2"))
        {
            CreateBullet(2);
        }
        if (Input.GetKeyUp("3"))
        {
            CreateBullet(3);
        }
        if (Input.GetKeyUp("4"))
        {
            CreateBullet(4);
        }
    }


    void CreateBullet(int type)
    {
        GameObject go;

        switch (type)
        {
            case 1:
                go = Instantiate(team1_ball);
                break;
            case 2:
                go = Instantiate(team2_ball);
                break;
            case 3:
                go = Instantiate(team3_ball);
                break;
            case 4:
                go = Instantiate(team4_ball);
                break;
            default:
                go = Instantiate(team4_ball);
                break;
        }

        go.transform.SetParent(team_root[type].parent);

        go.transform.localPosition = team_root[type].localPosition;
        go.transform.localScale = team_root[type].localScale;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.SetParent(team_root[type]);


        //给子弹添加一个初始的力
        go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * 50);
    }
}
