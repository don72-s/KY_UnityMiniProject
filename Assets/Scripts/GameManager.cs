using Cinemachine;
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

    [SerializeField]
    CinemachineVirtualCamera vCam;

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

        vCam.m_Lens.OrthographicSize = 20;
        Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 0.2f;

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

    public void ChangeViewMode() {

        vCam.Priority = vCam.Priority == 0 ? 90 : 0;
        Camera.main.orthographic = !Camera.main.orthographic;

    }

    public void GameOver() {

        //StopCoroutine(speedControlCoroutine);
        //Time.timeScale = 0;

    }

    IEnumerator SpeedControlCoroutine() {

        while (Time.timeScale < maxSpeed) {

            yield return waitSpeedUpDelay;
            Time.timeScale = Mathf.Clamp(Time.timeScale + speedUpSize, 1, maxSpeed);

        }
    }

}
