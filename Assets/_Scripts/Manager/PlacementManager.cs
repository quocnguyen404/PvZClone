using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlacementManager : MonoBehaviour
{

    [SerializeField] private GameObject mouseIndicator = null;
    [SerializeField] private InputManager inputManager = null;

    [SerializeField] private GameObject selectedPlant = null;


    private void Awake()
    {

    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            inputManager.GetSelectedNode().SelftActive();
        }

        Vector3 mousePos = inputManager.GetSelectedPlanePosition();
        mouseIndicator.transform.position = mousePos;
    }


}
