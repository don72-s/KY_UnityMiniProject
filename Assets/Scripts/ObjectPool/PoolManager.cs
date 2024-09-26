using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static ObjectPoolSO;

public class PoolManager : MonoBehaviour {

    private static PoolManager instance = null;
    Dictionary<(Type, string), ObjectPool> objPoolDic;

    [SerializeField]
    ObjectPoolSO objPoolData;

    private void Awake() {

        if (instance == null) {

            instance = this;

        } else {

            Destroy(gameObject);

        }

    }

    public static PoolManager GetInstance() { 
    
        return instance;

    }

    ObjectPool testPool;

    private void Start() {

        objPoolDic = new Dictionary<(Type, string), ObjectPool>();

        ObjPoolData[] data = objPoolData.poolDatas;

        for (int i = 0; i < data.Length; i++) {
            Transform parent = new GameObject($"{data[i].prefab.GetType()}_{data[i].exInfo}").transform;
            parent.SetParent(transform);


            objPoolDic.Add(

                (data[i].prefab.GetType(), data[i].exInfo),
                new ObjectPool(data[i].prefab, parent, data[i].startPoolCount)
                
            );

        }

    }


    public T GetObj<T>(string _exInfo = "") where T : PoolAble {

        if (objPoolDic.ContainsKey((typeof(T), _exInfo))) {

            return (T)objPoolDic[(typeof(T), _exInfo)].GetObject();

        } else {

            Debug.LogWarning("약속되지 않은 objectpool object");
            return null;
        }


    }

}
