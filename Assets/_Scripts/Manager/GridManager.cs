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

    public void Initialize(int row, int column, float nodeLen)
    {
        this.row = row;
        this.column = column;
        this.nodeLeng = nodeLen;

        float x = transform.position.x - ((column * nodeLeng) / 2) + (nodeLeng / 2);
        float z = transform.position.z - ((row * nodeLeng) / 2) + (nodeLeng / 2);

        nodeBottom = new Vector3(x, transform.position.y, z);
        GenerateGrid();
    }

    private void GenerateGrid()
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

                Vector3 worldPos = new Vector3(newNode.transform.position.x, GameConstant.NODE_THICKNESS, newNode.transform.position.z);

                newNode.WorldPosition = worldPos;
                newNode.transform.parent = parentTrans;

                if (((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0)) && j < 9)
                    newNode.ChangeColor(Color.green);

                nodes[i, j] = newNode;
                rows[i].Add(newNode);
            }
        }
    }

    public List<Node> GetRow(int row)
    {
        return rows[row];
    }

    public List<Node> GetArea(int row, int column)
    {
        List<Node> area = new List<Node>();

        int firstRow = row - 1;
        int firstColumn = column - 1;

        for (int i = firstRow; i < firstRow + 3; i++)
        {
            for (int j = firstColumn; j < firstColumn + 3; j++)
            {
                if (i >= 0 && j >= 0 && i < GameConstant.GARDEN_ROW && j < GameConstant.GARDEN_COLOUMN)
                    area.Add(nodes[i, j]);
            }
        }

        return area;
    }

    public Vector3 GetRandomPosition()
    {
        int x = Random.Range(0, GameConstant.GARDEN_ROW);
        int z = Random.Range(0, GameConstant.GARDEN_COLOUMN);

        return nodes[x, z].WorldPosition;
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
