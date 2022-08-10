using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateScore : MonoBehaviour
{
    public int teamNumber;
    private Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("team"+teamNumber.ToString()+"_score").GetComponent<Text>();
        score.text = (int.Parse(score.text) + 1).ToString();
        
    }

    private void OnDestroy()
    {
        score.text = (int.Parse(score.text) - 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
