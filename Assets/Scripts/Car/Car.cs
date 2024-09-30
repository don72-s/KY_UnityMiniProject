using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : PoolAble
{

    Character character;

    [SerializeField]
    public float yOffset;
    
    [Header("Animation")]
    [SerializeField]
    Animator animator;
    private void Start() {

        character = GameObject.FindWithTag("Player").GetComponent<Character>();

    }


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {

            //todo : 체력깎 또는 게임오버
            Debug.Log("게임오버 [ 옆추돌 ]");
            GameManager.GetInstance().GameOver();
            character.OnDamaged();
            animator.Play("crash");

        }

    }

}
