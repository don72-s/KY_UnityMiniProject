using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    public float yOffset;


    private void OnCollisionEnter(Collision collision) {

        //�ߵ� Ž��
        if (collision.gameObject.tag == "Player") {

            //todo : ü�±� �Ǵ� ���ӿ���
            Debug.Log("���ӿ��� [ ���ߵ� ]");
            Destroy(gameObject);
            Time.timeScale = 0;

        }

    }

}
