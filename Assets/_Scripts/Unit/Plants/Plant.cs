using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : IUnit
{
    public virtual void InitializeRange()
    {
        nodesPath = new List<Node>(OnGetPath?.Invoke(GridPosition.x));
    }

    public override void Update()
    {
        if (!IsOnNode)
            return;
    }

    public override void PlaceUnitOnNode(Node node)
    {
        base.PlaceUnitOnNode(node);
        Initialize(node.GridPosition);
        InitializeRange();
    }

    public override void Dead()
    {
        transform.position = PoolPosition;
        gameObject.SetActive(false);
        nodesPath.Find(n => n.GridPosition == GridPosition).RemoveUnit(this);
    }
}
