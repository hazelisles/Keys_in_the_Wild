using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopup : MonoBehaviour
{
    [SerializeField] private UIController ui;
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
    public void OnRestartButton()
    {
        Debug.Log("Restart!");
    }
    public void OnExitGameButton()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }
    public void OnReturnToGameButton()
    {
        Debug.Log("Return");
        Close();
        ui.SetGameActive(true);
    }
}
