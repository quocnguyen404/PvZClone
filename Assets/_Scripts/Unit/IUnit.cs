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

    public Data.UnitData UnitData = null;
    public Vector2Int GridPosition;
    public bool isOnNode = false;

    public System.Func<int, List<Node>> OnGetPath = null;
    public System.Func<Data.UnitData, IProduct> OnGetProduct = null;

    public virtual void PlaceUnitOnNode(Node node)
    {
        transform.position = node.WorldPosition;
        node.units.Add(this);
        GridPosition = node.GridPosition;
        isOnNode = true;
    }

    public virtual void TakeDamage(float damge)
    {

    }

    public virtual void Dead()
    {
        //return to pool
    }
}
