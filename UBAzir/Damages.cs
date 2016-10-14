using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UBAzir
{
    class Damages
    {
        // 65 / 85 / 105 / 125 / 145 (+ 50% AP)
        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 65f, 85f, 105f, 125f, 145f }[Spells.Q.Level] + 0.5f * Player.Instance.TotalMagicalDamage);
        }

        public static float WDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 50f, 55f, 60f, 65f, 70f, 75f, 80f, 85f, 90f, 95f, 100f, 110f, 120f, 130f, 140f, 150f, 160f, 170f }[Player.Instance.Level] + 0.6f * Player.Instance.TotalMagicalDamage, true, true);
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 60f, 90f, 120f, 150f, 180f }[Spells.E.Level] + 0.4f * Player.Instance.TotalMagicalDamage);
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 150f, 225f, 300f }[Spells.R.Level] + 0.6f * Player.Instance.TotalMagicalDamage);
        }

        public static float Damagefromspell(Obj_AI_Base target, bool Q, bool W, bool E, bool R)
        {
            if (target == null)
            {
                return 0;
            }
            var damage = 0f;
            if (Q)
            {
                damage = damage + QDamage(target);
            }
            if (W)
            {
                damage = damage + WDamage(target);
            }
            if (E)
            {
                damage = damage + EDamage(target);
            }
            if (R)
            {
                damage = damage + RDamage(target);
            }
            return damage;
        }
        public static void Damage_Indicator(EventArgs args)
        {
            if (Config.DrawMenu.Checked("drawdamage"))
            {
                foreach (var unit in EntityManager.Heroes.Enemies.Where(u => u.IsValidTarget() && u.IsHPBarRendered && !Extension.Unkillable(u))
                    )
                {
                    var Q = Spells.Q.IsLearned ? Spells.Q.IsReady() : false;
                    var W = Spells.W.IsLearned ? Spells.W.IsReady() || Orbwalker.ValidAzirSoldiers.Any(s => s.CountEnemiesInRange(375) > 1) : false;
                    var E = Spells.E.IsLearned ? Spells.E.IsReady() : false;
                    var R = Spells.R.IsLearned ? Spells.R.IsReady() : false;

                    var damage = Damagefromspell(unit, Q, W, E, R);

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
