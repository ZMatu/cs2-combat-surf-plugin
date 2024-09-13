using CounterStrikeSharp.API.Core;

namespace CombatSurf;

public class EventOnTickEvent
{

}

public class EventOnPlayerHurt
{
  public required EventPlayerHurt @event { get; init; }
  public required GameEventInfo info { get; init; }
}


public class EventOnPlayerConnect
{
  public required string SteamId { get; init; }
  public required int Slot { get; init; }
  public required string Name { get; init; }
}

public class EventOnPlayerDisconnect
{
  public required string SteamId { get; init; }
  public required int Slot { get; init; }
  public required string Name { get; init; }
}
