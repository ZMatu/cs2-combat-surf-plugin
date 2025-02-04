using CounterStrikeSharp.API;

namespace CombatSurf;
partial class CombatSurf
{
  public void SetupServerSettings()
  {
    Server.NextFrame(() =>
    {
      Server.ExecuteCommand("mp_roundtime             1.5");
      Server.ExecuteCommand("sv_friction              4");
      Server.ExecuteCommand("sv_accelerate            10");
      Server.ExecuteCommand("sv_air_max_wishspeed     40");
      Server.ExecuteCommand("sv_airaccelerate         9999");
      Server.ExecuteCommand("mp_round_restart_delay   4");
      Server.ExecuteCommand("sv_maxvelocity           4000");
      Server.ExecuteCommand("sv_maxspeed              400");
      Server.ExecuteCommand("sv_wateraccelerate       2000");
      Server.ExecuteCommand("sv_stopspeed             100");
      Server.ExecuteCommand("sv_falldamage_scale      0");
      Server.ExecuteCommand("sv_enablebunnyhopping    true");
      Server.ExecuteCommand("sv_autobunnyhopping      true");
      Server.ExecuteCommand("sv_staminajumpcost       0");
      Server.ExecuteCommand("sv_staminalandcost       0");
      Server.ExecuteCommand("sv_staminarecoveryrate   0");
      Server.ExecuteCommand("sv_staminamax            0");
    });
  }
}
