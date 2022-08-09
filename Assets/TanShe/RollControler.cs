using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RollControler : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //计算角度
            Vector3 mousePos = Input.mousePosition;
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 dic1 = Vector2.down;
            Vector2 dic2 = transform.position - clickPos;

            Vector3 v3 = Vector3.Cross(dic1, dic2);

            float angle = 0;
            if (v3.z > 0)
                angle = Vector3.Angle(dic1, dic2);
            else
                angle = 360 - Vector3.Angle(dic1, dic2);


            transform.eulerAngles = new Vector3(0, 0, angle);
        }



    }

    

   
}
