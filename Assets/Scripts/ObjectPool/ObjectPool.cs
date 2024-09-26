using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    
    Stack<PoolAble> pool;
    PoolAble prefab;
    Transform prefabParent;

    public ObjectPool(PoolAble _prefab, Transform _prefabParent, int _startPoolSize = 10) { 
    
        pool = new Stack<PoolAble>();
        prefab = _prefab;
        prefabParent = _prefabParent;

        for (int i = 0; i < _startPoolSize; i++) 
            CreateObject();

    }

    private void CreateObject() {

        PoolAble tmp = MonoBehaviour.Instantiate(prefab);
        tmp.transform.SetParent(prefabParent);
        tmp.gameObject.SetActive(false);
        tmp.BasePool = this;
        pool.Push(tmp);

    }

    public PoolAble GetObject() {

        if (pool.Count == 0) { 
            CreateObject();
        }

        PoolAble tmp = pool.Pop();
        tmp.transform.SetParent(null);
        tmp.gameObject.SetActive(true);
        return tmp;

    }

    public void BackObject(PoolAble _object) {

        _object.transform.SetParent(prefabParent);
        _object.gameObject.SetActive(false);
        pool.Push(_object);

    }

}
