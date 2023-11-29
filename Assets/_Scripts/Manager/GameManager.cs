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

    [Space]
    [SerializeField] private Button playButton = null;


    public static bool IsEndGame { get; private set; }

    private void Awake()
    {
        IsEndGame = false;
        gridManager.Initialize(GameConstant.GARDEN_ROW, GameConstant.GARDEN_COLOUMN + GameConstant.ZOMBIE_COLUMN, GameConstant.NODE_LENGTH);

        playButton.onClick.AddListener(() => { StartGame(); });

        placementManager.OnPlaceUnit = plantManager.AddUnit;
        zombieObjectPool.OnSpawnUnit = zombieManager.AddUnit;
        productObjectPool.OnSpawnProduct = sunManager.AddProduct;

        zombieManager.OnZombieGetPath = gridManager.GetRow;
        plantManager.PoolTransform = plantObjectPool.transform;
        plantManager.OnPlantGetNode = gridManager.GetNode;
        plantManager.OnPlantGetPath = gridManager.GetRow;
        plantManager.OnPlantGetArea = gridManager.GetArea;
        pickUnitManager.OnGetPlant = plantObjectPool.GetPlant;

        zombieManager.gridManager = gridManager;
        zombieManager.Initialize();
        plantManager.Initialize();
        zombieObjectPool.InitializePool(ConfigHelper.GetCurrentLevelConfig());
    }

    private void StartGame()
    {
        if (!pickUnitManager.PickFull())
        {
            //player pick plan not done

            return;
        }

        pickUnitManager.OnPickedUnit = placementManager.GetSelectedUnitData;
        sunManager.OnSunClick = placementManager.PickUpSun;

        pickUnitManager.InitializeUnitData();

        placementManager.Initialize();
        plantObjectPool.InitilizePool(pickUnitManager.PlantDatas());
        productObjectPool.InitializePool(pickUnitManager.PlantDatas());

        playButton.onClick.RemoveAllListeners();
        playButton.gameObject.SetActive(false);

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

    private void Win()
    {
        Debug.Log("Win");
        EndGame();
    }

    private void Lose()
    {
        Debug.Log("Lose");
        EndGame();
    }

    private void EndGame()
    {
        IsEndGame = true;
    }
}
