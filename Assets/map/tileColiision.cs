using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileColiision : MonoBehaviour
{

    //子弹
    public GameObject team1_tile;
    public GameObject team2_tile;
    public GameObject team3_tile;
    public GameObject team4_tile;

    public GameObject tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap= GameObject.Find("bound"); 
    }
    void OnCollisionEnter2D(Collision2D c)
    {
       // if (c.gameObject.layer != this.gameObject.layer&&c.gameObject.tag.ToString()!="tile")
        {
            Vector3 pos = this.transform.localPosition;
            Vector3 scale = this.transform.localScale;

            GameObject.Destroy(this.gameObject);
            GameObject tile;
            if (c.gameObject.tag.ToString().CompareTo("team1_ball") == 0)
                tile = Instantiate(team1_tile);
            else if (c.gameObject.tag.ToString().CompareTo("team2_ball")==0)
                tile = Instantiate(team2_tile);
            else if (c.gameObject.tag.ToString().CompareTo("team3_ball") == 0)
                tile = Instantiate(team3_tile);
            else if (c.gameObject.tag.ToString().CompareTo("team4_ball") == 0)
                tile = Instantiate(team4_tile);
            else
                tile = Instantiate(team2_tile);

            tile.transform.parent=tilemap.transform;
            tile.transform.localPosition = pos;
            tile.transform.localScale = scale;

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
