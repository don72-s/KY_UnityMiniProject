using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
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

    private void Awake() {
        
        rigid = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start()
    {

        curLane = (MAX_LANE + 1) / 2;
        centerOffset = curLane;
       
    }


    private void FixedUpdate() {
        
        rigid.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 0)
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.Space)) {

            curLane += moveDir;

            destPosX = (curLane - centerOffset) * laneDistance;

            if (curLane == 1 || curLane == MAX_LANE) {
                moveDir *= -1;
            }

        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        transform.position = Vector3.Lerp(transform.position,
                                          new Vector3(destPosX, transform.position.y, transform.position.z),
                                          laneChangeSpeed * Time.deltaTime);


    }

}
