using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UBRyze
{
    class Damages
    {
        public static float BonusMana
        {
            get { return Player.Instance.MaxMana - (392.4f + 52f * Player.Instance.Level); }
        }
        public static float QDamage(Obj_AI_Base target)
        {
            var Damage = new float[] { 0f, 60f, 85f, 110f, 135f, 160f, 185f }[Spells.Q.Level] + 0.45f * Player.Instance.TotalMagicalDamage + 0.03f * BonusMana;
            var Bonus = new[] { 0f, 1.6f, 1.7f, 1.8f, 1.9f, 2f }[Spells.E.Level];
            if (!Player.Instance.HasBuff("RyzeE"))
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, Damage);
            else return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, Damage * Bonus);
        }

        public static float WDamage(Obj_AI_Base target)
        {
            if (Spells.W.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 80f, 100f, 120f, 140f, 160f }[Spells.W.Level] + 0.2f * Player.Instance.TotalMagicalDamage + 0.01f * BonusMana);
            else
                return 0f;
        }

        public static float EDamage(Obj_AI_Base target)
        {
            if (Spells.E.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 50f, 75f, 100f, 125f, 150f }[Spells.E.Level] + 0.3f * Player.Instance.TotalMagicalDamage + 0.02f * BonusMana);
            else
                return 0f;
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return 0f;
        }
        public static float Damagefromspell(Obj_AI_Base target)
        {
            if (target == null)
            {
                return 0;
            }
            else if (target != null)
            {
                switch (Config.MiscMenu["dmg"].Cast<ComboBox>().CurrentValue)
                {                  
                    case 0:
                        return QDamage(target) + WDamage(target) + EDamage(target);
                    case 1:
                        return QDamage(target) * 3 + WDamage(target) + EDamage(target);
                    default:
                        return 0f;
                }
            }
            else return 0f;
        }
        public static void Damage_Indicator(EventArgs args)
        {
            if (Config.DrawMenu["drdamage"].Cast<CheckBox>().CurrentValue)
            {
                foreach (var unit in EntityManager.Heroes.Enemies.Where(u => u.IsValidTarget() && u.IsHPBarRendered)
                    )
                {

                    var damage = Damagefromspell(unit);

                    if (damage <= 0)
                    {
                        continue;
                    }
                    var Special_X = unit.ChampionName == "Jhin" || unit.ChampionName == "Annie" ? -10 : 0;
                    var Special_Y = unit.ChampionName == "Jhin" || unit.ChampionName == "Annie" ? -12 : 9;

                    var DamagePercent = ((unit.TotalShieldHealth() - damage) > 0
                        ? (unit.TotalShieldHealth() - damage)
                        : 0) / (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);
                    var currentHealthPercent = unit.TotalShieldHealth() / (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);

                    var StartPoint = new Vector2((int)(unit.HPBarPosition.X + Special_X + DamagePercent * 107) + 1,
                        (int)unit.HPBarPosition.Y + Special_Y);
                    var EndPoint = new Vector2((int)(unit.HPBarPosition.X + Special_X + currentHealthPercent * 107) + 1,
                        (int)unit.HPBarPosition.Y + Special_Y);
                    var Color = Config.DrawMenu["Color"].Cast<ColorPicker>().CurrentValue;
                    Drawing.DrawLine(StartPoint, EndPoint, 9.82f, Color);

                }
            }
        }
    }
}
