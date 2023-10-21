using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [Header("Component Reference")]
    [SerializeField] private PickUnitManager pickUnitManager = null;
    [SerializeField] private PlacementManager placementManager = null;

    [Space]
    [Header("Object Pooling Reference")]
    [SerializeField] private PlantObjectPool plantObjectPool = null;
    [SerializeField] private ProjectileObjectPool projectileObjectPool = null;

    [Space]
    [SerializeField] private Button playButton = null;

    private void Awake()
    {
        playButton.onClick.AddListener(() => { StartGame(); });
        pickUnitManager.GetPlant = plantObjectPool.GetPlant;
        pickUnitManager.OnPickedUnit = placementManager.GetSelectedUnitData;
    }

    private void StartGame()
    {
        if (!pickUnitManager.PickFull())
            return;

        pickUnitManager.InitializeUnitData();

        plantObjectPool.InitilizePool(pickUnitManager.PlantDatas());

        //need projectile prefab
        //projectileObjectPool.InitializePool(pickUnitManager.PlantDatas());

        placementManager.startPlacing = true;
        playButton.gameObject.SetActive(false);
    }
}
