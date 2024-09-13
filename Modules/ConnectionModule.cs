using CounterStrikeSharp.API;
using Microsoft.Extensions.Logging;
using CombatSurf.Managers;
using CounterStrikeSharp.API.Modules.Utils;

namespace CombatSurf;

public class ConnectionModule(PlayerManager playerManager)
{
    private readonly PlayerManager _playerManager = playerManager;

    public void OnPlayerConnect(EventOnPlayerConnect e)
    {
        CombatSurf._logger!.LogInformation("OnPlayerConnect");
        _ = Task.Run(() =>
        {
            try
            {
                // string query = @"
                //     INSERT INTO public.""user"" (steamid, username) 
                //     VALUES (@steamid, @username) 
                //     ON CONFLICT (steamid) 
                //     DO UPDATE SET username = EXCLUDED.username
                //     RETURNING id;
                // ";
                // var parameters = new NpgsqlParameter[]
                // {
                //     new("@steamid", e.SteamId),
                //     new("@username", e.Name)
                // };

                // int userId = await _database.ExecuteAsync(query, parameters);

                var player = new Player(e.Slot, e.SteamId, e.Name);
                _playerManager.AddPlayer(player);

                Server.NextFrame(() =>
                {
                    player.Client.Print($" {ChatColors.Gold}Здравствуй воин!");
                    player.Client.Print($" {ChatColors.Grey}To select a weapon you can write: {ChatColors.Purple} !awp {ChatColors.White} / {ChatColors.Purple} !nova");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        });
    }

    public void OnPlayerDisconnect(EventOnPlayerDisconnect e)
    {
        if (e.SteamId == null)
            return;

        var client = Utilities.GetPlayerFromSlot(e.Slot);

        if (client != null)
            _playerManager.RemovePlayer(client);
    }
}
