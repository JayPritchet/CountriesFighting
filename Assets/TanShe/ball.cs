using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ball : MonoBehaviour
{
    public string uid;
    public int radius;

    private int scale;

    private List<GameObject> clones;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(DownSprite());

    }

    IEnumerator DownSprite()
    {
        WebClient MyWebClient = new WebClient();
        string icon_url = "https://tenapi.cn/bilibili/?uid=" + uid;
        MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
        Byte[] pageData = MyWebClient.DownloadData(icon_url); //从指定网站下载数据
        string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这
        JObject json = (JObject)JsonConvert.DeserializeObject(pageHtml);
        icon_url = json["data"]["avatar"].ToString();

        UnityWebRequest wr = new UnityWebRequest(icon_url);
        DownloadHandlerTexture texD1 = new DownloadHandlerTexture(true);
        wr.downloadHandler = texD1;
        yield return wr.SendWebRequest();
        int width = 1920;
        int high = 1080;
        if (!wr.isNetworkError)
        {
            Texture2D tex = new Texture2D(width, high);
            tex = texD1.texture;

            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            GetComponent<CircleImage>().sprite = sprite;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    void DrawLineToClones()
    {
    
    }

    public void Clone()
    {
        GameObject clone=GameObject.Instantiate(this.gameObject);
        clones.Add(clone);

    }

    public void ReverseDirection()
    {
        this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity* (-1);
    }
    public void ChangeSize(float scale, int time = -1)
    {
        var trans = this.GetComponent<RectTransform>();
        var collider = this.GetComponent<CircleCollider2D>();
        var rect = trans.rect;

        float old_trans_width = trans.rect.width;
        float old_trans_height = trans.rect.height;
        float old_collider_radius = collider.radius;

        trans.rect.Set(trans.rect.x, trans.rect.y, old_trans_width * scale, old_trans_height * scale);
        collider.radius = old_collider_radius * scale;

        if (time > 0)
        {
            Thread t = new Thread(
            () => {
                Thread.Sleep(time * 1000);
                rect.Set(rect.x, rect.y, old_trans_width, old_trans_height);
                collider.radius = old_collider_radius;
                Thread.CurrentThread.Abort();
            }
            );
            t.Start();

            DateTime LastTime = DateTime.Now;

        }
    }
    public void SetSpeed(int speed, int time = -1)
    {
        var ball_speed = this.GetComponent<ballCollision>();
        ball_speed.speed = speed;
        if (time > 0)
        {
            Thread t = new Thread(
            () => {
                Thread.Sleep(time * 1000);
                ball_speed.speed = 2;
                Thread.CurrentThread.Abort();
            }
            );
            t.Start();
        }
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}
