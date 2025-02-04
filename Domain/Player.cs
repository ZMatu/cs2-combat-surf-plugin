using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace CombatSurf;

public class Player
{
  public class PlayerInfo
  {
    public required int Slot { get; init; }
    public required string SteamId { get; init; }
    public required string Name { get; init; }
  }

  public Player(int Slot, string SteamId, string Name)
  {
    Info = new PlayerInfo
    {
      Slot = Slot,
      SteamId = SteamId,
      Name = Name
    };
  }

  public readonly PlayerInfo Info;

  // Guns
  public string LastSelectedGun { get; set; } = "weapon_awp";
  public float? SpawnAt { get; set; } = null;

  public CCSPlayerController Client => Utilities.GetPlayerFromSlot(Info.Slot)!;
}
