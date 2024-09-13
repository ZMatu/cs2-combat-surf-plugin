using System.Drawing;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using CounterStrikeSharp.API.Modules.Timers;

namespace CombatSurf;

public partial class CombatSurf
{
  private void RegisterEvents()
  {
    // Listeners
    RegisterListener<Listeners.OnMapStart>(OnMapStart);
    RegisterListener<Listeners.OnGameServerSteamAPIActivated>(OnGameServerSteamAPIActivated);
    RegisterListener<Listeners.OnEntityCreated>(OnEntityCreated);


    // Events
    RegisterEventHandler<EventPlayerConnectFull>(OnEventPlayerConnectFull);
    RegisterEventHandler<EventPlayerHurt>(OnEventPlayerHurt);
    RegisterEventHandler<EventRoundStart>(OnEventRoundStart);

    // VirtualFunctions
    VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Hook(PreOnTakeDamage, HookMode.Pre);
  }
  private void UnRegisterEvents()
  {

    DeregisterEventHandler<EventPlayerConnectFull>(OnEventPlayerConnectFull);
    DeregisterEventHandler<EventPlayerHurt>(OnEventPlayerHurt);
    DeregisterEventHandler<EventRoundStart>(OnEventRoundStart);
    VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Unhook(PreOnTakeDamage, HookMode.Pre);
  }

  // Listeners
  private void OnEntityCreated(CEntityInstance entity)
  {
    IncreaseAwpAmmo(entity);
  }

  private void OnMapStart(string mapName)
  {

  }
  private void OnGameServerSteamAPIActivated()
  {

  }

  // Event
  private HookResult OnEventPlayerHurt(EventPlayerHurt @event, GameEventInfo info)
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

    var eventMsg = new EventOnPlayerHurt
    {
      @event = @event,
      info = info
    };
    _eventsManager.Publish(eventMsg);

    return HookResult.Continue;
  }

  private HookResult OnEventPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
  {
    CCSPlayerController? client = @event.Userid;

    if (client == null || !client.IsValid || client.IsBot || !client.UserId.HasValue)
      return HookResult.Continue;

    var eventMsg = new EventOnPlayerConnect
    {
      Name = client.PlayerName,
      Slot = client.Slot,
      SteamId = client.SteamID.ToString()
    };
    _eventsManager.Publish(eventMsg);

    return HookResult.Continue;
  }

  private HookResult OnEventRoundStart(EventRoundStart @event, GameEventInfo info)
  {
    AddTimer(1.0f, () =>
    {
      Server.NextFrame(() =>
      {
        Server.ExecuteCommand("sv_maxvelocity           4000");
      });
    }, TimerFlags.STOP_ON_MAPCHANGE);

    _gunManager.isAllowSelect = true;
    AddTimer(10.0f, () =>
    {
      _gunManager.isAllowSelect = false;
    });

    foreach (var client in GetValidPlayers())
    {
      var player = _playerManager.GetPlayer(client);
      _gunManager.GiveWeapon(client, player.lastGun);
      client.PlayerPawn.Value!.Render = Color.FromArgb(255, 40, 0);
    }

    return HookResult.Continue;
  }

  private HookResult PreOnTakeDamage(DynamicHook hook)
  {
    OneShootAwp(hook);

    return HookResult.Continue;
  }
}
