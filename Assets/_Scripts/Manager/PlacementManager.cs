using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    [Header("Component Reference")]
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private Button shovelButton = null;

    public bool startPlacing = false;
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

            PlaceUnitOnNode(selectedNode);
        }
    }

    public void PlaceUnitOnNode(Node node)
    {
        if (node.unit != null)
            return;

        selectedUnit.PlaceUnitOnNode(node);

        selectedUnit = null;
    }

    public void GetSelectedUnitData(IUnit unit)
    {
        selectedUnit = unit;
    }
}
