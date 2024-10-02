using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartModel : MonoBehaviour
{

    int health;

    [SerializeField]
    int maxHealth;
    [SerializeField]
    int oneHeartAmount;

    public event Action<int> hpChangedEvent;

    private void Awake() {

        health = maxHealth;

    }

    public int Health {

        get { return health; }
        set {health = Mathf.Clamp(value, 0, MaxHealth); hpChangedEvent?.Invoke(health);}

    }

    public int MaxHealth { get { return maxHealth; } private set { } }

    public int OneHeartAmount { get { return oneHeartAmount; } private set { } }

    public void TakeDamage(int _damage) {

        Health = Health - _damage;

    }

}
