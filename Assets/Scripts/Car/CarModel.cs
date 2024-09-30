using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CarModel : MonoBehaviour
{

    [SerializeField]
    Car carBase;

    [Header("Cur Model's local transform for repositioning")]
    [SerializeField]
    Vector3 localPos;
    [SerializeField]
    Quaternion localRot;
    public void EndCrashAnimation() {

        carBase.gameObject.SetActive(false);
        transform.SetLocalPositionAndRotation(localPos, localRot);

    }

}
