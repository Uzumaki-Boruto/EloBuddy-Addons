using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Events;
using SharpDX;


namespace UBAzir
{
    class Mode
    {
        #region Combo
        public static void Combo()
        {
            if (ObjManager.CountAzirSoldier < Config.ComboMenu["Wunitcb"].Cast<Slider>().CurrentValue
                && Config.ComboMenu["Wcb"].Cast<CheckBox>().CurrentValue
                && Spells.W.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                var Force = Orbwalker.ForcedTarget != null ? true : false;
                if (target != null && target.IsValidTarget())
                {
                    SpecialVector.WhereCastW(target, Force);
                }
            }
            if (ObjManager.CountAzirSoldier != 0 && Config.ComboMenu["Qcb"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                var Force = Orbwalker.ForcedTarget != null ? true : false;
                if (target != null && target.IsValidTarget() && !target.IsInRange(ObjManager.Soldier_Nearest_Enemy, Spells.WFocus.Range))
                {
                    SpecialVector.WhereCastQ(target, Force);
                }
            }
            if (Config.ComboMenu["Ecb"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                var priority = TargetSelector.GetPriority(target);
                if (target != null && !target.IsUnderHisturret() && Config.ComboMenu[target.ChampionName].Cast<CheckBox>().CurrentValue)
                {
                    if (priority >= 4
                    && target.IsValidTarget()
                    && !Event.Unkillable(target)
                    && !Event.HasSpellShield(target))
                    {
                        if (target.CountEnemiesInRange(1300) == 1
                            && Player.Instance.HealthPercent >= 75)
                        {
                            Spells.E.Cast(target.Position);
                        }
                        if (target.CountEnemiesInRange(1300) <= Config.ComboMenu["Edanger"].Cast<Slider>().CurrentValue + 1)
                        {
                            if (target.Health <= (Damages.Damagefromspell
                            (target,
                            Spells.Q.IsReady(),
                            Spells.W.IsReady() || target.Distance(ObjManager.Soldier_Nearest_Enemy) > 375,
                            Spells.E.IsReady(),
                            Spells.R.IsReady()))
                            + Damages.WDamage(target) * 4)
                            {
                                if (SpecialVector.Between(target))
                                {
                                    Spells.E.Cast();
                                }
                            }
                        }
                    }
                    if (priority < 3
                    && target.IsValidTarget()
                    && !Event.Unkillable(target)
                    && !Event.HasSpellShield(target))
                    {
                        if (target.CountEnemiesInRange(1300) <= Config.ComboMenu["Edanger"].Cast<Slider>().CurrentValue + 1)
                        {
                            if (target.Health <= (Damages.Damagefromspell
                             (target,
                             Spells.Q.IsReady(),
                             Spells.W.IsReady() || target.Distance(ObjManager.Soldier_Nearest_Enemy) > 375,
                             Spells.E.IsReady(),
                             Spells.R.IsReady()))
                             + Damages.WDamage(target) * 2)
                            {
                                if (SpecialVector.Between(target))
                                {
                                    Spells.E.Cast();
                                }
                            }
                        }
                    }
                }
            }
            if (Config.ComboMenu["Rcb"].Cast<CheckBox>().CurrentValue && Spells.R.IsReady())
            {
                if (Config.ComboMenu["teamfight"].Cast<KeyBind>().CurrentValue)
                {
                    var target = TargetSelector.GetTarget(Spells.R.Range, DamageType.Magical);
                    if (target != null)
                    {
                        if ((target.CountEnemiesInRange(1000) >= Config.ComboMenu["Rhitcb"].Cast<Slider>().CurrentValue
                            || Player.Instance.CountAlliesInRange(1000) >= Config.ComboMenu["Rhitcb"].Cast<Slider>().CurrentValue)
                            && Player.Instance.CountEnemiesInRange(Spells.R.Width) >= Config.ComboMenu["Rhitcb"].Cast<Slider>().CurrentValue)
                        {
                            Spells.R.Cast(target.Position);
                        }
                    }
                }
                if (!Config.ComboMenu["teamfight"].Cast<KeyBind>().CurrentValue)
                {
                    var target = TargetSelector.GetTarget(Spells.R.Range - 20, DamageType.Magical);
                    if (target != null && target.IsValidTarget() && target.HealthPercent <= 70 && Spells.Q.IsReady())
                    {
                        SpecialVector.WhereCastR(target, SpecialVector.I_want.All);
                    }
                }
            }
            if (ObjManager.Soldier_Nearest_Enemy != Vector3.Zero)
            {
                var target = TargetSelector.SelectedTarget != null &&
                                 TargetSelector.SelectedTarget.Distance(ObjManager.Soldier_Nearest_Enemy) < 500
                        ? TargetSelector.SelectedTarget
                        : TargetSelector.GetTarget(Spells.WLine.Range, DamageType.Magical, ObjManager.Soldier_Nearest_Enemy);
                if (target.IsValid())
                {
                    SpecialVector.AttackOtherObject();
                }
            }
            if (ObjManager.All_Basic_Is_Ready)
            {
                var target = TargetSelector.GetTarget(1000, DamageType.Magical);
                if (target != null && target.IsValid && target.HealthPercent <= 15
                    && !target.IsUnderHisturret() && target.CountEnemiesInRange(875) <= 1
                    && Config.ComboMenu[target.ChampionName].Cast<CheckBox>().CurrentValue
                    && Config.ComboMenu["Qcb"].Cast<CheckBox>().CurrentValue
                    && Config.ComboMenu["Wcb"].Cast<CheckBox>().CurrentValue
                    && Config.ComboMenu["Ecb"].Cast<CheckBox>().CurrentValue)
                {
                    var time = (Player.Instance.Distance(target) / Spells.E.Speed) * (750 - Game.Ping);
                    var pred = Prediction.Position.PredictUnitPosition(target, (int)time).To3D();
                    Flee(pred);
                }
            }
        }
        #endregion

        #region Harass
        public static void Harass()
        {
            if (Player.Instance.ManaPercent >= Config.HarassMenu["HrManager"].Cast<Slider>().CurrentValue)
            {
                if (ObjManager.CountAzirSoldier < Config.ComboMenu["Wunitcb"].Cast<Slider>().CurrentValue
                    && Config.ComboMenu["Whr"].Cast<CheckBox>().CurrentValue
                    && Config.ComboMenu["Qhr"].Cast<CheckBox>().CurrentValue
                    && Spells.W.IsReady()
                    && Spells.Q.IsReady())
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    if (target != null && target.IsValidTarget())
                    {
                        SpecialVector.WhereCastW(target);
                    }
                }
                if (ObjManager.CountAzirSoldier != 0 && Config.ComboMenu["Qhr"].Cast<CheckBox>().CurrentValue)
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    var Force = Orbwalker.ForcedTarget != null ? true : false;
                    if (target != null && target.IsValidTarget() && !target.IsInRange(ObjManager.Soldier_Nearest_Enemy, Spells.WFocus.Range))
                    {
                        SpecialVector.WhereCastQ(target, Force);
                    }
                }
            }
            if (ObjManager.CountAzirSoldier > 0 && ObjManager.Soldier_Nearest_Enemy != Vector3.Zero)
            {
                var Unit = TargetSelector.SelectedTarget != null &&
                                TargetSelector.SelectedTarget.Distance(ObjManager.Soldier_Nearest_Enemy) < 500
                       ? TargetSelector.SelectedTarget
                       : TargetSelector.GetTarget(Spells.WLine.Range, DamageType.Magical, ObjManager.Soldier_Nearest_Enemy);
                if (Unit.IsValid())
                {
                    SpecialVector.AttackOtherObject();
                }
            }
        }
        #endregion

        #region LaneClear
        public static void LaneClear()
        {
            if (Player.Instance.ManaPercent >= Config.LaneClear["LcManager"].Cast<Slider>().CurrentValue)
            {
                if (Config.LaneClear["Qlc"].Cast<CheckBox>().CurrentValue)
                {
                    var Soldier = ObjManager.SoldierPos;
                    if (Soldier != Vector3.Zero)
                    {
                        var minion = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(
                            EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy),
                            (float)Spells.W.Width,
                            (int)Spells.Q.Range);
                        if (minion.HitNumber >= 3)
                        {
                            Spells.Q.Cast(minion.CastPosition);
                        }
                    }
                }
                if (Config.LaneClear["Wlc"].Cast<CheckBox>().CurrentValue)
                {
                    var minion = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy),
                        (float)Spells.W.Width,
                        (int)Spells.W.Range);
                    if (ObjManager.CountAzirSoldier < Config.LaneClear["Wunitlc"].Cast<Slider>().CurrentValue && minion.HitNumber >= 3)
                    {
                        Spells.W.Cast(minion.CastPosition);
                    }
                }
            }
        }
        #endregion

        #region JungleClear
        public static void JungleClear()
        {
            var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
            if (monster != null && monster.IsValid)
            {
                var qpred = Spells.Q.GetPrediction(monster);
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                if (Config.JungleClear["Qjc"].Cast<CheckBox>().CurrentValue
                    && Player.Instance.ManaPercent >= Config.JungleClear["JcManager"].Cast<Slider>().CurrentValue
                    && Spells.Q.IsReady())
                {
                    Spells.Q.Cast(qpred.CastPosition);
                }
            }

            var wmonster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.W.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
            if (wmonster != null && wmonster.IsValid)
            {
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                if (Config.JungleClear["Wjc"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady()
                    && ObjManager.CountAzirSoldier < Config.JungleClear["Wunitjc"].Cast<Slider>().CurrentValue
                    && Player.Instance.ManaPercent >= Config.JungleClear["JcManager"].Cast<Slider>().CurrentValue
                    && Spells.W.IsReady()
                    && wmonster.IsInRange(Player.Instance, Spells.W.Radius + Spells.W.Range))
                {
                    Spells.W.Cast(Player.Instance.Position.Extend(wmonster, Spells.W.Range).To3D());
                }
            }
        }
        #endregion

        #region Lasthit
        public static void Lasthit()
        {
            if (Player.Instance.ManaPercent >= Config.LasthitMenu["LhManager"].Cast<Slider>().CurrentValue)
            {
                if (Config.LaneClear["Wlh"].Cast<CheckBox>().CurrentValue)
                {
                    var minion = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                        Player.Instance.Position, Spells.W.Radius), Spells.W.Radius, (int)Spells.W.Range, Player.Instance.Position.To2D());
                    if (ObjManager.CountAzirSoldier < Config.LaneClear["Wunitlc"].Cast<Slider>().CurrentValue)
                    {
                        Spells.W.Cast(minion.CastPosition);
                    }
                }
                if (Config.LaneClear["Qlh"].Cast<CheckBox>().CurrentValue)
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.OrderBy(a => a.Health).FirstOrDefault
                    (m => m.IsEnemy
                    && m.Distance(Player.Instance.Position) <= Spells.Q.Range
                    && !m.IsDead
                    && !m.IsInvulnerable
                    && m.IsValidTarget(Spells.Q.Range)
                    && m.Health <= Damages.QDamage(m));
                    if (minion != null && Spells.Q.IsReady() && ObjManager.CountAzirSoldier > 0)
                    {
                        Spells.Q.Cast(minion);
                    }
                }
            }
        }
        #endregion

        #region Flee
        public static void Flee(Vector3 destination, bool Insec = false)
        {
            var WCast = Player.Instance.Position.Extend(destination, Spells.W.Range).To3D();
            var Qcast = Player.Instance.Position.Extend(destination, Spells.Q.Range).To3D();
            var QCast2 = Player.Instance.Position.Extend(destination, Spells.Q.Range + Player.Instance.Position.Distance(ObjManager.Soldier_Nearest_Azir)).To3D();
            var ECast = Player.Instance.Position.Extend(destination, Spells.E.Range).To3D();
            var value = Insec ? 2 : Config.Flee["flee"].Cast<ComboBox>().CurrentValue;
            if (ObjManager.All_Basic_Is_Ready)
            {
                if (ObjManager.CountAzirSoldier == 0)
                {
                    Spells.W.Cast(WCast);
                }
                if (Player.Instance.Position.Distance(destination) < ObjManager.Soldier_Nearest_Azir.Distance(destination))
                {
                    Spells.W.Cast(WCast);
                }
            }
            switch (value)
            {
                case 0:
                    {
                        if (!Spells.E.IsReady()) return;
                        if (ObjManager.CountAzirSoldier >= 0)
                        {
                            Spells.E.Cast(ECast);
                        }
                    }
                    break;
                case 1:
                    {
                        if (!Spells.Q.IsReady() || !Spells.E.IsReady()) return;
                        if (ObjManager.CountAzirSoldier >= 0)
                        {
                            Spells.Q.Cast(Qcast);
                            Spells.E.Cast(ECast);
                        }
                    }
                    break;
                case 2:
                    {
                        if (!Spells.Q.IsReady() || !Spells.E.IsReady()) return;
                        if (ObjManager.CountAzirSoldier >= 0)
                        {
                            Spells.E.Cast(ECast);
                            var delay = Config.Flee["fleedelay"].Cast<Slider>().CurrentValue;
                            var time = (Player.Instance.ServerPosition.Distance(ObjManager.Soldier_Nearest_Azir) / Spells.E.Speed) * (700 + delay - Game.Ping);                            
                            Core.DelayAction(() => Spells.Q_in_Flee.Cast(QCast2), (int)time);
                        }
                    }
                    break;                
            }
        }
        #endregion

        #region On_Unkillable_Minion
        public static void On_Unkillable_Minion(Obj_AI_Base unit, Orbwalker.UnkillableMinionArgs args)
        {
            if (Player.Instance.ManaPercent >= Config.LasthitMenu["LhManager"].Cast<Slider>().CurrentValue
                && Config.LasthitMenu["Qautolh"].Cast<CheckBox>().CurrentValue
                && Spells.Q.IsReady()
                && ObjManager.CountAzirSoldier > 0)
            {
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                    Spells.Q.Cast(unit);
            }
        }
        #endregion

        #region KillSteal
        public static void KillSteal(EventArgs args)
        {
            var useQ = Config.MiscMenu["Qks"].Cast<CheckBox>().CurrentValue;
            var useW = Config.MiscMenu["Wks"].Cast<CheckBox>().CurrentValue;
            var useE = Config.MiscMenu["Eks"].Cast<CheckBox>().CurrentValue;
            var useR = Config.MiscMenu["Rks"].Cast<CheckBox>().CurrentValue;
            var useIg = Config.ComboMenu["ig"].Cast<CheckBox>().CurrentValue;
            if (Spells.Q.IsReady() && useQ)
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.Q.IsInRange(t)
                    && t.Health <= Damages.QDamage(t)), DamageType.Magical);

                if (target != null)
                {
                    var pred = Spells.Q.GetPrediction(target);
                    if (ObjManager.CountAzirSoldier == 0 && Spells.W.IsReady() && useW)
                    {
                        SpecialVector.WhereCastW(target);
                    }
                    if (ObjManager.CountAzirSoldier > 0)
                    {
                        Spells.Q.Cast(pred.UnitPosition);
                    }
                }
            }
            if (Spells.W.IsReady() && useW)
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.W.IsInRange(t)
                    && t.Health <= Damages.WDamage(t)), DamageType.Magical);

                if (target != null)
                {
                    SpecialVector.WhereCastW(target);
                    Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                }
            }
            if (Spells.E.IsReady() && useE)
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                   && t.IsValidTarget()
                   && Spells.E.IsInRange(t)
                   && t.Health <= Damages.EDamage(t)), DamageType.Magical);

                if (target != null && ObjManager.CountAzirSoldier > 0)
                {
                    if (SpecialVector.Between(target))
                    {
                        Spells.E.Cast();
                    }
                }
            }
            if (Spells.R.IsReady() && useR)
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.R.IsInRange(t)
                    && t.Health <= Damages.RDamage(t)), DamageType.Magical);

                if (target != null)
                {
                    Spells.R.Cast(target);
                }
            }
            if (Spells.Ignite != null && Spells.Ignite.IsReady() && useIg)
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.Ignite.IsInRange(t)
                    && t.Health <= Player.Instance.GetSummonerSpellDamage(t, DamageLibrary.SummonerSpells.Ignite)), DamageType.True);

                if (target != null)
                {
                    Spells.Ignite.Cast(target);
                }
            }
        }
        #endregion

        #region Auto Harass
        public static void Auto_Harass()
        {
            if (Config.HarassMenu["autokey"].Cast<KeyBind>().CurrentValue && Player.Instance.ManaPercent >= Config.HarassMenu["automng"].Cast<Slider>().CurrentValue)
            {
                if (ObjManager.CountAzirSoldier < 1
                    && Config.HarassMenu["Wauto"].Cast<ComboBox>().CurrentValue > 0
                    && Spells.W.IsReady())
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    var Force = Orbwalker.ForcedTarget != null ? true : false;
                    if (target != null && target.IsValidTarget() && !Player.Instance.IsUnderEnemyturret()
                        && Config.HarassMenu["Wauto"].Cast<ComboBox>().CurrentValue == 1)
                    {
                        SpecialVector.WhereCastW(target, true);
                    }
                    if (target != null && target.IsValidTarget() && !Player.Instance.IsUnderEnemyturret()
                        && Config.HarassMenu["Wauto"].Cast<ComboBox>().CurrentValue == 2
                        && Spells.Q.IsReady())
                    {
                        SpecialVector.WhereCastW(target, Force);
                    }
                }
                if (ObjManager.CountAzirSoldier != 0 && Config.HarassMenu["Qauto"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    var Force = Orbwalker.ForcedTarget != null ? true : false;
                    if (target != null && target.IsValidTarget() && !Player.Instance.IsUnderEnemyturret()
                        && !target.IsInRange(ObjManager.Soldier_Nearest_Enemy, Spells.WFocus.Range))
                    {
                        SpecialVector.WhereCastQ(target, Force);
                    }
                }
                if (ObjManager.CountAzirSoldier > 0 
                    && ObjManager.Soldier_Nearest_Enemy != Vector3.Zero
                    && Config.HarassMenu["aa.auto"].Cast<CheckBox>().CurrentValue)
                {
                    var target = TargetSelector.SelectedTarget != null &&
                                    TargetSelector.SelectedTarget.Distance(ObjManager.Soldier_Nearest_Enemy) < 500
                           ? TargetSelector.SelectedTarget
                           : TargetSelector.GetTarget(Spells.WLine.Range, DamageType.Magical, ObjManager.Soldier_Nearest_Enemy);
                    if (target.IsValid() && !Player.Instance.IsUnderEnemyturret())
                    {
                        SpecialVector.AttackOtherObject();
                    }
                }
                if (ObjManager.CountAzirSoldier > 0 
                    && ObjManager.Soldier_Nearest_Enemy != Vector3.Zero
                    && Config.HarassMenu["a.auto"].Cast<CheckBox>().CurrentValue)
                {
                    var target = TargetSelector.GetTarget(Spells.WLine.Range, DamageType.Magical, ObjManager.Soldier_Nearest_Enemy);
                    var Minion = Orbwalker.PriorityLastHitWaitingMinion;
                    if (target.IsValid() && Minion == null && !Player.Instance.IsUnderEnemyturret()
                        && target.IsInRange(ObjManager.Soldier_Nearest_Enemy, Spells.WFocus.Range))
                    {
                        Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                        Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                    }
                }
            }
        }
        #endregion
    }
}
