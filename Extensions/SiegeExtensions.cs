﻿using System.Linq;
using TaleWorlds.CampaignSystem;

namespace BannerlordCheats.Extensions
{
    public static class SiegeExtensions
    {
        public static bool IsPlayerSide(this ISiegeEventSide side)
        {
            return side?.SiegeParties?.Any(x => x.IsPlayerParty()) ?? false;
        }
    }
}
