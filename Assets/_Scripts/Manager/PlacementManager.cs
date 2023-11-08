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
    [SerializeField] private Transform dropSunPosition = null;

    public System.Action<IUnit> OnPlaceUnit = null;
    public System.Func<Data.UnitData, IProduct> OnGetSun = null;
    public System.Func<Vector3> OnGetPosition = null;

    private bool startPlacing = false;
    private IUnit selectedUnit = null;
    private UnitButton selectedButton = null;

    private Sun sunPre = null;
    private float timer = 0;



    public void Initialize()
    {
        startPlacing = true;
        currencyManager.Initialize();
    }

    private void Update()
    {
        if (!startPlacing)
            return;

        timer += Time.deltaTime;

        if (timer >= GameConstant.TIME_SUN_DROP)
        {
            DropSun();
            timer = 0;
        }


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
        selectedButton.Recharge(selectedUnit.UnitData.rechargeTime);

        selectedUnit = null;
        selectedButton = null;
    }

    public void GetSelectedUnitData(IUnit unit, UnitButton unitButton)
    {
        if (!currencyManager.CanBuy(unit.UnitData))
        {
            unit.gameObject.SetActive(false);
            return;
        }

        selectedUnit = unit;
        selectedButton = unitButton;
    }

    public void PickUpSun(Sun sun)
    {
        currencyManager.PickSunUp(sun.value);
        sun.MoveToSunBar(currencyManager.GetSunBarPosition());
    }

    private void DropSun()
    {
        sunPre = (Sun)OnGetSun?.Invoke(ConfigHelper.GameConfig.plants["Sunflower"]);

        Vector3 target = (Vector3)OnGetPosition?.Invoke();

        Vector3 initPos = new Vector3(target.x, dropSunPosition.position.y, target.z);

        sunPre.Initialize(initPos, GameConstant.SUN_DROP_COST);
        sunPre.Fall(target);

        sunPre = null;
    }
}
