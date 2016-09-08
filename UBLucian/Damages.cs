using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace UBLucian
{
    class Damages
    {
        public static float PassiveDamage(Obj_AI_Base target)
        {
            if (target is AIHeroClient)
            {
                if (Player.Instance.Level < 6)
                    return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, 1.3f * Player.Instance.TotalAttackDamage);
                else if (Player.Instance.Level < 11)
                    return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, 1.4f * Player.Instance.TotalAttackDamage);
                else if (Player.Instance.Level < 16)
                    return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, 1.5f * Player.Instance.TotalAttackDamage);
                else
                    return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, 1.6f * Player.Instance.TotalAttackDamage);
            }
            else
            {
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, 2f * Player.Instance.TotalAttackDamage);
            }
        }

        public static float QDamage(Obj_AI_Base target)
        {
            if (Spells.Q.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, new float[] { 0f, 80f, 110f, 140f, 170f, 200f }[Spells.Q.Level] + new float[] { 0f, 0.6f, 0.7f, 0.8f, 0.9f, 1f }[Spells.Q.Level] * Player.Instance.FlatPhysicalDamageMod);
            else
                return 0f;
        }

        public static float WDamage(Obj_AI_Base target)
        {
            if (Spells.W.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 60f, 100f, 140f, 180f, 220f }[Spells.W.Level] + 0.9f * Player.Instance.TotalMagicalDamage);
            else
                return 0f;
        }

        public static float EDamage(Obj_AI_Base target)
        {
            if (Spells.E.IsReady())
                return PassiveDamage(target);
            else
                return 0f;
        }

        public static float RDamage(Obj_AI_Base target, int Ticks)
        {
            if (Spells.R.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, new[] { 0f, 20f, 35f, 50f }[Spells.R.Level] + 0.2f * Player.Instance.TotalAttackDamage + 0.1f * Player.Instance.TotalMagicalDamage * Ticks);
            else
                return 0f;
        }

        public static float Damagefromspell(Obj_AI_Base target)
        {
            if (target == null)
            {
                return 0f;
            }
            else
            {
                return QDamage(target) + WDamage(target) + EDamage(target) + RDamage(target, Config.MiscMenu.GetValue("Rkstick"));
            }
        }
        public static void Damage_Indicator(EventArgs args)
        {
            if (Config.DrawMenu.Checked("dmg") && Config.DrawMenu.Checked("draw"))
            {
                foreach (var unit in EntityManager.Heroes.Enemies.Where(u => u.IsValidTarget() && u.IsHPBarRendered)
                    )
                {

                    var damage = Damagefromspell(unit);

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
