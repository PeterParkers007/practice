using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioSoundData
{
    public string soundName;
    public AudioClip sound;
}
public class AudioMusicData
{
    public string musicName;
    public AudioClip music;
}
public class AudioManager : Singleton<AudioManager>
{
    public List<AudioSoundData> Sounds = new List<AudioSoundData>();
    public List<AudioMusicData> Musics = new List<AudioMusicData>();
    public AudioSource audioSource;
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null )
            gameObject.AddComponent<AudioSource>();
    }
    public void PlayAudioSound(AudioSoundData soundData,Vector3 position)
    {
        AudioClip sound = soundData.sound;
        AudioSource.PlayClipAtPoint(sound,position);
    }
    public void PlayAudioMusic(AudioMusicData musicData)
    {
        audioSource.clip = musicData.music;
        audioSource.Play();
    }
    public void PlayPlayerDeathClip()
    {
        foreach (var music in Musics)
        {
            if(music.musicName == "PlayerDeathMusic")
            {
                PlayAudioMusic(music);
            }
            else
            {
                print("找不到名为PlayerDeathMusic的音频");
            }
        }
        foreach(var sound in Sounds)
        {
            if (sound.soundName == "PlayerDeathSound")
            {
                PlayAudioSound(sound,Camera.main.transform.position);
            }
            else
            {
                print("找不到名为PlayerDeathSound的音频");
            }
        }
    }
    private void OnEnable()
    {
        Events.PlayerEvent.OnPlayerDeathEvent += PlayPlayerDeathClip;   
    }
    private void OnDisable()
    {
        Events.PlayerEvent.OnPlayerDeathEvent -= PlayPlayerDeathClip;
    }
}
