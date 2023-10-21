using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRd = null;

    public Vector2Int GridPosition;
    public Vector3 WorldPosition;
    public IUnit unit = null;


    public void MoreBold()
    {
        meshRd.material.color = Color.green;
    }
}

