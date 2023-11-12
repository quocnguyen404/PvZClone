using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMine : IExplosePlant
{
    [SerializeField] private Collider col = null;
    public override void InitializeRange()
    {
        Node node = OnGetNode?.Invoke(GridPosition.x, GridPosition.y);
        nodesPath = new List<Node>();
        nodesPath.Add(node);
    }

    public override void PlaceUnitOnNode(Node node)
    {
        base.PlaceUnitOnNode(node);
        CountDown();
    }

    private void CountDown()
    {
        this.DelayCall(UnitData.attributes[(int)Data.AttributeType.AAI].value, () => 
        {
            Growth();
        });
    }

    private void Growth()
    {
        //do growth anim
        Debug.Log("Growth");
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Zombie zombie = other.GetComponent<Zombie>();

        if (zombie != null)
        {
            zombie.TakeDamage(UnitData.attributes[(int)Data.AttributeType.ATK].value);
            Dead();
        }
    }

    public override void Dead()
    {
        base.Dead();
        col.enabled = false;
    }
}
