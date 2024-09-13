using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace CombatSurf.Managers;

public class GunManager
{
    public bool isAllowSelect { get; set; } = true;

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

                CombatSurf._logger!.LogInformation($"AAAAAAAA {weaponData.GearSlot == slot} {weaponData.Name}");

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

        if (!isAllowSelect)
        {
            client.Print("Selection time has expired");
            return false;
        }

        RemoveWeapon(client, gear_slot_t.GEAR_SLOT_RIFLE);
        client.GiveNamedItem(weapon);

        return true;
    }
}
