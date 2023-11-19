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
        //Vector3 plantWorldPosition = new Vector3(node.WorldPosition.x, node.WorldPosition.y, node.WorldPosition.z);
        //transform.position = plantWorldPosition;
        transform.position = node.WorldPosition;
        ator.transform.eulerAngles = Helper.Cam.transform.eulerAngles;
        node.AddUnit(this);
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
