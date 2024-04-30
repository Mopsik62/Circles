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
    [SerializeField] private bool pressed = false;


    void Start()
    {
        if (button == null)
        {
          button = GetComponentInParent<Button>();
        }
    }
    public void ChangeButtonImage()
    {
        if (pressed)
        {
            button.image.sprite = firstButtonSprite;
            pressed = false;
        } else
        {
            button.image.sprite = secondButtonSprite;
            pressed = true;
        }
    }
}
