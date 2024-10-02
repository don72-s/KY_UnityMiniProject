using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeartColtroller : MonoBehaviour {

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

        for (int i = 0; i < heartModel.MaxHealth; i += heartModel.OneHeartAmount) {

            Image tmp = Instantiate(heratBaseImg);
            tmp.transform.SetParent(heartParentTransform);
            heartList.Add(tmp);
            tmp.transform.localScale = Vector3.one;

        }

        heartModel.hpChangedEvent += OnHpChanged;

        GameManager.GetInstance().GameResetEvent += HealAllHp;

    }

    public void OnHpChanged(int _changedHp) {

        foreach (Image tmp in heartList)
            tmp.fillAmount = 0;

        int idx = 0;

        while (_changedHp > 0) {

            if (_changedHp >= heartModel.OneHeartAmount) {
                heartList[idx].fillAmount = 1;
                _changedHp -= heartModel.OneHeartAmount;
                idx++;
            } else {
                heartList[idx].fillAmount = _changedHp / (float)heartModel.OneHeartAmount;
                _changedHp = 0;
            }

        }

    }

    public void HealHp(int _healAmount) {

        heartModel.Health = Mathf.Clamp(heartModel.Health + _healAmount, 0, heartModel.MaxHealth);

    }

    public void HealAllHp() {

        heartModel.Health = heartModel.MaxHealth;

    }

}
