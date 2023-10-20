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
    [SerializeField] private Button playButton = null;

    private void Awake()
    {
        playButton.onClick.AddListener(() => { StartGame(); });
        pickUnitManager.OnPickedUnit = placementManager.GetUnitData;
    }

    private void StartGame()
    {
        if (!pickUnitManager.PickFull())
            return;

        pickUnitManager.InitializeStartGame();
        placementManager.startPlacing = true;
        playButton.gameObject.SetActive(false);
    }

}
