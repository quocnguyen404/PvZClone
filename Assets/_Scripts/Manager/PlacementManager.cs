using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    [Header("Component Reference")]
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private SunInGameManager sunInGameManager = null;
    [SerializeField] private Transform dropSunPosition = null;
    [SerializeField] private ShovelToggle shovelToggle = null;
    [SerializeField] private MouseIndicator mouseIndicator = null;

    public System.Action<IUnit> OnPlaceUnit = null;
    public System.Func<Data.UnitData, IProduct> OnGetSun = null;
    public System.Func<Vector3> OnGetPosition = null;

    private bool startPlacing = false;
    private IUnit selectedUnit = null;
    private UnitButton selectedButton = null;

    private Sun sunPre = null;
    private float timer = 0;

    private void Awake()
    {
        mouseIndicator.transform.eulerAngles = Helper.Cam.transform.eulerAngles;
    }

    public void Initialize()
    {
        startPlacing = true;
        sunInGameManager.Initialize();
        shovelToggle.OnPickShovel = PickUpShovel;
    }

    private void Update()
    {
        if (!startPlacing)
            return;

        timer += Time.deltaTime;

        if (timer >= GameConstant.TIME_SUN_DROP && !GameManager.IsEndGame)
        {
            DropSun();
            timer = 0;
        }

        if (shovelToggle.IsPickShovel && Input.GetMouseButtonDown(0))
        {
            Node sNode = inputManager.GetSelectedNode();

            if (sNode != null && sNode.HasPlant())
                sNode.GetPlantFromNode().Dead();

            shovelToggle.UnPickShovel();
        }

        if (inputManager.IsOverPlane() && selectedUnit != null)
        {
            TurnOnMouseIndicator();
            mouseIndicator.transform.position = inputManager.GetSelectedNodePosition();
        }
        else
            TurnOffMouseIndicator();

        if (Input.GetMouseButtonDown(0) && selectedUnit != null)
        {
            Node selectedNode = inputManager.GetSelectedNode();

            if (selectedNode == null)
            {
                TurnOffMouseIndicator();
                mouseIndicator.ChangeMouseIndicatorSprite(null);

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

        if (node.GridPosition.y > GameConstant.GARDEN_COLOUMN - 1)
            return;

        AudioManager.Instance.PlaySound(Sound.Plant);
        OnPlaceUnit?.Invoke(selectedUnit);
        selectedUnit.PlaceUnitOnNode(node);
        sunInGameManager.BuyPlant(selectedUnit.UnitData);
        selectedButton.Recharge(selectedUnit.UnitData.rechargeTime);

        TurnOffMouseIndicator();
        mouseIndicator.ChangeMouseIndicatorSprite(null);

        selectedUnit = null;
        selectedButton = null;
    }

    public void GetSelectedUnitData(IUnit unit, UnitButton unitButton)
    {
        if (!sunInGameManager.CanBuy(unit.UnitData))
        {
            AudioManager.Instance.PlaySound(Sound.CantBuy);
            unit.gameObject.SetActive(false);
            return;
        }


        AudioManager.Instance.PlaySound(Sound.PickPlant);
        mouseIndicator.ChangeMouseIndicatorSprite(unitButton.GetUnitSprite());
        selectedUnit = unit;
        selectedButton = unitButton;
    }

    public void PickUpSun(Sun sun)
    {
        sunInGameManager.PickSunUp(sun.value);
        sun.MoveToSunBar(sunInGameManager.GetSunBarPosition());
    }

    public void PickUpShovel(bool value)
    {
        if (value)
        {
            Sprite s = shovelToggle.GetShovelSprite();
            mouseIndicator.ChangeMouseIndicatorSprite(s);
        }
        else
        {
            mouseIndicator.ChangeMouseIndicatorSprite(null);
            TurnOffMouseIndicator();
        }
    }

    private void DropSun()
    {
        sunPre = (Sun)OnGetSun?.Invoke(ConfigHelper.GameConfig.plants["Sunflower"]);

        Vector3 target = (Vector3)OnGetPosition?.Invoke();

        Vector3 initPos = new Vector3(target.x, dropSunPosition.position.y + 0.1f, 0f);

        sunPre.Initialize(initPos, GameConstant.SUN_DROP_COST);
        sunPre.Fall();

        sunPre = null;
    }

    private void TurnOnMouseIndicator()
    {
        mouseIndicator.gameObject.SetActive(true);
        Cursor.visible = false;
    }

    private void TurnOffMouseIndicator()
    {
        mouseIndicator.gameObject.SetActive(false);
        Cursor.visible = true;
    }
}
