using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private SpriteRenderer giftSR = null;

    public GiftType GiftType;
    public string Value;

    public System.Action OnRewardGift = null;

    public void Initialize()
    {
        if (GiftType is GiftType.Plant)
            giftSR.sprite = Resources.Load<Sprite>(string.Format(GameConstant.CARDS_SPRITES_PATH, Value));
        else
            giftSR.sprite = Resources.Load<Sprite>("Sprites/Item/Bag");
    }


    private void OnMouseDown()
    {
        OnRewardGift?.Invoke();
    }
}
