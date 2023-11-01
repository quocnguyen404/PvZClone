using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    [Header("Component Reference")]
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private CurrencyManager currencyManager = null;
    [SerializeField] private Button shovelButton = null;

    public System.Action<IUnit> OnPlaceUnit = null;

    private bool startPlacing = false;
    public IUnit selectedUnit = null;

    public void Initialize()
    {
        startPlacing = true;
        currencyManager.Initialize();
    }

    private void Update()
    {
        if (!startPlacing)
            return;

        if (Input.GetMouseButtonDown(0) && selectedUnit != null)
        {
            Node selectedNode = inputManager.GetSelectedNode();

            if (selectedNode == null)
            {
                selectedUnit.gameObject.SetActive(false);
                selectedUnit = null;
                return;
            }

            PlaceUnitOnNode(selectedNode);
        }
    }

    public void PlaceUnitOnNode(Node node)
    {
        if (node.HasPlant())
            return;

        selectedUnit.PlaceUnitOnNode(node);
        currencyManager.BuyPlant(selectedUnit.UnitData);
        OnPlaceUnit?.Invoke(selectedUnit);

        selectedUnit = null;
    }

    public void GetSelectedUnitData(IUnit unit)
    {
        if (!currencyManager.CanBuy(unit.UnitData))
        {
            unit.gameObject.SetActive(false);
            return;
        }

        selectedUnit = unit;
    }

    public void PickUpSun(Sun sun)
    {
        currencyManager.PickSunUp(sun.value);
        sun.MoveToSunBar(currencyManager.GetSunBarPosition());
    }
}
