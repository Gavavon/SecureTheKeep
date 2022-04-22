using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateMechanics : MonoBehaviour
{
    public float currentGateHealth;
    public float maxGateHealth;

    public Image HealthBarImage;

    public RectTransform bar;

    public Transform healthBar;
    public Transform player;

    public static GateMechanics instance;
    private void Awake()
    {
        instance = this;
        Physics.IgnoreLayerCollision(gameObject.layer, 0, true);
    }

    public void Update()
	{
		healthBar.LookAt(player.position);
        updateHealthBar();
    }
    public void updateHealthBar() 
    {
        if (currentGateHealth < (0.2f * maxGateHealth))
        {
            HealthBarImage.color = Color.red;
        }
        else if (currentGateHealth < (0.4f * maxGateHealth))
        {
            HealthBarImage.color = Color.yellow;
        }
        else
        {
            HealthBarImage.color = Color.green;
        }

        bar.sizeDelta = new Vector2(currentGateHealth / 20, 0.5f);
    }
    [ContextMenu("Do damage")]
    public void takeDamage(float damage) 
    {
        currentGateHealth -= damage;
        if (currentGateHealth <= 0) 
        {
            gameObject.SetActive(false);
        }
    }
    [ContextMenu("Do heal")]
    public void healGate()
    {
        int tempMinus;
        if (currentGateHealth <= 0)
        {
            return;
        }
        else 
        {
            float temp = maxGateHealth * 0.25f;
            if (currentGateHealth + (int)temp > maxGateHealth)
            {
                tempMinus = (int)((currentGateHealth + (int)temp) - maxGateHealth);
            }
            else 
            {
                tempMinus = 0;
            }
            currentGateHealth += (int)temp - tempMinus;
        }
    }
    [ContextMenu("Do Big heal")]
    public void healTenWaveGate()
    {
        int tempMinus;
        if (currentGateHealth <= 0)
        {
            gameObject.SetActive(true);
            float temp = maxGateHealth * 0.50f;
            currentGateHealth += (int)temp;
            return;
        }
        else 
        {
            float temp = maxGateHealth * 0.50f;
            if (currentGateHealth + (int)temp > maxGateHealth)
            {
                tempMinus = (int)((currentGateHealth + (int)temp) - maxGateHealth);
            }
            else 
            {
                tempMinus = 0;
            }
            currentGateHealth += (int)temp - tempMinus;
        }
    }

}
