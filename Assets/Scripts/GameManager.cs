using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private int keyCount = 0;
    private int score = 0;
    public float playerhealth { get; private set; } = 100;
    private bool gameover = false;
    private int popupsActive = 0;
    //private int enemyChaseActive = 0;

    [SerializeField] private AudioClip gameSound;
    [SerializeField] private AudioClip themeSound;
    [SerializeField] private AudioClip battleSound;
    [SerializeField] private AudioClip gameoverSound;
    [SerializeField] private AudioClip gameoverSfx;
    [SerializeField] private AudioClip collect;
    [SerializeField] private AudioClip slimeOugh;
    [SerializeField] private AudioClip turtleOugh;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private GameOverPopup gameOverPopup;
    [SerializeField] private WelcomePopup welcomePopup;
    
    [SerializeField] private UIController ui;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.AddListener(GameEvent.POPUP_OPEN, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSE, OnPopupClosed);
        Messenger<int>.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.AddListener(GameEvent.ENEMY_HURT, OnEnemyHurt);
        Messenger<float>.AddListener(GameEvent.PLAYER_HEALTH_CHANGE, OnPlayerHealthChange);
        Messenger.AddListener(GameEvent.COLLECT_KEY, OnCollectKey);
        Messenger.AddListener(GameEvent.BATTLE_SOUND_ON, OnBattleSoundOn);
        Messenger.AddListener(GameEvent.BATTLE_SOUND_OFF, OnBattleSoundOff);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.RemoveListener(GameEvent.POPUP_OPEN, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSE, OnPopupClosed);
        Messenger<int>.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.RemoveListener(GameEvent.ENEMY_HURT, OnEnemyHurt);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_HEALTH_CHANGE, OnPlayerHealthChange);
        Messenger.RemoveListener(GameEvent.COLLECT_KEY, OnCollectKey);
        Messenger.RemoveListener(GameEvent.BATTLE_SOUND_ON, OnBattleSoundOn);
        Messenger.RemoveListener(GameEvent.BATTLE_SOUND_OFF, OnBattleSoundOff);
    }

    // Start is called before the first frame update
    void Start()
    {
        ui.UpdateKeyCount(keyCount);
        ui.UpdateScore(score);
        ui.UpdateHealthBar(playerhealth);
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
            ui.UpdateTimer();
        }
        if (Input.GetKeyDown("space") && welcomePopup.IsActive())
        {
            welcomePopup.OnStartButton();
        }

        EnemyManager.Instance.battleSound();
    }
    public void SetGameActive(bool active)
    {
        //Debug.Log("SetGameActive(" + active + ")");
        if (active)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Cursor.visible = true;
        }
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
        ui.UpdateTimer();
        SetGameActive(false);
        if (keyCount >= 5 && score >= 10)
        {
            ui.UpdateGameOverText("Goal!", Color.white);
            
        }
        else if (playerhealth <= 0)
        {
            ui.UpdateGameOverText("Dead!", Color.red);
            PlayGameOver();
        }
        else
        {
            ui.UpdateGameOverText("Time out", Color.red);
            PlayGameOver();
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
        ui.UpdateScore(score);
        if (keyCount >= 5 && score >= 10)
        {
            Messenger.Broadcast(GameEvent.GAME_OVER);
        }
    }

    private void OnEnemyHurt(int monsterNum)
    {
        switch (monsterNum)
        {
            case 1:
                SoundManager.Instance.PlaySfx(slimeOugh);
                break;
            case 2:
                SoundManager.Instance.PlaySfx(turtleOugh);
                break;
        }
    }

    private void OnPlayerHealthChange(float point)
    {
        playerhealth -= point;
        ui.UpdateHealthBar(playerhealth);
    }

    private void OnCollectKey()
    {
        SoundManager.Instance.PlaySfx(collect);
        keyCount++;
        ui.UpdateKeyCount(keyCount);
        if (keyCount >= 5 && score >= 10)
        {
            Messenger.Broadcast(GameEvent.GAME_OVER);
        }
    }

    private void OnBattleSoundOn()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayMusic(battleSound);
    }
    private void OnBattleSoundOff()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayMusic(gameSound);
    }
}
