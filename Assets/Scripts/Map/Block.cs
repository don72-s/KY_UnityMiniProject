using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : PoolAble
{
    PoolManager poolManager;
    List<Car> carL;
    public Vector3 nextTileOffset { get; protected set; }
    HashSet<int> voidIdx;

    [SerializeField]
    public float heightOffset;

    [SerializeField]
    GameObject RightObjectParent;

    private void Awake() {
        
        carL = new List<Car>(16);
        poolManager = PoolManager.GetInstance();
        nextTileOffset = Vector3.zero;

    }


    /// <summary>
    /// 블록 생성시의 초기세팅(장애물 등의 배치)
    /// </summary>
    public virtual void SettingBlock(CarSpawnPosSO _spawnInfo, bool _is3DBlock) {

        int lines = _spawnInfo.LineCount;
        int cars = _spawnInfo.CarCount;

        if (_is3DBlock) {

            for (int i = -(lines - 1); i <= lines - 1; i += 2) {

                int j = -(cars - 1);
                int r = Random.Range(0, cars);
                HashSet<int> set = new HashSet<int>();

                while (set.Count < r) {
                    set.Add(Random.Range(0, cars));
                }


                foreach (int val in set) {

                    Car c = poolManager.GetObj<Car>(Random.Range(0, 2) == 0 ? "Car" : "Truck");
                    c.transform.SetPositionAndRotation(transform.position + new Vector3((j + 2 * val) * _spawnInfo.HorizontalOffset, c.yOffset, i * _spawnInfo.VerticalOffset), Quaternion.identity);
                    c.transform.SetParent(transform);
                    carL.Add(c);

                }

            }

        } else {

            for (int i = -(lines - 1); i <= lines - 1; i += 2) {

                int j = -(cars - 1);

                int r = Random.Range(0, 2);

                if (r == 0) { 
                
                    for (int k = 0; k < cars; k++) {

                        Car c = poolManager.GetObj<Car>(Random.Range(0, 2) == 0 ? "Car" : "Truck");
                        c.transform.SetPositionAndRotation(transform.position + new Vector3((j + 2 * k) * _spawnInfo.HorizontalOffset, c.yOffset, i * _spawnInfo.VerticalOffset), Quaternion.identity);
                        c.transform.SetParent(transform);
                        carL.Add(c);

                    }


                }

            }

        }

    }


    public void SetRightObjectVisivle(bool _isVisible = true) { 
    
        RightObjectParent.SetActive(_isVisible);

    }

    /// <summary>
    /// 블록과 배치된 장애물 등의 요소들을 풀로 반환.
    /// </summary>
    public void RemoveBlock() {

        foreach (Car car in carL) { 
        
            car.BackToPool();

        }

        BackToPool();
        carL.Clear();

    }


}
