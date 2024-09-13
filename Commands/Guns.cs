using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;

namespace CombatSurf;

partial class CombatSurf
{

  [ConsoleCommand("nova", "Give nova weapon")]
  [CommandHelper(whoCanExecute: CommandUsage.CLIENT_ONLY)]
  public void CmdNova(CCSPlayerController client, CommandInfo _)
  {
    var weapon = "weapon_nova";
    var player = _playerManager.GetPlayer(client);
    var isSuccess = _gunManager.GiveWeapon(client, weapon);

    if (isSuccess)
      player.lastGun = weapon;
  }

  [ConsoleCommand("awp", "Give awp weapon")]
  [CommandHelper(whoCanExecute: CommandUsage.CLIENT_ONLY)]
  public void CmdAwp(CCSPlayerController client, CommandInfo _)
  {
    var weapon = "weapon_awp";
    var player = _playerManager.GetPlayer(client);
    var isSuccess = _gunManager.GiveWeapon(client, weapon);

    if (isSuccess)
      player.lastGun = weapon;
  }
}
