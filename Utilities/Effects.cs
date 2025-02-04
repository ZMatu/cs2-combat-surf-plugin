using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Vector = CounterStrikeSharp.API.Modules.Utils.Vector;
using QAngle = CounterStrikeSharp.API.Modules.Utils.QAngle;

namespace CombatSurf;
partial class CombatSurf
{

  public void ExplosionEffects(CCSPlayerController player, Vector location)
  {
    var particle = Utilities.CreateEntityByName<CHEGrenadeProjectile>("hegrenade_projectile");
    if (particle == null || !particle.IsValid) return;

    particle.TicksAtZeroVelocity = 100;
    particle.TeamNum = player.Pawn.Value!.TeamNum;
    particle.Damage = 0;
    particle.DmgRadius = 0;
    particle.Teleport(location, new QAngle(0, 0, 0), new Vector(0, 0, -10));
    particle.DispatchSpawn();
    particle.AcceptInput("InitializeSpawnFromWorld", player.PlayerPawn.Value, player.PlayerPawn.Value, "");
    particle.DetonateTime = 0;

    AddTimer(2.0f, () =>
   {
     if (particle != null && particle.IsValid)
     {
       particle.Remove();
     }
   });
  }

  public void KnockbackEffects(CCSPlayerController player, Vector location)
  {
    var particle = Utilities.CreateEntityByName<CBaseEntity>("info_particle_system");
    if (particle == null || !particle.IsValid) return;

    particle.Teleport(location, new QAngle(0, 0, 0), new Vector(0, 0, 20));
    particle.DispatchSpawn();
    particle.AcceptInput("InitializeSpawnFromWorld", player.PlayerPawn.Value, player.PlayerPawn.Value, "");

    AddTimer(2.0f, () =>
      {
        if (particle != null && particle.IsValid)
        {
          particle.Remove();
        }
      });
  }
}
