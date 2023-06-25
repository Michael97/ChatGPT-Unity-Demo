public interface IPlayerStats
{
    int Health { get; }
    int MaxHealth { get; }
    int Hunger { get; }
    int MaxHunger { get; }
    int Thirst { get; }
    int MaxThirst { get; }
    int Stamina { get; }
    int MaxStamina { get; }
    float Temperature { get; }
    float MinTemperature { get; }
    float MaxTemperature { get; }

    void SetHealth(int value);
    void SetMaxHealth(int value);
    void SetHunger(int value);
    void SetMaxHunger(int value);
    void SetThirst(int value);
    void SetMaxThirst(int value);
    void SetStamina(int value);
    void SetMaxStamina(int value);
    void SetTemperature(float value);

    void TakeDamage(int damage);
    void Heal(int amount);
    void ConsumeFood(int foodValue);
    void ConsumeWater(int waterValue);
    void Rest(int duration);
    void UpdateTemperature(float deltaTemperature);

    string GetStats();

    string GetStatsAsString();
}
