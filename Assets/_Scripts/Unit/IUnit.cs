using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public abstract class IUnit : MonoBehaviour
{
    [SerializeField] protected UnitAnimator animator = null;
    [SerializeField] protected List<Node> nodesPath = null;

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

    private float maxHealth => UnitData.attributes[(int)Data.AttributeType.HP].value;
    private float currentHealth;

    public bool IsAlive => currentHealth > 0;

    public virtual void Initialize()
    {
        currentHealth = maxHealth;
    }

    public virtual void PlaceUnitOnNode(Node node)
    {
        transform.position = node.WorldPosition;
        node.AddUnit(this);
        GridPosition = node.GridPosition;
    }

    public virtual void TakeDamage(float damge)
    {
        Debug.Log(UnitData.unitName + " is take damage");
        currentHealth -= damge;

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        //return to pool
        Debug.Log(UnitData.unitName + " is die");
    }
}
