using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : IUnit
{
    public virtual void InitializeRow()
    {
        nodesPath = new List<Node>(OnGetPath?.Invoke(GridPosition.x));
    }

    public override void PlaceUnitOnNode(Node node)
    {
        base.PlaceUnitOnNode(node);
        Initialize(node.GridPosition);
    }

    public override void Dead()
    {
        base.Dead();
        gameObject.SetActive(false);
    }
}
