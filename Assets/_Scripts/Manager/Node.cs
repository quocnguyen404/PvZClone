using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 WorldPosition;
    public bool isEmpty = true;
    public IUnit unit;


    public void SelftActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}

