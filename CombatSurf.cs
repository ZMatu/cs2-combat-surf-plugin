using CombatSurf.Managers;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace CombatSurf;

[MinimumApiVersion(260)]
public partial class CombatSurf : BasePlugin, IPluginConfig<CombatSurfConfig>
{
  public override string ModuleName => "CombatSurf";
  public override string ModuleDescription => "CombatSurf";
  public override string ModuleAuthor => "injurka";
  public override string ModuleVersion => "0.2.4";


  private static PlayerManager _playerManager = new();
  private static GunManager _gunManager = new();
  private static EventManager _eventsManager = new();


  internal static ILogger? _logger;

  public override void Load(bool hotReload)
  {
    _logger = Logger;

    _playerManager = new PlayerManager();
    _gunManager = new GunManager();
    _eventsManager = new EventManager();

    SubscribeEvents();
    RegisterEvents();

    if (hotReload)
    {
      _logger!.LogInformation($"HotReloaded");
    }
  }

  public override void Unload(bool hotReload)
  {
    if (hotReload) return;

    UnRegisterEvents();
  }
}
