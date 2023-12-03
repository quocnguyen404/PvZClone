using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr = null;

    public void ChangeMouseIndicatorSprite(Sprite sprite)
    {
        sr.sprite = sprite;
    }
}
