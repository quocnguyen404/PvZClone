using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : IUnit
{
    public virtual void InitializeNodePath()
    {
        nodePaths = new List<Node>(OnGetPath?.Invoke(GridPosition.x));
    }

    public override void Initialize(Vector2Int pos)
    {
        base.Initialize(pos);
        ator.PInitialize();
    }

    public override void PlaceUnitOnNode(Node node)
    {
        //Vector3 plantWorldPosition = new Vector3(node.WorldPosition.x, node.WorldPosition.y, node.WorldPosition.z);
        //transform.position = plantWorldPosition;
        transform.position = node.WorldPosition;
        ator.transform.eulerAngles = Helper.Cam.transform.eulerAngles;
        node.AddUnit(this);
        Initialize(node.GridPosition);
        InitializeNodePath();
    }

    public override void Dead()
    {
        nodePaths.Find(n => n.GridPosition == GridPosition).RemoveUnit(this);
        nodePaths.Clear();
        transform.position = PoolPosition;
        gameObject.SetActive(false);
    }
}
