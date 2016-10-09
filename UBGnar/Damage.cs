using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Linq;

namespace UBGnar
{
    class Damage
    {
        public static float QDamage(Obj_AI_Base target)
        {
            var RawDamage = Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, new[] { 0f, 5f, 35f, 65f, 95f, 125f }[Spells.QTiny.Level] + 1.15f * Player.Instance.TotalAttackDamage);
            var RawDamage2 = Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, new[] { 0f, 5f, 45f, 85f, 125f, 165f }[Spells.QMega.Level] + 1.2f * Player.Instance.TotalAttackDamage);            
            if (Spells.QTiny.IsReady())
                return Extension.IsTiny ? RawDamage : RawDamage2;
            else
                return 0f;
        }

        public static float WDamage(Obj_AI_Base target)
        {
            var floatheath = new[] { 0f, 0.06f, 0.08f, 0.1f, 0.12f, 0.14f }[Spells.WMega.Level];
            var RawDamage = target.HasWTinyBuff() ?
                Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 10f, 20f, 30f, 40f, 50f }[Spells.WMega.Level] + floatheath * target.MaxHealth + Player.Instance.TotalMagicalDamage) : 0;           
            var RawDamage2 = Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, new[] { 0f, 25f, 45f, 65f, 85f, 105f }[Spells.WMega.Level] + Player.Instance.TotalAttackDamage);
            return Extension.IsTiny ? RawDamage : Spells.WMega.IsReady() ? RawDamage2 : 0f;
        }

        public static float EDamage(Obj_AI_Base target)
        {
            if (Spells.ETiny.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, new[] { 0f, 20f, 60f, 100f, 140f, 160f }[Spells.ETiny.Level] + 0.06f * Player.Instance.MaxHealth);
            else
                return 0f;
        }

        public static float RDamage(Obj_AI_Base target)
        {
            if (Spells.R.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, new[] { 0f, 200f, 300f, 400f }[Spells.R.Level] + 0.2f * Player.Instance.FlatPhysicalDamageMod + 0.5f * Player.Instance.TotalMagicalDamage);
            else
                return 0f;
        }
        public static float Damagefromspell(Obj_AI_Base target)
        {
            if (target != null)
            {
                return QDamage(target) + WDamage(target) + EDamage(target) + RDamage(target);
            }
            else return 0f;
        }
        public static void Damage_Indicator(EventArgs args)
        {
            if (!Config.DrawMenu.Checked("draw")) return;
            if (Config.DrawMenu.Checked("dmg"))
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
