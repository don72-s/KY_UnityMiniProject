using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    
    Character character;

    private void Awake() {
        
        character = GameObject.FindWithTag("Player").GetComponent<Character>();

    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {

            character.GetHeartItem();
            gameObject.SetActive(false);

        }

    }

}
