using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void OnEnable() {

        SceneManager.sceneLoaded += InitFunction;

    }

    private void OnDisable() {

        SceneManager.sceneLoaded -= InitFunction;

    }

    void InitFunction(Scene _scene, LoadSceneMode _mode) {

        Time.timeScale = 1;

    }

}
