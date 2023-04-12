using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private int keyCount = 0;
    [SerializeField] private TextMeshProUGUI keyValue;
    [SerializeField] private OptionsPopup optionsPopup;
    // Start is called before the first frame update
    void Start()
    {
        UpdateKeyCount(keyCount);
        SetGameActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && !optionsPopup.IsActive())
        {
            SetGameActive(false);
            optionsPopup.Open();
        }
    }

    public void SetGameActive(bool active)
    {
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
        keyValue.text = count.ToString();
    }
}
