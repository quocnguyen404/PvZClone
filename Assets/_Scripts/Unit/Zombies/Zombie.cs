using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Zombie : IUnit
{
    public System.Action OnZombieDie = null;

    protected int currentNodeIndex;
    protected float animOffSet = 0.5f;

    public System.Action OnZombieGetInHouse = null;
    public System.Func<Vector3> OnGetHousePosition = null;

    private float unitSpeed = -1f;
    public float UnitSpeed
    {
        get
        {
            if (unitSpeed <= 0f)
                unitSpeed = UnitData.attributes[(int)Data.AttributeType.SP].value;
            
            return unitSpeed;
        }

        set
        {
            unitSpeed = value;
        }
    }

    public bool isDebuff = false;

    public virtual void InitializeRow(int row)
    {
        nodesPath = OnGetPath?.Invoke(row);

        currentNodeIndex = GridPosition.y;
        Move();
    }

    private void Update()
    {

        if (!IsAlive)
            return;
    }

    public virtual void Move()
    {
        if (!IsAlive)
            return;

        if (currentNodeIndex < 0)
        {
            MoveToDestination((Vector3)OnGetHousePosition?.Invoke(), UnitSpeed, null);
            OnZombieGetInHouse?.Invoke();
            return;
        }

        Vector3 nodePosition = Vector3.zero;

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
            this.DelayCall(UnitSpeed, Move);

            MoveToDestination(destination, UnitSpeed, () =>
            {

            });
        }

    }

    public virtual void Attack(IUnit target)
    {
        ator.SetTriggger("Attack");

        DOVirtual.DelayedCall(UnitData.attributes[(int)Data.AttributeType.AAI].value - animOffSet, () =>
        {
            target.TakeDamage(UnitData.attributes[(int)Data.AttributeType.ATK].value);
        }).SetAutoKill();

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
                nodesPath[currentNodeIndex].AddUnit(this);
                Callback?.Invoke();
            }).SetAutoKill();
    }

    public virtual void TakeSlowDebuff()
    {

    }

    public override void Dead()
    {
        ator.SetTriggger("Death");
        gameObject.SetActive(false);
        transform.position = PoolPosition;
        nodesPath[currentNodeIndex].RemoveUnit(this);
        OnZombieDie?.Invoke();
    }
}
