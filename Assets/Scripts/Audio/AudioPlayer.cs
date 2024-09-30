using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    static AudioPlayer instance;

    [SerializeField]
    AudioSource bgmSource;
    [SerializeField]
    AudioSource sfxSource;

    private void Awake() {

        if (instance == null) {

            instance = this;

        } else {

            Destroy(gameObject);

        }

    }

    public static AudioPlayer GetInstance() { 
    
        return instance;

    }

    public void PlayBGM(AudioClip _bgm, float _volume) { 
    
        bgmSource.clip = _bgm;
        bgmSource.volume = _volume;
        bgmSource.Play();

    }

    public void PlaySFX(AudioClip _audioClip) {

        sfxSource.PlayOneShot(_audioClip);

    }

    public void PlaySFX(AudioClip _audioClip, float _volume) {

        sfxSource.PlayOneShot(_audioClip, _volume);

    }

}
