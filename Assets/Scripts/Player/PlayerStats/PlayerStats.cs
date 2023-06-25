using UnityEngine;

public class PlayerStats : MonoBehaviour, IPlayerStats
{
    // Variables for stats
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int hunger = 100;
    [SerializeField]
    private int maxHunger = 100;
    [SerializeField]
    private int thirst = 100;
    [SerializeField]
    private int maxThirst = 100;
    [SerializeField]
    private int stamina = 100;
    [SerializeField]
    private int maxStamina = 100;
    [SerializeField]
    private float temperature = 98.6f;
    [SerializeField]
    private float minTemperature = 95.0f;
    [SerializeField]
    private float maxTemperature = 104.0f;

    // Implement the IPlayerStats properties
    public int Health => health;
    public int MaxHealth => maxHealth;
    public int Hunger => hunger;
    public int MaxHunger => maxHunger;
    public int Thirst => thirst;
    public int MaxThirst => maxThirst;
    public int Stamina => stamina;
    public int MaxStamina => maxStamina;
    public float Temperature => temperature;
    public float MinTemperature => minTemperature;
    public float MaxTemperature => maxTemperature;

    // Implement the IPlayerStats methods
    public void SetHealth(int value) => health = Mathf.Clamp(value, 0, maxHealth);
    public void SetMaxHealth(int value) => maxHealth = Mathf.Max(value, 0);
    public void SetHunger(int value) => hunger = Mathf.Clamp(value, 0, maxHunger);
    public void SetMaxHunger(int value) => maxHunger = Mathf.Max(value, 0);
    public void SetThirst(int value) => thirst = Mathf.Clamp(value, 0, maxThirst);
    public void SetMaxThirst(int value) => maxThirst = Mathf.Max(value, 0);
    public void SetStamina(int value) => stamina = Mathf.Clamp(value, 0, maxStamina);
    public void SetMaxStamina(int value) => maxStamina = Mathf.Max(value, 0);
    public void SetTemperature(float value) => temperature = Mathf.Clamp(value, minTemperature, maxTemperature);

    public void TakeDamage(int damage) => SetHealth(health - damage);
    public void Heal(int amount) => SetHealth(health + amount);
    public void ConsumeFood(int foodValue) => SetHunger(hunger + foodValue);
    public void ConsumeWater(int waterValue) => SetThirst(thirst + waterValue);
    public void Rest(int duration) => SetStamina(stamina + duration);
    public void UpdateTemperature(float deltaTemperature) => SetTemperature(temperature + deltaTemperature);

    public string GetStats()
    {
        var stats = $"Health: {health}/{maxHealth}\n" +
               $"Hunger: {hunger}/{maxHunger}\n" +
               $"Thirst: {thirst}/{maxThirst}\n" +
               $"Stamina: {stamina}/{maxStamina}\n" +
               $"Temperature: {temperature}°F";

        //GameLogger.LogMessage($"GetStats Request: {stats}", LogType.ToChatGpt);

        return stats;
    }

    public string GetStatsAsString()
    {
        var stats = $"Health: {health}/{maxHealth}\n" +
               $"Hunger: {hunger}/{maxHunger}\n" +
               $"Thirst: {thirst}/{maxThirst}\n" +
               $"Stamina: {stamina}/{maxStamina}\n" +
               $"Temperature: {temperature}°F";

        return stats;
    }
}
