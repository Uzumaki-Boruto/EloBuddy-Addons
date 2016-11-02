using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;

namespace UBNidalee
{
    class Mode
    {
        //Combo
        #region Combo
        public static void Combo()
        {
            if (Event.Humanform)
            {
                var Qhitchance = Config.QMenu["hitQcb"].Cast<Slider>().CurrentValue;
                if (Config.QMenu["Qcb"].Cast<CheckBox>().CurrentValue
                    && Spells.Q.IsReady())
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    if (target != null)
                    {
                        var Qgp = Spells.Q.GetPrediction(target);
                        if (target.IsValidTarget() && Qgp.HitChancePercent >= Qhitchance && Qgp.CollisionObjects.Length == 0)
                        {
                            Spells.Q.Cast(Qgp.UnitPosition);
                        }
                    }
                }
                if (Config.WMenu["Wcb"].Cast<CheckBox>().CurrentValue
                   && Spells.W.IsReady())
                {
                    var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                    if (target != null)
                    {
                        var Wgp = Spells.W.GetPrediction(target);
                        if (target.IsValidTarget())
                        {
                            Spells.W.Cast(Wgp.UnitPosition);
                        }
                    }
                }
                if (Config.RMenu["Rform"].Cast<ComboBox>().CurrentValue == 0)
                {
                    var target = TargetSelector.GetTarget(Spells.W2p.Range, DamageType.Mixed);
                    if (target != null)
                    {
                        if (!Spells.Q.IsReady() && (!Spells.W.IsReady() || !Config.WMenu["Wcb"].Cast<CheckBox>().CurrentValue) && Spells.R.IsReady())
                        {
                            Spells.R.Cast();
                            Orbwalker.ResetAutoAttack();
                        }
                    }
                }
            }
            if (!Event.Humanform)
            {
                if (Config.EMenu["E2cb"].Cast<CheckBox>().CurrentValue
                    && Spells.E2.IsReady())
                {
                    var target = TargetSelector.GetTarget(Spells.E2.Range, DamageType.Magical);
                    if (target != null && target.IsValidTarget() && Spells.E2.IsReady())
                    {
                        Spells.E2.Cast(target.Position);
                    }
                }
                if (Config.QMenu["Q2cb"].Cast<CheckBox>().CurrentValue && Config.QMenu["Q2aa"].Cast<CheckBox>().CurrentValue)
                {
                    var target = TargetSelector.GetTarget(Player.Instance.GetAutoAttackRange(), DamageType.Magical);
                    if (Spells.Q2.IsReady()
                        && target != null
                        && target.IsValidTarget()
                        && Spells.Q2.Cast())
                        Orbwalker.ResetAutoAttack();
                    Player.IssueOrder(GameObjectOrder.AttackTo, target);
                }
                if (Config.QMenu["Q2cb"].Cast<CheckBox>().CurrentValue && !Config.QMenu["Q2aa"].Cast<CheckBox>().CurrentValue)
                {
                    Spells.Q2.Cast();
                    Orbwalker.ResetAutoAttack();
                }
                if (Config.RMenu["Rform"].Cast<ComboBox>().CurrentValue <= 1)
                {
                    if (Config.RMenu["Rform"].Cast<ComboBox>().CurrentValue == 1)
                    {
                        var AttackTarget = TargetSelector.GetTarget(Player.Instance.GetAutoAttackRange(), DamageType.Physical);
                        if (AttackTarget == null)
                        {
                            if (!Spells.Q2.IsReady() && !Spells.W2.IsReady() && !Spells.E2.IsReady() && Spells.R.IsReady())
                            {
                                Spells.R.Cast();
                                Orbwalker.ResetAutoAttack();
                            }
                        }
                    }
                    else
                    {
                        if (!Spells.Q2.IsReady() && !Spells.W2.IsReady() && !Spells.E2.IsReady() && Spells.R.IsReady())
                        {
                            Spells.R.Cast();
                            Orbwalker.ResetAutoAttack();
                        }
                    }
                }
            }
            if (Config.MiscMenu["smcb"].Cast<CheckBox>().CurrentValue && SmiteManager.CanUseOnChamp)
            {
                var target = TargetSelector.GetTarget(Spells.Smite.Range, DamageType.True);
                if (target != null && target.IsValidTarget() && Spells.Smite.IsReady())
                {
                    Spells.Smite.Cast(target.Position);
                }
            }
            //W Cougar logic
            #region W Cougar logic
            if (!Event.Humanform && Config.WMenu["W2cb"].Cast<ComboBox>().CurrentValue > 0)
            {
                if (Config.WMenu["W2cb"].Cast<ComboBox>().CurrentValue == 1)
                {
                    var target = TargetSelector.GetTarget(Spells.W2.Range, DamageType.Magical);
                    if (target != null && target.IsValidTarget() && Spells.W2.IsReady() && Player.Instance.CountEnemiesInRange(200) == 0)
                    {
                        Spells.W2.Cast(target.Position);
                    }
                    var target2 = TargetSelector.GetTarget(Spells.W2p.Range, DamageType.Magical);
                    if (target2 != null && target2.IsValidTarget() && Event.IsPassive(target2) && Spells.W2p.IsReady())
                    {
                        Spells.W2p.Cast(target2.Position);
                    }
                }
                if (Config.WMenu["W2cb"].Cast<ComboBox>().CurrentValue == 2)
                {
                    var target = TargetSelector.GetTarget(Spells.W2.Range, DamageType.Magical);
                    if (target != null
                    && target.IsValidTarget()
                    && Event.IsPassive(target)
                    && Spells.W2p.IsReady())
                    {
                        Spells.W2p.Cast(target.Position);
                    }
                }
                if (Config.WMenu["W2cb"].Cast<ComboBox>().CurrentValue == 3)
                {
                    var target = TargetSelector.GetTarget(Spells.W2p.Range, DamageType.Magical);
                    var priority = TargetSelector.GetPriority(target);
                    if (target != null)
                    {
                        if (Event.IsPassive(target))
                        {
                            if (priority >= 3
                            && target != null
                            && target.IsValidTarget()
                            && !Event.Unkillable(target)
                            && !Event.HasSpellShield(target)
                            && Spells.W2p.IsReady())
                            {
                                if (target.CountEnemiesInRange(800) <= 2)
                                {
                                    Spells.W2p.Cast(target.Position);
                                }
                                if (target.CountEnemiesInRange(800) >= 3)
                                {
                                    if (target.Health <= (Damage.DamageinCougarform
                                    (target,
                                    Event.IsReady(Event.CD["Takedown"]),
                                    Event.IsReady(Event.CD["Pounce"]),
                                    Event.IsReady(Event.CD["Swipe"])))
                                    + Damage.WCougarDamage(target)
                                    + Player.Instance.TotalAttackDamage * 2
                                    + Damage.QHumanDamage(target))
                                    {
                                        Spells.W2p.Cast(target.Position);
                                    }
                                }
                            }
                            if (priority < 3
                            && target != null
                            && target.IsValidTarget()
                            && !Event.Unkillable(target)
                            && !Event.HasSpellShield(target)
                            && Spells.W2p.IsReady())
                            {
                                if (target.CountEnemiesInRange(800) == 1)
                                {
                                    Spells.W2p.Cast(target.Position);
                                }
                                if (target.CountEnemiesInRange(800) >= 2)
                                {
                                    if (target.Health <= (Damage.DamageinCougarform
                                    (target,
                                    Event.IsReady(Event.CD["Takedown"]),
                                    Event.IsReady(Event.CD["Pounce"]),
                                    Event.IsReady(Event.CD["Swipe"])))
                                    + Damage.WCougarDamage(target)
                                    + Player.Instance.TotalAttackDamage * 2
                                    + Damage.QHumanDamage(target))
                                    {
                                        Spells.W2p.Cast(target.Position);
                                    }
                                }
                            }
                        }
                        if (!Event.IsPassive(target))
                        {
                            var target2 = TargetSelector.GetTarget(Spells.W2.Range, DamageType.Magical);
                            if (priority >= 4)
                            {
                                if (target2 != null && target2.IsValidTarget()
                                && !Event.Unkillable(target)
                                && !Event.HasSpellShield(target)
                                && Spells.W2.IsReady()
                                && Player.Instance.IsInRange(target2, 200))
                                {
                                    if (target2.CountEnemiesInRange(800) == 1)
                                    {
                                        Spells.W2.Cast(target2.Position);
                                    }
                                    if (target2.CountEnemiesInRange(800) >= 2
                                    && target2.Health <= (Damage.DamageinCougarform
                                    (target2,
                                    Event.IsReady(Event.CD["Takedown"]),
                                    Event.IsReady(Event.CD["Pounce"]),
                                    Event.IsReady(Event.CD["Swipe"])))
                                    + Player.Instance.TotalAttackDamage * 2
                                    + Damage.QHumanDamage(target))
                                    {
                                        Spells.W2.Cast(target2.Position);
                                    }
                                }
                            }
                            if (priority <= 3)
                            {
                                if (target2 != null
                                && target2.IsValidTarget()
                                && !Event.Unkillable(target)
                                && !Event.HasSpellShield(target)
                                && Spells.W2.IsReady()
                                && Player.Instance.IsInRange(target2, 200)
                                && (target2.Health <= (Damage.DamageinCougarform
                                (target2,
                                Event.IsReady(Event.CD["Takedown"]),
                                Event.IsReady(Event.CD["Pounce"]),
                                Event.IsReady(Event.CD["Swipe"])))
                                + Player.Instance.TotalAttackDamage * 2
                                || (target2.Health <= (Damage.DamageinCougarform
                                (target2,
                                Event.IsReady(Event.CD["Takedown"]),
                                Event.IsReady(Event.CD["Pounce"]),
                                Event.IsReady(Event.CD["Swipe"])))
                                + Damage.WCougarDamage(target2)
                                + Player.Instance.TotalAttackDamage * 3
                                + Damage.QHumanDamage(target2)
                                && target2.CountEnemiesInRange(1000) <= 2)))
                                {
                                    Spells.W2.Cast(target2.Position);
                                }
                            }
                        }
                    }
                }
            }
            #endregion                 
        }
        #endregion
        //Harass
        #region Harass
        public static void Harass()
        {
            if (Event.Humanform)
            {
                var Qhitchance = Config.QMenu["hitQhr"].Cast<Slider>().CurrentValue;
                if (Config.QMenu["Qhr"].Cast<CheckBox>().CurrentValue
                    && Config.QMenu["Qmodehr"].Cast<ComboBox>().CurrentValue == 0
                    && Spells.Q.IsReady()
                    && Player.Instance.ManaPercent >= Config.QMenu["Qmnghr"].Cast<Slider>().CurrentValue)
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    if (target != null)
                    {
                        var Qgp = Spells.W.GetPrediction(target);
                        if (target != null && target.IsValidTarget() && Qgp.HitChancePercent >= Qhitchance && Qgp.CollisionObjects.Length == 0)
                        {
                            Spells.Q.Cast(Qgp.CastPosition);
                        }
                    }
                }
            }
        }
        #endregion
        //LaneClear
        #region LaneClear
        public static void LaneClear()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLineFarmLocation(EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, ObjectManager.Player.Position, Spells.W2.Range), Spells.W2.Width, (int)Spells.W2.Range);
            if (!Event.Humanform)
            {
                if (Config.WMenu["W2lc"].Cast<CheckBox>().CurrentValue
                && Spells.W2.IsReady())
                {
                    Spells.W2.Cast(minions.CastPosition);
                }
                if (Config.EMenu["E2lc"].Cast<CheckBox>().CurrentValue
                    && Spells.E2.IsReady())
                {
                    Spells.E2.Cast(minions.CastPosition);
                }
            }
        }
        #endregion
        //JungleClear
        #region JungleClear
        public static void JungleClear()
        {
            if (Event.Humanform)
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster == null || !monster.IsValid) return;
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                if (Config.QMenu["Qjc"].Cast<CheckBox>().CurrentValue
                    && Player.Instance.ManaPercent >= Config.QMenu["Qmngjc"].Cast<Slider>().CurrentValue
                    && Spells.Q.IsReady())
                {
                    Spells.Q.Cast(monster);
                }

                var wmonster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.W.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (wmonster == null || !wmonster.IsValid) return;
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                if (Config.WMenu["Wjc"].Cast<CheckBox>().CurrentValue
                    && Player.Instance.ManaPercent >= Config.WMenu["Wmng"].Cast<Slider>().CurrentValue
                    && Spells.W.IsReady())
                {
                    Spells.W.Cast(wmonster.ServerPosition);
                }
            }
            if (!Event.Humanform)
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.W2.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster == null || !monster.IsValid) return;
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                if (Config.WMenu["W2jc"].Cast<CheckBox>().CurrentValue
                    && Spells.W2.IsReady())
                {
                    Core.DelayAction(() => Spells.W2.Cast(monster), 500);
                }

                var emonster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.E2.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (emonster == null || !emonster.IsValid) return;
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                if (Config.EMenu["E2jc"].Cast<CheckBox>().CurrentValue
                    && Spells.E2.IsReady())
                {
                    Spells.E2.Cast(emonster.ServerPosition);
                }
                if (Config.QMenu["Q2jc"].Cast<CheckBox>().CurrentValue)
                {
                    if (Spells.Q2.IsReady()
                        && emonster != null
                        && emonster.IsValidTarget()
                        && Spells.Q2.Cast())
                    Orbwalker.ResetAutoAttack();
                }
            }
            if (Config.RMenu["Rformjc"].Cast<CheckBox>().CurrentValue)
            {
                if (Event.Humanform)
                {
                    if (!Spells.Q.IsReady() && !Config.WMenu["Wcb"].Cast<CheckBox>().CurrentValue && Spells.R.IsReady())
                    {
                        Spells.R.Cast();
                    }
                    if (!Spells.Q.IsReady() && !Spells.W.IsReady() && Spells.R.IsReady())
                    {
                        Spells.R.Cast();
                    }
                }
                if (!Event.Humanform)
                {
                    if (!Spells.Q2.IsReady() && !Spells.W2.IsReady() && !Spells.E2.IsReady() && Spells.R.IsReady())
                    {
                        Spells.R.Cast();
                        Orbwalker.ResetAutoAttack();
                    }
                }
            }
        }
        #endregion
        //KillSteal
        #region Killsteal
        public static void KillSteal(EventArgs args)
        {
            var useQ = Config.QMenu["Qks"].Cast<CheckBox>().CurrentValue;
            var useW = Config.WMenu["W2ks"].Cast<CheckBox>().CurrentValue;
            var useE = Config.EMenu["E2ks"].Cast<CheckBox>().CurrentValue;
            var useIgnite = Config.MiscMenu["ig"].Cast<CheckBox>().CurrentValue;
            var useSmite = Config.MiscMenu["sm"].Cast<CheckBox>().CurrentValue;

            if (useIgnite && Spells.Ignite != null)
            {
                var target = EntityManager.Heroes.Enemies.FirstOrDefault(
                        t =>
                            t.IsValidTarget(Spells.Ignite.Range) &&
                            t.Health <= Player.Instance.GetSpellDamage(t, Spells.Ignite.Slot));

                if (target != null && Spells.Ignite.IsReady())
                {
                    Spells.Ignite.Cast(target);
                }
            }
            if (useSmite && Spells.Smite != null && SmiteManager.CanUseOnChamp)
            {
                var target = EntityManager.Heroes.Enemies.FirstOrDefault(
                        t =>
                            t.IsValidTarget(Spells.Ignite.Range) &&
                            t.Health <= Player.Instance.GetSummonerSpellDamage(t, DamageLibrary.SummonerSpells.Smite));

                if (target != null && Spells.Smite.IsReady())
                {
                    Spells.Smite.Cast(target);
                }
            }
            if (Event.Humanform)
            {
                if (Spells.Q.IsReady() && useQ)
                {
                    var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                        && t.IsValidTarget()
                        && Spells.Q.IsInRange(t)
                        && t.Health <= Damage.QHumanDamage(t)), DamageType.Magical);

                    if (target != null)
                    {
                        var pred = Spells.Q.GetPrediction(target);
                        if (pred.CollisionObjects.Length == 0)
                        {
                            Spells.Q.Cast(pred.CastPosition);
                        }
                    }
                }
            }
            if (!Event.Humanform)
            {
                if (Spells.W2.IsReady() && useW)
                {
                    var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t.IsValidTarget()
                        && Spells.W2.IsInRange(t)
                        && t.Health <= Damage.WCougarDamage(t)), DamageType.Magical);
                    if (target != null)
                    {
                        Spells.W2.Cast(target);
                    }
                }
                if (Spells.W2.IsReady() && useE)
                {
                    var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t.IsValidTarget()
                        && Spells.E2.IsInRange(t)
                        && t.Health <= Damage.EDamage(t)), DamageType.Magical);
                    if (target != null)
                    {
                        Spells.E2.Cast(target);
                    }
                }
            }
        }
        #endregion
        //Auto E
        #region AutoE
        public static void AutoE(EventArgs args)
        {
            if (!Event.Humanform)
            {
                return;
            }

            if (!Spells.E.IsReady() || Player.Instance.IsRecalling())
            {
                return;
            }
            if (Config.EMenu["Emode"].Cast<ComboBox>().CurrentValue == 0
                && Config.EMenu["E"].Cast<CheckBox>().CurrentValue)
            {
                var leastHP = EntityManager.Heroes.Allies.Where(a => !a.IsDead && !a.IsInvulnerable && !a.IsZombie && Spells.E.IsInRange(a)).OrderBy(a => a.Health).FirstOrDefault();
                if (leastHP != null
                   && leastHP.HealthPercent <= Config.EMenu["Emng2"].Cast<Slider>().CurrentValue
                   && Player.Instance.ManaPercent >= Config.EMenu["Emng"].Cast<Slider>().CurrentValue
                   && Config.EMenu[leastHP.ChampionName].Cast<CheckBox>().CurrentValue)
                {
                    Spells.E.Cast(leastHP);
                }
            }
            if (Config.EMenu["Emode"].Cast<ComboBox>().CurrentValue == 1
                && Config.EMenu["E"].Cast<CheckBox>().CurrentValue)
            {
                var mostAD = EntityManager.Heroes.Allies.Where(a => !a.IsDead && !a.IsInvulnerable && !a.IsZombie && Spells.E.IsInRange(a)).OrderBy(a => a.TotalAttackDamage).LastOrDefault();
                if (mostAD != null
                   && mostAD.HealthPercent <= Config.EMenu["Emng2"].Cast<Slider>().CurrentValue
                   && Player.Instance.ManaPercent >= Config.EMenu["Emng"].Cast<Slider>().CurrentValue
                   && Config.EMenu[mostAD.ChampionName].Cast<CheckBox>().CurrentValue)
                {
                    Spells.E.Cast(mostAD);
                }
            }
            if (Config.EMenu["Emode"].Cast<ComboBox>().CurrentValue == 2
                && Config.EMenu["E"].Cast<CheckBox>().CurrentValue)
            {
                var mostAP = EntityManager.Heroes.Allies.Where(a => !a.IsDead && !a.IsInvulnerable && !a.IsZombie && Spells.E.IsInRange(a)).OrderBy(a => a.TotalMagicalDamage).LastOrDefault();
                if (mostAP != null
                    && mostAP.HealthPercent <= Config.EMenu["Emng2"].Cast<Slider>().CurrentValue
                    && Player.Instance.ManaPercent >= Config.EMenu["Emng"].Cast<Slider>().CurrentValue
                    && Config.EMenu[mostAP.ChampionName].Cast<CheckBox>().CurrentValue)
                {
                    Spells.E.Cast(mostAP);
                }
            }
            if (Config.EMenu["Emode"].Cast<ComboBox>().CurrentValue == 3
                && Config.EMenu["E"].Cast<CheckBox>().CurrentValue)
            {
                if (Player.Instance.HealthPercent <= Config.EMenu["Emng2"].Cast<Slider>().CurrentValue
                    && Player.Instance.ManaPercent >= Config.EMenu["Emng"].Cast<Slider>().CurrentValue
                    && Config.EMenu[Player.Instance.ChampionName].Cast<CheckBox>().CurrentValue)
                {
                    Spells.E.Cast(Player.Instance);
                }
            }
        }
        #endregion
        private static readonly string[] JungleMonster =
        {
            "SRU_Red", "SRU_Blue", "SRU_Dragon", "SRU_Baron", "SRU_RiftHerald", "SRU_Gromp",
            "SRU_Murkwolf", "SRU_Razorbeak", "SRU_Krug", "Sru_Crab"
        };

        public static void JungleSteal(EventArgs args)
        {
            if (Config.QMenu["Qjs"].Cast<CheckBox>().CurrentValue)
            {
                var Qmonster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && JungleMonster.Contains(x.BaseSkinName) && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (Qmonster != null && Event.Humanform && Qmonster.Health <= Damage.QHumanDamage(Qmonster) && Spells.Q.IsReady())
                {
                    Spells.Q.Cast(Qmonster);
                }
                if (Qmonster != null && !Event.Humanform && Event.IsReady(Event.CD["Javelintoss"]) && Spells.R.IsReady() && Qmonster.Health <= Damage.QHumanDamage(Qmonster))
                {
                    Spells.R.Cast();
                    Spells.Q.Cast(Qmonster);
                }
            }
        }
    }
}
