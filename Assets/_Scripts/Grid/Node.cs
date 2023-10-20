using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRd = null;

    public Vector3 WorldPosition;
    public Data.UnitData unitData = null;

    public void MoreBold()
    {
        meshRd.material.color = Color.green;
    }



}

