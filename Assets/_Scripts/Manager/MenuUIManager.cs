using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] Image adventureBtn = null;
    [SerializeField] Sprite startAdventureSprite = null;
    [SerializeField] Sprite adventureSprite = null;


    private void Awake()
    {
        if (ConfigHelper.UserData.userLevel <= 1)
        {
            adventureBtn.sprite = startAdventureSprite;
        }
    }
}
