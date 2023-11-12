using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantButtonHold : MonoBehaviour
{
    [SerializeField] private Transform content = null;
    public List<Slot> slots;
    public List<Data.UnitData> unitDatas;

    public System.Action<UnitButton> OnUnitButtonClick;

    public void Initialize()
    {
        //foreach (Slot slot in slots)
        //    slot.gameObject.SetActive(true);
    }

    public void AddToHoldPanel(UnitButton unitButton)
    {
        Slot emptySlot = GetEmptySlot();

        if (emptySlot == null)
            return;

        emptySlot.GetUnitButton(unitButton.unitData);
        unitButton.slotOnHold = emptySlot;

        unitButton.transform.parent = emptySlot.transform;
        unitButton.transform.localPosition = Vector3.zero;

        unitButton.OnUnitButtonClick = UnitButtonClick;
    }

    public void UnitButtonClick(UnitButton unitButton)
    {
        OnUnitButtonClick.Invoke(unitButton);
    }

    public Slot GetEmptySlot()
    {
        Slot emptySlot = slots.Find(s => s.unitData == null);

        return emptySlot;
    }

    public void InitializeUnitData()
    {
        unitDatas = new List<Data.UnitData>();

        foreach (Slot slot in slots)
        {
            unitDatas.Add(slot.unitData);
        }
    }
}
