using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Zombie : IUnit
{
    [SerializeField] protected Agent agent = null;
    [SerializeField] protected CapsuleCollider col = null;

    protected int currentNodeIndex;
    protected float timer = 0;
    protected bool arried = true;
    public bool isDebuff = false;

    public System.Action OnZombieDie = null;
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


    public virtual void InitializeRow(int row)
    {
        nodesPath = OnGetPath?.Invoke(row);

        currentNodeIndex = GridPosition.y;
        agent.OnArried = Arried;
        arried = false;
        agent.Initialize(UnitSpeed, col.radius);
    }

    public virtual void Update()
    {
        if (!IsAlive)
            return;

        if (arried)
            return;

        if (CanAttack())
        {
            agent.Stop();
            timer += Time.deltaTime;
            if (timer >= UnitData.attributes[(int)Data.AttributeType.AAI].value)
            {
                Attack(nodesPath[currentNodeIndex].GetPlantFromNode());
                timer = 0;
            }

            return;
        }

        agent.SetDestination(nodesPath[currentNodeIndex].WorldPosition);
    }

    public virtual void Arried()
    {
        arried = true;

        this.DelayCall(GameUtilities.TimeToDestination(transform.position, nodesPath[currentNodeIndex].WorldPosition, UnitSpeed), () =>
        {
            ator.SetMove(UnitAnimator.MovementType.Move);
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
                    ator.SetTriggger("Attack");
                    OnZombieGetInHouse?.Invoke();
                    StopAllCoroutines();
                });

                return;
            }

            nodesPath[currentNodeIndex].AddUnit(this);

            arried = false;
        });
    }

    public virtual bool CanAttack()
    {
        return nodesPath[currentNodeIndex].HasPlant() 
            && Vector3.Distance(transform.position, nodesPath[currentNodeIndex].WorldPosition) 
            <= (col.radius /*+ GameConstant.NODE_LENGTH/2*/);
    }

    public virtual void Attack(IUnit target)
    {
        ator.Reset();
        ator.SetTriggger("Attack");
        this.DelayCall(UnitData.attributes[(int)Data.AttributeType.AAI].value, () =>
        {
            target.TakeDamage(UnitData.attributes[(int)Data.AttributeType.ATK].value);
        });
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
