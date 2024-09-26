using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ObjPoolData")]
public class ObjectPoolSO : ScriptableObject
{

    [Serializable]
    public struct ObjPoolData {

        [SerializeField]
        public PoolAble prefab;
        [SerializeField]
        public int startPoolCount;
        [Header("Unique Info When Using Same Script Inherits From Poolable")]
        [SerializeField]
        public string exInfo;

    }

    [SerializeField]
    public ObjPoolData[] poolDatas;

}
