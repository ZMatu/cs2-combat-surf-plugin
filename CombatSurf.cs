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
    RegisterEvents();
    OneShootAwp();
  }
}
