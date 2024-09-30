using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    public enum State { RUN, JUMP, STUN}

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

    private void Awake() {

        rigid = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start() {

        audioPlayer = AudioPlayer.GetInstance();
        gameManager = GameManager.GetInstance();

        gameManager.GameStartEvent += () => { ChangeState(State.RUN); };

        curLane = (MAX_LANE + 1) / 2;
        centerOffset = curLane;

    }


    // Update is called once per frame
    void Update() {


        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 0) {
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.Space)) {

            PlayAction();

        }

        transform.position = Vector3.Lerp(transform.position,
                                          new Vector3(destPosX, transform.position.y, transform.position.z),
                                          laneChangeSpeed * Time.deltaTime / Time.timeScale);


    }

    public void ChangeState(State _destState) {

        switch (_destState) {

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

        ChangeState(State.STUN);
        characterDamaged?.Invoke();
        audioPlayer.PlaySFX(mistakeSFX);

    }

    public void OnRecovered() {

        ChangeState(State.RUN);
        characterRecovered?.Invoke();

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
