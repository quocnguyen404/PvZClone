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


    public void Initialize(float speed, float radius)
    {
        this.speed = speed;
        this.radius = radius;
        isStop = true;
    }

    public void SetDestination(Vector3 destination)
    {
        isStop = false;

        MoveToDestination(destination);

        if (Vector3.Distance(transform.position, destination) <= radius)
            OnArried?.Invoke();
    }


    public void MoveToDestination(Vector3 destination)
    {
        isStop = false;

        transform.DOKill();
        transform.DOMove(destination, GameUtilities.TimeToDestination(transform.position, destination, speed))
            .SetEase(Ease.Linear)
            .SetAutoKill();
    }



    public void Stop()
    {
        if (isStop)
            return;

        transform.DOKill();
        isStop = true;
    }
}