using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private int keyCount = 0;
    [SerializeField] private TextMeshProUGUI keyValue;
    // Start is called before the first frame update
    void Start()
    {
        UpdateKeyCount(keyCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateKeyCount(int count)
    {
        keyValue.text = count.ToString();
    }
}
