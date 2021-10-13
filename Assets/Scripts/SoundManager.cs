using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioClip _PumpkinPickedUpSound;
    private AudioSource _AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _PumpkinPickedUpSound = Resources.Load<AudioClip>("Sounds/PumpkinPickedUp");
        _AudioSource = GetComponent<AudioSource>();
    }

    public void playSound(string clip) {
        switch (clip) {
            case "PumpkinPickedUp":
                Debug.Log("playing sound");
                _AudioSource.PlayOneShot(_PumpkinPickedUpSound);
                break;
        }
    }

}
