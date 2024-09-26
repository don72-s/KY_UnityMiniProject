using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolAble : MonoBehaviour {

    private ObjectPool basePool = null;
    public ObjectPool BasePool {

        get { return basePool; }

        set {

            if (basePool == null) {

                basePool = value;

            }

        }

    }

    public void BackToPool() {

        basePool.BackObject(this);

    }

}
