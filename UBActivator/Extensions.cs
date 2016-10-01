using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace UBActivator
{
    public enum HextechItem
    {
        Ice, Fire, Gunblade
    }
    public static class Extensions
    {
        public static bool IsImportant(Obj_AI_Minion minion)
        {
            return minion.IsValidTarget()
                && (minion.Name.ToLower().Contains("baron")
                || minion.Name.ToLower().Contains("dragon")
                || minion.Name.ToLower().Contains("herald"));
        }
        public static bool CanUseOnChamp
        {
            get
            {
                if (Spells.Smite != null && Spells.Smite.IsReady())
                {
                    var name = Spells.Smite.ToString().ToLower();
                    if (name.Contains("smiteduel") || name.Contains("smiteplayerganker"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public static byte lv = 1;
        public static int QOff = 0, WOff = 0, EOff = 0, ROff;
        public static bool ChampNoMana
        {
            get
            {
                if (Player.Instance.ChampionName == "Aatrox"
                    || Player.Instance.ChampionName == "Akali"
                    || Player.Instance.ChampionName == "DrMundo"
                    || Player.Instance.ChampionName == "Kennen"
                    || Player.Instance.ChampionName == "Katarina"
                    || Player.Instance.ChampionName == "Leesin"
                    || Player.Instance.ChampionName == "Mordekaiser"
                    || Player.Instance.ChampionName == "Reksai"
                    || Player.Instance.ChampionName == "Rengar"
                    || Player.Instance.ChampionName == "Riven"
                    || Player.Instance.ChampionName == "Rumble"
                    || Player.Instance.ChampionName == "Shen"
                    || Player.Instance.ChampionName == "Tryndamere"
                    || Player.Instance.ChampionName == "Vladimir"
                    || Player.Instance.ChampionName == "Yasuo"
                    || Player.Instance.ChampionName == "Zed")
                    return true;
                else
                    return false;
           }
        }
        public static bool Unkillable(this AIHeroClient target)
        {
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "UndyingRage"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "ChronoShift"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "JudicatorIntervention"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "kindredrnodeathbuff"))
            {
                return true;
            }
            if (target.HasBuffOfType(BuffType.Invulnerability))
            {
                return true;
            }
            return target.IsInvulnerable;
        }
    }
}
