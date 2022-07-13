using System;
using Exiled.Events.EventArgs;
using Exiled.API.Features;
using Respawning.NamingRules;
using Respawning;

namespace MtfFreezeFix
{
    public class MtfFreezeFix: Plugin<PluginConfig>
    {
        public override string Name => "MtfFreezeFix";

        public override string Author => "Qidan475";

        public override Version Version { get; } = new Version(1, 0, 0);

        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 2);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RestartingRound += OnRoundRestart;
            Exiled.Events.Handlers.Server.RespawningTeam += OnTeamRespawn;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RestartingRound -= OnRoundRestart;
            Exiled.Events.Handlers.Server.RespawningTeam -= OnTeamRespawn;
            base.OnDisabled();
        }


        private void OnRoundRestart()
        {
            Log.Debug("[UnitNamingRule::UsedCombinations]", Config.Debug);
            foreach (var item in UnitNamingRule.UsedCombinations)
            {
                Log.Debug(item, Config.Debug);
            }
            Log.Debug("[RespawnManager::AllUnitNames]", Config.Debug);
            foreach (var item in RespawnManager.Singleton.NamingManager.AllUnitNames)
            {
                Log.Debug(item.UnitName, Config.Debug);
            }

            UnitNamingRule.UsedCombinations.Clear();
        }

        private void OnTeamRespawn(RespawningTeamEventArgs ev)
        {
            if (ev.NextKnownTeam != SpawnableTeamType.NineTailedFox)
                return;

            int possibleNumbersCount = 19;
            int maxPossibleUnits = NineTailedFoxNamingRule.PossibleCodes.Length * possibleNumbersCount;
            if (RespawnManager.Singleton.NamingManager.AllUnitNames.Count > maxPossibleUnits - 20)
            {
                Log.Warn("That's quite a lot of mtf waves. New waves won't be spawned otherwise server will be borked");
                ev.IsAllowed = false;
            }
        }
    }
}
