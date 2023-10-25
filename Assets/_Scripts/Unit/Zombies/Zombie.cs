using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Zombie : IUnit
{
    public bool isAlive = false;
    public bool isOnNode = false;
    public System.Action OnZombieDie = null;

    protected List<Node> nodesPath = null;
    protected int currentNodeIndex;
    protected bool arried = false;

    public float unitSpeed
    {
        get
        {
            return UnitData.attributes[(int)Data.AttributeType.SP].value;
        }
    }

    public System.Func<int, List<Node>> OnGetPath = null;


    public virtual void InitializeRow(int row)
    {
        nodesPath = OnGetPath?.Invoke(row);

        currentNodeIndex = GameConstant.GARDEN_COLOUMN - 1;

        Move();
    }

    public virtual void Move()
    {
        if (!isAlive)
            return;

        Vector3 destination = nodesPath[currentNodeIndex].WorldPosition;
        IUnit target = nodesPath[currentNodeIndex].unit;

        if (target != null)
        {
            Attack(target);
            StopCoroutine("Move");
        }
        else
        {
            this.DelayCall(UnitData.attributes[(int)Data.AttributeType.SP].value, Move);

            MoveToDestination(destination, () =>
            {
                arried = true;
                currentNodeIndex--;
            });
        }
    }

    public virtual void Attack(IUnit target)
    {
        Vector3 destination = new Vector3(target.transform.position.x + 0.5f, target.transform.position.y, target.transform.position.z);
        MoveToDestination(destination, () =>
        {

        });
    }

    public virtual void MoveToDestination(Vector3 destination, System.Action Callback)
    {
        transform.DOMove(destination, unitSpeed)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Callback?.Invoke();
            }).SetAutoKill();
    }

    public override void Dead()
    {
        base.Dead();

        OnZombieDie?.Invoke();
    }
}
