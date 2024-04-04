using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Transform mainMenu, settingsMenu;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Slider musicSlider, fxSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume",1);// if there's no exist volume record before, set the value 1
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

        fxSlider.value = 1;//assign the value of fx sounds 1
    }

    public void showSettingsMenu()
    {
        mainMenu.GetComponent<RectTransform>().DOLocalMoveX(-1200, 0.5f);
        settingsMenu.GetComponent<RectTransform>().DOLocalMoveX(0f, 0.5f);
    }

    public void closeSettingsMenu()
    {
        mainMenu.GetComponent<RectTransform>().DOLocalMoveX(-0, 0.5f);
        settingsMenu.GetComponent<RectTransform>().DOLocalMoveX(1200f, 0.5f);
    }

    public void gamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void quitFromGame()
    {
        Application.Quit();
    }

    public void backgroundMusicVolume()
    {
        musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume",musicSlider.value);// saving music value named musicVolume for using another scene
    }

    public void fxvolume()
    {
        PlayerPrefs.SetFloat("fxVolume",fxSlider.value);
    }
}
