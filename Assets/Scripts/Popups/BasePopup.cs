using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    virtual public void Open()
    {
        if (!IsActive())
        {
            this.gameObject.SetActive(true);
            Debug.Log(this.gameObject.name + " OPEN");
            Messenger.Broadcast(GameEvent.POPUP_OPEN);
        }
        else
        {
            Debug.LogError(this + ".Open() - trying to open a popup that is active!");
        }
    }

    virtual public void Close()
    {
        if (IsActive())
        {
            this.gameObject.SetActive(false);
            Messenger.Broadcast(GameEvent.POPUP_CLOSE);
        }
        else
        {
            Debug.LogError(this + ".Close() - trying to close a popup that is inactive!");
        }
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
