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

    protected virtual void Explose()
    {

    }

}
