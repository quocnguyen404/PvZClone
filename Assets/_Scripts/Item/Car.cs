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

    public bool isTrigger = false;
    public bool IsTrigger
    {
        get
        {
            if (!isTrigger)
                isTrigger = nodePaths[0].HasZombie() &&
                Vector3.Distance(nodePaths[0].GetZombieFromNode().transform.position, transform.position) <= radius + GameConstant.NODE_LENGTH/2f;

            return isTrigger;
        }
    }

    public Vector3 PoolPosition;

    public void Initialize(Vector3 position, List<Node> nodes)
    {
        gameObject.SetActive(true);
        transform.position = position;
        transform.eulerAngles = Helper.Cam.transform.eulerAngles;
        nodePaths = new List<Node>(nodes);
        agent.Initialize(speed, radius);
        agent.OnArried = Arried;
        arried = false;
    }

    private bool first = true;
    private void Update()
    {
        if (!IsTrigger)
            return;

        if (first)
        {
            AudioManager.Instance.PlaySound(Sound.CarRun);
            first = false;
        }

        if (arried)
            return;

        agent.SetDestination(new Vector3(nodePaths[currentNodeIndex].WorldPosition.x + GameConstant.NODE_LENGTH / 2f,
                                         nodePaths[currentNodeIndex].WorldPosition.y,
                                         nodePaths[currentNodeIndex].WorldPosition.z));
    }

    private void Arried()
    {
        arried = true;

        if (nodePaths[currentNodeIndex].HasZombie())
        {
            foreach (Zombie zombie in nodePaths[currentNodeIndex].GetAllZobmie())
                zombie.InstantDead();
        }

        currentNodeIndex++;

        if (currentNodeIndex >= nodePaths.Count)
        {
            Dead();
            return;
        }
        arried = false;
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        transform.position = PoolPosition;
    }
}
