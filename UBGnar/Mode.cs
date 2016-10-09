using System;
using System.Linq;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Events;
using SharpDX;

namespace UBGnar
{
    class Mode
    {
        #region Other Void
        public static void QTinyCast(bool Focus, Menu Mode)
        {
            if (Mode.Checked("Q") && Spells.QTiny.IsReady())
            {
                var tarWget = Focus ? TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                              && t.IsValidTarget()
                              && Spells.QTiny.IsInRange(t)
                              && t.HasWTinyBuff()), DamageType.Physical) : null;
                var Target = tarWget == null ? Spells.QTiny.GetTarget() : tarWget;
                if (Target != null)
                {
                    var pred = Spells.QTiny.GetPrediction(Target);
                    if (pred.CollisionObjects.Count() == 0 || pred.CollisionObjects.First().Distance(Target) <= 250)
                    {
                        Spells.QTiny.Cast(pred.CastPosition);
                    }
                }
            }
        }
        public static void ETinyCast(bool Focus ,Menu Mode)
        {
            if (Mode.GetValue("E", false) > 0 && Spells.ETiny.IsReady())
            {
                var tarWget = Focus ? TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                                && t.IsValidTarget()
                                && Spells.QTiny.IsInRange(t)
                                && t.HasWTinyBuff()), DamageType.Physical) : null;
                var Target = tarWget == null ? Spells.ETiny.GetTarget() : tarWget;
                switch (Mode.GetValue("E", false))
                {
                    case 1:
                        {
                            if (Target != null)
                            {
                                var pred = Spells.ETiny.GetPrediction(Target);
                                if ((!Mode.Checked("Etiny") || !pred.CastPosition.IsUnderEnemyTurret()) &&
                                    (!Mode.Checked("Etiny2") || !pred.CastPosition.EEndPred().IsUnderEnemyTurret()))
                                {
                                    Spells.ETiny.Cast(pred.CastPosition);
                                }
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Target != null)
                            {
                                var pred = Spells.ETiny.GetPrediction(Target);
                                if (Target.IsMelee)
                                {
                                    if (Player.Instance.Distance(Target) <= Target.GetAutoAttackRange() + 50)
                                    {
                                        if ((!Mode.Checked("Etiny") || !pred.CastPosition.IsUnderEnemyTurret()) &&
                                            (!Mode.Checked("Etiny2") || !pred.CastPosition.EEndPred().IsUnderEnemyTurret()))
                                        {
                                            Spells.ETiny.Cast(pred.CastPosition);
                                        }
                                    }
                                }
                                else
                                {
                                    if ((!Mode.Checked("Etiny") || !pred.CastPosition.IsUnderEnemyTurret()) &&
                                        (!Mode.Checked("Etiny2") || !pred.CastPosition.EEndPred().IsUnderEnemyTurret()))
                                    {
                                        Spells.ETiny.Cast(pred.CastPosition);
                                    }
                                }
                            }
                        }
                        break;
                    case 3:
                        {
                            if (Target != null)
                            {
                                var pred = Spells.ETiny.GetPrediction(Target);
                                if (Player.Instance.Distance(Target.Position) <= Player.Instance.Distance(pred.CastPosition))
                                {
                                    if ((!Mode.Checked("Etiny") || !pred.CastPosition.IsUnderEnemyTurret()) &&
                                        (!Mode.Checked("Etiny2") || !pred.CastPosition.EEndPred().IsUnderEnemyTurret()))
                                    {
                                        Spells.ETiny.Cast(pred.CastPosition);
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
        public static void QMegaCast(Menu Mode)
        {
            if (Mode.Checked("Qbig") && Spells.QMega.IsReady())
            {
                var Target = Spells.QMega.GetTarget();
                if (Target != null)
                {
                    var pred = Spells.QMega.GetPrediction(Target);
                    Spells.QMega.Cast(pred.CastPosition);
                }
            }
        }
        public static void WMegaCast(Menu Mode)
        {
            if (Mode.Checked("Wbig") && Spells.WMega.IsReady())
            {
                var Target = Spells.WMega.GetTarget();
                if (Target != null)
                {
                    var pred = Spells.WMega.GetPrediction(Target);
                    Spells.WMega.Cast(pred.CastPosition);
                }
            }
        }
        public static void EMegaCast(Menu Mode)
        {
            var Target = Spells.EMega.GetTarget();
            if (Target != null && Spells.EMega.IsReady())
            {
                var pred = Spells.EMega.GetPrediction(Target);
                if (!Mode.Checked("Ebig1") || !pred.CastPosition.IsUnderEnemyTurret())
                {
                    Spells.EMega.Cast(pred.CastPosition);
                }
            }
        }
        public static void RCast()
        {
            if (!Config.ComboMenu.Checked("R")) return;
            var Target = Spells.R.GetTarget();
            var WhereWall = new List<Vector3>();
            var WhereCastR = new Vector3();
            if (Target != null && Spells.R.IsReady())
            {
                for (var i = 0; i <= 640; i++)
                {
                    WhereWall = VectorHelp.GetWallAroundMe(Spells.R.Range, i / 100f);
                }
            }
            if (WhereWall.Count != 0)
            {
                if (Config.DrawMenu.Checked("rpos"))
                {
                    Circle.Draw(Color.Green, 40, WhereWall.ToArray());
                }
                var Wall = WhereWall.OrderBy(x => x.Distance(Target)).FirstOrDefault();
                WhereCastR = Target.Position.To2D().Parallel(Wall.To2D(), Player.Instance.Position.To2D());
            }
            if (WhereCastR != new Vector3() && Player.Instance.CountEnemyHeroesInRangeWithPrediction((int)Spells.R.Range, 500) >= Config.ComboMenu.GetValue("unit"))
            {
                if (Spells.R.IsInRange(WhereCastR))
                {
                    Spells.R.Cast(WhereCastR);
                }
                else
                {
                    Spells.R.Cast(Player.Instance.Position.Extend(WhereCastR, Spells.R.Range).To3DWorld());
                }
            }

        }
        #endregion

        #region Orbwalker
        public static GameObject MissileEnd;
        public static Vector3 Missile;
        public static void Obj_GeneralParticleEmitter_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name.Contains("Gnar_Global_Indicator_Line_Beam.troy"))
            {
                MissileEnd = sender;
            }
        }
        public static void Obj_GeneralParticleEmitter_OnDelete(GameObject sender, EventArgs args)
        {
            if (sender.Name.Contains("Gnar_Global_Indicator_Line_Beam.troy"))
            {
                MissileEnd = null;
                Missile = new Vector3();
                Orbwalker.DisableMovement = false;
            }
        }
        public static void Player_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.Slot == SpellSlot.Q && Extension.IsTiny)
                Missile = args.End;
        }
        public static void CatchQ(EventArgs args)
        {
            if (Missile == new Vector3() || MissileEnd == null || !Config.ComboMenu.Checked("takeQ")) return;
            var Distance = Player.Instance.Distance(Game.CursorPos);
            var Intersection = Missile.To2D().Intersection(MissileEnd.Position.To2D(), Player.Instance.Position.Extend(Game.CursorPos, Distance + 500), Game.CursorPos.Extend(Player.Instance, Distance + 500));
            if (Intersection.Point != new Vector2())
            {
                Orbwalker.DisableMovement = true;
                Intersection.Point.To3DWorld().DrawCircle(40, Color.HotPink);
                if (Player.Instance.Distance(Intersection.Point) > 100)
                {
                    Orbwalker.MoveTo(Intersection.Point.To3DWorld());
                }                
            }
        }
        #endregion

        #region Combo
        public static void Combo()
        {
            var Focus = Config.ComboMenu.Checked("W");
            switch (Extension.IsTiny)
            {
                case true:
                    {
                        switch (Extension.IsPrepareToBig)
                        {
                            case true:
                                {
                                    WMegaCast(Config.ComboMenu);
                                    EMegaCast(Config.ComboMenu);
                                    RCast();
                                }
                                break;
                            case false:
                                {
                                    QTinyCast(Focus, Config.ComboMenu);
                                    ETinyCast(Focus, Config.ComboMenu);
                                }
                                break;
                        }
                    }
                    break;
                case false:
                    {
                        QMegaCast(Config.ComboMenu);
                        WMegaCast(Config.ComboMenu);
                        EMegaCast(Config.ComboMenu);
                        RCast();
                    }
                    break;
            }
        }
        #endregion

        #region Harass
        public static void Harass()
        {
            var Focus = Config.HarassMenu.Checked("W");
            switch (Extension.IsTiny)
            {
                case true:
                    {
                        switch (Extension.IsPrepareToBig)
                        {
                            case true:
                                {
                                    WMegaCast(Config.HarassMenu);
                                    EMegaCast(Config.HarassMenu);
                                }
                                break;
                            case false:
                                {
                                    QTinyCast(Focus, Config.HarassMenu);
                                    ETinyCast(Focus, Config.HarassMenu);
                                }
                                break;
                        }
                    }
                    break;
                case false:
                    {
                        QMegaCast(Config.HarassMenu);
                        WMegaCast(Config.HarassMenu);
                        EMegaCast(Config.HarassMenu);
                    }
                    break;
            }
        }
        #endregion

        #region LaneClear
        public static void LaneClear()
        {
            switch (Extension.IsTiny)
            {
                case true:
                    {
                        if (Extension.IsPrepareToBig) return;
                        if (Config.LaneClear.Checked("Q") && Spells.QTiny.IsReady())
                        {
                            Spells.QTiny.CastOnBestFarmPosition(1);
                        }
                        if (Config.LaneClear.Checked("E") && Spells.ETiny.IsReady())
                        {
                            var Minion = Orbwalker.LaneClearMinion;
                            if (Minion != null)
                            {
                                Spells.ETiny.Cast(Minion);
                            }
                        }
                    }
                    break;
                case false:
                    {
                        if (Config.LaneClear.Checked("Qbig") && Spells.QTiny.IsReady())
                        {
                            Spells.QTiny.CastOnBestFarmPosition(1);
                        }
                        if (Config.LaneClear.Checked("Wbig") && Spells.QTiny.IsReady())
                        {
                            Spells.WMega.CastOnBestFarmPosition(3);
                        }
                        if (Config.LaneClear.Checked("Ebig") && Spells.ETiny.IsReady())
                        {
                            var Minion = Orbwalker.LaneClearMinion;
                            if (Minion != null)
                            {
                                Spells.EMega.CastOnBestFarmPosition(3);
                            }
                        }
                    }
                    break;
            }
        }
        #endregion

        #region JungleClear
        public static void JungleClear()
        {
            if (Extension.IsTiny)
            {
                if (Config.JungleClear.Checked("Q") && Spells.QTiny.IsReady())
                {
                    var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.QTiny.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                    if (monster == null || !monster.IsValid) return;
                    if (Orbwalker.IsAutoAttacking) return;
                    Orbwalker.ForcedTarget = null;
                    if (Config.JungleClear.Checked("Q") && Spells.QTiny.IsReady())
                    {
                        Spells.QTiny.Cast(monster);
                    }
                }
                if (Config.JungleClear.Checked("E") && Spells.ETiny.IsReady())
                {
                    var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.ETiny.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                    if (monster != null)
                    {
                        Spells.ETiny.Cast(monster);
                    }
                }
            }
            else
            {
                if (Config.JungleClear.Checked("Qbig") && Spells.QMega.IsReady())
                {
                    var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.QMega.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                    if (monster != null)
                    {
                        Spells.QMega.Cast(monster);
                    }
                }
                if (Config.JungleClear.Checked("Wbig") && Spells.WMega.IsReady())
                {
                    var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.WMega.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                    if (monster != null)
                    {
                        Spells.WMega.Cast(monster);
                    }
                }
                if (Config.JungleClear.Checked("Ebig") && Spells.EMega.IsReady())
                {
                    var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.EMega.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                    if (monster != null)
                    {
                        Spells.EMega.Cast(monster);
                    }
                }
            }
        }
        #endregion

        #region Flee
        public static void Flee()
        {
            if (!Extension.IsTiny) return;
            var Rectangle = new Geometry.Polygon.Rectangle(Player.Instance.Position, Game.CursorPos, 80);
            var obj = ObjectManager.Get<Obj_AI_Base>().Where(x => Rectangle.IsInside(x)).OrderByDescending(x => x.Distance(Player.Instance)).First();
            if (obj != null && Spells.ETiny.IsReady())
            {
                Spells.ETiny.Cast(obj);
            }
        }
        #endregion

        #region On_Unkillable_Minion
        public static void On_Unkillable_Minion(Obj_AI_Base unit, Orbwalker.UnkillableMinionArgs args)
        {
            if (unit == null || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) return;
            if (unit.Health <= Damage.QDamage(unit) && Spells.QTiny.IsReady() && Config.LasthitMenu.Checked("Q"))
            {
                Spells.QTiny.Cast(unit);
            }
            if (unit.Health <= Damage.EDamage(unit) && Spells.ETiny.IsReady() && Config.LasthitMenu.Checked("E"))
            {
                Spells.ETiny.Cast(unit);
            }
        }
        #endregion

        #region KillSteal
        public static void Killsteal(EventArgs args)
        {
            if (Spells.QMega.IsReady() && Config.MiscMenu.Checked("Qks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.QMega.IsInRange(t)
                    && t.Health <= Damage.QDamage(t)), DamageType.Magical);

                if (target != null && !target.Unkillable())
                {
                    var pred = Spells.QMega.GetPrediction(target);
                    {
                        Spells.QMega.Cast(pred.CastPosition);
                    }
                }
            }
            if (Spells.WMega.IsReady() && Config.MiscMenu.Checked("Wks") && !Extension.IsTiny)
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.WMega.IsInRange(t)
                    && t.Health <= Damage.WDamage(t)
                    && !t.Unkillable()), DamageType.Magical);

                if (target != null)
                {
                    var pred = Spells.WMega.GetPrediction(target);
                    {
                        Spells.WMega.Cast(pred.CastPosition);
                    }
                }
            }
            if (Spells.EMega.IsReady() && Config.MiscMenu.Checked("Eks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.EMega.IsInRange(t)
                    && t.Health <= Damage.EDamage(t)
                    && !t.Unkillable()), DamageType.Magical);

                if (target != null)
                {
                    Spells.EMega.Cast(target);
                }
            }
        }
        #endregion

        #region AutoHarass
        public static void AutoHarass(EventArgs args)
        {
            if (!Config.HarassMenu.Checked("keyharass", false)
                || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) return;
            var Focus = Config.HarassMenu.Checked("W");
            switch (Extension.IsTiny)
            {
                case true:
                    {
                        switch (Extension.IsPrepareToBig)
                        {
                            case true:
                                {
                                    WMegaCast(Config.HarassMenu);
                                    EMegaCast(Config.HarassMenu);
                                }
                                break;
                            case false:
                                {
                                    QTinyCast(Focus, Config.HarassMenu);
                                    ETinyCast(Focus, Config.HarassMenu);
                                }
                                break;
                        }
                    }
                    break;
                case false:
                    {
                        QMegaCast(Config.HarassMenu);
                        WMegaCast(Config.HarassMenu);
                        EMegaCast(Config.HarassMenu);
                    }
                    break;
            }
        }
        #endregion

        #region Gapclose
        
        public static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (Extension.IsTiny || !Extension.IsPrepareToBig) return;
            if (Spells.WMega.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 225)
                && (sender.Spellbook.CastEndTime - Game.Time) * 1000 <= Spells.WMega.CastDelay
                && Config.MiscMenu.Checked("gapcloser"))
            {
                Spells.WMega.Cast(sender);
            }         
        }
        #endregion

        #region Interrupter
        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            var Value = Config.MiscMenu.GetValue("interrupt.level", false);
            var Danger = Value == 2 ? DangerLevel.High : Value == 1 ? DangerLevel.Medium : Value == 0 ? DangerLevel.Low : DangerLevel.High;
            if (sender != null
                && sender.IsEnemy
                && Config.MiscMenu.Checked("interrupt")
                && sender.IsValidTarget(Spells.WMega.Range)
                && e.DangerLevel == Danger)
            {
                if (Spells.WMega.IsInRange(sender) && Spells.WMega.IsReady())
                {
                    var pred = Spells.WMega.GetPrediction(sender);
                    Spells.WMega.Cast(pred.CastPosition);
                }
                else if (Spells.R.IsInRange(sender) && Spells.R.IsReady())
                {
                    var WhereWall = new List<Vector3>();
                    var WhereCastR = new Vector3();
                    for (var i = 0; i <= 600; i++)
                    {
                        WhereWall = VectorHelp.GetWallAroundMe(Spells.R.Range, i / 100f);
                    }
                    if (WhereWall.Count != 0)
                    {
                        var Wall = WhereWall.OrderBy(x => x.Distance(sender)).FirstOrDefault();
                        WhereCastR = sender.Position.To2D().Parallel(Wall.To2D(), Player.Instance.Position.To2D());
                    }
                    if (WhereCastR != new Vector3() && Spells.R.IsInRange(WhereCastR))
                    {
                        Spells.R.Cast(WhereCastR);
                    }
                    if (WhereCastR == new Vector3())
                    {
                        Spells.R.Cast(sender);
                    }
                }
            }
        }
        #endregion
    }
}