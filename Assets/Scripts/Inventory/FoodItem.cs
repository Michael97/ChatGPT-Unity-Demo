using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item
{

    public FoodItem(string name, int quantity) : base(name, quantity)
    {

    }

    public override void Use(Player player)
    {
        player.Stats.ConsumeFood(10);
        base.Use(player);
    }
}
