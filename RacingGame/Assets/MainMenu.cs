using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        AudioManager.Instance.StopAllSounds();
        volumeSlider.value = AudioManager.Instance.MasterVolume;
        AudioManager.Instance.Play("MainMenu");
    }

    public void ChangeVolume(float _value)
    {
        AudioManager.Instance.SetMasterVolume(_value);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {
        OptionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }


    public void CloseOptions()
    {
        OptionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
