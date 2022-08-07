using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanseBullet : MonoBehaviour
{
    Rigidbody2D rig2d;
    RectTransform rect;

    //反弹力
    float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rect= transform.GetComponent<RectTransform>();
        rig2d = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBoundary();
    }

    //检测边界
    void CheckBoundary()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
       // if(rect.rect.)


        Vector3 norm = Vector3.zero;
        if (pos.x > Screen.width)//右边界
        {
            norm = Vector3.left;
        }
        if (pos.x < 0)//左边界
        {
            norm = Vector3.right;
        }
        if (pos.y < 0)//下边界
        {
            norm = Vector3.up;
        }
        if (pos.y > Screen.height)//上边界
        {
            norm = Vector3.down;
        }
        if (norm != Vector3.zero)
        {
            //反弹逻辑
            Vector2 dir = Vector2.Reflect(transform.up, norm);
            transform.rotation = Quaternion.FromToRotation(Vector2.up, dir);
            rig2d.velocity = (new Vector2(dir.x, dir.y)).normalized * speed;
            norm = Vector3.zero;
        }
        

    }


}
