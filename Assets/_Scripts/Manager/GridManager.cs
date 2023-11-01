using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Component")]
    [SerializeField] private Node nodePrefab = null;
    [SerializeField] private Transform parentTrans = null;

    private int row;
    private int column;
    private float nodeLeng;
    private Dictionary<int, List<Node>> rows;

    [Space]
    public bool draw = false;

    private Vector3 nodeBottom;
    private Node[,] nodes;

    public void Initialize()
    {
        row = GameConstant.GARDEN_ROW;
        column = GameConstant.GARDEN_COLOUMN;
        nodeLeng = GameConstant.NODE_LENGTH;

        float x = transform.position.x - ((column * nodeLeng) / 2) + (nodeLeng / 2);
        float z = transform.position.z - ((row * nodeLeng) / 2) + (nodeLeng / 2);

        nodeBottom = new Vector3(x, transform.position.y, z);
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        nodes = new Node[row, column];
        rows = new Dictionary<int, List<Node>>();

        for (int i = 0; i < row; i++)
        {
            rows[i] = new List<Node>();
            for (int j = 0; j < column; j++)
            {
                float x = nodeBottom.x + j * nodeLeng;
                float z = nodeBottom.z + i * nodeLeng;
                Node newNode = Instantiate(nodePrefab, new Vector3(x, transform.position.y, z), Quaternion.identity);

                newNode.Initialize();
                newNode.GridPosition = new Vector2Int(i, j);
                newNode.WorldPosition = newNode.transform.position;
                newNode.transform.parent = parentTrans;

                if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    newNode.MoreBold();

                nodes[i, j] = newNode;
                rows[i].Add(newNode);
            }
        }
    }

    public List<Node> GetRow(int row)
    {
        return rows[row];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(column * nodeLeng, 0.1f, row * nodeLeng));

        if (draw)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(nodeBottom, Vector3.one * 0.3f);

            foreach (Node node in nodes)
            {
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * 0.3f);
            }
        }
    }


}
