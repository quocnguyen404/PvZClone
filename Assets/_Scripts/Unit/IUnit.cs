using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUnit : MonoBehaviour
{
    public string _name;

    private Data.UnitData unitData = null;

    public Data.UnitData UnitData
    {
        get
        {
            if (unitData == null)
            {

            }

            return unitData;
        }
    }

    public virtual void Attack(IUnit target)
    {

    }

    public virtual void TakeDamage(float damge)
    {

    }
}
