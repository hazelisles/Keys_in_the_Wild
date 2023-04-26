using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopup : BasePopup
{
    [SerializeField] private GameManager gm;
    override public void Open()
    {
        base.Open();
    }
    override public void Close()
    {
        base.Close();
    }

    public void OnRestartButton()
    {
        Debug.Log("Restart!");
        Messenger.Broadcast(GameEvent.GAME_RESTART);
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
        gm.SetGameActive(true);
    }
}
