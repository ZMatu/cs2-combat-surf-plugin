using CounterStrikeSharp.API.Core;

namespace CombatSurf.Managers;

public class PlayerManager
{
  private readonly List<Player> _players = new();

  public void AddPlayer(Player player)
  {
    _players.Add(player);
  }

  public void RemovePlayer(CCSPlayerController client)
  {
    var playerToRemove = _players.FirstOrDefault(p => p.Client.Index == client.Index);
    if (playerToRemove != null)
    {

      _players.Remove(playerToRemove);
    }
  }

  public Player GetPlayer(CCSPlayerController client)
  {
    return _players.FirstOrDefault(p => p.Client.Index == client.Index)!;
  }

  public List<Player> GetPlayerList()
  {
    return _players;
  }

  public void RemoveAll()
  {
    _players.Clear();
  }
}
