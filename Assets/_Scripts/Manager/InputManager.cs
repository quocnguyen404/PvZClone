using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera cam = null;
    [SerializeField] private LayerMask planeLayer;

    private Vector3 lastPosition;

    public bool IsOverPlane()
    {
        Node sNode = GetSelectedNode();

        return sNode != null && !sNode.HasPlant() && sNode.GridPosition.y < GameConstant.GARDEN_COLOUMN;
    }

    public Vector3 GetSelectedNodePosition()
    {
        Node sNode = GetSelectedNode();

        return sNode.WorldPosition;
    }

    public Node GetSelectedNode()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeLayer))
        {
            Debug.DrawLine(ray.origin, lastPosition, Color.red);
            return hit.collider.GetComponent<Node>();
        }
        else 
            return null;
    }
}
