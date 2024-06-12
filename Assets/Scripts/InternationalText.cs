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
        UpdateText();
    }

    private void OnDisable()
    {
        Language.OnLanguageChanged -= UpdateText;
    }

    private void UpdateText()
    {
        if (GetComponent<TextMeshProUGUI>().text.Contains(":")) //for Score and HS
        {
            string[] parts = GetComponent<TextMeshProUGUI>().text.Split(':');

            switch (Language.instance.currentLanguage)
            {
                case "en":
                    GetComponent<TextMeshProUGUI>().text = _en + parts[1];
                    Debug.Log(GetComponent<TextMeshProUGUI>().text = _en + parts[1]);
                    break;
                case "ru":
                    GetComponent<TextMeshProUGUI>().text = _ru + parts[1];
                    Debug.Log(GetComponent<TextMeshProUGUI>().text = _ru + parts[1]);
                    break;
                default:
                    GetComponent<TextMeshProUGUI>().text = _en + parts[1];
                    break;
            }
        }
        else
        { //for other
            switch (Language.instance.currentLanguage)
            {
                case "en":
                    GetComponent<TextMeshProUGUI>().text = _en;
                    break;
                case "ru":
                    GetComponent<TextMeshProUGUI>().text = _ru;
                    break;
                default:
                    GetComponent<TextMeshProUGUI>().text = _en;
                    break;
            }
        }

    }
}