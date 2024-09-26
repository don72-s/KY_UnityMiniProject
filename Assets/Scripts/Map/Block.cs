using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : PoolAble
{
    PoolManager poolManager;
    List<Car> carL;

    private void Awake() {
        
        carL = new List<Car>(16);
        poolManager = PoolManager.GetInstance();

    }


    /// <summary>
    /// 블록 생성시의 초기세팅(장애물 등의 배치)
    /// </summary>
    public void SettingBlock(CarSpawnPosSO _spawnInfo) {

        int lines = _spawnInfo.LineCount;
        int cars = _spawnInfo.CarCount;


        for (int i = -(lines - 1); i <= lines - 1; i += 2) {

            int j = -(cars - 1);
            int r = Random.Range(0, cars);

            for (int idx = 0; idx < cars; idx++) {

                if (r != idx) {
                    Car c = poolManager.GetObj<Car>();
                    c.transform.SetPositionAndRotation(transform.position + new Vector3(j * _spawnInfo.HorizontalOffset, c.yOffset, i * _spawnInfo.VerticalOffset), Quaternion.identity);
                    c.transform.SetParent(transform);
                    carL.Add(c);
                }
                j += 2;

            }

        }

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
