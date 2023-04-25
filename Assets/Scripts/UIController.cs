using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class UIController : MonoBehaviour
{
    private int keyCount = 0;
    private int score = 0;
    private int playerhealth;
    private bool gameover = false;
    [SerializeField] private TextMeshProUGUI keyValue;
    [SerializeField] private TextMeshProUGUI timervalue;
    [SerializeField] private TextMeshProUGUI timeleft;
    [SerializeField] private TextMeshProUGUI gameoverkeyvalue;
    [SerializeField] private TextMeshProUGUI gameoverText;
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private TextMeshProUGUI gameoverscore;

    [SerializeField] private Slider healthbar;

    [SerializeField] private AudioClip gameSound;
    [SerializeField] private AudioClip themeSound;
    [SerializeField] private AudioClip gameoverSound;
    [SerializeField] private AudioClip gameoverSfx;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private GameOverPopup gameOverPopup;
    [SerializeField] private WelcomePopup welcomePopup;
    [SerializeField] private SceneController sceneController;
    private int popupsActive = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.AddListener(GameEvent.POPUP_OPEN, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSE, OnPopupClosed);
        Messenger<int>.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.RemoveListener(GameEvent.POPUP_OPEN, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSE, OnPopupClosed);
        Messenger<int>.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateKeyCount(keyCount);
        UpdateScore();
        SetPlayerHealth(100);
        SetGameActive(true);
        gameover = false;
        musicSlider.value = SoundManager.Instance.MusicVolume;
        sfxSlider.value = SoundManager.Instance.SfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu") && !optionsPopup.IsActive())
        {
            if (!welcomePopup.IsActive() && !gameOverPopup.IsActive()) 
            {
                optionsPopup.Open();
            }
            
        } 
        else if (Input.GetButtonDown("Menu") && optionsPopup.IsActive())
        {
            optionsPopup.Close();
        }
        if (Time.timeScale > 0)
        {
            UpdateTimer();
        }
        if(Input.GetKeyDown("space") && welcomePopup.IsActive())
        {
            welcomePopup.OnStartButton();
        }
    }

    public void SetGameActive(bool active)
    {
        //Debug.Log("SetGameActive(" + active + ")");
        if (active)
        {
            Time.timeScale = 1.0f;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.visible = true;
        }
    }

    public void SetPlayerHealth(int newHealth)
    {
        playerhealth = newHealth;
        UpdateHealthBar();
    }

    public void UpdateKeyCount(int count)
    {
        keyCount = count;
        keyValue.text = count.ToString();
        gameoverkeyvalue.text = count.ToString();
    }

    public void UpdateTimer()
    {
        float minutes = Mathf.FloorToInt(sceneController.timeRemaining / 60f);
        float seconds = Mathf.FloorToInt(sceneController.timeRemaining % 60f);
        timervalue.text = minutes.ToString() + " : " + seconds.ToString();
        timeleft.text = minutes.ToString() + " : " + seconds.ToString();
    }

    private void UpdateScore()
    {
        scoreValue.text = score.ToString();
        gameoverscore.text = score.ToString();
    }

    private void UpdateHealthBar()
    {
        healthbar.value = playerhealth;
    }


    public void OnMusicVolumeChanged(float value)
    {
        SoundManager.Instance.MusicVolume = value;
    }

    public void OnSfxVolumeChanged(float value)
    {
        SoundManager.Instance.SfxVolume = value;
    }

    private void OnGameOver()
    {
        gameover = true;
        UpdateTimer();
        SetGameActive(false);
        if(keyCount < 5)
        {
            gameoverText.text = "Time out";
            gameoverText.color = Color.red;
            PlayGameOver();
        }
        else if(playerhealth <= 0)
        {
            gameoverText.text = "Dead!";
            gameoverText.color = Color.red;
            PlayGameOver();
        }
        else
        {
            gameoverText.text = "Goal!";
            gameoverText.color = Color.white;
        }
        gameOverPopup.Open();
    }

    private void PlayGameOver()
    {
        SoundManager.Instance.PlaySfx(gameoverSfx);
    }

    private void OnPopupOpened()
    {
        if (popupsActive == 0)
        {
            SetGameActive(false);
            SoundManager.Instance.StopMusic();
            if (gameover)
            {
                SoundManager.Instance.PlayMusic(gameoverSound);
            }
            else
            {
                SoundManager.Instance.PlayMusic(themeSound);
            }            
        }
        popupsActive++;
    }

    private void OnPopupClosed()
    {
        popupsActive--;
        if (popupsActive == 0)
        {
            SetGameActive(true);
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.PlayMusic(gameSound);
        }
    }

    private void OnEnemyDead(int monsterNum)
    {
        score += monsterNum;
        UpdateScore();
    }
}
