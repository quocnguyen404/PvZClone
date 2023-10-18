using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Data.UnitData unitData
    {
        get; private set;
    }

    public void Initialize(Data.UnitData unitData)
    {
        this.unitData = unitData;
    }
}
