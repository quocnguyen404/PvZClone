using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] Agent agent = null;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float radius;

    private int currentNodeIndex = 0;
    private List<Node> nodePaths = null;
    private bool arried = false;

    public bool isTrigger => nodePaths != null && nodePaths[0].HasZombie();

    public void Initialize(Vector3 position, List<Node> nodes)
    {
        transform.eulerAngles = Helper.Cam.transform.eulerAngles;
        nodePaths = new List<Node>(nodes);
        agent.Initialize(speed, radius);
        agent.OnArried = Arried;
    }

    private void Update()
    {
        if (!isTrigger)
            return;

        if (arried)
            return;


    }

    private void Arried()
    {
        arried = true;

        currentNodeIndex++;

        if (currentNodeIndex >= nodePaths.Count)
        {

            return;
        }

        arried = false;
    }

}
