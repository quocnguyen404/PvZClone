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

    [Space]
    [Header("Object Pooling Reference")]
    [SerializeField] private PlantObjectPool plantObjectPool = null;
    [SerializeField] private ZombieObjectPool zombieObjectPool = null;
    [SerializeField] private ProjectileObjectPool projectileObjectPool = null;

    [Space]
    [Header("Unit Manager Reference")]
    [SerializeField] private PlantManager plantManager = null;
    [SerializeField] private ZombieManager zombieManager = null;

    [Space]
    [SerializeField] private Button playButton = null;

    private void Awake()
    {
        gridManager.Initialize();

        playButton.onClick.AddListener(() => { StartGame(); });

        placementManager.OnPlaceUnit = plantManager.AddUnit;
        zombieObjectPool.OnSpawnUnit = zombieManager.AddUnit;
        zombieManager.OnZombieGetPath = gridManager.GetRow;
        pickUnitManager.OnGetPlant = plantObjectPool.GetPlant;

        zombieManager.Initialize();
        plantManager.Initialize();
        zombieObjectPool.InitializePool(ConfigHelper.GetCurrentLevelConfig());
    }

    private void StartGame()
    {
        if (!pickUnitManager.PickFull())
            return;

        pickUnitManager.OnPickedUnit = placementManager.GetSelectedUnitData;

        pickUnitManager.InitializeUnitData();

        placementManager.Initialize();
        plantObjectPool.InitilizePool(pickUnitManager.PlantDatas());
        projectileObjectPool.InitializePool(pickUnitManager.PlantDatas());

        playButton.gameObject.SetActive(false);

        ZombieStart();
    }

    private void ZombieStart()
    {
        zombieManager.DispatcherZombie(1);
    }
}
