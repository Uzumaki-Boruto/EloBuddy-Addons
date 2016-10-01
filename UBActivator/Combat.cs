using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace UBActivator
{
    class Combat
    {
        public static void OnTick()
        {
            if (Config.Combat["Glory"].Cast<CheckBox>().CurrentValue
                && ((Config.Combat["GloryCb"].Cast<CheckBox>().CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                || !Config.Combat["GloryCb"].Cast<CheckBox>().CurrentValue))
            {
                if (Player.Instance.CountEnemiesInRange(Player.Instance.MoveSpeed * 2) < 1
                    || Config.Combat["Glorycountally"].Cast<Slider>().CurrentValue < Player.Instance.CountAlliesInRange(500)) return;

                if (Items.Righteous_Glory != null)
                {
                    if (Items.Righteous_Glory.IsOwned() && Items.Righteous_Glory.IsReady()) Items.Righteous_Glory.Cast();
                }
            }

            if (Config.Combat["Randuin"].Cast<CheckBox>().CurrentValue
                && ((Config.Combat["RanduinCb"].Cast<CheckBox>().CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                || !Config.Combat["RanduinCb"].Cast<CheckBox>().CurrentValue))
            {
                if (Config.Combat["Randuincount"].Cast<Slider>().CurrentValue < Player.Instance.CountEnemiesInRange(500)) return;

                if (Items.Randuin_s_Omen != null)
                {
                    if (Items.Randuin_s_Omen.IsOwned() && Items.Randuin_s_Omen.IsReady()) Items.Randuin_s_Omen.Cast();
                }
            }
        }
    }
}
