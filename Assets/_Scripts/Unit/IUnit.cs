using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public abstract class IUnit : MonoBehaviour
{
    public string Name;
    public int Cost => UnitData.cost;
    public Data.UnitData UnitData = null;

    public virtual void PlaceUnitOnNode(Node node)
    {
        transform.position = node.WorldPosition;
        node.unit = this;
    }

    public virtual void TakeDamage(float damge)
    {

    }

    public virtual void Dead()
    {
        //return to pool
    }
}
