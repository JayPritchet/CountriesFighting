using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanShe : MonoBehaviour
{
    
    int next_ball = 0;
    
    //枪杆
    public Transform gang;
    //子弹
    public GameObject team1_ball;
    public GameObject team2_ball;
    public GameObject team3_ball;
    public GameObject team4_ball;
    //子弹容器
    public Transform bulletRoot;

    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //FireAnim();
            CreateBullet();
        }
       
    }

    void CreateBullet()
    {
        GameObject go;

        switch (next_ball)
        {
            case 0:
                go = Instantiate(team1_ball);
                break;
            case 1:
                go = Instantiate(team2_ball);
                break;
            case 2:
                go = Instantiate(team3_ball);
                break;
            case 3:
                go = Instantiate(team4_ball);
                break;
            default:
                go = Instantiate(team4_ball);
                break;
        }
        next_ball++;
        next_ball %= 4;


        go.transform.SetParent(gang);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.SetParent(bulletRoot);

        //给子弹添加一个初始的力
        go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * 50);
    }

    //void FireAnim()
    //{
    //    //90~360~270
    //    float x = 0, y = 10;
    //    float angle = transform.eulerAngles.z;
    //    //print("角度:" + angle);
    //    if (angle > 0 && angle < 90)
    //    {
    //        x = 10-(10 * (angle - 90) / 90.0f);
    //        x = x * -1;
    //    }
    //    transform.DOLocalMove(new Vector3(x, y, 0), 0.25f).SetLoops(2, LoopType.Yoyo);
    //}
}
