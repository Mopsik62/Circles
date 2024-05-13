using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Yandex : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void GiveMePlayerData();
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] RawImage playerImage;

    public void HelloButton()
    {
        GiveMePlayerData();
    }
    public void SetName(string name)
    {
        nameText.text = name;
    }
    public void SetPhoto(string url)
    {
        StartCoroutine(DownloadImage(url));
    }

    IEnumerator DownloadImage(string mediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
            playerImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;



    }
}
