using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using SharpDX;
using Modes = EloBuddy.SDK.Orbwalker.ActiveModes;


namespace UBTaliyah
{
    class Mode
    {
        #region Combo
        public static void Combo()
        {
            if (Config.ComboMenu.GetValue("Q", false) > 0 && Spells.Q.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (Target != null)
                {
                    if ((Config.ComboMenu.GetValue("Q", false) == 1 && !Extension.In_Q_Side) || Config.ComboMenu.GetValue("Q", false) == 2)
                    {
                        var pred = Spells.Q.GetPrediction(Target);
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
            if (Config.ComboMenu.GetValue("W", false) > 0 && Spells.W.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                if (Target != null)
                {
                    var pred = Spells.W.GetPrediction(Target);
                    Vector3 StartPos;
                    switch (Config.ComboMenu.GetValue("W", false))
                    {
                        case 1:
                            {
                                StartPos = pred.CastPosition.Extend(Player.Instance, pred.CastPosition.Distance(Player.Instance) + 100f).To3D();
                                Spells.W.CastStartToEnd(StartPos, pred.CastPosition);
                            }
                            break;
                        case 2:
                            {
                                StartPos = Player.Instance.Position.Extend(pred.CastPosition, pred.CastPosition.Distance(Player.Instance) + 100f).To3D();
                                Spells.W.CastStartToEnd(StartPos, pred.CastPosition);
                            }
                            break;
                        case 3:
                            {
                                if (Obj_Manager.E_Object != null)
                                {
                                    StartPos = pred.CastPosition.Extend(Obj_Manager.E_Object.Position, pred.CastPosition.Distance(Obj_Manager.E_Object.Position) + 100f).To3D();
                                    Spells.W.CastStartToEnd(StartPos, pred.CastPosition);
                                }
                            }
                            break;
                    }
                }
            }
            if (Config.ComboMenu.Checked("E") && Spells.E.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (Target != null)
                {
                    var pred = Spells.E.GetPrediction(Target);
                    Spells.E.Cast(pred.CastPosition);
                }
            }
        }
        #endregion

        #region Harass
        public static void Harass()
        {
            if (Config.HarassMenu.GetValue("hr") > Player.Instance.ManaPercent) return;
            if (Config.HarassMenu.GetValue("Q", false) > 0 && Spells.Q.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Physical);
                if (Target != null)
                {
                    if ((Config.HarassMenu.GetValue("Q") == 1 && !Extension.In_Q_Side) || Config.HarassMenu.GetValue("Q") == 2)
                    {
                        var pred = Spells.Q.GetPrediction(Target);
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
            if (Config.HarassMenu.GetValue("W", false) > 0 && Spells.W.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Physical);
                if (Target != null)
                {
                    var pred = Spells.W.GetPrediction(Target);
                    Vector3 StartPos;
                    switch (Config.HarassMenu.GetValue("W", false))
                    {
                        case 1:
                            {
                                StartPos = pred.CastPosition.Extend(Player.Instance, pred.CastPosition.Distance(Player.Instance) + 100f).To3D();
                                Spells.W.CastStartToEnd(StartPos, pred.CastPosition);
                            }
                            break;
                        case 2:
                            {
                                StartPos = Player.Instance.Position.Extend(pred.CastPosition, pred.CastPosition.Distance(Player.Instance) + 100f).To3D();
                                Spells.W.CastStartToEnd(StartPos, pred.CastPosition);
                            }
                            break;
                        case 3:
                            {
                                if (Obj_Manager.E_Object != null)
                                {
                                    StartPos = pred.CastPosition.Extend(Obj_Manager.E_Object.Position, pred.CastPosition.Distance(Obj_Manager.E_Object.Position) + 100f).To3D();
                                    Spells.W.CastStartToEnd(StartPos, pred.CastPosition);
                                }
                            }
                            break;
                    }
                }
            }
            if (Config.HarassMenu.Checked("E") && Spells.E.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Physical);
                if (Target != null)
                {
                    var pred = Spells.E.GetPrediction(Target);
                    Spells.E.Cast(pred.CastPosition);
                }
            }         
        }
        #endregion

        #region LaneClear
        public static void LaneClear()
        {
            if (Config.LaneClear.GetValue("lc") > Player.Instance.ManaPercent || Orbwalker.LaneClearMinionsList.Count() < Config.LaneClear.GetValue("hit")) return;
            if (Config.LaneClear.Checked("Q") && Spells.Q.IsReady())
            {
                var Minion = Orbwalker.LaneClearMinion;
                if (Minion != null)
                {
                    Spells.Q.Cast(Minion);
                }
            }
            if (Config.LaneClear.Checked("W") && Spells.W.IsReady())
            {
                var Minion = Orbwalker.LaneClearMinion;
                if (Minion != null && Obj_Manager.E_Object != null)
                {
                    var Location = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(Orbwalker.LaneClearMinionsList, Spells.W.Width, (int)Spells.W.Range);
                    var StartPos = Location.CastPosition.Extend(Obj_Manager.E_Object.Position, Location.CastPosition.Distance(Obj_Manager.E_Object.Position) + 100f).To3D();
                    Spells.W.CastStartToEnd(StartPos, Location.CastPosition);
                }
            }
            if (Config.LaneClear.Checked("E") && Spells.E.IsReady())
            {
                var Minion = Orbwalker.LaneClearMinion;
                if (Minion != null)
                {
                    Spells.E.Cast(Minion);
                }
            }
        }
        #endregion

        #region JungleClear
        public static void JungleClear()
        {
            if (Config.JungleClear.GetValue("jc") > Player.Instance.ManaPercent) return;
            if (Config.JungleClear.Checked("Q") && Spells.Q.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster == null || !monster.IsValid) return;
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                Spells.Q.Cast(monster);
            }
            if (Config.JungleClear.Checked("W") && Spells.W.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.W.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster != null && Obj_Manager.E_Object != null)
                {
                    var Location = monster.Position;
                    var StartPos = Location.Extend(Obj_Manager.E_Object.Position, Location.Distance(Obj_Manager.E_Object.Position) + 100f).To3D();
                    Spells.W.CastStartToEnd(StartPos, Location);
                }
            }
            if (Config.JungleClear.Checked("E") && Spells.E.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.E.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster != null)
                {
                    Spells.E.Cast(monster);
                }
            }
        }
        #endregion

        #region Killsteal
        public static void Killsteal(EventArgs args)
        {
            if (Spells.Q.IsReady() && Config.MiscMenu.Checked("Qks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.Q.IsInRange(t)
                    && t.Health <= Damages.QDamage(t)), DamageType.Magical);

                if (target != null && !target.Unkillable())
                {
                    var pred = Spells.Q.GetPrediction(target);
                    Spells.Q.Cast(pred.CastPosition);
                }
            }
            if (Spells.W.IsReady() && Config.MiscMenu.Checked("Wks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.W.IsInRange(t)
                    && t.Health <= Damages.WDamage(t)), DamageType.Magical);

                if (target != null && !target.Unkillable())
                {
                    var pred = Spells.W.GetPrediction(target);
                    var StartPos = pred.CastPosition.Extend(Player.Instance, pred.CastPosition.Distance(Player.Instance) + 100f).To3D();
                    Spells.W.CastStartToEnd(StartPos, pred.CastPosition);

                }
            }
            if (Spells.E.IsReady() && Config.MiscMenu.Checked("Eks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.W.IsInRange(t)
                    && t.Health <= Damages.WDamage(t)), DamageType.Magical);

                if (target != null && !target.Unkillable())
                {
                    var pred = Spells.E.GetPrediction(target);
                    Spells.E.Cast(pred.CastPosition);

                }
            }
        }                
        #endregion

        #region On Unkillable Minion
        public static void On_Unkillable_Minion(Obj_AI_Base unit, Orbwalker.UnkillableMinionArgs args)
        {
            if (Config.LasthitMenu.GetValue("lh") > Player.Instance.ManaPercent || unit == null
                || Modes.Combo.IsActive()) return;
            if (args.RemainingHealth <= Damages.QDamage(unit) && Spells.Q.IsReady() && Config.LasthitMenu.Checked("Q"))
            {
                Spells.Q.Cast(unit);
            }
            if (args.RemainingHealth <= Damages.WDamage(unit) && Spells.W.IsReady() && Config.LasthitMenu.Checked("W"))
            {
                Spells.W.CastStartToEnd(Player.Instance.Position.Extend(unit, Player.Instance.Distance(unit) + 100f).To3D() ,unit.Position);
            }
            if (args.RemainingHealth <= Damages.EDamage(unit) && Spells.E.IsReady() && Config.LasthitMenu.Checked("E"))
            {
                Spells.E.Cast(unit);
            }
        }
        #endregion

        #region AutoHarass
        public static void AutoHarass(EventArgs args)
        {
            if (Player.Instance.ManaPercent < Config.HarassMenu.GetValue("autohrmng")) return;
            if (!Config.HarassMenu.Checked("keyharass", false)) return;
            if (Player.Instance.IsUnderEnemyturret()) return;
            if (Modes.Combo.IsActive() || Modes.Harass.IsActive() || Modes.Flee.IsActive()) return;
            if (Config.HarassMenu.GetValue("Q", false) > 0 && Spells.Q.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Physical);
                if (Target != null)
                {
                    if ((Config.HarassMenu.GetValue("Q", false) == 1 && !Extension.In_Q_Side) || Config.HarassMenu.GetValue("Q", false) == 2)
                    {
                        var pred = Spells.Q.GetPrediction(Target);
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
            if (Config.HarassMenu.GetValue("W", false) > 0 && Spells.W.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Physical);
                if (Target != null)
                {
                    var pred = Spells.W.GetPrediction(Target);
                    Vector3 StartPos;
                    switch (Config.HarassMenu.GetValue("W", false))
                    {
                        case 1:
                            {
                                StartPos = pred.CastPosition.Extend(Player.Instance, pred.CastPosition.Distance(Player.Instance) + 100f).To3D();
                                Spells.W.CastStartToEnd(StartPos, pred.CastPosition);
                            }
                            break;
                        case 2:
                            {
                                StartPos = Player.Instance.Position.Extend(pred.CastPosition, pred.CastPosition.Distance(Player.Instance) + 100f).To3D();
                                Spells.W.CastStartToEnd(StartPos, pred.CastPosition);
                            }
                            break;
                        case 3:
                            {
                                if (Obj_Manager.E_Object != null)
                                {
                                    StartPos = pred.CastPosition.Extend(Obj_Manager.E_Object.Position, pred.CastPosition.Distance(Obj_Manager.E_Object.Position) + 100f).To3D();
                                    Spells.W.CastStartToEnd(StartPos, pred.CastPosition);
                                }
                            }
                            break;
                    }
                }
            }
            if (Config.HarassMenu.Checked("E") && Spells.E.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Physical);
                if (Target != null)
                {
                    var pred = Spells.E.GetPrediction(Target);
                    Spells.E.Cast(pred.CastPosition);
                }
            }
        }
        #endregion

        #region Gapcloser
        public static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (Spells.W.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 250)
                && (sender.Spellbook.CastEndTime - Game.Time) * 1000 <= Spells.W.CastDelay
                && Config.MiscMenu.Checked("gap"))
            {
                Vector3 StartPos = Player.Instance.Position.Extend(args.End, Player.Instance.Distance(args.End) + 100f).To3D();
                Spells.W.CastStartToEnd(StartPos, args.End);
            }
        }
        #endregion
    }
}
