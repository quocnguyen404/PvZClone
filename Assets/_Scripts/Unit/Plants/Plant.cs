using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : IUnit
{
    public virtual void InitializeRow()
    {
        nodesPath = OnGetPath?.Invoke(GridPosition.x);
    }

    public override void PlaceUnitOnNode(Node node)
    {
        base.PlaceUnitOnNode(node);
        Initialize();
    }

    public override void Dead()
    {
        base.Dead();
        gameObject.SetActive(false);
        nodesPath[GridPosition.y].RemoveUnit(this);
        transform.position = Vector3.zero;
        nodesPath.Clear();
    }
}
