using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : IUnit
{
    [SerializeField] protected List<Node> nodesPath;


    public virtual void InitializeRow()
    {
        nodesPath = OnGetPath?.Invoke(GridPosition.x);
    }
}
