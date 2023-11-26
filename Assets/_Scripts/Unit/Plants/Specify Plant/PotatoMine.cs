using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        DOVirtual.DelayedCall(UnitData.attributes[(int)Data.AttributeType.AAI].value, () =>
        {
            Growth();
        }).SetAutoKill();
    }

    private void Growth()
    {
        //do growth anim
        Debug.Log("Growth");
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

            DOVirtual.DelayedCall(1f, () => { Dead(); }).SetAutoKill();
        }
    }

    public override void Dead()
    {

        base.Dead();
    }
}
