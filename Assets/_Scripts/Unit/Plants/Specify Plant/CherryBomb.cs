using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBomb : IExplosePlant
{
    [SerializeField] private Collider col = null;

    public override void PlaceUnitOnNode(Node node)
    {
        base.PlaceUnitOnNode(node);
        Explose();
    }

    protected override void Explose()
    {
        this.DelayCall(0.25f, () =>
        {
            col.enabled = true;

            this.DelayCall(UnitData.attributes[(int)Data.AttributeType.AAI].value, () => { Dead(); });
        });
    }

    protected void OnTriggerEnter(Collider other)
    {
        Zombie zombie = other.GetComponent<Zombie>();

        if (zombie != null)
        {
            zombie.TakeDamage(UnitData.attributes[(int)Data.AttributeType.ATK].value);
        }
    }

    public override void Dead()
    {
        base.Dead();
        col.enabled = false;
    }
}
