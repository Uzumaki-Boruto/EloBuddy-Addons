using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UBVeigar
{
    class Damage
    {
        public static float QDamage(Obj_AI_Base target)
        {
            if (Spells.Q.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 70f, 110f, 150f, 190f, 230f }[Spells.Q.Level] + 0.6f * Player.Instance.TotalMagicalDamage);
            else
                return 0f;
        }

        public static float WDamage(Obj_AI_Base target)
        {
            if (Spells.W.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 100f, 150f, 200f, 250f, 300f }[Spells.W.Level] + Player.Instance.TotalMagicalDamage);
            else
                return 0f;
        }

        public static float RDamage(Obj_AI_Base target, float Qadd = 0f)
        {
            var MissingHealth = ((target.MaxHealth - target.Health) - Qadd) / 100f;
            var bonus = 0f;
            for (int i = 0; i <= MissingHealth; i++)
            {
                bonus += 1.5f;
            }
            var ExtraDmg = MissingHealth > 33f ? (bonus + 100f / 100f) : 2f;
            if (Spells.R.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 175f, 250f, 325f }[Spells.R.Level] * ExtraDmg + 0.75f * Player.Instance.TotalMagicalDamage * ExtraDmg);
            else
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
                return QDamage(target) + WDamage(target) + RDamage(target, QDamage(target));
            }
            else return 0f;
        }
        public static void Damage_Indicator(EventArgs args)
        {
            if (Config.DrawMenu.Checked("drdamage") && Config.DrawMenu.Checked("draw"))
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
