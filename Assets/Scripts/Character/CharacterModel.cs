using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    [SerializeField]
    Character character;

    public void Recover() {

        character.OnRecovered();

    }
}
