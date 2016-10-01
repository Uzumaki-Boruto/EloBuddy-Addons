using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

namespace UBAnivia
{
    class Mode
    {
        private static float LastCastQ;

        #region Combo
        public static void Combo()
        {
            if (Config.ComboMenu.Checked("Q"))
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target != null && target.IsValid)
                {
                    Extension.QTarget = target;
                    var pred = Spells.Q.GetPrediction(target);
                    if (Spells.Q.IsReady() && (!Extension.QActive || LastCastQ < Game.Time - 1.1f) && pred.HitChancePercent >= Config.ComboMenu.GetValue("Qcb"))
                    {
                        Spells.Q.Cast(pred.CastPosition);
                        LastCastQ = Game.Time;
                    }
                }
            }
            if (Config.ComboMenu.Checked("W") && Spells.W.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                if (target != null && target.IsValid())
                {
                    var pred = Spells.W.GetPrediction(target);
                    Spells.W.Cast(pred.CastPosition);
                }
            }
            if (Config.ComboMenu.Checked("E") && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (target != null && target.IsValid())
                {
                    if (Extension.Chilled(target) || !Config.ComboMenu.Checked("chill"))
                    {
                        Spells.E.Cast(target);
                    }
                }

            }
            if (Config.ComboMenu.Checked("R") && Extension.HasR)
            {
                var target = TargetSelector.GetTarget(Spells.R.Range + Spells.R.Radius, DamageType.Magical);
                if (target != null && target.IsValid())
                {
                    var pred = Spells.R.GetPrediction(target);
                    Spells.R.Cast(pred.CastPosition);
                }
            }
        }                                  
        #endregion

        #region Harass
        public static void Harass()
        {
            if (Player.Instance.ManaPercent < Config.HarassMenu.GetValue("hr")) return;
            if (Config.HarassMenu.Checked("Q"))
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target != null && target.IsValid)
                {
                    Extension.QTarget = target;
                    var pred = Spells.Q.GetPrediction(target);
                    if (Spells.Q.IsReady() && (!Extension.QActive || LastCastQ < Game.Time - 1.1f) && pred.HitChancePercent >= Config.ComboMenu.GetValue("Qcb"))
                    {
                        Spells.Q.Cast(pred.CastPosition);
                        LastCastQ = Game.Time;
                    }
                }          
            }
            if (Config.HarassMenu.Checked("E") && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (target != null && target.IsValid())
                {
                    if (Extension.Chilled(target) || !Config.ComboMenu.Checked("chill"))
                    {
                        Spells.E.Cast(target);
                    }
                }              
            }
            if (Config.HarassMenu.Checked("R") && Extension.HasR)
            {
                var target = TargetSelector.GetTarget(Spells.R.Range + Spells.R.Radius, DamageType.Magical);
                if (target != null && target.IsValid())
                {
                    var pred = Spells.R.GetPrediction(target);
                    Spells.R.Cast(pred.CastPosition);
                }
            }
        }
        #endregion

        #region LaneClear
        public static void LaneClear()
        {
            if (Player.Instance.ManaPercent < Config.LaneClear.GetValue("lc")) return;
            if (Config.LaneClear.Checked("Q") && Spells.Q.IsReady() && (!Extension.QActive || LastCastQ < Game.Time - 1.1f))
            {
                var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.Q.Range) && Spells.Q.IsInRange(m)).FirstOrDefault();
                {
                    if (minion != null)
                    {
                        Spells.Q.Cast(minion);
                        LastCastQ = Game.Time;
                    }
                }
            }
            if (Config.LaneClear.Checked("E") && Spells.E.IsReady())
            {
                var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.E.Range) && Spells.E.IsInRange(m)).FirstOrDefault();
                {
                    if (minion != null)
                    {
                        Spells.E.Cast(minion);
                    }
                }
            }
            if (Config.LaneClear.Checked("R") && Extension.HasR)
            {
                var minion = Orbwalker.LaneClearMinionsList;
                var FarmLoc = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(minion, Spells.RMax.Radius, (int)Spells.R.Range + Spells.R.Radius);
                if (minion != null && FarmLoc.CastPosition.CountEnemyMinionsInRange(Spells.RMax.Radius) >= Config.LaneClear.GetValue("Rlc"))
                {
                    Spells.R.Cast(FarmLoc.CastPosition);
                }
            }
        }
        #endregion

        #region JungleClear
        public static void JungleClear()
        {
            if (Player.Instance.ManaPercent < Config.JungleClear.GetValue("jc")) return;
            if (Config.JungleClear.Checked("Q") && Spells.Q.IsReady() && (!Extension.QActive || LastCastQ < Game.Time - 1.1f))
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster == null || !monster.IsValid) return;
                if (Orbwalker.IsAutoAttacking) return;
                Spells.Q.Cast(monster);
                LastCastQ = Game.Time;
            }
            if (Config.JungleClear.Checked("E") && Spells.E.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.E.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster != null)
                {
                    Spells.E.Cast(monster);
                }
            }
            if (Config.JungleClear.Checked("R") && Extension.HasR)
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.R.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster != null)
                {
                    Spells.R.Cast(monster);
                }
            }
        }
        #endregion

        #region On_Unkillable_Minion
        public static void On_Unkillable_Minion(Obj_AI_Base unit, Orbwalker.UnkillableMinionArgs args)
        {
            if (Config.LasthitMenu.GetValue("manage") < Player.Instance.ManaPercent || unit == null
                || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) return;
            if (args.RemainingHealth <= Damages.QDamage(unit) && Spells.Q.IsReady() && Config.LasthitMenu.Checked("Qlh"))
            {
                Spells.Q.Cast(unit);
            }
            if (args.RemainingHealth <= Damages.EDamage(unit) && Spells.E.IsReady() && Config.LasthitMenu.Checked("Elh"))
            {
                Spells.E.Cast(unit);
            }
            if (args.RemainingHealth <= Damages.RDamage(unit) && Extension.HasR && Config.LasthitMenu.Checked("Rlh"))
            {
                Spells.R.Cast(unit);
            }
        }
        #endregion

        #region KillSteal
        public static void Killsteal(EventArgs args)
        {
            if (Spells.Q.IsReady() && Config.MiscMenu.Checked("Qks") && (!Extension.QActive || LastCastQ < Game.Time - 1.1f))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.Q.IsInRange(t)
                    && t.Health <= Damages.QDamage(t)), DamageType.Magical);

                if (target != null && Extension.Unkillable(target) == false)
                {
                    Extension.QTarget = target;
                    var pred = Spells.Q.GetPrediction(target);
                    {
                        Spells.Q.Cast(pred.CastPosition);
                        LastCastQ = Game.Time;
                    }
                }
            }
            if (Spells.E.IsReady() && Config.MiscMenu.Checked("Eks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.W.IsInRange(t)
                    && t.Health <= Damages.EDamage(t)
                    && !Extension.Unkillable(t)), DamageType.Magical);

                if (target != null)
                {
                    Spells.E.Cast(target);
                }
            }
            if (Extension.HasR && Config.MiscMenu.Checked("Rks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.R.IsInRange(t)
                    && t.Health <= Damages.RDamage(t) * Config.MiscMenu.GetValue("dmg")
                    && !Extension.Unkillable(t)), DamageType.Magical);
                if (target != null)
                {
                    Spells.R.Cast(target);
                }
            }
        }
        #endregion

        #region AutoHarass
        public static void AutoHarass(EventArgs args)
        {
            if (Player.Instance.ManaPercent < Config.HarassMenu.GetValue("autohrmng")) return;
            if (!Config.HarassMenu.Checked("keyharass", false)) return;
            if (Config.HarassMenu.Checked("Q"))
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target != null && target.IsValid)
                {
                    Extension.QTarget = target;
                    var pred = Spells.Q.GetPrediction(target);
                    if (Spells.Q.IsReady() && (!Extension.QActive || LastCastQ < Game.Time - 1.1f) && pred.HitChancePercent >= Config.ComboMenu.GetValue("Qcb"))
                    {
                        Spells.Q.Cast(pred.CastPosition);
                        LastCastQ = Game.Time;
                    }
                }
            }
            if (Config.HarassMenu.Checked("E") && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (target != null && target.IsValid())
                {
                    if (Extension.Chilled(target) || !Config.ComboMenu.Checked("chill"))
                    {
                        Spells.E.Cast(target);
                    }
                }
            }
            if (Config.HarassMenu.Checked("R") && Extension.HasR)
            {
                var target = TargetSelector.GetTarget(Spells.R.Range + Spells.R.Radius, DamageType.Magical);
                if (target != null && target.IsValid())
                {
                    var pred = Spells.R.GetPrediction(target);
                    Spells.R.Cast(pred.CastPosition);
                }
            }
        }
        #endregion

        #region Gapclose
        public static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (Spells.Q.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 250)
                && (sender.Spellbook.CastEndTime - Game.Time) * 1000 <= Spells.Q.CastDelay
                && Config.MiscMenu.Checked("Qgap")
                && !Extension.QActive)
            {
                Extension.QTarget = sender;
                Spells.Q.Cast(args.End);
            }
            else if (Spells.W.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 250)
                && sender.Spellbook.CastEndTime <= Spells.W.CastDelay
                && Config.MiscMenu.Checked("Wgap"))
            {
                Spells.W.Cast(Player.Instance.Position.Extend(args.End, Player.Instance.Distance(args.End) - 30f).To3D());
            }

        }
        #endregion

        #region Interupt
        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            var Value = Config.MiscMenu.GetValue("level", false);
            var Danger = Value == 0 ? DangerLevel.High : Value == 1 ? DangerLevel.Medium : Value == 2 ? DangerLevel.Low : DangerLevel.High;
            if (sender.IsEnemy && e.DangerLevel == Danger)
            {
                if (Config.MiscMenu.Checked("Qinter")
                && sender.IsValidTarget(Spells.Q.Range)
                && Spells.Q.IsReady()
                && (!Extension.QActive || LastCastQ < Game.Time - 1.1f))
                {
                    Extension.QTarget = sender as AIHeroClient;
                    Spells.Q.Cast(sender);
                    LastCastQ = Game.Time;
                }
                else if (Config.MiscMenu.Checked("Winter")
                    && sender.IsValidTarget(Spells.W.Range)
                    && Spells.W.IsReady())
                {
                    Spells.W.Cast(sender);
                }
            }
        }
        #endregion 
    }
}
