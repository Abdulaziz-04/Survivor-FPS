using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    private AudioSource audio_player;
    [SerializeField] private AudioClip scream_sound, die_sound;
    [SerializeField] private AudioClip[] attack_sound;
    private void Awake()
    {
        audio_player = GetComponent<AudioSource>();
    }
    public void playScream()
    {
        audio_player.clip = scream_sound;
        audio_player.Play();
    }
    public void playDead()
    {
        audio_player.clip = die_sound;
        audio_player.Play();
    }
    public void playAttack()
    {
        audio_player.clip = attack_sound[Random.Range(0, attack_sound.Length)];
        audio_player.Play();
    }


}
