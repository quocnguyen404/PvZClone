using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PotatoMine : IExplosePlant
{
    [SerializeField] private Collider col = null;
    public override void InitializeNodePath()
    {
        Node node = OnGetNode?.Invoke(GridPosition.x, GridPosition.y);
        nodePaths = new List<Node>();
        nodePaths.Add(node);
    }

    public override void PlaceUnitOnNode(Node node)
    {
        base.PlaceUnitOnNode(node);
        CountDown();
    }

    private void CountDown()
    {
        DOVirtual.DelayedCall(UnitData.attributes[(int)Data.AttributeType.AAI].value, () =>
        {
            Growth();
        }).SetAutoKill();
    }

    private void Growth()
    {
        //do growth anim
        AudioManager.Instance.PlaySound(Sound.PotatoGrow);
        ator.SetPlantMove(UnitAnimator.PlantStateType.Idle1);
        DOVirtual.DelayedCall(1f, () => { col.enabled = true; }).SetAutoKill();
    }

    private void OnTriggerEnter(Collider other)
    {
        Zombie zombie = other.GetComponent<Zombie>();

        if (zombie != null)
        {
            currentHealth = Mathf.Infinity;
            zombie.Explose(UnitData.attributes[(int)Data.AttributeType.ATK].value);
            ator.SetPlantMove(UnitAnimator.PlantStateType.Attack);
            col.enabled = false;

            AudioManager.Instance.PlaySound(Sound.PotateBoom);

            DOVirtual.DelayedCall(1f, () => { Dead(); }).SetAutoKill();
        }
    }

    public override void Dead()
    {

        base.Dead();
    }
}
