using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUnit : MonoBehaviour
{
    public string _name;
    public int cost => unitData.cost;

    public Data.UnitData unitData = null;

    public virtual void Attack(IUnit target)
    {

    }

    public virtual void TakeDamage(float damge)
    {

    }
}
