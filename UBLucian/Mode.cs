using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using SharpDX;
using Modes = EloBuddy.SDK.Orbwalker.ActiveModes;


namespace UBLucian
{
    class Mode
    {
        public static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.Slot == SpellSlot.Q || args.Slot == SpellSlot.W || args.Slot == SpellSlot.E)
            {
                Extension.HasPassive = true;
                Orbwalker.ResetAutoAttack();
                if (args.Slot == SpellSlot.Q && Config.MiscMenu.Checked("Qcancel"))
                {
                    var minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.IsValid && !x.IsDead && Player.Instance.IsInAutoAttackRange(x)).OrderBy(x => x.Distance(Player.Instance));
                    var champs = EntityManager.Heroes.Enemies.Where(x => x.IsValid && !x.IsDead && Player.Instance.IsInAutoAttackRange(x)).OrderBy(x => x.Distance(Player.Instance));
                    var monsters = EntityManager.MinionsAndMonsters.Monsters.Where(x => x.IsValid && !x.IsDead && Player.Instance.IsInAutoAttackRange(x)).OrderBy(x => x.Distance(Player.Instance));
                    if (champs != null)
                    {
                        Core.DelayAction(() =>
                        Player.IssueOrder(GameObjectOrder.AttackUnit, champs.FirstOrDefault()), 375);
                    }
                    else if (monsters != null)
                    {
                        Core.DelayAction(() =>
                        Player.IssueOrder(GameObjectOrder.AttackUnit, monsters.FirstOrDefault()), 375);
                    }
                    else if (minions != null)
                    {
                        Core.DelayAction(() =>
                        Player.IssueOrder(GameObjectOrder.AttackUnit, minions.FirstOrDefault()), 375);
                    }
                    else
                    {
                        return;
                    }
                }
            }          
        }
        public static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.Buff.Name.ToLower() == "lucianpassivebuff")
                Extension.HasPassive = true;
        }
        public static void Obj_AI_Base_OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.Buff.Name.ToLower() == "lucianpassivebuff")
                Extension.HasPassive = false;
        }
        public static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            Core.DelayAction(() => Extension.HasPassive = false, 100);
        }

        #region Combo
        public static void Combo()
        {
            if (Config.ComboMenu.GetValue("Q", false) > 0 && Extension.CanCastNextSpell(Config.ComboMenu) && !Player.Instance.IsDashing() && Spells.Q.IsReady() && !Spells.E.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.Q2.Range, DamageType.Physical);
                if (Target != null)
                {
                    if (Spells.Q.IsInRange(Target) && Config.ComboMenu.GetValue("Q", false) != 2)
                    {
                        Spells.Q.Cast(Target);
                    }
                    else
                    {
                        var pred = Spells.Q2.GetPrediction(Target);
                        pred.QExtend();
                    }
                }
            }
            if (Config.ComboMenu.GetValue("W", false) > 0 && Extension.CanCastNextSpell(Config.ComboMenu) && !Player.Instance.IsDashing() && Spells.W.IsReady() && !Spells.E.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                if (Target != null)
                {
                    var pred = Spells.W.GetPrediction(Target);
                    switch (Config.ComboMenu.GetValue("W", false))
                    {
                        case 1:
                            {
                                if (EntityManager.Heroes.Enemies.Any(x => Player.Instance.IsInAutoAttackRange(x)))
                                {
                                    var Collision = pred.CollisionObjects.FirstOrDefault();
                                    if (Collision != null)
                                    {
                                        Spells.W.Cast(Collision);
                                    }
                                    else
                                    {
                                        Spells.W.Cast(pred.CastPosition);
                                    }
                                }
                            }
                            break;
                        case 2:
                            {
                                var Collision = pred.CollisionObjects.FirstOrDefault();
                                if (Collision.Distance(Target) <= 300 || Collision == null)
                                {
                                    Spells.W.Cast(pred.CastPosition);
                                }
                            }
                            break;
                    }
                }
            }
            if (Config.ESettings.GetValue("E", false) > 0 && Config.ComboMenu.Checked("E") && Extension.CanCastNextSpell(Config.ComboMenu) && Spells.E.IsReady())
            {
                if (_E_.GetPos() != new Vector3() || _E_.GetPos() != Vector3.Zero)
                {
                    Spells.E.Cast(_E_.GetPos());
                }
            }
        }
        #endregion

        #region Harass
        public static void Harass()
        {
            if (Config.HarassMenu.GetValue("hr") > Player.Instance.ManaPercent) return;
            if (Config.HarassMenu.GetValue("Q", false) > 0 && Extension.CanCastNextSpell(Config.HarassMenu) && !Player.Instance.IsDashing() && Spells.Q.IsReady() && !Spells.E.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.Q2.Range, DamageType.Physical);
                if (Target != null)
                {
                    if (Spells.Q.IsInRange(Target) && Config.HarassMenu.GetValue("Q", false) != 2)
                    {
                        Spells.Q.Cast(Target);
                    }
                    else
                    {
                        var pred = Spells.Q2.GetPrediction(Target);
                        pred.QExtend();
                    }
                }
            }
            if (Config.HarassMenu.Checked("W") && Extension.CanCastNextSpell(Config.HarassMenu) && !Player.Instance.IsDashing() && Spells.W.IsReady() && !Spells.E.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                if (Target != null)
                {
                    var pred = Spells.W.GetPrediction(Target);
                    switch (Config.ComboMenu.GetValue("W", false))
                    {
                        case 1:
                            {
                                if (EntityManager.Heroes.Enemies.Any(x => Player.Instance.IsInAutoAttackRange(x)))
                                {
                                    var Collision = pred.CollisionObjects.FirstOrDefault();
                                    if (Collision != null)
                                    {
                                        Spells.W.Cast(Collision);
                                    }
                                    else
                                    {
                                        Spells.W.Cast(pred.CastPosition);
                                    }
                                }
                            }
                            break;
                        case 2:
                            {
                                var Collision = pred.CollisionObjects.FirstOrDefault();
                                if (Collision.Distance(Target) <= 300 || Collision == null)
                                {
                                    Spells.W.Cast(pred.CastPosition);
                                }
                            }
                            break;
                    }
                }
            }
            if (Config.ESettings.GetValue("E", false) > 0 && Config.HarassMenu.Checked("E") && Extension.CanCastNextSpell(Config.HarassMenu) && Spells.E.IsReady())
            {
                if (_E_.GetPos() != new Vector3() || _E_.GetPos() != Vector3.Zero)
                {
                    Spells.E.Cast(_E_.GetPos());
                }
            }
        }
        #endregion

        #region LaneClear
        public static void LaneClear()
        {
            if (Config.LaneClear.GetValue("lc") > Player.Instance.ManaPercent) return;
            if (Config.LaneClear.Checked("Q") && Spells.Q.IsReady() && Extension.CanCastNextSpell(Config.LaneClear) && !Player.Instance.IsDashing())
            {
                var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Spells.Q.Range);
                if (minions.Count() >= Config.LaneClear.GetValue("Qhit"))
                {
                    foreach (var minion in minions)
                    {
                        var hitbox = new Geometry.Polygon.Rectangle(Player.Instance.Position, minion.Position, 65f);
                        var Inside = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => hitbox.IsInside(x));
                        if (Inside.Count() >= Config.LaneClear.GetValue("Qhit"))
                        {
                            Spells.Q.Cast(minion);
                            break;
                        }
                    }
                }
            }
            if (Config.LaneClear.Checked("W") && Spells.W.IsReady() && Extension.CanCastNextSpell(Config.LaneClear) && !Player.Instance.IsDashing())
            {
                var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.W.Range)).FirstOrDefault();
                if (minion != null)
                {
                    Spells.W.Cast(minion);
                }
            }
            if (Config.LaneClear.Checked("E") && Spells.E.IsReady() && Extension.CanCastNextSpell(Config.LaneClear))
            {
                var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.E.Range + Player.Instance.GetAutoAttackRange(m))).FirstOrDefault();
                if (minion != null)
                {
                    var Position = _E_.Intersection_Of_2Circle(Player.Instance.Position.To2D(), Spells.E.Range, minion.Position.To2D(), Player.Instance.GetAutoAttackRange(minion));
                    if (Position.Count() > 0)
                    {
                        Spells.E.Cast(Position.OrderBy(x => x.Distance(Game.CursorPos)).FirstOrDefault().To3DWorld());
                    }
                }
            }
        }
        #endregion

        #region JungleClear
        public static void JungleClear()
        {
            if (Config.JungleClear.GetValue("jc") > Player.Instance.ManaPercent) return;
            if (Config.JungleClear.Checked("Q") && Spells.Q.IsReady() && Extension.CanCastNextSpell(Config.JungleClear) && !Player.Instance.IsDashing())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster == null || !monster.IsValid) return;
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                Spells.Q.Cast(monster);
            }
            if (Config.JungleClear.Checked("W") && Spells.W.IsReady() && Extension.CanCastNextSpell(Config.JungleClear) && !Player.Instance.IsDashing())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.W.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster != null)
                {
                    Spells.W.Cast(monster);
                }
            }
            if (Config.JungleClear.Checked("E") && Spells.E.IsReady() && Extension.CanCastNextSpell(Config.JungleClear))
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.E.Range + Player.Instance.GetAutoAttackRange(x))).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster != null)
                {
                    var Position = _E_.Intersection_Of_2Circle(Player.Instance.Position.To2D(), Spells.E.Range, monster.Position.To2D(), Player.Instance.GetAutoAttackRange(monster));
                    if (Position.Count() > 0)
                    {
                        Spells.E.Cast(Position.OrderBy(x => x.Distance(Game.CursorPos)).FirstOrDefault().To3DWorld());
                    }
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
                    && Spells.Q2.IsInRange(t)
                    && t.Health <= Damages.QDamage(t)), DamageType.Physical);

                if (target != null && !target.Unkillable())
                {
                    if (Spells.Q.IsInRange(target))
                    {
                        Spells.Q.Cast(target);
                    }
                    else if (Spells.Q2.IsInRange(target))
                    {
                        var pred = Spells.Q2.GetPrediction(target);
                        pred.QExtend();
                    }
                }
            }
            if (Spells.W.IsReady() && Config.MiscMenu.Checked("Wks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.W.IsInRange(t)
                    && t.Health <= Damages.WDamage(t)
                    && !t.Unkillable()), DamageType.Magical);

                if (target != null && !target.Unkillable())
                {
                    var pred = Spells.W.GetPrediction(target);
                    Spells.W.Cast(pred.CastPosition);
                }
            }
            if (Spells.E.IsReady() && Config.MiscMenu.Checked("Eks"))
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.E.IsInRange(t)
                    && t.Health <= Damages.EDamage(t)
                    && !t.Unkillable()), DamageType.Physical);

                if (target != null)
                {
                    if (_E_.GetPos() != new Vector3() || _E_.GetPos() != Vector3.Zero)
                    {
                        Spells.E.Cast(_E_.GetPos());
                    }
                }
            }
            if (Spells.R.IsReady() && Config.MiscMenu.GetValue("Rks", false) > 0)
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.R.IsInRange(t)
                    && t.Health <= Damages.RDamage(t, Config.MiscMenu.GetValue("Rkstick"))
                    && !t.Unkillable()), DamageType.Physical);
                if (target != null)
                {
                    var CanRKS = Config.MiscMenu.GetValue("Rks", false) == 1 ? true :
                        (Spells.Q2.IsInRange(target) && Spells.Q.IsReady())
                        || (Spells.W.IsInRange(target) && Spells.W.IsReady())
                        || Spells.E.IsReady()
                        || Player.Instance.IsInAutoAttackRange(target) ? false : true;

                    if (CanRKS)
                    {
                        var pred = Spells.R.GetPrediction(target);
                        Spells.R.Cast(pred.CastPosition);
                    }
                }
            }
        }
            
        
        #endregion

        #region On Unkillable Minion
        public static void On_Unkillable_Minion(Obj_AI_Base unit, Orbwalker.UnkillableMinionArgs args)
        {
            if (Config.LasthitMenu.GetValue("lh") > Player.Instance.ManaPercent || unit == null
                || Orbwalker.ActiveModes.Combo.IsActive()) return;
            if (args.RemainingHealth <= Damages.QDamage(unit) && Spells.Q.IsReady() && Config.LasthitMenu.Checked("Q"))
            {
                if (Spells.Q.IsInRange(unit))
                {
                    Spells.Q.Cast(unit);
                }
                else if (Spells.Q2.IsInRange(unit))
                {
                    var pred = Spells.Q2.GetPrediction(unit);
                    pred.QExtend();
                }
            }
            if (args.RemainingHealth <= Damages.WDamage(unit) && Spells.W.IsReady() && Config.LasthitMenu.Checked("W"))
            {
                Spells.W.Cast(unit);
            }
            if (args.RemainingHealth <= Damages.EDamage(unit) && Spells.E.IsReady() && Config.LasthitMenu.Checked("E"))
            {
                var Position = _E_.Intersection_Of_2Circle(Player.Instance.Position.To2D(), Spells.E.Range, unit.Position.To2D(), Player.Instance.GetAutoAttackRange(unit));
                if (Position.Count() > 0)
                {
                    Spells.E.Cast(Position.OrderBy(x => x.Distance(Game.CursorPos)).FirstOrDefault().To3DWorld());
                }
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
            if (Config.HarassMenu.GetValue("Q", false) > 0 && Extension.CanCastNextSpell(Config.HarassMenu) && !Player.Instance.IsDashing() && Spells.Q.IsReady() && !Spells.E.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.Q2.Range, DamageType.Physical);
                if (Target != null)
                {
                    if (Spells.Q.IsInRange(Target) && Config.ComboMenu.GetValue("Q", false) != 2)
                    {
                        Spells.Q.Cast(Target);
                    }
                    else
                    {
                        var pred = Spells.Q2.GetPrediction(Target);
                        pred.QExtend();
                    }
                }
            }
            if (Config.HarassMenu.GetValue("W", false) > 0 && Extension.CanCastNextSpell(Config.HarassMenu) && !Player.Instance.IsDashing() && Spells.W.IsReady() && !Spells.E.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                if (Target != null)
                {
                    var pred = Spells.W.GetPrediction(Target);
                    switch (Config.HarassMenu.GetValue("W", false))
                    {
                        case 1:
                            {
                                if (EntityManager.Heroes.Enemies.Any(x => Player.Instance.IsInAutoAttackRange(x)))
                                {
                                    var Collision = pred.CollisionObjects.FirstOrDefault();
                                    if (Collision != null)
                                    {
                                        Spells.W.Cast(Collision);
                                    }
                                    else
                                    {
                                        Spells.W.Cast(pred.CastPosition);
                                    }
                                }
                            }
                            break;
                        case 2:
                            {
                                var Collision = pred.CollisionObjects.FirstOrDefault();
                                if (Collision.Distance(Target) <= 300 || Collision == null)
                                {
                                    Spells.W.Cast(pred.CastPosition);
                                }
                            }
                            break;
                    }
                }
            }
            if (Config.ESettings.GetValue("E", false) > 0 && Config.HarassMenu.Checked("E") && Extension.CanCastNextSpell(Config.HarassMenu) && Spells.E.IsReady())
            {
                if (_E_.GetPos() != new Vector3() || _E_.GetPos() != Vector3.Zero)
                {
                    Spells.E.Cast(_E_.GetPos());
                }
            }
        }
        #endregion

        #region Gapcloser
        public static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (Spells.E.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 100 || args.End.IsInRange(Player.Instance, Spells.E.Range))
                && sender.Spellbook.CastEndTime <= Spells.W.CastDelay
                && Config.MiscMenu.Checked("Egap"))
            {
                var ECast = args.Start.Extend(Player.Instance, args.Start.Distance(Player.Instance) + Spells.E.Range).To3DWorld();
                Spells.E.Cast(ECast);
            }
        }
        #endregion
    }
}
