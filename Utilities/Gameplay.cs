using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory;

namespace CombatSurf;
partial class CombatSurf
{
  public void IncreaseAwpAmmo(CEntityInstance entity)
  {
    if (entity == null || entity.Entity == null || !entity.IsValid || !entity.DesignerName.Contains("weapon_awp"))
      return;

    Server.NextFrame(() =>
    {
      CBasePlayerWeapon weapon = new(entity.Handle);

      if (!weapon.IsValid) return;

      CCSWeaponBase _weapon = weapon.As<CCSWeaponBase>();
      if (_weapon == null) return;

      if (_weapon.VData != null)
      {
        _weapon.VData.MaxClip1 = 10;
        _weapon.VData.DefaultClip1 = 10;
      }
      _weapon.Clip1 = 10;

      Utilities.SetStateChanged(weapon.As<CCSWeaponBase>(), "CBasePlayerWeapon", "m_iClip1");
    });
  }

  public void OneShootAwp()
  {
    VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Hook(h =>
    {
      var victim = h.GetParam<CEntityInstance>(0);
      var damageInfo = h.GetParam<CTakeDamageInfo>(1);

      // AWP weapon index 5
      if (damageInfo.AmmoType == 5)
      {
        damageInfo.Damage = 1337;
        damageInfo.ShouldBleed = false;
      }

      return HookResult.Continue;
    }, HookMode.Pre);
  }
}
