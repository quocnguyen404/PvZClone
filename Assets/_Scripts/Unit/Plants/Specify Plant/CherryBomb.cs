using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CherryBomb : IExplosePlant
{
    [SerializeField] private Collider col = null;

    public override void PlaceUnitOnNode(Node node)
    {
        base.PlaceUnitOnNode(node);
        ator.SetPlantMove(UnitAnimator.PlantStateType.Attack, Explose);
    }

    protected override void Explose()
    {
        currentHealth = Mathf.Infinity;
        col.enabled = true;

        Dead();
    }

    protected void OnTriggerEnter(Collider other)
    {
        Zombie zombie = other.GetComponent<Zombie>();

        if (zombie != null)
        {
            zombie.Explose(UnitData.attributes[(int)Data.AttributeType.ATK].value);
        }
    }

    public override void Dead()
    {
        base.Dead();
        col.enabled = false;
    }
}
