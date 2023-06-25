using System;
using UnityEngine;

public class MockGetStatsAction : GetStatsAction
{
    public MockGetStatsAction(IPlayerStats playerStats) : base(playerStats)
    {
    }

}
