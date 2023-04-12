using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    //[SerializeField] private UIController ui;
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

    //public void OnRestartButton()
    //{
    //    Debug.Log("Restart!");
    //    Messenger.Broadcast(GameEvent.GAME_RESTART);
    //}
}
