using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomePopup : BasePopup
{
    public void OnStartButton()
    {
        Close();
        // popupclosed event automatically set game active
    }
}
