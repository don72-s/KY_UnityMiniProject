using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeartColtroller : MonoBehaviour {

    [SerializeField]
    Sprite emptyHeart;
    [SerializeField]
    Sprite fullHeart;
    [SerializeField]
    Image heratBaseImg;

    [SerializeField]
    Transform heartParentTransform;

    [SerializeField]
    HeartModel heartModel;

    List<Image> heartList;

    // Start is called before the first frame update
    void Start() {

        heartList = new List<Image>();

        for (int i = 0; i < heartModel.MaxHealth; i++) {

            Image tmp = Instantiate(heratBaseImg);
            tmp.transform.SetParent(heartParentTransform);
            tmp.sprite = fullHeart;
            heartList.Add(tmp);
            tmp.transform.localScale = Vector3.one;

        }

        heartModel.hpChangedEvent += OnHpChanged;

        GameManager.GetInstance().GameResetEvent += HealAllHp;

    }

    public void OnHpChanged(int _changedHp) {

        foreach (Image heart in heartList) {

            heart.sprite = emptyHeart;

        }

        for (int i = 0; i < _changedHp; i++) { 
        
            heartList[i].sprite = fullHeart;

        }

    }

    public void HealAllHp() {

        heartModel.Health = heartModel.MaxHealth;

    }

}
