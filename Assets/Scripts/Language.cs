using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

public class Language : MonoBehaviour
{
    public static Language instance;
    public TextMeshProUGUI debug;

    public string currentLanguage; // ru, en

    public delegate void LanguageChanged();
    public static event LanguageChanged OnLanguageChanged;

    [DllImport("__Internal")]
    private static extern string GetLang();

    private void Awake()
    {
        instance = this;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            StartCoroutine(InitializeLanguage());
        }
    }

    private IEnumerator InitializeLanguage()
    {
        yield return StartCoroutine(WaitForLanguage());

        // Уведомляем всех подписчиков об изменении языка
        OnLanguageChanged?.Invoke();

        debug.text = debug.text + currentLanguage + "/";
    }

    private IEnumerator WaitForLanguage()
    {
        // Ожидание завершения инициализации SDK
        while (string.IsNullOrEmpty(currentLanguage))
        {
            currentLanguage = GetLang();
            yield return null;
        }
    }
}
