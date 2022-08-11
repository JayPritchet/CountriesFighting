using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateScore : MonoBehaviour
{
    public int teamNumber;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("team" + teamNumber.ToString() + "_score") != null)
            GameObject.Find("team" + teamNumber.ToString() + "_score").GetComponent<teamScore>().score = GameObject.Find("team" + teamNumber.ToString() + "_score").GetComponent<teamScore>().score + 1;

    }

    private void OnDestroy()
    {
        if(GameObject.Find("team" + teamNumber.ToString() + "_score")!=null)
        GameObject.Find("team" + teamNumber.ToString() + "_score").GetComponent<teamScore>().score = GameObject.Find("team" + teamNumber.ToString() + "_score").GetComponent<teamScore>().score - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
