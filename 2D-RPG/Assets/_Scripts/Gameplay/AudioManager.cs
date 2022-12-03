using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource townAudio;
    [SerializeField] AudioSource battleAudio;

    private void Start() {
        townAudio.Stop();
        battleAudio.Stop();
    }

    public void PlayTownAudio() {
        if (!townAudio.isPlaying) {
            battleAudio.Stop();
            townAudio.Play();
        }
    }

    public void PlayBattleAudio() {
        if (!battleAudio.isPlaying) {
            Debug.Log("battle music");
            townAudio.Stop();
            battleAudio.Play();
        }
    }
}
