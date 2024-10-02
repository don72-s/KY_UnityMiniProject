using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    enum GameState { TITLE, GET_READY, PLAYING, GAME_OVER}

    static GameManager instance = null;

    GameState curState;

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
    [SerializeField]
    GameObject healthUI;
    [SerializeField]
    GameObject scoreBoardUI;

    [Header("SFX")]
    [SerializeField]
    AudioClip gameBGM;

    [Header("Models")]
    [SerializeField]
    HeartModel heartModel;

    [Header("GameOver UI Animator")]
    [SerializeField]
    Animator gameOverAnimator;

    public event Action GameStartEvent;
    public event Action GameResetEvent;

    public bool is3DMode { get; private set; }

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

        orthoCam.m_Lens.OrthographicSize = 8;
        Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 0.2f;

        is3DMode = true;
        arrowObj.SetActive(false);

        curState = GameState.TITLE;
    }


    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {

            if (curState == GameState.TITLE) {

                StartCoroutine(TitleCoroutine());

            } else if (curState == GameState.GAME_OVER) {

                gameOverAnimator.Play("countDown");
                GetReady();
                curState = GameState.GET_READY;

            }

        }

        if (Input.GetKeyDown(KeyCode.Escape)) {

            Application.Quit();

        }
            
    }

    public bool IsPlaying() { 
    
        return curState == GameState.PLAYING;

    }

    IEnumerator TitleCoroutine() {

        titleUI.SetActive(false);
        titleCam.Priority = 0;
        yield return new WaitForSeconds(2.5f);
        GameStart();
        AudioPlayer.GetInstance().PlayBGM(gameBGM, 0.1f);
        curState = GameState.PLAYING;

    }

    public void GameStart() {

        GameStartEvent?.Invoke();
        arrowObj.SetActive(true);
        edgeBlock.SetActive(false);
        sideBackground.SetActive(true);
        healthUI.SetActive(true);
        scoreBoardUI.SetActive(true);


    }

    public void GetReady() {

        if(!is3DMode)
            ChangeViewMode();

        arrowObj.SetActive(false);
        edgeBlock.SetActive(true);
        
        GameResetEvent?.Invoke();

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

    public void RestartGame() {

        GameStart();
        curState = GameState.PLAYING;

    }

    public void GameOver() { 
    
        curState = GameState.GAME_OVER;
        gameOverAnimator.Play("appear");

    }

}
