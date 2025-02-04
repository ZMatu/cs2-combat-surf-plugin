using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;

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

      var awp = weapon.As<CWeaponAWP>();

      if (weapon == null) return;

      if (awp.VData != null)
      {
        awp.VData.Range = 100000.0f;
        awp.VData.RangeModifier = 1.0f;
        awp.VData.AttackMovespeedFactor = 1;
        awp.VData.CrosshairDeltaDistance = 1;
        awp.VData.CrosshairMinDistance = 9999;
        awp.VData.MaxClip1 = 10;
        awp.VData.DefaultClip1 = 10;
      }
      awp.Clip1 = 10;

      Utilities.SetStateChanged(weapon.As<CCSWeaponBase>(), "CBasePlayerWeapon", "m_iClip1");
    });
  }

  public void OneShootAwp(DynamicHook hook)
  {
    var victim = hook.GetParam<CEntityInstance>(0);
    var damageInfo = hook.GetParam<CTakeDamageInfo>(1);

    // AWP weapon index 5
    if (damageInfo.AmmoType == 5)
    {
      damageInfo.Damage = 1337;
      damageInfo.ShouldBleed = false;
    }
  }
}
