using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballCollision : MonoBehaviour
{
    public GameObject team_tile;
    private GameObject tilemap;

    public Text score1;
    public Text score2;
    public Text score3;
    public Text score4;
    public int speed=2;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.Find("bound");
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag != "tile")
            return;

        Vector3 pos = c.transform.localPosition;
        Vector3 scale = c.transform.localScale;

        GameObject.Destroy(c.gameObject);
        GameObject tile;
        tile = Instantiate(team_tile);
        tile.transform.parent = tilemap.transform;
        tile.transform.localPosition = pos;
        tile.transform.localScale = scale;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Rigidbody2D>().velocity.x == 0 && this.GetComponent<Rigidbody2D>().velocity.y == 0)
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(1,1);
        
        this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity.normalized*speed;
    }
}
