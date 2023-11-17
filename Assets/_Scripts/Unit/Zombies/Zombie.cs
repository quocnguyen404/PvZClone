using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Zombie : IUnit
{
    protected int currentNodeIndex;

    #region Event 
    public System.Action OnZombieDie = null;
    public System.Action OnZombieGetInHouse = null;
    public System.Func<Vector3> OnGetHousePosition = null;
    #endregion

    #region Attributes
    protected float unitSpeed = -1f;
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
    #endregion

    #region Debuff Handle
    protected Dictionary<DebuffType, float> dictValueDebuff;
    protected Dictionary<DebuffType, float> dictDebuff;
    public List<DebuffType> currentDebuffs
    {
        get
        {
            List<DebuffType> d = new List<DebuffType>();

            foreach (var e in dictDebuff)
            {
                if (e.Value > 0)
                    d.Add(e.Key);
            }

            return d;
        }
    }
    public bool IsDebuff
    {
        get
        {
            return currentDebuffs.Count > 0;
        }
    }
    protected float debuffTimer = 0f;
    #endregion

    public override void Initialize(Vector2Int pos)
    {
        base.Initialize(pos);

        dictValueDebuff = new Dictionary<DebuffType, float>();
        dictDebuff = new Dictionary<DebuffType, float>();

        dictDebuff[DebuffType.Slow] = 0f;
        dictDebuff[DebuffType.Bleed] = 0f;
        dictDebuff[DebuffType.Burn] = 0f;
    }

    public virtual void InitializeRow(int row)
    {
        nodesPath = OnGetPath?.Invoke(row);

        currentNodeIndex = GridPosition.y;

        Move();
    }

    public override void Update()
    {
        if (!IsAlive)
            return;

        if (IsDebuff)
        {
            foreach (DebuffType t in currentDebuffs)
                CountDownDebuff(t);

            if (debuffTimer <= 0f)
            {
                foreach (DebuffType t in currentDebuffs)
                    DoDebuff(dictValueDebuff[t], t);

                debuffTimer = 1f;
            }
        }

        debuffTimer -= Time.deltaTime;
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

        DOVirtual.DelayedCall(UnitData.attributes[(int)Data.AttributeType.AAI].value, () =>
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

    public virtual void TakeDebuff(float duration, DebuffType type, float debuffValue)
    {
        dictDebuff[type] = duration;
        dictValueDebuff[type] = debuffValue;
    }

    public virtual void DoDebuff(float debuffValue, DebuffType type)
    {
        switch (type)
        {
            case DebuffType.Slow:

                if (UnitSpeed == UnitSpeed - debuffValue)
                    return;

                UnitSpeed -= debuffValue;

                return;

            case DebuffType.Burn:
                currentHealth -= debuffValue;
                return;

            case DebuffType.Bleed:
                currentHealth -= debuffValue;
                return;

            default:
                return;
        }
    }

    public virtual void CountDownDebuff(DebuffType type)
    {
        dictDebuff[type] -= Time.deltaTime;
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
