using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public abstract class IUnit : MonoBehaviour
{
    #region Composition Reference
    [SerializeField] protected UnitAnimator ator = null;
    [SerializeField] protected List<Node> nodePaths = null;
    #endregion

    #region Unit Data
    public Data.UnitData UnitData = null;
    public string Name
    {
        get
        {
            return UnitData.unitName;
        }
    }
    protected float maxHealth => UnitData.attributes[(int)Data.AttributeType.HP].value;
    protected float currentHealth;
    public virtual bool IsAlive => currentHealth > 0;
    public bool IsOnNode => nodePaths.Count > 0;
    public bool IsEndGame => GameManager.IsEndGame;

    public Vector3 PoolPosition = Vector3.zero;
    public Vector2Int GridPosition;
    #endregion

    #region Event
    public System.Func<int, int, Node> OnGetNode = null;
    public System.Func<int, List<Node>> OnGetPath = null;
    public System.Func<int, int, List<Node>> OnGetArea = null;
    public System.Func<Data.UnitData, IProduct> OnGetProduct = null;
    #endregion

    public virtual void Initialize(Vector2Int pos)
    {
        currentHealth = maxHealth;
        GridPosition = pos;
    }


    public virtual void Update()
    {

    }

    public virtual void PlaceUnitOnNode(Node node)
    {
        transform.position = node.WorldPosition;
        node.AddUnit(this);
    }

    public virtual void TakeDamage(float damge)
    {
        if (!IsAlive)
            return;

        currentHealth -= damge;

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
    }
}
