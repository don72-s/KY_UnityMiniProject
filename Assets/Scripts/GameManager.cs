using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;


    [SerializeField]
    float speedUpTime;
    [SerializeField]
    float speedUpSize;
    [SerializeField]
    float maxSpeed;

    WaitForSecondsRealtime waitSpeedUpDelay;
    Coroutine speedControlCoroutine;

    private void Awake() {

        if (instance == null) {

            instance = this;

        } else { 
        
            Destroy(gameObject);

        }

    }

    public static GameManager GetInstance() {

        return instance;

    }

    private void Start() {

        waitSpeedUpDelay = new WaitForSecondsRealtime(speedUpTime);

        speedControlCoroutine = StartCoroutine(SpeedControlCoroutine());

    }

    private void OnEnable() {

        SceneManager.sceneLoaded += InitFunction;

    }

    private void OnDisable() {

        SceneManager.sceneLoaded -= InitFunction;

    }

    void InitFunction(Scene _scene, LoadSceneMode _mode) {

        Time.timeScale = 1;

    }

    public void GameOver() {

        StopCoroutine(speedControlCoroutine);
        Time.timeScale = 0;

    }

    IEnumerator SpeedControlCoroutine() {

        while (Time.timeScale < maxSpeed) {

            yield return waitSpeedUpDelay;
            Time.timeScale = Mathf.Clamp(Time.timeScale + speedUpSize, 1, maxSpeed);

        }
    }

}
