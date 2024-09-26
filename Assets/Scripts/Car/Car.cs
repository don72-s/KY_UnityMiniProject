using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : PoolAble
{
    [SerializeField]
    public float yOffset;


    private void OnCollisionEnter(Collision collision) {

        //추돌 탐지
        if (collision.gameObject.tag == "Player") {

            //todo : 체력깎 또는 게임오버
            Debug.Log("게임오버 [ 옆추돌 ]");
            gameObject.SetActive(false);
            GameManager.GetInstance().GameOver();

        }

    }

}
