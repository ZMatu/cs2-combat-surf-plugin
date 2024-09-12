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

    if (!player.IsValid || player.Connected != PlayerConnectedState.PlayerConnected || !player.PlayerPawn.IsValid)
      return HookResult.Continue;

    player.PlayerPawn.Value!.VelocityModifier = 1;

    return HookResult.Continue;
  }
}


// VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Hook(h =>
//     {
//       var victim = h.GetParam<CEntityInstance>(0);
// var damageInfo = h.GetParam<CTakeDamageInfo>(1);

// Logger.LogInformation($"DAMAGE {damageInfo.AmmoType} {damageInfo.Damage} {damageInfo.NumObjectsPenetrated}");

//       if (damageInfo.AmmoType == 5)
//       {
//         damageInfo.Damage = 5;
//       }
//       else
// {
//   if (damageInfo.Damage < 100 && damageInfo.NumObjectsPenetrated < 1)
//   {
//     damageInfo.Damage = 100;
//   }
// }
// damageInfo.ShouldBleed = true;

// return HookResult.Continue;
//     }, HookMode.Pre);
//   }

//   private void OnEntityCreated(CEntityInstance entity)
// {
//   if (entity == null || entity.Entity == null || !entity.IsValid || !entity.DesignerName.Contains("weapon_awp"))
//     return;

//   Server.NextFrame(() =>
//   {
//     CBasePlayerWeapon weapon = new(entity.Handle);

//     if (!weapon.IsValid) return;

//     CCSWeaponBase _weapon = weapon.As<CCSWeaponBase>();
//     if (_weapon == null) return;

//     if (_weapon.VData != null)
//     {
//       _weapon.VData.MaxClip1 = 10;
//       _weapon.VData.DefaultClip1 = 10;
//     }
//     _weapon.Clip1 = 10;

//     Utilities.SetStateChanged(weapon.As<CCSWeaponBase>(), "CBasePlayerWeapon", "m_iClip1");
//   });
// }

