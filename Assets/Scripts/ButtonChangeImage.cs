using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeImage : MonoBehaviour
{
    // Решил вынести в отдельный скрипт ибо техничнее будет.
    public Sprite firstButtonSprite;
    public Sprite secondButtonSprite;
    public Button button;
    [SerializeField] private bool mute = false;


    void Start()
    {
        if (button == null)
        {
          button = GetComponentInParent<Button>();
        }
    }
    public void ChangeButtonImage()
    {
        if (mute)
        {
            button.image.sprite = firstButtonSprite;
            mute = false;
        } else
        {
            button.image.sprite = secondButtonSprite;
            mute=true;
        }
    }
}
