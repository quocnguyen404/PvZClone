using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public abstract class IUnit : MonoBehaviour
{
    [SerializeField] protected UnitAnimator ator = null;

    [SerializeField]
    protected List<Node> nodesPath = null;

    public string Name
    {
        get
        {
            return UnitData.unitName;
        }
    }

    public Data.UnitData UnitData = null;
    public Vector2Int GridPosition;
    public bool IsOnNode
    {
        get
        {
            return nodesPath.Count > 0;
        }
    }


    public System.Func<int, List<Node>> OnGetPath = null;
    public System.Func<Data.UnitData, IProduct> OnGetProduct = null;

    protected float maxHealth => UnitData.attributes[(int)Data.AttributeType.HP].value;
    protected float currentHealth;

    public bool IsAlive => currentHealth > 0;

    public virtual void Initialize(Vector2Int pos)
    {
        currentHealth = maxHealth;
        GridPosition = pos;
    }

    public virtual void PlaceUnitOnNode(Node node)
    {
        transform.position = node.WorldPosition;
        node.AddUnit(this);
    }

    public virtual void TakeDamage(float damge)
    {
        currentHealth -= damge;

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        //return to pool
        nodesPath[GridPosition.y].RemoveUnit(this);
        transform.position = Vector3.zero;
        nodesPath.Clear();
    }
}
