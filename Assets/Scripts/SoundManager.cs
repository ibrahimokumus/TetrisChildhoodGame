
using UnityEngine;



public class SoundManager : MonoBehaviour
{
    //Singleton pattern
    public static SoundManager instance;

    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource[] soundEffects;
    [SerializeField] private AudioSource[] vokals;
    private AudioClip randomMusicClip;
    

    public bool isPlayMusic = true;
    public bool isPlayEffect = true;

    public IconTurnOnOff musicIcon;
    public IconTurnOnOff fcIcon;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        randomMusicClip = chooseRandomClip(musicClips);
        playBackgroundMusic(randomMusicClip);
    }

    public void makeSoundEffect(int index)
    {
        if (isPlayEffect && index < soundEffects.Length)
        {
            soundEffects[index].volume = PlayerPrefs.GetFloat("fxVolume");
            soundEffects[index].Stop();
            soundEffects[index].Play();
        }
    }
    AudioClip chooseRandomClip(AudioClip[] clips)
    {
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        return randomClip;
    }

    public void playBackgroundMusic(AudioClip musicClip)
    {
        //bir tanesi bile false ise calma
        if (!musicClip || !musicSource || !isPlayMusic)
        {
            return;
        }

        musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    void updateMusic()
    {
        if (musicSource.isPlaying != isPlayMusic)
        {
            if (isPlayMusic)
            {
                randomMusicClip = chooseRandomClip(musicClips);
                playBackgroundMusic(randomMusicClip);  
            }
            else
            {
                musicSource.Stop();
            }
        }
    }
// using for turning music on/off button function
    public void musicTurnOnOff()
    {
        isPlayMusic = !isPlayMusic;
        updateMusic();
        musicIcon.chooseIcon(isPlayMusic);
    }
// using for turning effect on/off button function
    public void effectTurnOnOff()
    {
        isPlayEffect = !isPlayEffect;
        fcIcon.chooseIcon(isPlayEffect);
    }

    public void makeVocalSound()
    {
        AudioSource source = vokals[Random.Range(0, vokals.Length)];
        source.Stop();
        source.Play();
    }
}
