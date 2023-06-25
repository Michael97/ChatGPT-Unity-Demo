using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; private set; }
    public PlayerController Controller { get; private set; }
    public PlayerStats Stats { get; private set; }
    public PlayerInteraction Interaction { get; private set; }
    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        Name = "";
        Controller = gameObject.AddComponent<PlayerController>();
        Stats = new PlayerStats();
        Interaction = new PlayerInteraction();
        Inventory = new Inventory();
    }
}