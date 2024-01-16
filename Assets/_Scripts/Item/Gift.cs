using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private SpriteRenderer giftSR = null;
    public System.Action OnRewardGift = null;

    public GiftData Data;

    private void Awake()
    {
        transform.eulerAngles = Helper.Cam.transform.eulerAngles;
    }

    public void Initialize()
    {
        if (Data.GiftType is GiftType.Plant)
            giftSR.sprite = Resources.Load<Sprite>(string.Format(GameConstant.CARDS_SPRITES_PATH, Data.Value));
        else
            giftSR.sprite = Resources.Load<Sprite>("Sprites/Item/Bag");
    }


    private void OnMouseDown()
    {
        OnRewardGift?.Invoke();
    }
}
