using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;

namespace UBSyndra
{
    class Mode
    {
        #region Combo
        public static void Combo()
        {
            if (Config.ComboMenu.Checked("Qcb") && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = Spells.Q.GetPrediction(target);
                    Spells.CastQ(pred);
                }
            }
            if (Config.ComboMenu.Checked("Wcb") && Spells.W.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = Spells.W.GetPrediction(target);
                    Spells.Grab();
                    Spells.W.Cast(pred.CastPosition);
                }
            }
            if (Config.ComboMenu.Checked("Ecb") && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = Spells.E.GetPrediction(target);
                    Spells.CastE(pred);
                }
            }
            if (Config.ComboMenu.Checked("QEcb") && Spells.Q.IsReady() && Spells.E.IsReady())
            {
                Spells.ComboEQ();
            }
            if (Spells.R.IsReady() && Config.ComboMenu.Checked("R"))
            {
                var target = TargetSelector.GetTarget(Spells.R.Range, DamageType.Magical);

                if (target != null && !target.HasSpellShield() && Config.ComboMenu.Checked(target.ChampionName) && Damage.Damagefromspell(target) >= target.Health)
                {
                    Spells.R.Cast(target);
                }
            }
        }
        #endregion

        #region Harass
        public static void Harass()
        {
            if (Player.Instance.ManaPercent <= Config.HarassMenu.GetValue("hr")) return;
            if (Config.HarassMenu.Checked("Qhr") && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = Spells.Q.GetPrediction(target);
                    Spells.CastQ(pred);
                }
            }
            if (Config.HarassMenu.Checked("Whr") && Spells.W.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = Spells.W.GetPrediction(target);
                    Spells.Grab();
                    Spells.W.Cast(pred.CastPosition);
                }
            }
            if (Config.HarassMenu.Checked("Ehr") && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = Spells.E.GetPrediction(target);
                    Spells.CastE(pred);
                }
            }
            if (Config.HarassMenu.Checked("QEhr") && Spells.Q.IsReady() && Spells.E.IsReady())
            {
                Spells.ComboEQ();
            }
        }
        #endregion

        #region LaneClear
        public static void LaneClear()
        {
            if (Player.Instance.ManaPercent <= Config.LaneClear.GetValue("lc")) return;
            if (Config.LaneClear.Checked("Qlc") && Spells.Q.IsReady())
            {
                var Worst = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy), Spells.Q.Radius, (int)Spells.Q.Range);
                if (Worst.HitNumber >= Config.LaneClear.GetValue("Qlchit"))
                {
                    Spells.Q.Cast(Worst.CastPosition);
                }
            }
            if (Config.LaneClear.Checked("Wlc") && Spells.W.IsReady())
            {
                var Worst = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy), Spells.W.Radius, (int)Spells.W.Range);
                if (Worst.HitNumber >= Config.LaneClear.GetValue("Wlchit"))
                {
                    Spells.Grab();
                    Spells.W.Cast(Worst.CastPosition);
                }
            }
        }
        #endregion

        #region JungleClear
        public static void JungleClear()
        {
            if (Player.Instance.ManaPercent <= Config.JungleClear.GetValue("jc")) return;
            if (Config.JungleClear.Checked("Qjc") && Spells.Q.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster == null || !monster.IsValid) return;
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                Spells.Q.Cast(monster);               
            }
            if (Config.JungleClear.Checked("Wjc") && Spells.W.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster != null)
                {
                    Spells.Grab();
                    Spells.W.Cast(monster);                
                }
            }
        }
        #endregion

        #region On_Unkillable_Minion
        public static void On_Unkillable_Minion(Obj_AI_Base unit, Orbwalker.UnkillableMinionArgs args)
        {
            if (unit == null || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) || Player.Instance.ManaPercent < Config.Lasthit.GetValue("lh")) return;
            if (Config.Lasthit.Checked("Qlh") && Spells.Q.IsReady() && args.RemainingHealth <= Damage.QDamage(unit))
            {
                Spells.Q.Cast(unit);
            }
            if (Config.Lasthit.Checked("Wlh") && Spells.W.IsReady() && args.RemainingHealth <= Damage.WDamage(unit))
            {
                Spells.Grab();
                Spells.W.Cast(unit);
            }
        }
        #endregion

        #region KillSteal
        public static void Killsteal(EventArgs args)
        {
            if (Spells.Q.IsReady() && Config.MiscMenu.Checked("Qks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.Q.IsInRange(t)
                    && t.Health <= Damage.QDamage(t)), DamageType.Magical);

                if (target != null && !target.Unkillable())
                {
                    var pred = Spells.Q.GetPrediction(target);
                    Spells.CastQ(pred);
                }
            }
            if (Spells.W.IsReady() && Config.MiscMenu.Checked("Wks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.W.IsInRange(t)
                    && t.Health <= Damage.WDamage(t)), DamageType.Magical);

                if (target != null && !target.Unkillable())
                {
                    var pred = Spells.W.GetPrediction(target);
                    Spells.Grab();
                    Spells.W.Cast(pred.CastPosition);

                }
            }
            if (Spells.E.IsReady() && Config.MiscMenu.Checked("Eks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.QE.IsInRange(t)
                    && t.Health <= Damage.EDamage(t)), DamageType.Magical);

                if (target != null && !target.Unkillable() && Spells.E.IsInRange(target))
                {
                    var pred = Spells.E.GetPrediction(target);
                    Spells.CastE(pred);
                }
                if (target != null && !target.Unkillable() && !Spells.E.IsInRange(target))
                {
                    Spells.ComboEQ();
                }
            }
            if (Spells.R.IsReady() && Config.MiscMenu.Checked("Rks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.R.IsInRange(t)
                    && t.Health <= Damage.RDamage(t)), DamageType.Magical);

                if (target != null && !target.Unkillable() && !target.HasSpellShield() && Config.MiscMenu.Checked(target.ChampionName))
                {
                    Spells.R.Cast(target);
                }
            }
        }
        #endregion

        #region AutoHarass
        public static void AutoHarass(EventArgs args)
        {
            if (Player.Instance.ManaPercent <= Config.HarassMenu.GetValue("autohrmng") || !Config.HarassMenu.Checked("keyharass", false)) return;
            if (Config.HarassMenu.Checked("Qhr") && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = Spells.Q.GetPrediction(target);
                    Spells.CastQ(pred);
                }
            }
            if (Config.HarassMenu.Checked("Whr") && Spells.W.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = Spells.W.GetPrediction(target);
                    Spells.Grab();
                    Spells.W.Cast(pred.CastPosition);

                }
            }
            if (Config.HarassMenu.Checked("Ehr") && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = Spells.E.GetPrediction(target);
                    Spells.CastE(pred);
                }
            }
            if (Config.HarassMenu.Checked("QEhr") && Spells.Q.IsReady() && Spells.E.IsReady())
            {
                Spells.ComboEQ();
            }
        }
        #endregion

        #region Gapclose
        public static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (Spells.E.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 200 || args.End.IsInRange(Player.Instance, Spells.E.Range))
                && (sender.Spellbook.CastEndTime - Game.Time) * 1000 <= Spells.E.CastDelay
                && Config.MiscMenu.Checked("gapcloser"))
            {
                Spells.E.Cast(args.End);
            }
        }
        #endregion

        #region Interrupt
        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            var Value = Config.MiscMenu.GetValue("interrupt.level", false);
            var Danger = Value == 2 ? DangerLevel.High : Value == 1 ? DangerLevel.Medium : Value == 0 ? DangerLevel.Low : DangerLevel.High;
            if (sender != null
                && sender.IsEnemy
                && Config.MiscMenu.Checked("interrupt")
                && sender.IsValidTarget(Spells.QE.Range - 20)
                && e.DangerLevel == Danger)
            {
                if (Player.Instance.Distance(sender) <= Spells.E.Range && Spells.E.IsReady())
                {
                    var pred = Spells.E.GetPrediction(sender);
                    Spells.CastE(pred);
                }
                if (Player.Instance.Distance(sender) <= Spells.QE.Range && Spells.Q.IsReady() && Spells.E.IsReady())
                {
                    Spells.ComboEQ();
                }
            }
        }
        #endregion
    }
}
