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

    [SerializeField] [Range(1, 10)] int timeScale;

    public static bool IsEndGame { get; private set; }
    public static bool IsStartGame { get; private set; }

    private void Awake()
    {
        Time.timeScale = timeScale;
        IsEndGame = false;
        IsStartGame = false;

        uiManager.Initialize();
        uiManager.BeginMatch();
        StartPickPlant();
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
        plantObjectPool.InitilizePool(pickUnitManager.PlantDatas());
        productObjectPool.InitializePool(pickUnitManager.PlantDatas());

        uiManager.PlayButton.RemoveAllListener();
        uiManager.PlayButton.SetVisuability(false);

        placementManager.OnGetPosition = gridManager.GetRandomPosition;
        plantManager.OnUnitGetProduct = productObjectPool.GetProduct;
        placementManager.OnGetSun = productObjectPool.GetProduct;
        zombieManager.OnZombieDie = phaseManager.ZombieDie;
        phaseManager.OnZombieDispatcher = zombieManager.DispatcherZombie;

        phaseManager.OnWin = Win;
        zombieManager.OnZombieWin = Lose;

        ZombieStart();
    }

    private void ZombieStart()
    {
        phaseManager.StartLevel();
    }

    private void Win(Vector3 tossPos)
    {
        rewardManager.TossGift(tossPos);
        uiManager.WinTransition();
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
    }

    private void OnDestroy()
    {
        IsEndGame = false;
        IsStartGame = false;
    }
}
