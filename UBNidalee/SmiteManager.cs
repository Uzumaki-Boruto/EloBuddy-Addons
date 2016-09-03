using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;


namespace UBNidalee
{
    class SmiteManager
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
        public static void Init()
        {
            if (Spells.Smite != null)
            {
                Game.OnTick += Game_OnTick;
            }
        }
        public static void Game_OnTick(EventArgs agrs)
        {
            var Important = Config.MiscMenu["smjc"].Cast<CheckBox>().CurrentValue;
            var minion = ObjectManager.Get<Obj_AI_Minion>().Where(m => m.IsMonster && m.IsValidTarget(Spells.Smite.Range) && IsImportant(m)).OrderBy(x => x.MaxHealth).LastOrDefault();
            if (minion != null && minion.IsValid && Important)
            {
                if (minion.Health <= Damage.SmiteDamage(minion))
                {
                    Spells.Smite.Cast(minion);
                }
            }
        }
    }
}
