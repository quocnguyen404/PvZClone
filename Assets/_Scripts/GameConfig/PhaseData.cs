using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Batch
{
    public int amount;
    public string name;
    public float timeCallNext;
}

public class PhaseData
{
    public int phaseIndex;
    public float timeBetweenSpawn;
    public List<Batch> batchs;
}
