using CounterStrikeSharp.API.Core;

namespace CombatSurf;

public partial class CombatSurf
{
  public required CombatSurfConfig Config { get; set; }

  public void OnConfigParsed(CombatSurfConfig config)
  {
    Config = config;
  }
}

public class CombatSurfConfig : BasePluginConfig
{

}
