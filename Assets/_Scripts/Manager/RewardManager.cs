using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public float jumpTime = 0.5f;
    public float jumpForce = 0.5f;
    [SerializeField] private Gift gift = null;

    public System.Action<Gift> OnGiftClick = null;

    public void Initialize()
    {
        gift.gameObject.SetActive(false);
        gift.Data = GameUtilities.GetCurrentGiftData();
        gift.Initialize();
        gift.OnRewardGift = GiftClick;
    }

    public void GiftClick(Gift gift)
    {
        OnGiftClick?.Invoke(gift);

        //Reward here
    }

    private Tween giftTween = null;
    public void TossGift(Vector3 initPos)
    {
        GameUtilities.GetGiftValue(gift);

        gift.transform.position = initPos;
        gift.gameObject.SetActive(true);

        if (giftTween != null)
            giftTween.Kill();

        float x = Random.Range(-100, 100);
        float z = Random.Range(-100, 100);

        if (x == 0 && z == 0)
            x = 100;

        Vector3 dir = new Vector3(x, GameConstant.NODE_THICKNESS, z);

        dir = dir.normalized;
        dir.y = 0.5f;

        Vector3 jumpTarget = transform.position + dir * jumpForce;

        giftTween = transform.DOJump(jumpTarget, jumpForce, 1, jumpTime).SetEase(Ease.OutCubic).SetAutoKill();
    }
}
