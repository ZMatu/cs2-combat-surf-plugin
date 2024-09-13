namespace CombatSurf;

public partial class CombatSurf
{
  private static void SubscribeEvents()
  {
    var connectionHandler = new ConnectionModule(_playerManager);

    _eventsManager.Subscribe<EventOnPlayerConnect>(connectionHandler.OnPlayerConnect);
    _eventsManager.Subscribe<EventOnPlayerDisconnect>(connectionHandler.OnPlayerDisconnect);
  }
}
