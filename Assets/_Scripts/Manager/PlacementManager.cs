using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private GameObject mouseIndicator = null;
    [SerializeField] private IUnit onSelectedPlant = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && onSelectedPlant != null)
        {
            Node selectedNode = inputManager.GetSelectedNode();

            if (selectedNode == null)
                return;

            PlacePlantOnNode(selectedNode);
        }

        Vector3 mousePosition = inputManager.GetSelectedPlanePosition();

        mouseIndicator.transform.position = mousePosition;
    }


    public void PlacePlantOnNode(Node node)
    {
        if (!node.isEmpty)
            return;

        node.isEmpty = false;
        IUnit plant = Instantiate(onSelectedPlant, node.WorldPosition, Quaternion.identity);
        node.unit = plant;
    }
}
