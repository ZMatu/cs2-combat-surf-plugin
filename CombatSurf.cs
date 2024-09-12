using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;

namespace CombatSurf;

[MinimumApiVersion(260)]
public partial class CombatSurf : BasePlugin, IPluginConfig<CombatSurfConfig>
{
  public override string ModuleName => "CombatSurf";
  public override string ModuleDescription => "CombatSurf";
  public override string ModuleAuthor => "injurka";
  public override string ModuleVersion => "0.0.1";

  public override void Load(bool hotReload)
  {
    RegisterListener<Listeners.OnEntityCreated>(entity =>
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
    });
  }
}
