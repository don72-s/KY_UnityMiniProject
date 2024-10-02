using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    public enum State { IDLE, RUN, JUMP, STUN}

    State curState = State.RUN;

    int moveDir = 1;
    const int MAX_LANE = 3;
    int curLane;

    int centerOffset;

    float destPosX;

    [SerializeField]
    float laneDistance;
    [SerializeField]
    float laneChangeSpeed;

    Rigidbody rigid;
    [SerializeField]
    float gravityMultiplier;
    [SerializeField]
    float jumpPower;
    bool isGrounded = true;

    AudioPlayer audioPlayer;
    [Header("SFX")]
    [SerializeField]
    AudioClip move_jumpSFX;
    [SerializeField]
    AudioClip mistakeSFX;

    GameManager gameManager;

    [Header("Animation")]
    [SerializeField]
    Animator animator;

    [Header("Arrow Obj")]
    [SerializeField]
    GameObject arrowObj;

    public event Action characterDamaged;
    public event Action characterRecovered;

    [Header("heart mvc model")]
    [SerializeField]
    HeartModel heartModel;

    private void Awake() {

        rigid = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start() {

        audioPlayer = AudioPlayer.GetInstance();
        gameManager = GameManager.GetInstance();

        gameManager.GameStartEvent += () => { ChangeState(State.RUN); };
        gameManager.GameResetEvent += ResetGame;

        curLane = (MAX_LANE + 1) / 2;
        centerOffset = curLane;

    }


    // Update is called once per frame
    void Update() {

        if (!gameManager.IsPlaying())
            return;


        if (Input.GetKeyDown(KeyCode.Space)) {

            PlayAction();

        }

        transform.position = Vector3.Lerp(transform.position,
                                          new Vector3(destPosX, transform.position.y, transform.position.z),
                                          laneChangeSpeed * Time.deltaTime / Time.timeScale);


    }

    public void ChangeState(State _destState) {

        switch (_destState) {

            case State.IDLE:
                animator.Play("idle");
                break;

            case State.RUN:
                animator.Play("sprint");
                break;

            case State.JUMP:
                animator.Play("jump");
                break;

            case State.STUN:
                animator.Play("die");
                break;

        }

        curState = _destState;

    }

    void PlayAction() {

        if (gameManager.is3DMode && curState == State.RUN) {

            curLane += moveDir;

            destPosX = (curLane - centerOffset) * laneDistance;

            if (curLane == 1 || curLane == MAX_LANE) {
                moveDir *= -1;
                arrowObj.transform.rotation = Quaternion.Euler(0, moveDir * 90, 0);
            }

            audioPlayer.PlaySFX(move_jumpSFX);

        } else if (!gameManager.is3DMode && isGrounded && curState == State.RUN) {

            rigid.velocity = Vector3.up * jumpPower * gravityMultiplier;
            ChangeState(State.JUMP);
            isGrounded = false;
            audioPlayer.PlaySFX(move_jumpSFX);

        }

    }


    Vector3 offsetVec = Vector3.zero;
    public void SetGravityVec(Vector3 _vec) {

        offsetVec = _vec;

    }

    private void FixedUpdate() {

        rigid.AddForce(Vector3.down * 90, ForceMode.Acceleration);

        rigid.AddForce(offsetVec, ForceMode.Acceleration);

    }

    public void OnDamaged() {

        if (curState == State.STUN)
            return;

        ChangeState(State.STUN);
        characterDamaged?.Invoke();
        audioPlayer.PlaySFX(mistakeSFX);

        heartModel.TakeDamage(1);

    }

    public void OnRecovered() {

        if (heartModel.Health <= 0) {
            gameManager.GameOver();
            return;
        }

        ChangeState(State.RUN);
        characterRecovered?.Invoke();

    }

    void ResetGame() {

        moveDir = 1;
        curLane = (MAX_LANE + 1) / 2;
        centerOffset = curLane;
        transform.position = Vector3.zero;
        destPosX = (curLane - centerOffset) * laneDistance;
        arrowObj.transform.rotation = Quaternion.Euler(0, moveDir * 90, 0);

    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Flat")) {

            if (!isGrounded && curState == State.JUMP) {

                ChangeState(State.RUN);
                
            }

            isGrounded = true;

            return;
        }

    }

}
