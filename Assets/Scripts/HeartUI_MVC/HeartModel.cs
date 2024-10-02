using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartModel : MonoBehaviour
{

    int health;

    [SerializeField]
    int maxHealth;

    public event Action<int> hpChangedEvent;

    private void Awake() {

        health = maxHealth;

    }

    public int Health {

        get { return health; }
        set { health = value; hpChangedEvent?.Invoke(health); }

    }

    public int MaxHealth { get { return maxHealth; } private set { } }

    public void TakeDamage(int _damage) {

        Health = Math.Clamp(Health - _damage, 0, maxHealth);

    }

}
