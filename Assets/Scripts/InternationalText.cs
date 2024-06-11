using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InternationalText : MonoBehaviour
{
    [SerializeField] string _en;
    [SerializeField] string _ru;

    private void OnEnable()
    {
        Language.OnLanguageChanged += UpdateText;
        // ≈сли €зык уже загружен, обновл€ем текст сразу
        if (!string.IsNullOrEmpty(Language.instance.currentLanguage))
        {
            UpdateText();
        }
    }

    private void OnDisable()
    {
        Language.OnLanguageChanged -= UpdateText;
    }

    private void UpdateText()
    {
        string[] parts = GetComponent<TextMeshProUGUI>().text.Split(':');
        Debug.Log(parts[0] + "   " + parts[1]);

        switch (Language.instance.currentLanguage)
        {
            case "en":
                GetComponent<TextMeshProUGUI>().text = _en + ":" + parts[1];
                break;
            case "ru":
                GetComponent<TextMeshProUGUI>().text = _ru + ":" + parts[1];
                break;
            default:
                GetComponent<TextMeshProUGUI>().text = _en + ":" + parts[1];
                break;
        }
    }
}
