using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class UIController : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI keyValue;
    [SerializeField] private TextMeshProUGUI timervalue;
    [SerializeField] private TextMeshProUGUI timeleft;
    [SerializeField] private TextMeshProUGUI gameoverkeyvalue;
    [SerializeField] private TextMeshProUGUI gameoverText;
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private TextMeshProUGUI gameoverscore;

    [SerializeField] private Slider healthbar;

    [SerializeField] private SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void UpdateKeyCount(int count)
    {
        //keyCount = count;
        keyValue.text = count.ToString();
        gameoverkeyvalue.text = count.ToString();
    }

    public void UpdateTimer()
    {
        string minText;
        string secText;
        float minutes = Mathf.FloorToInt(sceneController.timeRemaining / 60f);
        float seconds = Mathf.FloorToInt(sceneController.timeRemaining % 60f);
        if(minutes < 10)
        {
            minText = "0" + minutes.ToString();
        }
        else { minText = minutes.ToString(); }
        if(seconds < 10)
        {
            secText = "0" + seconds.ToString();
        }
        else { secText = seconds.ToString(); }
        timervalue.text = minText + " : " + secText;
        timeleft.text = minText + " : " + secText;
    }

    public void UpdateScore(int score)
    {
        scoreValue.text = score.ToString();
        gameoverscore.text = score.ToString();
    }

    public void UpdateHealthBar(float playerhealth)
    {
        healthbar.value = playerhealth;
    }

    public void UpdateGameOverText(string text, Color color)
    {
        gameoverText.text = text;
        gameoverText.color = color;
    }
    
}
