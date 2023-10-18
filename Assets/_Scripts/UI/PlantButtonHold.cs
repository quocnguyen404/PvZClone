using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantButtonHold : MonoBehaviour
{
    [SerializeField] private Transform content = null;
    public List<Slot> slots;

    public void Initialize()
    {
        slots = new List<Slot>();

        foreach (Transform slot in content)
            slots.Add(slot.gameObject.GetComponent<Slot>());
    }

    public Slot GetEmptySlot()
    {
        Slot emptySlot = slots.Find(s => s.isEmpty);

        return emptySlot;
    }
}
