using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebuff
{
    public float DebuffValue
    {
        get;
        set;
    }

    public void IDebuff(IUnit unit);
}
