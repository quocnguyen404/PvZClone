using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Data/Unit Data")]
    public class UnitData : ScriptableObject
    {
        public string unitName = "";
        public int cost = 0;
        public float rechargeTime = 0f;
        public List<Attribute> attributes = null;
    }
}


