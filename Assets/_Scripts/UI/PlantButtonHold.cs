using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantButtonHold : MonoBehaviour
{
    [SerializeField] private Transform content = null;
    public List<Slot> slots;

    public System.Action<Slot> OnSelectedButton = null;

    public void Initialize()
    {
        slots = new List<Slot>();

        foreach (Transform slot in content)
            slots.Add(slot.gameObject.GetComponent<Slot>());
    }

    public void AddToHoldPanel(UnitButton buttonClicked)
    {
        Slot emptySlot = GetEmptySlot();

        if (emptySlot == null)
            return;

        emptySlot.Initialize(buttonClicked.unitData);
    }

    public Slot GetEmptySlot()
    {
        Slot emptySlot = slots.Find(s => s.unitData == null);

        return emptySlot;
    }
}
