using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Agent : MonoBehaviour
{
    public float speed;
    public float radius;
    public bool isStop = true;

    public System.Action OnArried = null;
    public System.Action OnMoveAnimation = null;


    public void Initialize(float speed, float radius)
    {
        this.speed = speed;
        this.radius = radius;
        isStop = true;
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void OnTurnOff()
    {
        speed = 0;
        radius = 0;
        isStop = true;
        OnArried = null;
        OnMoveAnimation = null;
    }


    public void SetDestination(Vector3 destination)
    {
        isStop = false;

        MoveToDestination(destination);
    }

    private Tween moveTween = null;
    public void MoveToDestination(Vector3 destination)
    {
        isStop = false;

        transform.DOKill();
        OnMoveAnimation?.Invoke();
        moveTween = transform.DOMove(destination, GameUtilities.TimeToDestination(transform.position, destination, speed))
            .SetEase(Ease.Linear)
            .OnComplete(() => 
            {
                    OnArried?.Invoke();
            })
            .SetAutoKill();
    }

    public void AttackStop()
    {
        if (isStop)
            return;

        moveTween.Kill();
    }

    public void Stop()
    {
        //if (isStop)
        //    return;

        OnArried?.Invoke();
        moveTween.Kill();
        isStop = true;
    }

}
