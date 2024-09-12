using CounterStrikeSharp.API.Core;

namespace CombatSurf;

public partial class CombatSurf
{
  private void RegisterEvents()
  {
    // Listeners
    RegisterListener<Listeners.OnEntityCreated>(OnEntityCreated);

    // Events
    RegisterEventHandler<EventPlayerHurt>(OnPlayerHurt);
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
