using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using Colour = System.Drawing.Color;

namespace UBNidalee
{
    class Damage
    {
        private const int BarWidth = 106;
        public static float QHumanDamage(Obj_AI_Base target)
        {
            var dt = target.Distance(Player.Instance);
            var extraDmg2 = 0;

            for (double i = 0; i < (dt); i++)
            {
                i = i + 3.875;
                extraDmg2 += 1;
            }

            var finalExtra2 = extraDmg2 <= 250f ? extraDmg2 / 100f : 2.5f;

            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
                (new[] { 0, 60, 77.5f, 95, 112.5f, 130 }[Spells.Q.Level] +
                 (Player.Instance.TotalMagicalDamage * (0.4f)))) * finalExtra2;
        }
        public static float QCougarDamage(Obj_AI_Base target)
        {
            var missingHealth = (target.MaxHealth - target.Health) / 100f;
            var extraDmg = 0f;
            var bonus = new[] { 0f, 1f, 1.25f, 1.5f, 1.75f }[Spells.R.Level];
            for (var i = 0; i < missingHealth; i++)
            {
                extraDmg += bonus;
            }
            var finalExtra = extraDmg <= bonus * 100 ? (extraDmg / 100f) : bonus;

            if (Event.IsPassive(target))
            {
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Mixed,
                    (new[] { 0f, 5.3f, 26.7f, 66.7f, 120f }[Spells.R.Level] +
                     (Player.Instance.TotalAttackDamage * finalExtra) +
                     (Player.Instance.TotalMagicalDamage * (0.48f * finalExtra))));
            }
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Mixed,
                (new[] { 0f, 4f, 20f, 50f, 90f }[Spells.R.Level] +
                 (Player.Instance.TotalAttackDamage * (0.75f * finalExtra)) +
                 (Player.Instance.TotalMagicalDamage * (0.36f * finalExtra))));
        }
        public static float WHumanDamage(Obj_AI_Base target)
        {

            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
                   new[] { 0, 10, 20, 30, 40, 50 }[Spells.W.Level] + (Player.Instance.TotalMagicalDamage * 0.05f));

        }
        public static float WCougarDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
               new[] { 0, 60, 110, 160, 210 }[Spells.R.Level] + (Player.Instance.TotalMagicalDamage * 0.3f));
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
                new[] { 0, 70, 130, 190, 250 }[Spells.R.Level] + (Player.Instance.TotalMagicalDamage * 0.45f));
        }
        public static float SmiteDamage(Obj_AI_Base target)
        {
            if (target.IsValidTarget() && Spells.Smite.IsReady())
            {
                if (target is AIHeroClient)
                {
                    if (SmiteManager.CanUseOnChamp)
                    {
                        return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Smite);
                    }
                }
                else
                {
                    return (int)(new int[] { 390, 410, 430, 450, 480, 510, 540, 570, 600, 640, 680, 720, 760, 800, 850, 900, 950, 1000 }[Player.Instance.Level]);
                }
            }
            return 0;
        }
        public static float DamageinHumanform(Obj_AI_Base target, bool Q, bool W)
        {
            if (target == null)
            {
                return 0;
            }
            var damage = 0f;
            if (Q)
            {
                damage = damage + QHumanDamage(target);
            }
            if (W)
            {
                damage = damage + WHumanDamage(target);
            }
            return damage;
        }
        public static float DamageinCougarform(Obj_AI_Base target, bool Q, bool W, bool E)
        {
            if (target == null)
            {
                return 0;
            }
            var damage = 0f;
            if (Q)
            {
                damage = damage + QCougarDamage(target);
            }
            if (W)
            {
                damage = damage + WCougarDamage(target);
            }
            if (E)
            {
                damage = damage + EDamage(target);
            }
            return damage;
        }

        public static void Damage_Indicator(EventArgs args)
        {
            if (Config.DrawMenu["drawdamage"].Cast<CheckBox>().CurrentValue)
            {
                foreach (var unit in EntityManager.Heroes.Enemies.Where(u => u.IsValidTarget() && u.IsHPBarRendered && !Event.Unkillable(u))
                    )
                {

                    var Q = Spells.Q.IsLearned ? Event.IsReady(Event.CD["Javelintoss"]) : false;
                    var Q2 = Spells.Q2.IsLearned ? Event.IsReady(Event.CD["Takedown"]) : false;
                    var W = Spells.W.IsLearned ? Event.IsReady(Event.CD["Bushwhack"]) : false;
                    var W2 = Spells.W2.IsLearned ? Event.IsReady(Event.CD["Pounce"]) : false;
                    var E = Spells.E2.IsLearned ? Event.IsReady(Event.CD["Swipe"]) : false;

                    var damage = DamageinHumanform(unit, Q, W);
                    var damage2 = DamageinCougarform(unit, Q2, W2, E);

                    if (damage <= 0 && damage2 <= 0)
                    {
                        continue;
                    }
                    var damagePercentinHuman = ((unit.TotalShieldHealth() - damage) > 0
                        ? (unit.TotalShieldHealth() - damage)
                        : 0) / (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);
                    var damagePercentinCougar = ((unit.TotalShieldHealth() - damage2 - damage) > 0
                       ? (unit.TotalShieldHealth() - damage2 - damage)
                       : 0) / (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);
                    var currentHealthPercent = unit.TotalShieldHealth() / (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);

                    var startPoint = new Vector2((int)(unit.HPBarPosition.X + damagePercentinCougar * BarWidth) + 1,
                       (int)unit.HPBarPosition.Y + 9);
                    var midPoint = new Vector2((int)(unit.HPBarPosition.X + damagePercentinHuman * BarWidth) + 1,
                        (int)unit.HPBarPosition.Y + 9);
                    var endPoint = new Vector2((int)(unit.HPBarPosition.X + currentHealthPercent * BarWidth) + 1,
                        (int)unit.HPBarPosition.Y + 9);

                    Drawing.DrawLine(startPoint, endPoint, 9.8f, Colour.Gold);
                    Drawing.DrawLine(midPoint, endPoint, 10f, Colour.LimeGreen);

                }
            }
        }
    }
}
