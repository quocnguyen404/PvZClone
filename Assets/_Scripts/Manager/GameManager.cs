using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [Header("Component Reference")]
    [SerializeField] private PickUnitManager pickUnitManager = null;
    [SerializeField] private PlacementManager placementManager = null;
    [SerializeField] private GridManager gridManager = null;
    [SerializeField] private PhaseManager phaseManager = null;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private RewardManager rewardManager = null;

    [Space]
    [Header("Object Pooling Reference")]
    [SerializeField] private PlantObjectPool plantObjectPool = null;
    [SerializeField] private ZombieObjectPool zombieObjectPool = null;
    [SerializeField] private ProductObjectPool productObjectPool = null;

    [Space]
    [Header("Unit Manager Reference")]
    [SerializeField] private PlantManager plantManager = null;
    [SerializeField] private ZombieManager zombieManager = null;
    [SerializeField] private SunManager sunManager = null;
    [SerializeField] private CarManager carManager = null;


    public static bool IsEndGame { get; private set; }
    public static bool IsStartGame { get; private set; }

    private void Awake()
    {
        //Config camera field of view by ratio

        if (Screen.width <= 2300)
            Helper.Cam.fieldOfView = 53;

        if (Screen.width > 2300)
            Helper.Cam.fieldOfView = 38;


        IsEndGame = false;
        IsStartGame = false;

        uiManager.Initialize();
        uiManager.BeginMatch();
        StartPickPlant();

        AudioManager.Instance.StopMusic();
    }

    private void StartPickPlant()
    {
        gridManager.Initialize(GameConstant.GARDEN_ROW,
                               GameConstant.GARDEN_COLOUMN + GameConstant.ZOMBIE_COLUMN,
                               GameConstant.NODE_LENGTH);

        uiManager.PlayButton.AddListener(StartGame);

        placementManager.OnPlaceUnit = plantManager.AddUnit;
        zombieObjectPool.OnSpawnUnit = zombieManager.AddUnit;
        productObjectPool.OnSpawnProduct = sunManager.AddProduct;
        zombieManager.OnGetZombie = zombieObjectPool.GetZombie;
        zombieManager.OnZombieMoveToHouse = uiManager.ZombieMoveToHouseTransition;
        zombieManager.OnZombieGetPath = gridManager.GetRow;
        plantManager.PoolTransform = plantObjectPool.transform;
        plantManager.OnPlantGetNode = gridManager.GetNode;
        plantManager.OnPlantGetPath = gridManager.GetRow;
        plantManager.OnPlantGetArea = gridManager.GetArea;
        carManager.OnGetColumn = gridManager.GetColumn;
        carManager.OnGetRow = gridManager.GetRow;
        pickUnitManager.OnGetPlant = plantObjectPool.GetPlant;

        zombieManager.gridManager = gridManager;
        zombieManager.Initialize();
        plantManager.Initialize();
        zombieObjectPool.InitializePool();
        carManager.Initialize(GameConstant.GARDEN_ROW);
        rewardManager.Initialize();
    }

    private void StartGame()
    {
        if (!pickUnitManager.PickFull())
        {
            //player pick plan not done
            return;
        }

        IsStartGame = true;

        uiManager.StartGameTransition();

        pickUnitManager.OnPickedUnit = placementManager.GetSelectedUnitData;
        sunManager.OnSunClick = placementManager.PickUpSun;

        pickUnitManager.InitializeUnitData();
        placementManager.Initialize();
        plantObjectPool.InitilizePool(pickUnitManager.PlantData());
        productObjectPool.InitializePool(pickUnitManager.PlantData());

        uiManager.PlayButton.RemoveAllListener();
        uiManager.PlayButton.SetVisuability(false);

        placementManager.OnGetPosition = gridManager.GetRandomPosition;
        plantManager.OnUnitGetProduct = productObjectPool.GetProduct;
        placementManager.OnGetSun = productObjectPool.GetProduct;
        zombieManager.OnZombieDie = phaseManager.ZombieDie;
        phaseManager.OnZombieDispatcher = zombieManager.DispatcherZombie;

        phaseManager.OnWin = Win;
        zombieManager.OnZombieWin = Lose;
        rewardManager.OnGiftClick = uiManager.WinTransition;

        ZombieStart();
    }

    private void ZombieStart()
    {
        DOVirtual.DelayedCall(GameConstant.TIME_START_MATCH, () => { phaseManager.StartLevel(); }).SetAutoKill();
    }

    private void Win(Vector3 tossPos)
    {
        rewardManager.TossGift(tossPos);
        ConfigHelper.LevelUp();
        EndGame();
    }

    private void Lose()
    {
        uiManager.LoseTransition();
        EndGame();
    }

    private void EndGame()
    {
        IsStartGame = false;
        IsEndGame = true;
        AudioManager.Instance.StopMusic();
        ConfigHelper.SaveUserData();
    }

    public static void SetEndGame()
    {
        IsEndGame = true;
    }

    private void OnDestroy()
    {
        IsEndGame = false;
        IsStartGame = false;
    }
}
