using UnityEngine;

public class PlayerStats : MonoBehaviour, IPlayerStats
{
    // Variables for stats
    [SerializeField]
    private int m_health = 100;
    [SerializeField]
    private int m_maxHealth = 100;
    [SerializeField]
    private int m_hunger = 100;
    [SerializeField]
    private int m_maxHunger = 100;
    [SerializeField]
    private int m_thirst = 100;
    [SerializeField]
    private int m_maxThirst = 100;
    [SerializeField]
    private int m_stamina = 100;
    [SerializeField]
    private int m_maxStamina = 100;
    [SerializeField]
    private float m_temperature = 98.6f;
    [SerializeField]
    private float m_minTemperature = 95.0f;
    [SerializeField]
    private float m_maxTemperature = 104.0f;

    // Implement the IPlayerStats properties
    public int Health => m_health;
    public int MaxHealth => m_maxHealth;
    public int Hunger => m_hunger;
    public int MaxHunger => m_maxHunger;
    public int Thirst => m_thirst;
    public int MaxThirst => m_maxThirst;
    public int Stamina => m_stamina;
    public int MaxStamina => m_maxStamina;
    public float Temperature => m_temperature;
    public float MinTemperature => m_minTemperature;
    public float MaxTemperature => m_maxTemperature;

    // Implement the IPlayerStats methods
    public void SetHealth(int value) => m_health = Mathf.Clamp(value, 0, m_maxHealth);
    public void SetMaxHealth(int value) => m_maxHealth = Mathf.Max(value, 0);
    public void SetHunger(int value) => m_hunger = Mathf.Clamp(value, 0, m_maxHunger);
    public void SetMaxHunger(int value) => m_maxHunger = Mathf.Max(value, 0);
    public void SetThirst(int value) => m_thirst = Mathf.Clamp(value, 0, m_maxThirst);
    public void SetMaxThirst(int value) => m_maxThirst = Mathf.Max(value, 0);
    public void SetStamina(int value) => m_stamina = Mathf.Clamp(value, 0, m_maxStamina);
    public void SetMaxStamina(int value) => m_maxStamina = Mathf.Max(value, 0);
    public void SetTemperature(float value) => m_temperature = Mathf.Clamp(value, m_minTemperature, m_maxTemperature);

    public void TakeDamage(int damage) => SetHealth(m_health - damage);
    public void Heal(int amount) => SetHealth(m_health + amount);
    public void ConsumeFood(int foodValue) => SetHunger(m_hunger + foodValue);
    public void ConsumeWater(int waterValue) => SetThirst(m_thirst + waterValue);
    public void Rest(int duration) => SetStamina(m_stamina + duration);
    public void UpdateTemperature(float deltaTemperature) => SetTemperature(m_temperature + deltaTemperature);

    public string GetStats()
    {
        var stats = $"Health: {m_health}/{m_maxHealth}\n" +
               $"Hunger: {m_hunger}/{m_maxHunger}\n" +
               $"Thirst: {m_thirst}/{m_maxThirst}\n" +
               $"Stamina: {m_stamina}/{m_maxStamina}\n" +
               $"Temperature: {m_temperature}�F";

        return stats;
    }

    public string GetStatsAsString()
    {
        var stats = $"Health: {m_health}/{m_maxHealth}\n" +
               $"Hunger: {m_hunger}/{m_maxHunger}\n" +
               $"Thirst: {m_thirst}/{m_maxThirst}\n" +
               $"Stamina: {m_stamina}/{m_maxStamina}\n" +
               $"Temperature: {m_temperature}�F";

        return stats;
    }
}
