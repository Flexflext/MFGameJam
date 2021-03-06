using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance;

    [Header("HUD")]
    [SerializeField] private PlayerHealthStats playerStats;

    [SerializeField] private GameObject killAnimationPrefab;

    [SerializeField] private Slider SpeedSlider;
    [SerializeField] private Slider HealthSilder;

    [SerializeField] private TMP_Text scoreText;

    [Header("Menu")]
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject DeathMenu;
    [SerializeField] private TMP_Text endScoreText;

    private float maxHealth;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        AudioManager.Instance.StopAllSounds();
        volumeSlider.value = AudioManager.Instance.MasterVolume;
        maxHealth = playerStats.CurrentHealth;
        HealthSilder.maxValue = maxHealth;
        AudioManager.Instance.Play("RacingMusic");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            Pause();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf && !OptionsMenu.activeSelf)
        {
            Resume();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf && OptionsMenu.activeSelf)
        {
            CloseOptions();
        }

        ChangeHealthUI(playerStats.CurrentHealth);
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

    public void ChangeVolume(float _value)
    {
        AudioManager.Instance.SetMasterVolume(_value);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Pause");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void ChangeScore(int _score)
    {
        scoreText.text = _score.ToString();
    }

    public void ShowKillUIAnimation(int _scoretoadd)
    {
        GameObject animation = Instantiate(killAnimationPrefab, this.transform.position, Quaternion.identity, this.transform);
        ScoreAddBehavior behav = animation.GetComponent<ScoreAddBehavior>();
        behav.ScoreToAdd = _scoretoadd;
    }

    private void ChangeHealthUI(float _currentHealth)
    {
        HealthSilder.value = _currentHealth;
    }

    public void ChangeSpeedValue(float _speed)
    {
        SpeedSlider.value = _speed;
    }

    public void OpenDeathMenu()
    {
        Time.timeScale = 0;
        OptionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        endScoreText.text = scoreText.text;
        DeathMenu.SetActive(true);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

}
