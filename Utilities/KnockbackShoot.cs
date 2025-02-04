using CounterStrikeSharp.API.Core;
using Vector = CounterStrikeSharp.API.Modules.Utils.Vector;

namespace CombatSurf;
partial class CombatSurf
{
  private readonly Dictionary<int, DateTime> _lastKnockbackTime = new();

  public void ApplyKnockback(CCSPlayerController player, Vector impactPos)
  {
    const float KNOCKBACK_FORCE = 400.0f;
    const float VERTICAL_BOOST = 100.0f;
    const float MAX_DISTANCE = 200.0f;

    var pawn = player.Pawn.Value!;
    var playerPos = pawn.AbsOrigin!;
    var direction = playerPos - impactPos;
    float distance = direction.Length();

    if (distance <= MAX_DISTANCE)
    {
      // Нормализуем вектор направления
      direction.X /= distance;
      direction.Y /= distance;
      direction.Z /= distance;

      // Создаем вектор отбрасывания
      var newVelocity = new Vector(
          pawn.AbsVelocity!.X + (direction.X * KNOCKBACK_FORCE),
          pawn.AbsVelocity!.Y + (direction.Y * KNOCKBACK_FORCE),
          pawn.AbsVelocity!.Z + (direction.Z * KNOCKBACK_FORCE + VERTICAL_BOOST)
      );

      // Применяем новую скорость
      pawn.Teleport(null, null, newVelocity);
    }
  }

  public bool CanApplyKnockback(CCSPlayerController player)
  {
    if (!player.UserId.HasValue)
      return false;

    if (!_lastKnockbackTime.TryGetValue(player.UserId.Value, out DateTime lastTime))
    {
      _lastKnockbackTime[player.UserId.Value] = DateTime.Now;
      return true;
    }

    if ((DateTime.Now - lastTime).TotalSeconds >= 1.0)
    {
      _lastKnockbackTime[player.UserId.Value] = DateTime.Now;
      return true;
    }

    return false;
  }
}
