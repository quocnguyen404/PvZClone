using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IExplosePlant : Plant
{
    public override void InitializeRange()
    {
        nodesPath = new List<Node>(OnGetArea?.Invoke(GridPosition.x, GridPosition.y));
    }

    public override void PlaceUnitOnNode(Node node)
    {
        base.PlaceUnitOnNode(node);
    }

    protected virtual void Explose()
    {

    }

    public override void Dead()
    {
        base.Dead();
        nodesPath.Find(n => n.GridPosition == GridPosition).RemoveUnit(this);
    }
}
