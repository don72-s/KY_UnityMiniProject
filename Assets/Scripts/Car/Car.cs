using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : PoolAble
{
    [SerializeField]
    public float yOffset;


    private void OnCollisionEnter(Collision collision) {

        //�ߵ� Ž��
        if (collision.gameObject.tag == "Player") {

            //todo : ü�±� �Ǵ� ���ӿ���
            Debug.Log("���ӿ��� [ ���ߵ� ]");
            gameObject.SetActive(false);
            GameManager.GetInstance().GameOver();

        }

    }

}
