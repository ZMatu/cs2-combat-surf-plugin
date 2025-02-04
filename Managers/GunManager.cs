using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace CombatSurf.Managers;

public class GunManager
{
    public void RemoveWeapon(CCSPlayerController client, gear_slot_t slot)
    {
        foreach (var weapon in client!.PlayerPawn.Value!.WeaponServices!.MyWeapons)
        {
            if (!weapon.IsValid || weapon.Value == null ||
                !weapon.Value.IsValid || !weapon.Value.DesignerName.Contains("weapon_"))
                continue;

            CCSWeaponBaseGun gun = weapon.Value.As<CCSWeaponBaseGun>();

            if (weapon.Value.Entity == null) continue;
            if (!weapon.Value.OwnerEntity.IsValid) continue;
            if (gun.Entity == null) continue;
            if (!gun.IsValid) continue;
            if (!gun.VisibleinPVS) continue;

            try
            {
                CCSWeaponBaseVData? weaponData = weapon.Value.As<CCSWeaponBase>().VData;

                if (weaponData == null) continue;
                if (weaponData.GearSlot == slot)
                    weapon.Value.Remove();
            }
            catch (Exception ex)
            {
                CombatSurf._logger!.LogWarning(ex.Message);
                continue;
            }
        }
    }

    public bool GiveWeapon(CCSPlayerController client, string weapon)
    {
        if (!(client is { PawnIsAlive: true, IsBot: false }))
        {
            client.Print("Only alive players can call this command");
            return false;
        }

        RemoveWeapon(client, gear_slot_t.GEAR_SLOT_RIFLE);
        client.GiveNamedItem(weapon);

        return true;
    }

    public bool GivePlayerWeapon(Player player, string weapon)
    {
        if (!(player.Client is { PawnIsAlive: true, IsBot: false }))
        {
            player.Client.Print("Only alive players can call this command");
            return false;
        }

        if ((Server.CurrentTime - player.SpawnAt) >= 10.0f)
        {
            player.Client.Print("Selection time has expired");
            return false;
        }

        RemoveWeapon(player.Client, gear_slot_t.GEAR_SLOT_RIFLE);
        player.Client.GiveNamedItem(weapon);

        return true;
    }
}
