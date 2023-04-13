using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class UIController : MonoBehaviour
{
    private int keyCount = 0;
    [SerializeField] private TextMeshProUGUI keyValue;
    [SerializeField] private TextMeshProUGUI timervalue;
    [SerializeField] private TextMeshProUGUI timeleft;
    [SerializeField] private TextMeshProUGUI gameoverkeyvalue;
    [SerializeField] private TextMeshProUGUI gameoverText;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private GameOverPopup gameOverPopup;
    [SerializeField] private SceneController sceneController;
    private int popupsActive = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.AddListener(GameEvent.POPUP_OPEN, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSE, OnPopupClosed);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.RemoveListener(GameEvent.POPUP_OPEN, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSE, OnPopupClosed);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateKeyCount(keyCount);
        SetGameActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu") && !optionsPopup.IsActive())
        {
            SetGameActive(false);
            optionsPopup.Open();
        }
        if (Time.timeScale > 0)
        {
            UpdateTimer();
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

    private void OnGameOver()
    {
        UpdateTimer();
        SetGameActive(false);
        if(keyCount < 5)
        {
            gameoverText.text = "Time out";
            gameoverText.color = Color.red;
        }
        else
        {
            gameoverText.text = "Goal!";
            gameoverText.color = Color.white;
        }
        gameOverPopup.Open();
    }

    private void OnPopupOpened()
    {
        if (popupsActive == 0)
        {
            SetGameActive(false);
        }
        popupsActive++;
    }

    private void OnPopupClosed()
    {
        popupsActive--;
        if (popupsActive == 0)
        {
            SetGameActive(true);
        }
    }
}
