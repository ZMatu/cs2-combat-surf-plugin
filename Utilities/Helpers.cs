using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace CombatSurf;

partial class CombatSurf
{
  private void SetHitEffetct(CCSPlayerController client)
  {
    client.PlayerPawn.Value!.HealthShotBoostExpirationTime = Server.CurrentTime + 1;
    Utilities.SetStateChanged(client.PlayerPawn.Value, "CCSPlayerPawn", "m_flHealthShotBoostExpirationTime");
    client.ExecuteClientCommand($"play sounds\\weapons\\flashbang\\flashbang_explode1_distant.vsnd_c");
  }
}
