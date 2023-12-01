using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ElementData
{
    public int amount;
    public string name;
}

public class PhaseData
{
    public int phaseIndex;
    public float timeBetweenSpawn;
    public List<ElementData> zombies;
}
