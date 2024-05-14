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
    private static extern void RateGame();


    public void RateGameButton()
    {
        RateGame();
    }

   
}
