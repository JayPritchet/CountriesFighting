using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ball : MonoBehaviour
{
    public string uid;

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


    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}
