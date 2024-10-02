using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSG_UI_animationEvent : MonoBehaviour
{
    GameManager gameManager;

    private void Start() {
        
        gameManager = GameManager.GetInstance();

    }
    void EndCountDownAnimation() {

        gameManager.RestartGame();

    }
}
