using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Zombie : IUnit
{
    public System.Action OnZombieDie = null;

    protected int currentNodeIndex;

    public float unitSpeed
    {
        get
        {
            return UnitData.attributes[(int)Data.AttributeType.SP].value;
        }
    }

    public virtual void InitializeRow(int row)
    {
        nodesPath = new List<Node>(OnGetPath?.Invoke(row));

        currentNodeIndex = GameConstant.GARDEN_COLOUMN + GameConstant.ZOMBIE_COLUMN - 1;

        transform.position = nodesPath[currentNodeIndex].WorldPosition;
    }


    public virtual void Move()
    {
        if (!IsAlive)
            return;

        if (currentNodeIndex > nodesPath.Count)
        {

        }

        Vector3 nodePosition = Vector3.zero;
        try
        {
            nodePosition = nodesPath[currentNodeIndex].WorldPosition;
        }
        catch
        {
            Debug.Log("AA");
        }

        Vector3 destination = new Vector3(nodePosition.x - GameConstant.NODE_LENGTH / 2, nodePosition.y, nodePosition.z);
        IUnit target = nodesPath[currentNodeIndex].GetPlantFromNode();

        if (target != null && target.IsAlive)
        {
            StopAllCoroutines();
            Attack(target);
        }
        else
        {
            this.DelayCall(unitSpeed, Move);

            MoveToDestination(destination, unitSpeed, () =>
            {

                nodesPath[currentNodeIndex].AddUnit(this);
            });
        }

    }

    public virtual void Attack(IUnit target)
    {

        target.TakeDamage(UnitData.attributes[(int)Data.AttributeType.ATK].value);

        this.DelayCall(UnitData.attributes[(int)Data.AttributeType.AAI].value, Move);

        //Vector3 destination = new Vector3(target.transform.position.x + GameConstant.NODE_LENGTH / 2, target.transform.position.y, target.transform.position.z);
        //MoveToDestination(destination, unitSpeed / 2, () =>
        //{
        //});
    }

    public virtual void MoveToDestination(Vector3 destination, float time, System.Action Callback)
    {
        transform.DOMove(destination, time)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                nodesPath[currentNodeIndex].RemoveUnit(this);
                currentNodeIndex--;
                Callback?.Invoke();
            }).SetAutoKill();
    }

    public override void Dead()
    {
        base.Dead();
        OnZombieDie?.Invoke();
    }
}
