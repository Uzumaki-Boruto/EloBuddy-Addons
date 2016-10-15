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
            if ( Config.ComboMenu.Checked("W")
                && Spells.W.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target != null)
                {
                    SpecialVector.WhereCastW(target, Config.ComboMenu.GetValue("Wunit"));
                }
            }
            if (ObjManager.CountAzirSoldier != 0 && Config.ComboMenu.Checked("Q") && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target != null)
                {
                    SpecialVector.WhereCastQ(target, Config.ComboMenu.GetValue("Qbonus"));
                }
            }
            if (Config.ComboMenu.Checked("E") && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                var priority = TargetSelector.GetPriority(target);
                if (target != null && !target.IsUnderHisturret() && Config.ComboMenu.Checked(target.ChampionName))
                {
                    if (priority >= 4
                    && target.IsValidTarget()
                    && !Extension.Unkillable(target)
                    && !Extension.HasSpellShield(target))
                    {                       
                        if (target.CountEnemiesInRange(1300) <= Config.ComboMenu["Edanger"].Cast<Slider>().CurrentValue)
                        {
                            if (target.Health <= (Damages.Damagefromspell
                            (target,
                            Spells.Q.IsReady(),
                            Spells.W.IsReady() || target.Distance(ObjManager.Soldier_Nearest_Enemy) > 375,
                            Spells.E.IsReady(),
                            Spells.R.IsReady()))
                            + Damages.WDamage(target) * 4)
                            {
                                foreach (var soldier in Orbwalker.AzirSoldiers)
                                {
                                    if (SpecialVector.Between(target, soldier.Position))
                                    {
                                        Spells.E.Cast(soldier);
                                    }
                                }
                            }
                        }
                    }
                    if (priority < 3
                    && target.IsValidTarget()
                    && !Extension.Unkillable(target)
                    && !Extension.HasSpellShield(target))
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
                                foreach (var soldier in Orbwalker.AzirSoldiers)
                                {
                                    if (SpecialVector.Between(target, soldier.Position))
                                    {
                                        Spells.E.Cast(soldier);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (Config.ComboMenu.Checked("R") && Spells.R.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.R.Range - 20, DamageType.Magical);
                if (target != null && Player.Instance.CountEnemiesInRange(Spells.R.Range) >= Config.ComboMenu.GetValue("Rhit") && target.IsValidTarget() && target.HealthPercent <= 70 && Spells.Q.IsReady())
                {
                    SpecialVector.WhereCastR(target, SpecialVector.I_want.All);
                }
            }        
            if (ObjManager.Soldier_Nearest_Enemy != Vector3.Zero)
            {
                var target = TargetSelector.SelectedTarget != null &&
                                 TargetSelector.SelectedTarget.Distance(ObjManager.Soldier_Nearest_Enemy) < 500
                        ? TargetSelector.SelectedTarget
                        : TargetSelector.GetTarget(425, DamageType.Magical, ObjManager.Soldier_Nearest_Enemy);
                if (target.IsValid())
                {
                    SpecialVector.AttackOtherObject();
                }
            }
            //if (ObjManager.All_Basic_Is_Ready)
            //{
            //    var target = TargetSelector.GetTarget(1000, DamageType.Magical);
            //    if (target != null && target.IsValid && target.HealthPercent <= 15
            //        && !target.IsUnderHisturret() && target.CountEnemiesInRange(875) <= 1
            //        && Config.ComboMenu.Checked(target.ChampionName)
            //        && Config.ComboMenu.Checked("Q")
            //        && Config.ComboMenu.Checked("W")
            //        && Config.ComboMenu.Checked("E"))
            //    {
            //        var time = (Player.Instance.Distance(target) / Spells.E.Speed) * (750 - Game.Ping);
            //        var pred = Prediction.Position.PredictUnitPosition(target, (int)time).To3D();
            //        Flee(pred);
            //    }
            //}
        }
        #endregion

        #region Harass
        public static void Harass()
        {
            if (Player.Instance.ManaPercent >= Config.HarassMenu.GetValue("HrManage"))
            {
                if (Config.HarassMenu.Checked("W")
                    && Config.HarassMenu.Checked("Q")
                    && Spells.W.IsReady()
                    && Spells.Q.IsReady())
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    if (target != null && target.IsValidTarget())
                    {
                        SpecialVector.WhereCastW(target, Config.HarassMenu.GetValue("Wunit"));
                    }
                }
                if (ObjManager.CountAzirSoldier != 0 && Config.HarassMenu.Checked("Q"))
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    if (target != null && target.IsValidTarget() && !target.IsInRange(ObjManager.Soldier_Nearest_Enemy, 375))
                    {
                        SpecialVector.WhereCastQ(target, Config.HarassMenu.GetValue("Qbonus"));
                    }
                }
            }
            if (ObjManager.CountAzirSoldier > 0 && ObjManager.Soldier_Nearest_Enemy != Vector3.Zero)
            {
                var Unit = TargetSelector.SelectedTarget != null &&
                                TargetSelector.SelectedTarget.Distance(ObjManager.Soldier_Nearest_Enemy) < 500
                       ? TargetSelector.SelectedTarget
                       : TargetSelector.GetTarget(425, DamageType.Magical, ObjManager.Soldier_Nearest_Enemy);
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
            if (Player.Instance.ManaPercent >= Config.LaneClear.GetValue("LcManager"))
            {
                if (Config.LaneClear.Checked("Q"))
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
                if (Config.LaneClear.Checked("W"))
                {
                    var minion = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy),
                        (float)Spells.W.Width,
                        (int)Spells.W.Range);
                    if (ObjManager.CountAzirSoldier < Config.LaneClear.GetValue("Wunit") && minion.HitNumber >= 3)
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
                if (Config.JungleClear.Checked("Q")
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
                if (Config.JungleClear.Checked("W") && Spells.W.IsReady()
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

        #region Flee
        public static void Flee(Vector3 destination, bool Insec = false)
        {
            var WCast = Player.Instance.Position.Extend(destination, Spells.W.Range).To3DWorld();
            var Qcast = Player.Instance.Position.Extend(destination, Spells.Q.Range).To3DWorld();
            var ECast = Player.Instance.Position.Extend(destination, Spells.W.Range).To3DWorld();
            var value = Insec ? 2 : Config.Flee["flee"].Cast<ComboBox>().CurrentValue;
            if (ObjManager.All_Basic_Is_Ready)
            {                
                Spells.W.Cast(WCast);                
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
                && Config.LasthitMenu.Checked("Q")
                && Spells.Q.IsReady()
                && ObjManager.CountAzirSoldier > 0)
            {
                if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                    Spells.Q.Cast(unit);
            }
        }
        #endregion

        #region KillSteal
        public static void KillSteal(EventArgs args)
        {
            if (Spells.Q.IsReady() && Config.MiscMenu.Checked("Qks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.Q.IsInRange(t)
                    && t.Health <= Damages.QDamage(t)), DamageType.Magical);

                if (target != null)
                {
                    var pred = Spells.Q.GetPrediction(target);
                    if (ObjManager.CountAzirSoldier == 0 && Spells.W.IsReady() && Config.MiscMenu.Checked("Wks"))
                    {
                        SpecialVector.WhereCastW(target, 1);
                    }
                    if (ObjManager.CountAzirSoldier > 0)
                    {
                        Spells.Q.Cast(pred.UnitPosition);
                    }
                }
            }
            if (Spells.W.IsReady() && Config.MiscMenu.Checked("Wks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.W.IsInRange(t)
                    && t.Health <= Damages.WDamage(t)), DamageType.Magical);

                if (target != null)
                {
                    SpecialVector.WhereCastW(target, 1);
                    Orbwalker.OrbwalkTo(Game.CursorPos);
                }
            }
            if (Spells.E.IsReady() && Config.MiscMenu.Checked("Eks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                   && t.IsValidTarget()
                   && Spells.E.IsInRange(t)
                   && t.Health <= Damages.EDamage(t)), DamageType.Magical);

                if (target != null && ObjManager.CountAzirSoldier > 0)
                {
                    foreach (var soldier in Orbwalker.AzirSoldiers)
                    {
                        if (SpecialVector.Between(target, soldier.Position))
                        {
                            Spells.E.Cast(soldier);
                        }
                    }
                }
            }
            if (Spells.R.IsReady() && Config.MiscMenu.Checked("Rks"))
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
        }
        #endregion

        #region Auto Harass
        public static void Auto_Harass()
        {
            if (Config.HarassMenu["autokey"].Cast<KeyBind>().CurrentValue && Player.Instance.ManaPercent >= Config.HarassMenu["automng"].Cast<Slider>().CurrentValue)
            {
                if (ObjManager.CountAzirSoldier < Config.HarassMenu["Wunit"].Cast<Slider>().CurrentValue
                    && Config.HarassMenu.Checked("W")
                    && Config.HarassMenu.Checked("Q")
                    && Spells.W.IsReady()
                    && Spells.Q.IsReady())
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    if (target != null && target.IsValidTarget())
                    {
                        SpecialVector.WhereCastW(target, Config.HarassMenu.GetValue("Wunit"));
                    }
                }
                if (ObjManager.CountAzirSoldier != 0 && Config.HarassMenu.Checked("Q"))
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    if (target != null && target.IsValidTarget() && !target.IsInRange(ObjManager.Soldier_Nearest_Enemy, 375))
                    {
                        SpecialVector.WhereCastQ(target, Config.HarassMenu.GetValue("Qbonus"));
                    }
                }
            }
        }
        #endregion
    }
}
