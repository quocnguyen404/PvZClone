using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    [Header("Component Reference")]
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private GameObject mouseIndicator = null;
    [SerializeField] private Button shovelButton = null;

    public bool startPlacing = false;

    //get object from pool
    public IUnit selectedUnit = null;

    private void Update()
    {
        if (!startPlacing)
            return;

        if (Input.GetMouseButtonDown(0) && selectedUnit != null)
        {
            Node selectedNode = inputManager.GetSelectedNode();

            if (selectedNode == null)
            {
                selectedUnit.gameObject.SetActive(false);
                selectedUnit = null;
                return;
            }

            PlacePlantOnNode(selectedNode);
        }

        Vector3 mousePosition = inputManager.GetSelectedPlanePosition();

        mouseIndicator.transform.position = mousePosition;
    }

    public void PlacePlantOnNode(Node node)
    {
        if (node.unitData != null)
            return;

        //selectedUnit.transform.parent = node.transform;
        selectedUnit.transform.position = node.WorldPosition;

        node.unitData = selectedUnit.unitData;

        selectedUnit = null;
    }

    public void GetUnitData(IUnit unit)
    {
        selectedUnit = unit;
    }
}
