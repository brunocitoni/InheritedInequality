using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public void OnClickPlaySound(AudioSource audioToPlay)
    {
        audioToPlay.Play();
    }
}
