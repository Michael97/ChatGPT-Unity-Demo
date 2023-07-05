using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name => m_name;
    [SerializeField] private string m_name;

    public PlayerController Controller => m_controller;
    [SerializeField] private PlayerController m_controller;
    public PlayerStats Stats => m_stats;
    [SerializeField] private PlayerStats m_stats;

    public PlayerInteraction Interaction => m_interaction;
    [SerializeField] private PlayerInteraction m_interaction;

    public Inventory Inventory => m_inventory;
    [SerializeField] private Inventory m_inventory;

    private void Awake()
    {
        m_name = "";
        m_controller = gameObject.AddComponent<PlayerController>();
        m_stats = new PlayerStats();
        m_interaction = new PlayerInteraction();
        m_inventory = new Inventory();
    }
}