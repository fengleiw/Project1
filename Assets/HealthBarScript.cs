using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    public Slider HealthBar;
    Damageable damageablePlayer;

    // Start is called before the first frame update
    private void Awake()
    {
       GameObject player = GameObject.FindGameObjectWithTag("Player");
       damageablePlayer = player.GetComponent<Damageable>();
    }
    void Start()
    {
        
        HealthBar.value = CalculateSliderPercentage(damageablePlayer.Health, damageablePlayer.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        damageablePlayer.HealthChange.AddListener(OnPlayerHealthChange);
    }
    private void OnPlayerHealthChange(int newHealth, int maxHealth)
    {
        HealthBar.value = CalculateSliderPercentage(newHealth, maxHealth);
    }
    private void OnDisable()
    {
        damageablePlayer.HealthChange.RemoveListener(OnPlayerHealthChange);
    }
    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
}
