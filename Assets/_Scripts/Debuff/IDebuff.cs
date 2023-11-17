using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebuff
{
    public DebuffType Type { get; set; }
    public float DebuffDuration
    {
        get;
        set;
    }

    public float DebuffValue
    {
        get;
        set;
    }

    public void IDebuff(IUnit unit);
}
