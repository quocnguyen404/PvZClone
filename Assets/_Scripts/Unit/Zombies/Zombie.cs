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
    protected float bleedTimer = 0;
    protected bool arried = true;

    public override bool IsAlive => currentHealth > 0 || Armour > 0;
    public virtual bool IsInitialize => nodePaths.Count > 0;


    #region Event
    public System.Action<Zombie> OnZombieDie = null;
    public System.Action OnZombieGetInHouse = null;
    public System.Action OnZombieMoveToHouse = null;
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
    public bool IsDebuff => currentDebuffs.Count > 0;
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
        nodePaths = OnGetPath?.Invoke(row);

        ator.ZInitialize();
        ator.OnAgentStop = agent.Dead;

        transform.eulerAngles = Helper.Cam.transform.eulerAngles;
        UnitSpeed = maxUnitSpeed;
        Armour = maxArmour;
        col.enabled = true;

        attackTimer = UnitData.attributes[(int)Data.AttributeType.AAI].value;
        currentNodeIndex = GridPosition.y;

        agent.Initialize(UnitSpeed, col.radius);
        agent.OnArried = Arried;
        agent.OnMoveAnimation = () =>
        {
            if (currentHealth > 0)
                ator.SetZombieMove(UnitAnimator.ZombieStateType.Walk);
            else if (Armour > 0)
                ator.SetZombieLostHead();
        };
        arried = false;
    }

    public override void Update()
    {
        base.Update();

        if (GameManager.IsEndGame)
            return;

        if (!IsAlive)
            return;

        if (currentHealth <= 0 && bleedTimer >= 1f)
        {
            TakeDamage(10f);
            bleedTimer = 0f;
        }

        bleedTimer += Time.deltaTime;

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
                Attack(nodePaths[currentNodeIndex].GetPlantFromNode());
                attackTimer = 0;
            }

            return;
        }

        agent.SetDestination(new Vector3(nodePaths[currentNodeIndex].WorldPosition.x - GameConstant.NODE_LENGTH / 2f,
                                         nodePaths[currentNodeIndex].WorldPosition.y,
                                         nodePaths[currentNodeIndex].WorldPosition.z));
    }

    protected virtual void Arried()
    {
        arried = true;

        if (currentHealth < 0)
            return;

        nodePaths[currentNodeIndex].RemoveUnit(this);
        currentNodeIndex--;

        //Zombie go out
        if (currentNodeIndex < 0)
        {
            Vector3 housePos = (Vector3)OnGetHousePosition?.Invoke();

            if (GameManager.IsEndGame)
                return;
            
            if (nodePaths[0].WorldPosition.x >= transform.position.x)
                OnZombieMoveToHouse?.Invoke();

            agent.OnArried = () =>
            {
                ator.SetZombieMove(UnitAnimator.ZombieStateType.Attack);
                AudioManager.Instance.PlaySound(Sound.ZombieEating);
                OnZombieGetInHouse?.Invoke();
                StopAllCoroutines();
            };

            agent.SetDestination(housePos);
            arried = true;

            return;
        }

        nodePaths[currentNodeIndex].AddUnit(this);

        Groan();

        arried = false;
    }

    public virtual void Groan()
    {
        GameUtilities.RandomBehaviour(() =>
        {
            Sound sound = GameUtilities.RandomSound(Sound.ZombieGroan, Sound.ZombieGroan2);

            AudioClip clip = OnGetSound?.Invoke(sound);

            if (clip == null)
                return;

            audioSource.PlayOneShot(clip, 1);
        });
    }

    public virtual bool CanAttack()
    {
        return currentHealth > 0
            && nodePaths[currentNodeIndex].HasPlant()
            && nodePaths[currentNodeIndex].GetPlantFromNode().IsAlive
            && Vector3.Distance(transform.position, nodePaths[currentNodeIndex].WorldPosition) <= (col.radius);
    }

    public virtual void Attack(IUnit target)
    {
        if (!target.IsAlive)
        {
            AudioManager.Instance.PlaySound(Sound.ZombieEat);
            return;
        }

        AudioManager.Instance.PlaySound(Sound.ZombieEating);
        ator.SetZombieMove(UnitAnimator.ZombieStateType.Attack);
        target.TakeDamage(UnitData.attributes[(int)Data.AttributeType.ATK].value);

        //if (target.IsAlive)
        //    audioSource.PlayOneShot(OnGetSound?.Invoke(Sound.ZombieEat));
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

                if (UnitSpeed >= maxUnitSpeed + debuffValue)
                    return;

                AudioManager.Instance.PlaySound(Sound.Frozen);
                sr.color = Color.blue;
                UpdateZombieSpeed(UnitSpeed += debuffValue);
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

    bool first = true;
    public override void TakeDamage(float damage)
    {

        if (!IsAlive)
            return;

        if (currentHealth > 0)
            currentHealth -= damage;
        else if (currentHealth <= 0)
            Armour -= damage;

        if (currentHealth <= 0 && first)
        {
            ator.SetZombieLostHead();
            AudioManager.Instance.PlaySound(Sound.HeadFall);
            first = false;
        }

        if (currentHealth <= 0 && Armour <= 0)
        {
            ator.SetZombieMove(UnitAnimator.ZombieStateType.Die);
            AudioManager.Instance.PlaySound(Sound.ZombieFall);
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
            InstantDead();
        }
        else
            TakeDamage(damage);
    }

    public virtual void UpdateZombieSpeed(float speed)
    {
        UnitSpeed = speed;
        agent.ChangeSpeed(speed);
    }

    public virtual void ResetDebug(DebuffType type)
    {
        switch (type)
        {
            case DebuffType.Slow:
                sr.color = Color.white;
                dictDebuff[type] = 0f;
                UpdateZombieSpeed(maxUnitSpeed);
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

    private Tween deadTween = null;
    public override void Dead()
    {
        agent.Stop();
        agent.TurnOff();
        col.enabled = false;

        if (deadTween != null)
            deadTween.Kill();

        deadTween = DOVirtual.DelayedCall(GameConstant.TIME_ZOMBIE_DIE, () =>
        {
            DeadAction();
        }).SetAutoKill();
    }

    public virtual void InstantDead()
    {
        agent.Stop();
        agent.TurnOff();
        DeadAction();
    }

    public virtual void DeadAction()
    {
        foreach (DebuffType type in currentDebuffs)
            ResetDebug(type);

        currentHealth = 0;
        Armour = 0;
        
        OnZombieDie?.Invoke(this);

        ator.OnTurnOff();
        agent.TurnOff();
        col.enabled = false;
        transform.position = PoolPosition;
        arried = true;

        deadTween.Kill();

        if (currentNodeIndex >= 0)
        {
            try
            {
                nodePaths[currentNodeIndex].RemoveUnit(this);
            }
            catch
            {
                Debug.Log("nodePaths have been delete");
            }
        }

        nodePaths.Clear();
    }


    private void OnDrawGizmos()
    {
        if (currentNodeIndex <= 0 || currentNodeIndex >= nodePaths.Count)
            return;

        Gizmos.color = Color.black;

        Gizmos.DrawLine(transform.position, new Vector3(nodePaths[currentNodeIndex].WorldPosition.x - GameConstant.NODE_LENGTH / 2f,
                                         nodePaths[currentNodeIndex].WorldPosition.y,
                                         nodePaths[currentNodeIndex].WorldPosition.z));
    }
}
