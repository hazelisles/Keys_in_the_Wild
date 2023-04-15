using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIC : MonoBehaviour
{
    private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<Image>();
        healthBar.fillAmount = 1;
        healthBar.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float healthPercentage)
    {
        healthBar.fillAmount = healthPercentage;
    }
}
