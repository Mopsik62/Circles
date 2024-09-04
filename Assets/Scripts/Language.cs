using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

public class Language : MonoBehaviour
{
    public static Language instance;

    public string currentLanguage; // ru, en

    public delegate void LanguageChanged();
    public static event LanguageChanged OnLanguageChanged;

    [DllImport("__Internal")]
    private static extern string GetLang();

    private void Awake()
    {
        instance = this;

       /* if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("WebGLPlayer");
            StartCoroutine(InitializeLanguage());
        }*/
    }
    private void Start()
    {
        currentLanguage = GetLang();
        //Debug.Log("curret language in Unity = " + currentLanguage);
        OnLanguageChanged?.Invoke();

    }

    /*    private IEnumerator InitializeLanguage()
        {
            yield return StartCoroutine(WaitForLanguage());

            // Уведомляем всех подписчиков об изменении языка
            OnLanguageChanged?.Invoke();
        }
        private IEnumerator WaitForLanguage()
        {
            // Ожидание завершения инициализации SDK
            while (string.IsNullOrEmpty(currentLanguage))
            {
                Debug.Log("currentLanguage IsNullOrEmpty");
                Debug.Log("BEFORE currentLanguage = " + currentLanguage);
                currentLanguage = GetLang();
                Debug.Log("AFTER currentLanguage = " + currentLanguage);
                yield return null;
            }
        }*/
}
