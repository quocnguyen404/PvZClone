using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Sun : IProduct
{
    [SerializeField] private float jumpSpeed;

    public System.Action<Sun> OnClick = null;

    public float jumpTime = 0.5f;
    public float jumpForce = 0.5f;
    public int value = 50;

    public void Initialize(Vector3 initPos, int value)
    {
        transform.position = initPos;
        this.value = value;
    }

    public void TossSun(Vector3 parentPos)
    {
        float x = Random.Range(-100, 100);
        float z = Random.Range(-100, 100);

        if (x == 0 && z == 0)
            x = 100;

        Vector3 dir = new Vector3(x, 0, z);

        dir = dir.normalized;

        Vector3 jumpTarget = transform.position + dir * jumpForce;

        transform.DOJump(jumpTarget, jumpForce, 1, jumpTime).SetEase(Ease.OutCubic).SetAutoKill();

        ReturnPool(4f);
    }

    public void Fall(Vector3 ground)
    {
        transform.DOMoveY(ground.y, 9f).SetAutoKill();
    }

    public void MoveToSunBar(Vector3 pos)
    {
        //transform.DOMove(pos, GameConstant.TIME_SUN_MOVE)
        //    .OnComplete(() =>
        //    {
        //        gameObject.SetActive(false);
        //    })
        //    .SetAutoKill();

        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        transform.DOKill();
        OnClick?.Invoke(this);
    }
}
