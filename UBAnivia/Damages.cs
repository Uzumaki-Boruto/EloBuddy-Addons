using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace UBAnivia
{
    class Damages
    {
        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 60f, 85f, 110f, 135f, 160f }[Spells.Q.Level] + 0.4f * Player.Instance.TotalMagicalDamage);
        }

        public static float EDamage(Obj_AI_Base target)
        {
            if (target.Chilled())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, (new[] { 0f, 55f, 85f, 115f, 145f, 175f }[Spells.E.Level] + 0.5f * Player.Instance.TotalMagicalDamage) * 2);
            else
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 55f, 85f, 115f, 145f, 175f }[Spells.E.Level] + 0.5f * Player.Instance.TotalMagicalDamage);
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 80f, 120f, 160f }[Spells.R.Level] + 0.25f * Player.Instance.TotalMagicalDamage);
        }
        public static float Damagefromspell(Obj_AI_Base target, bool Q, bool E, bool R)
        {
            var damage = 0f;
            if (target == null)
            {
                return damage;
            }
            else if (target != null)
            {
                var Tick = Config.MiscMenu.GetValue("dmg");
                if (Q)
                {
                    damage += QDamage(target);
                }
                if (E)
                {
                    damage += EDamage(target);
                }
                if (R)
                {
                    damage += RDamage(target) * Tick;
                }
                return damage;
            }
            else return damage;
        }
        public static void Damage_Indicator(EventArgs args)
        {
            if (Config.DrawMenu.Checked("draw") && Config.DrawMenu.Checked("dmg"))
            {
                foreach (var unit in EntityManager.Heroes.Enemies.Where(u => u.IsValidTarget() && u.IsHPBarRendered)
                    )
                {

                    var Q = Spells.Q.IsLearned ? Spells.Q.IsReady() : false;
                    var E = Spells.E.IsLearned ? Spells.E.IsReady() : false;
                    var R = Spells.R.IsLearned ? Spells.R.IsReady() : false;
                    var damage = Damagefromspell(unit, Q, E, R);

                    if (damage <= 0)
                    {
                        continue;
                    }
                    var Special_X = unit.ChampionName == "Jhin" || unit.ChampionName == "Annie" ? -12 : 0;
                    var Special_Y = unit.ChampionName == "Jhin" || unit.ChampionName == "Annie" ? -3 : 9;

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
