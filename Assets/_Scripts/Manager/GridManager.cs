using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Component")]
    [SerializeField] private int row;
    [SerializeField] private int column;
    [SerializeField] private float nodeLeng;
    [SerializeField] private Node nodePrefab = null;

    public bool draw = false;

    private Vector3 nodeBottom = Vector3.zero;

    [SerializeField] private Node[,] nodes;

    private void Awake()
    {
        float x = transform.position.x - ((column * nodeLeng) / 2) + (nodeLeng / 2);
        float z = transform.position.z - ((row * nodeLeng) / 2) + (nodeLeng / 2);

        nodeBottom = new Vector3(x, transform.position.y, z);
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        nodes = new Node[row, column];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                float x = nodeBottom.x + j * nodeLeng;
                float z = nodeBottom.z + i * nodeLeng;

                Node newNode = Instantiate(nodePrefab, new Vector3(x, transform.position.y, z), Quaternion.identity);
                newNode.WorldPosition = newNode.transform.position;
                nodes[i, j] = newNode;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(column * nodeLeng, 0.1f, row * nodeLeng));

        Gizmos.color = Color.red;
        Gizmos.DrawCube(nodeBottom, Vector3.one * 0.3f);

        if (draw)
        {
            foreach (Node node in nodes)
            {
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * 0.3f);
            }
        }
    }


}
