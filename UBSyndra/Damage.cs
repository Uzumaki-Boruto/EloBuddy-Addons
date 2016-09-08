using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;


namespace UBSyndra
{
    class Damage
    {
        public static float QDamage(Obj_AI_Base target)
        {
            if (Spells.Q.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 50f, 95f, 140f, 185f, 264f }[Spells.Q.Level] + 0.75f * Player.Instance.TotalMagicalDamage);
            else
                return 0f;
        }

        public static float WDamage(Obj_AI_Base target)
        {
            if (Spells.W.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 80f, 120f, 160f, 200f, 240f }[Spells.W.Level] + 0.8f * Player.Instance.TotalMagicalDamage);
            else
                return 0f;
        }
        public static float EDamage(Obj_AI_Base target)
        {
            if (Spells.E.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 70f, 115f, 160f, 205f, 250f }[Spells.E.Level] + 0.5f * Player.Instance.TotalMagicalDamage);
            else
                return 0f;
        }

        public static float RDamage(Obj_AI_Base target)
        {
            int Ball = Player.GetSpell(SpellSlot.R).Ammo;
            if (Spells.R.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 90f, 135f, 180f }[Spells.R.Level] * Ball + 0.2f * Player.Instance.TotalMagicalDamage * Ball);
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
                return QDamage(target) + WDamage(target) + EDamage(target) + RDamage(target);
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
