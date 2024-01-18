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

    private Tween deadTween = null;
    protected override void Explose()
    {
        currentHealth = Mathf.Infinity;
        col.enabled = true;

        if (deadTween != null)
            deadTween.Kill();

        ator.SetPlantMove(UnitAnimator.PlantStateType.Ready);

        AudioManager.Instance.PlaySound(Sound.CherryBomb);
        Dead();
    }

    public override void Update()
    {
        base.Update();

        if (IsEndGame)
            return;
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
        deadTween = DOVirtual.DelayedCall(UnitData.attributes[(int)Data.AttributeType.AAI].value, () =>
        {
            base.Dead();
            col.enabled = false;

        }).SetAutoKill();
    }
}
