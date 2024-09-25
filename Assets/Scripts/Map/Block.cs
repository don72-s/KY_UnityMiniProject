using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //todo : ��ɰ���
    /// <summary>
    /// ����� ����� ��ֹ� �������� �����ϰ� ������ƮǮ�� ����
    /// </summary>
    public void ResetBlock(CarSpawnPosSO _spawnInfo, Car _carPrefab) {

        int lines = _spawnInfo.LineCount;//Ȧ��, ¦�� �ٸ�
        int cars = _spawnInfo.CarCount;//Ȧ��, ¦�� �ٸ�



        for (int i = -(lines - 1); i <= lines - 1; i += 2) {

            int j = -(cars - 1);
            int r = Random.Range(0, cars - 1);

            for (int idx = 0; idx < cars; idx++) {

                if (r != idx) {
                    Car c = Instantiate(_carPrefab, transform.position + new Vector3(j * _spawnInfo.HorizontalOffset, _carPrefab.yOffset, i * _spawnInfo.VerticalOffset), Quaternion.identity);
                    c.transform.SetParent(transform);
                }
                j += 2;

            }

        }


    }


}
