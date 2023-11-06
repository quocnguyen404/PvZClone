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
        nodesPath = OnGetPath?.Invoke(row);


        currentNodeIndex = GridPosition.y + 9;
        transform.position = nodesPath[currentNodeIndex].WorldPosition;

        Move();
    }


    public virtual void Move()
    {
        if (!IsAlive)
            return;


        if (currentNodeIndex > nodesPath.Count)
        {
            StopAllCoroutines();
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
            ator.Reset();
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
        ator.SetTriggger("Attack");

        DOVirtual.DelayedCall(UnitData.attributes[(int)Data.AttributeType.AAI].value, () =>
        {
            target.TakeDamage(UnitData.attributes[(int)Data.AttributeType.ATK].value);
        });

        this.DelayCall(UnitData.attributes[(int)Data.AttributeType.AAI].value, Move);
    }

    public virtual void MoveToDestination(Vector3 destination, float time, System.Action Callback)
    {
        ator.SetMove(UnitAnimator.MovementType.Move);
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
        //nodesPath[GridPosition.y].RemoveUnit(this);
        //transform.position = Vector3.zero;
        //nodesPath.Clear();
        ator.SetTriggger("Death");
        gameObject.SetActive(false);
        OnZombieDie?.Invoke();
    }
}
