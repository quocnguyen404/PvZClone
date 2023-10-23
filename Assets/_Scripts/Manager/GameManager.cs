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
    [SerializeField] private ZombieDispatcher zombieDispatcher = null;

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

        zombieObjectPool.OnSpawnUnit = zombieManager.AddUnit;

        zombieManager.Initialize();
        plantManager.Initialize();
        zombieObjectPool.InitializePool(ConfigHelper.GetCurrentLevelConfig());

        pickUnitManager.OnGetPlant = plantObjectPool.GetPlant;
        pickUnitManager.OnPickedUnit = placementManager.GetSelectedUnitData;
        placementManager.OnPlaceUnit = plantManager.AddUnit;
    }

    private void StartGame()
    {
        if (!pickUnitManager.PickFull())
            return;

        pickUnitManager.InitializeUnitData();

        plantObjectPool.InitilizePool(pickUnitManager.PlantDatas());
        projectileObjectPool.InitializePool(pickUnitManager.PlantDatas());

        placementManager.Initialize();
        playButton.gameObject.SetActive(false);
    }
}
