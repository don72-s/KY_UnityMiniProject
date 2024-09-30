using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;


    [Header("Camera")]
    [SerializeField]
    CinemachineVirtualCamera titleCam;
    [SerializeField]
    CinemachineVirtualCamera orthoCam;
    [SerializeField]
    CinemachineVirtualCamera quaterCam;

    [Header("Objects")]
    [SerializeField]
    GameObject arrowObj;
    [SerializeField]
    GameObject edgeBlock;
    [SerializeField]
    GameObject sideBackground;
    [SerializeField]
    GameObject titleUI;

    [Header("SFX")]
    [SerializeField]
    AudioClip gameBGM;

    public event Action GameStartEvent;

    public bool isStart { get; private set; }
    public bool is3DMode { get; private set; }

    private void Awake() {

        if (instance == null) {

            instance = this;

        } else { 
        
            Destroy(gameObject);

        }

        isStart = false;

    }

    public static GameManager GetInstance() {

        return instance;

    }

    private void Start() {

        orthoCam.m_Lens.OrthographicSize = 8;
        Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 0.2f;

        is3DMode = true;
        arrowObj.SetActive(false);

    }


    private void Update() {

        if (!isStart && Input.GetKeyDown(KeyCode.Space)) {

            StartCoroutine(TitleCoroutine());

        }
            
    }

    IEnumerator TitleCoroutine() {

        titleUI.SetActive(false);
        titleCam.Priority = 0;
        yield return new WaitForSeconds(2.5f);
        GameStart();

    }

    public void GameStart() {

        GameStartEvent?.Invoke();
        arrowObj.SetActive(true);
        edgeBlock.SetActive(false);
        sideBackground.SetActive(true);
        isStart = true;
        AudioPlayer.GetInstance().PlayBGM(gameBGM, 0.1f);
        
    }

    public void ChangeViewMode() {

        if (is3DMode) {

            if (UnityEngine.Random.Range(0, 2) == 0) {

                orthoCam.Priority = 90;

            } else {

                quaterCam.Priority = 90;

            }

            arrowObj.SetActive(false);

        } else {

            orthoCam.Priority = quaterCam.Priority = 0;
            arrowObj.SetActive(true);

        }

        Camera.main.orthographic = !Camera.main.orthographic;
        is3DMode = !is3DMode;

    }


    public void GameOver() {

        //todo : 게임 오버시 처리

    }


}
