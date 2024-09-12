using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using Microsoft.Extensions.Logging;

namespace CombatSurf;

public partial class CombatSurf
{
  private void RegisterEvents()
  {
    // Listeners
    RegisterListener<Listeners.OnEntityCreated>(OnEntityCreated);

    // Events
    RegisterEventHandler<EventPlayerHurt>(OnPlayerHurt);
    RegisterEventHandler<EventPlayerShoot>(OnPlayerShoot);
    RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
  }

  private HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
  {
    Logger.LogInformation("NICE1");
    CCSPlayerController? player = @event.Userid;
    if (player is null || !player.IsValid || !player.PlayerPawn.IsValid)
      return HookResult.Continue;

    player.PlayerPawn.Value!.HealthShotBoostExpirationTime = Server.CurrentTime + 1;
    Utilities.SetStateChanged(player.PlayerPawn.Value, "CCSPlayerPawn", "m_flHealthShotBoostExpirationTime");

    return HookResult.Continue;
  }

  private HookResult OnPlayerShoot(EventPlayerShoot @event, GameEventInfo info)
  {
    // @event!.Userid!.PlayerPawn!.Value!.CBodyComponent!.SceneNode.GetSkeletonInstance().Scale += 0.01f;
    // Utilities.SetStateChanged(@event!.Userid.PlayerPawn.Value, "CBaseEntity", "m_CBodyComponent");

    var Player = @event!.Userid!;


    // DispatchEffect(new BloodlustEffect(Player, _bloodlustLength));
    Logger.LogInformation("NICE2");

    return HookResult.Continue;
  }


  private void OnEntityCreated(CEntityInstance entity)
  {
    IncreaseAwpAmmo(entity);
  }

  private HookResult OnPlayerHurt(EventPlayerHurt @event, GameEventInfo info)
  {
    CCSPlayerController player = @event.Userid!;
    CCSPlayerController attacker = @event.Attacker!;

    if (!player.IsValid || player.Connected != PlayerConnectedState.PlayerConnected || !player.PlayerPawn.IsValid)
      return HookResult.Continue;

    player.PlayerPawn.Value!.VelocityModifier = 1;

    if (@event.Weapon == "awp" && @event.Health == 0)
    {
      SetHitEffetct(attacker);
    }

    return HookResult.Continue;
  }
}
