using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public abstract class IUnit : MonoBehaviour
{
    [SerializeField] protected UnitAnimator animator = null;
    public string Name
    {
        get
        {
            return UnitData.unitName;
        }
    }

    public int Cost => UnitData.cost;
    public Data.UnitData UnitData = null;
    public Vector2Int GridPosition;

    public System.Func<int, List<Node>> OnGetPath = null;
    public System.Func<IProduct> OnGetProduct = null;

    public virtual void PlaceUnitOnNode(Node node)
    {
        transform.position = node.WorldPosition;
        node.unit = this;
        GridPosition = node.GridPosition;
    }

    public virtual void TakeDamage(float damge)
    {

    }

    public virtual void Dead()
    {
        //return to pool
    }
}
