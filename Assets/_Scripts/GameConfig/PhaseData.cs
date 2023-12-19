using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BatchComparer : IComparer<Batch>
{
    public int Compare(Batch batch, Batch other)
    {
        if (other == null)
            return -1;

        int nameCompare = string.Compare(batch.name, other.name);

        if (nameCompare != 0)
            return nameCompare;
        else
            return -batch.amount.CompareTo(other.amount);
    }
}

public class Batch
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
