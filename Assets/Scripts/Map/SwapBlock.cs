using UnityEngine;

public class SwapBlock : Block {

    //GameManager gameManager;

    [SerializeField]
    GameObject heartItemObject;

    private void OnEnable() {

        heartItemObject.SetActive(true);

    }

    private void OnDisable() {

        heartItemObject.SetActive(false);

    }

}
