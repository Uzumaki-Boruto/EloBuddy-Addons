using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBZilean
{
    static class Extension
    {
        public static int GetValue(this Menu menu, string id, bool IsSlider = true)
        {
            if (IsSlider)
                return menu[id].Cast<Slider>().CurrentValue;
            else
                return menu[id].Cast<ComboBox>().CurrentValue;
        }
        public static bool Checked(this Menu menu, string id, bool IsCheckBox = true)
        {
            if (IsCheckBox)
                return menu[id].Cast<CheckBox>().CurrentValue;
            else
                return menu[id].Cast<KeyBind>().CurrentValue;
        }
        public static bool HasQBuff(this Obj_AI_Base target)
        {
            if (target.IsEnemy)
            {
                return target.HasBuff("ZileanQEnemyBomb");
            }
            else
            {
                return target.HasBuff("ZileanQAllyBomb");
            }
        }
        public static bool HasEBuff(this Obj_AI_Base target)
        {
            return target.HasBuff("TimeWarp");
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
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName.ToLower() == "kindredrnodeathbuff"))
            {
                return true;
            }
            if (target.HasBuffOfType(BuffType.Invulnerability))
            {
                return true;
            }
            return target.IsInvulnerable;
        }
        public static bool IsActive(this Enum Flag)
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Flag);
        }
        public static AIHeroClient[] Superman
        {
            get
            {
                return EntityManager.Heroes.Allies.Where(x => Spells.Q.IsInRange(x) && x.HasQBuff()).ToArray();
            }
        }
        public static AIHeroClient[] Batman
        {
            get
            {
                return EntityManager.Heroes.Enemies.Where(x => Spells.Q.IsInRange(x) && x.HasQBuff()).ToArray();
            }
        }
        public static AIHeroClient[] League_Of_Bomber
        {
            get
            {
                return EntityManager.Heroes.AllHeroes.Where(x => Spells.Q.IsInRange(x) && x.HasQBuff()).ToArray();
            }
        }
    }
}
