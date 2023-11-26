using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Zombie : IUnit
{
    [SerializeField] protected SpriteRenderer sr = null;
    [SerializeField] protected Agent agent = null;
    [SerializeField] protected CapsuleCollider col = null;

    [SerializeField] protected int currentNodeIndex;
    protected float attackTimer = 0;
    protected bool arried = true;

    public override bool IsAlive => currentHealth > 0 || Armour > 0;
    public virtual bool IsInitialize => nodesPath.Count > 0;


    #region Event
    public System.Action OnZombieDie = null;
    public System.Action OnZombieGetInHouse = null;
    public System.Func<Vector3> OnGetHousePosition = null;
    #endregion

    #region Attributes
    protected float maxUnitSpeed => UnitData.attributes[(int)Data.AttributeType.SP].value;
    public float UnitSpeed = 0f;

    
    protected float maxArmour => UnitData.attributes[(int)Data.AttributeType.DFS].value;
    public float Armour = 0f;
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
        ator.ZInitialize();
    }

    public virtual void InitializeRow(int row)
    {
        nodesPath = OnGetPath?.Invoke(row);

        transform.eulerAngles = Helper.Cam.transform.eulerAngles;
        UnitSpeed = maxUnitSpeed;
        Armour = maxArmour;
        col.enabled = true;

        attackTimer = UnitData.attributes[(int)Data.AttributeType.AAI].value;
        currentNodeIndex = GridPosition.y;
        agent.OnArried = Arried;
        agent.OnMoveAnimation = () => 
        {
            if (currentHealth > 0)
                ator.SetZombieMove(UnitAnimator.ZombieStateType.Walk);
            else if (Armour > 0)
                ator.SetZombieMove(UnitAnimator.ZombieStateType.LostHeadWalk);
        };
        arried = false;

        agent.Initialize(UnitSpeed, col.radius);
    }

    public override void Update()
    {
        if (!IsAlive)
        {
            return;
        }

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

        if (arried)
            return;

        if (CanAttack())
        {
            agent.AttackStop();
            attackTimer += Time.deltaTime;

            if (attackTimer >= UnitData.attributes[(int)Data.AttributeType.AAI].value)
            {
                Attack(nodesPath[currentNodeIndex].GetPlantFromNode());
                attackTimer = 0;
            }

            return;
        }

        agent.SetDestination(nodesPath[currentNodeIndex].WorldPosition);
    }

    protected virtual void Arried()
    {
        arried = true;

        if (!IsAlive)
            return;

        nodesPath[currentNodeIndex].RemoveUnit(this);
        currentNodeIndex--;

        if (currentNodeIndex < 0)
        {
            Vector3 housePos = (Vector3)OnGetHousePosition?.Invoke();
            agent.OnArried = null;
            agent.SetDestination(housePos);
            arried = true;

            this.DelayCall(GameUtilities.TimeToDestination(transform.position, housePos, UnitSpeed), () =>
            {
                ator.SetZombieMove(UnitAnimator.ZombieStateType.Attack);
                OnZombieGetInHouse?.Invoke();
                StopAllCoroutines();
            });

            return;
        }

        nodesPath[currentNodeIndex].AddUnit(this);

        arried = false;
    }

    public virtual bool CanAttack()
    {
        return nodesPath[currentNodeIndex].HasPlant()
            && Vector3.Distance(transform.position, nodesPath[currentNodeIndex].WorldPosition)
            <= (col.radius /*+ GameConstant.NODE_LENGTH/2*/);
    }

    public virtual void Attack(IUnit target)
    {
        ator.SetZombieMove(UnitAnimator.ZombieStateType.Attack);
        target.TakeDamage(UnitData.attributes[(int)Data.AttributeType.ATK].value);
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

                if (UnitSpeed == UnitSpeed + debuffValue)
                    return;

                sr.color = Color.blue;
                UnitSpeed += debuffValue;
                break;

            case DebuffType.Burn:
                currentHealth -= debuffValue;
                break;

            case DebuffType.Bleed:
                currentHealth -= debuffValue;
                break;
        }
    }

    public virtual void CountDownDebuff(DebuffType type)
    {
        dictDebuff[type] -= Time.deltaTime;

        if (dictDebuff[type] <= 0)
            ResetDebug(type);
    }

    public override void TakeDamage(float damage)
    {
        if (currentHealth > 0)
            currentHealth -= damage;
        else if (currentHealth <= 0)
            Armour -= damage;

        if (currentHealth <= 0)
            ator.SetZombieLostHead();

        if (currentHealth <= 0 && Armour <= 0)
        {
            ator.SetZombieMove(UnitAnimator.ZombieStateType.Die);
            Dead();
        }
    }

    public virtual void Explose(float damage)
    {
        if (currentHealth + Armour - damage <= 0)
        {
            currentHealth = 0;
            Armour = 0;
            ator.SetZombieMove(UnitAnimator.ZombieStateType.BombDie);
            Dead();
        }
        else
            TakeDamage(damage);
    }

    public virtual void GetBite(float damage)
    {
        if (currentHealth + Armour - damage <= 0)
        {
            currentHealth = 0;
            Armour = 0;

            InstantDead();
        }
        else
            TakeDamage(damage);
    }


    public virtual void ResetDebug(DebuffType type)
    {
        switch(type)
        {
            case DebuffType.Slow:
                sr.color = Color.white;
                dictDebuff[type] = 0f;
                break;

            case DebuffType.Burn:
                sr.color = Color.white;
                dictDebuff[type] = 0f;
                break;

            case DebuffType.Bleed:
                sr.color = Color.white;
                dictDebuff[type] = 0f;
                break;
        }
    }

    public override void Dead()
    {
        agent.Stop();
        col.enabled = false;
        DOVirtual.DelayedCall(1f, () =>
        {
            transform.position = PoolPosition;
            nodesPath[currentNodeIndex].RemoveUnit(this);
            nodesPath.Clear();
            OnZombieDie?.Invoke();
        }).SetAutoKill();
    }

    public virtual void InstantDead()
    {
        agent.Stop();
        col.enabled = false;
        transform.position = PoolPosition;
        nodesPath[currentNodeIndex].RemoveUnit(this);
        nodesPath.Clear();
        OnZombieDie?.Invoke();
    }

}
