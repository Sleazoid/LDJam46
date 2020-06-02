using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBarScript : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    private float decreaseAmount;
    private float healthPercent = 1.0f;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = healthBar.canvas;
    }

    public void InitHealthBarValues(float health)
    {

        decreaseAmount = 1f / health;
        healthBar.fillAmount=(healthPercent);
    }

    public void UpdateHealthBar()
    {
        healthPercent -= decreaseAmount;
        healthBar.fillAmount=(healthPercent);
    }

    public void DisableHealthBar()
    {
        if(canvas.gameObject)
        {
            Destroy(canvas.gameObject);
        }
    
    }
}
