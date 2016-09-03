using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UBRyze
{
    class Mode
    {
        #region Combo
        public static void Combo()
        {
            switch (Config.ComboMenu["combostyle"].Cast<ComboBox>().CurrentValue)
            {
                case 0:
                    {
                        if (Config.ComboMenu["useWcb"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() && !Spells.Q.IsReady())
                        {
                            var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                            {
                                if (target != null && target.IsValid() && !target.HasBuff("RyzeE"))
                                {
                                    Spells.W.Cast(target);
                                }
                            }
                        }
                        if (Config.ComboMenu["useQcb"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
                        {
                            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                            {
                                if (target != null && target.IsValid())
                                {
                                    var pred = Spells.Q.GetPrediction(target);
                                    Spells.Q.Cast(pred.CastPosition);
                                }
                                if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                                {
                                    var pred = Spells.Q.GetPrediction(target);
                                    var CollisionObject = pred.GetCollisionObjects<Obj_AI_Base>().ToList();
                                    var CollisionObjectE = pred.GetCollisionObjects<Obj_AI_Base>().Where(x => x.HasBuff("RyzeE")).OrderBy(x => x.Distance(target));
                                    if (!CollisionObject.Any())
                                    {
                                        Spells.Q.Cast(pred.CastPosition);
                                    }
                                    else
                                    {
                                        if (CollisionObjectE.Any())
                                        {
                                            if (CollisionObjectE.First().Distance(target) <= 300)
                                            {
                                                Spells.Q.Cast(CollisionObjectE.First());
                                            }
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        if (Config.ComboMenu["useEcb"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() && !Spells.Q.IsReady()
                            && (!Spells.W.IsReady() || Math.Abs(Player.Instance.PercentCooldownMod) >= 35))
                        {
                            if (Extensions.MinionHasEBuff != null)
                            {
                                var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionHasEBuff.Position, true);
                                if (targetE != null && Extensions.MinionHasEBuff.Health <= Damages.EDamage(Extensions.MinionHasEBuff))
                                {
                                    Spells.E.Cast(Extensions.MinionHasEBuff);
                                }
                            }
                            if (Extensions.MinionEDie != null)
                            {
                                var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionEDie.Position, true);
                                {
                                    if (targetE != null && Extensions.MinionEDie.Health <= Damages.EDamage(Extensions.MinionEDie))
                                    {
                                        Spells.E.Cast(Extensions.MinionEDie);
                                    }
                                }
                            }
                            var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                            {
                                if (target != null && target.IsValid())
                                {
                                    Spells.E.Cast(target);
                                }
                            }
                        }
                        if (Config.ComboMenu["useRcb"].Cast<CheckBox>().CurrentValue && Spells.R.IsReady() && !Spells.Q.IsReady())
                        {
                            var target = TargetSelector.GetTarget(Spells.R.Range, DamageType.Magical);
                            {
                                if (target != null && target.IsValid())
                                {
                                    var pred = Spells.R.GetPrediction(target);
                                    if (!pred.CastPosition.IsUnderTurret())
                                    {
                                        Spells.R.Cast(pred.CastPosition);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        if (Config.ComboMenu["useWcb"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() && !Spells.Q.IsReady())
                        {
                            var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                            {
                                if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                                {
                                    Spells.W.Cast(target);
                                }
                            }
                        }
                        if (Config.ComboMenu["useQcb"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady()
                        && (Player.Instance.HasBuff("RyzeQIconFullCharge") || Player.Instance.HasBuff("RyzeQIconNoCharge")))
                        {
                            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                            {
                                if (target != null && target.IsValid())
                                {
                                    var pred = Spells.Q.GetPrediction(target);
                                    Spells.Q.Cast(pred.CastPosition);
                                }
                                if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                                {
                                    var pred = Spells.Q.GetPrediction(target);
                                    var CollisionObject = pred.GetCollisionObjects<Obj_AI_Base>().ToList();
                                    var CollisionObjectE = pred.GetCollisionObjects<Obj_AI_Base>().Where(x => x.HasBuff("RyzeE")).OrderBy(x => x.Distance(target));
                                    if (!CollisionObject.Any())
                                    {
                                        Spells.Q.Cast(pred.CastPosition);
                                    }
                                    else
                                    {
                                        if (CollisionObjectE.Any())
                                        {
                                            if (CollisionObjectE.First().Distance(target) <= 300)
                                            {
                                                Spells.Q.Cast(CollisionObjectE.First());
                                            }
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        if (Config.ComboMenu["useEcb"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() && !Spells.Q.IsReady())
                        {
                            if (Extensions.MinionEDie != null)
                            {
                                var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionEDie.Position, true);
                                {
                                    if (targetE != null && Extensions.MinionEDie.Health <= Damages.EDamage(Extensions.MinionEDie))
                                    {
                                        Spells.E.Cast(Extensions.MinionEDie);
                                    }
                                }
                            }
                            var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                            {
                                if (target != null && target.IsValid())
                                {
                                    Spells.E.Cast(target);
                                }
                            }
                            if (Extensions.MinionHasEBuff != null)
                            {
                                var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionHasEBuff.Position, true);
                                if (targetE != null && Extensions.MinionHasEBuff.Health <= Damages.EDamage(Extensions.MinionHasEBuff))
                                {
                                    Spells.E.Cast(Extensions.MinionHasEBuff);
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        if (Player.Instance.HealthPercent <= Config.ComboMenu["hpcbsmart"].Cast<Slider>().CurrentValue)
                        {
                            if (Config.ComboMenu["useWcb"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() && !Spells.Q.IsReady())
                            {
                                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                                {
                                    if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                                    {
                                        Spells.W.Cast(target);
                                    }
                                }
                            }
                            if (Config.ComboMenu["useQcb"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady()
                            && (Player.Instance.HasBuff("RyzeQIconFullCharge") || Player.Instance.HasBuff("RyzeQIconNoCharge")))
                            {
                                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                                {
                                    if (target != null && target.IsValid())
                                    {
                                        var pred = Spells.Q.GetPrediction(target);
                                        Spells.Q.Cast(pred.CastPosition);
                                    }
                                    if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                                    {
                                        var pred = Spells.Q.GetPrediction(target);
                                        var CollisionObject = pred.GetCollisionObjects<Obj_AI_Base>().ToList();
                                        var CollisionObjectE = pred.GetCollisionObjects<Obj_AI_Base>().Where(x => x.HasBuff("RyzeE")).OrderBy(x => x.Distance(target));
                                        if (!CollisionObject.Any())
                                        {
                                            Spells.Q.Cast(pred.CastPosition);
                                        }
                                        else
                                        {
                                            if (CollisionObjectE.Any())
                                            {
                                                if (CollisionObjectE.First().Distance(target) <= 300)
                                                {
                                                    Spells.Q.Cast(CollisionObjectE.First());
                                                }
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            if (Config.ComboMenu["useEcb"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() && !Spells.Q.IsReady())
                            {
                                if (Extensions.MinionHasEBuff != null)
                                {
                                    var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionHasEBuff.Position, true);
                                    if (targetE != null && Extensions.MinionHasEBuff.Health <= Damages.EDamage(Extensions.MinionHasEBuff))
                                    {
                                        Spells.E.Cast(Extensions.MinionHasEBuff);
                                    }
                                }
                                if (Extensions.MinionEDie != null)
                                {
                                    var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionEDie.Position, true);
                                    {
                                        if (targetE != null && Extensions.MinionEDie.Health <= Damages.EDamage(Extensions.MinionEDie))
                                        {
                                            Spells.E.Cast(Extensions.MinionEDie);
                                        }
                                    }
                                }
                                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                                {
                                    if (target != null && target.IsValid())
                                    {
                                        Spells.E.Cast(target);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Config.ComboMenu["useWcb"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() && !Spells.Q.IsReady()
                                && (!Spells.E.IsReady() || Math.Abs(Player.Instance.PercentCooldownMod) < 35))
                            {
                                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                                {
                                    if (target != null && target.IsValid() && !target.HasBuff("RyzeE"))
                                    {
                                        Spells.W.Cast(target);
                                    }
                                }
                            }
                            if (Config.ComboMenu["useQcb"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
                            {
                                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                                {
                                    if (target != null && target.IsValid())
                                    {
                                        var pred = Spells.Q.GetPrediction(target);
                                        Spells.Q.Cast(pred.CastPosition);
                                    }
                                    if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                                    {
                                        var pred = Spells.Q.GetPrediction(target);
                                        var CollisionObject = pred.GetCollisionObjects<Obj_AI_Base>().ToList();
                                        var CollisionObjectE = pred.GetCollisionObjects<Obj_AI_Base>().Where(x => x.HasBuff("RyzeE")).OrderBy(x => x.Distance(target));
                                        if (!CollisionObject.Any())
                                        {
                                            Spells.Q.Cast(pred.CastPosition);
                                        }
                                        else
                                        {
                                            if (CollisionObjectE.Any())
                                            {
                                                if (CollisionObjectE.First().Distance(target) <= 300)
                                                {
                                                    Spells.Q.Cast(CollisionObjectE.First());
                                                }
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            if (Config.ComboMenu["useEcb"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() && !Spells.Q.IsReady()
                                && (!Spells.W.IsReady() || Math.Abs(Player.Instance.PercentCooldownMod) >= 35))
                            {
                                if (Extensions.MinionHasEBuff != null)
                                {
                                    var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionHasEBuff.Position, true);
                                    if (targetE != null)
                                    {
                                        Spells.E.Cast(Extensions.MinionHasEBuff);
                                    }
                                }
                                if (Extensions.MinionEDie != null)
                                {
                                    var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionEDie.Position, true);
                                    {
                                        if (targetE != null && Extensions.MinionEDie.Health <= Damages.EDamage(Extensions.MinionEDie))
                                        {
                                            Spells.E.Cast(Extensions.MinionEDie);
                                        }
                                    }
                                }
                                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                                {
                                    if (target != null && target.IsValid())
                                    {
                                        Spells.E.Cast(target);
                                    }
                                }
                            }
                            if (Config.ComboMenu["useRcb"].Cast<CheckBox>().CurrentValue && Spells.R.IsReady() && !Spells.Q.IsReady())
                            {
                                var target = TargetSelector.GetTarget(Spells.R.Range, DamageType.Magical);
                                {
                                    if (target != null && target.IsValid())
                                    {
                                        var pred = Spells.R.GetPrediction(target);
                                        if (!pred.CastPosition.IsUnderTurret())
                                        {
                                            Spells.R.Cast(pred.CastPosition);
                                        }

                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }
        #endregion

        #region Harass
        public static void Harass()
        {
            if (Player.Instance.ManaPercent < Config.HarassMenu["hrmanage"].Cast<Slider>().CurrentValue) return;
            if (Config.HarassMenu["useQhr"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                {
                    if (target != null && target.IsValid())
                    {
                        var pred = Spells.Q.GetPrediction(target);
                        Spells.Q.Cast(pred.CastPosition);
                    }
                    if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                    {
                        var pred = Spells.Q.GetPrediction(target);
                        var CollisionObject = pred.GetCollisionObjects<Obj_AI_Base>().ToList();
                        var CollisionObjectE = pred.GetCollisionObjects<Obj_AI_Base>().Where(x => x.HasBuff("RyzeE")).OrderBy(x => x.Distance(target)).FirstOrDefault();
                        if (!CollisionObject.Any())
                        {
                            Spells.Q.Cast(pred.CastPosition);
                        }
                        else
                        {
                            if (CollisionObjectE.Distance(target) <= 300)
                            {
                                Spells.Q.Cast(CollisionObjectE);
                            }
                        }
                    }
                }
            }
            if (Config.HarassMenu["useWhr"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() && !Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                {
                    if (target != null && target.IsValid())
                    {
                        Spells.W.Cast(target);
                    }
                }
            }
            if (Config.HarassMenu["useEhr"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() && !Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                {
                    if (target != null && target.IsValid())
                    {
                        Spells.E.Cast(target);
                    }
                }
                if (Extensions.MinionHasEBuff != null)
                {
                    var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionHasEBuff.Position, true);
                    if (targetE != null && Extensions.MinionHasEBuff.Health <= Damages.EDamage(Extensions.MinionHasEBuff))
                    {
                        Spells.E.Cast(Extensions.MinionHasEBuff);
                    }
                }
                if (Extensions.MinionEDie != null)
                {
                    var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionEDie.Position, true);
                    {
                        if (targetE != null && Extensions.MinionEDie.Health <= Damages.EDamage(Extensions.MinionEDie))
                        {
                            Spells.E.Cast(Extensions.MinionEDie);
                        }
                    }
                }
            }
            //if (Config.HarassMenu["useEQhr"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() && Spells.Q.IsLearned)
            //{
            //    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            //    {
            //        if (target != null && target.IsValid())
            //        {
            //            var pred = Spells.Q.GetPrediction(target);
            //            if (pred.CollisionObjects != null)
            //            {
            //                var Obj = pred.CollisionObjects.Where(a => a.Distance(pred.UnitPosition) <= 300).FirstOrDefault();
            //                if (Obj !=  null)
            //                Spells.E.Cast(Obj);
            //            }
            //        }
            //    }
            //}
        }
        #endregion

        #region LaneClear
        public static void LaneClear()
        {
            if (Player.Instance.ManaPercent < Config.LaneClear["lcmanage"].Cast<Slider>().CurrentValue ) return;
            if (!Config.LaneClear["logiclc"].Cast<CheckBox>().CurrentValue)
            {
                if (Config.LaneClear["useQlc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.Q.Range) && Spells.Q.IsInRange(m)).FirstOrDefault();
                    {
                        if (minion != null)
                        {
                            Spells.Q.Cast(minion);
                        }
                    }
                }
                if (Config.LaneClear["useWlc"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.W.Range) && Spells.W.IsInRange(m)).FirstOrDefault();
                    {
                        if (minion != null)
                        {
                            Spells.W.Cast(minion);
                        }
                    }
                }
                if (Config.LaneClear["useElc"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.E.Range) && Spells.E.IsInRange(m)).FirstOrDefault();
                    {
                        if (minion != null)
                        {
                            Spells.E.Cast(minion);
                        }
                    }
                }
            }
            else
            {
                var WorstPos = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy), 300f, (int)Spells.E.Range);
                var minionnear = EntityManager.MinionsAndMonsters.EnemyMinions.OrderBy(m => m.Distance(WorstPos.CastPosition)).FirstOrDefault();
                if (Config.LaneClear["useQlc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
                {
                    if (Extensions.CountminionEbuff >= Config.LaneClear["Qlc"].Cast<Slider>().CurrentValue)
                    {
                        var minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy).Where(m => m.HasBuff("RyzeE")).FirstOrDefault();
                        Spells.Q.Cast(minion);
                    }
                }
                if (Config.LaneClear["useWlc"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady())
                {
                    if (Extensions.MinionHasEBuff != null && Extensions.MinionHasEBuff.Health <= Damages.WDamage(Extensions.MinionHasEBuff))
                    {
                        Spells.W.Cast(Extensions.MinionHasEBuff);
                    }
                }
                if (Config.LaneClear["Elc"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
                {
                    if (Extensions.MinionHasEBuff == null && Extensions.MinionEDie == null)
                    {
                        Spells.E.Cast(minionnear);
                    }
                    if (Extensions.MinionHasEBuff != null)
                    {
                        Spells.E.Cast(Extensions.MinionHasEBuff);
                    }
                    if (Extensions.MinionEDie != null)
                    {
                        Spells.E.Cast(Extensions.MinionEDie);
                    }             
                }
            }
        }
        #endregion

        #region JungleClear
        public static void JungleClear()
        {
            if (Player.Instance.ManaPercent < Config.JungleClear["jcmanage"].Cast<Slider>().CurrentValue) return;
            if (Config.JungleClear["useQjc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster == null || !monster.IsValid) return;
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                if (Config.JungleClear["useQjc"].Cast<CheckBox>().CurrentValue
                    && Spells.Q.IsReady())
                {
                    Spells.Q.Cast(monster);
                }
            }
            if (Config.JungleClear["useEjc"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.W.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster != null)
                {
                    Spells.E.Cast(monster);
                }
            }
            if (Config.JungleClear["useWjc"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.E.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster != null)
                {
                    Spells.W.Cast(monster);
                }
            }
        }
        #endregion

        #region Lasthit
        public static void Lasthit()
        {
            if (Player.Instance.ManaPercent >= Config.LasthitMenu["lhmanage"].Cast<Slider>().CurrentValue)
            {
                if (Config.LaneClear["useQlh"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.Q.Range) && m.Health <= Damages.QDamage(m)).FirstOrDefault();
                    if (minion != null)
                    {
                        Spells.Q.Cast(minion);
                    }
                }
                if (Config.LaneClear["useWlh"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.W.Range) && m.Health <= Damages.WDamage(m)).FirstOrDefault();
                    if (minion != null)
                    {
                        Spells.W.Cast(minion);
                    }
                }
                if (Config.LaneClear["useElh"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.E.Range) && m.Health <= Damages.EDamage(m)).FirstOrDefault();
                    if (minion != null)
                    {
                        Spells.E.Cast(minion);
                    }
                }
            }
        }
        #endregion

        #region Flee
        public static void Flee()
        {
            if (Config.ComboMenu["useflee"].Cast<CheckBox>().CurrentValue)
            {
                if (Config.ComboMenu["useWcb"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() && !Spells.Q.IsReady())
                        {
                            var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                            {
                                if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                                {
                                    Spells.W.Cast(target);
                                }
                            }
                        }
                if (Config.ComboMenu["useQcb"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady()
                && (Player.Instance.HasBuff("RyzeQIconFullCharge") || Player.Instance.HasBuff("RyzeQIconNoCharge")))
                {
                    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    {
                        if (target != null && target.IsValid())
                        {
                            var pred = Spells.Q.GetPrediction(target);
                            Spells.Q.Cast(pred.CastPosition);
                        }
                        if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                        {
                            var pred = Spells.Q.GetPrediction(target);
                            var CollisionObject = pred.GetCollisionObjects<Obj_AI_Base>().ToList();
                            var CollisionObjectE = pred.GetCollisionObjects<Obj_AI_Base>().Where(x => x.HasBuff("RyzeE")).OrderBy(x => x.Distance(target));
                            if (!CollisionObject.Any())
                            {
                                Spells.Q.Cast(pred.CastPosition);
                            }
                            else
                            {
                                if (CollisionObjectE.Any())
                                {
                                    if (CollisionObjectE.First().Distance(target) <= 300)
                                    {
                                        Spells.Q.Cast(CollisionObjectE.First());
                                    }
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region On_Unkillable_Minion
        public static void On_Unkillable_Minion(Obj_AI_Base unit, Orbwalker.UnkillableMinionArgs args)
        {
            if (Config.LasthitMenu["unkillmanage"].Cast<Slider>().CurrentValue > Player.Instance.ManaPercent || unit == null
                || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) return;
            if(unit.Health <= Damages.QDamage(unit) && Spells.Q.IsReady() && Config.LasthitMenu["Qlh"].Cast<CheckBox>().CurrentValue)
            {
                Spells.Q.Cast(unit);
            }
            if (unit.Health <= Damages.WDamage(unit) && Spells.W.IsReady() && Config.LasthitMenu["Wlh"].Cast<CheckBox>().CurrentValue)
            {
                Spells.W.Cast(unit);
            }
            if (unit.Health <= Damages.EDamage(unit) && Spells.E.IsReady() && Config.LasthitMenu["Elh"].Cast<CheckBox>().CurrentValue)
            {
                Spells.E.Cast(unit);
            }
        }
        #endregion
        
        #region KillSteal
        public static void Killsteal(EventArgs args)
        {
            if (Spells.Q.IsReady() && Config.MiscMenu["Qks"].Cast<CheckBox>().CurrentValue)
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.Q.IsInRange(t)
                    && t.Health <= Damages.QDamage(t)), DamageType.Magical);

                if (target != null && Extensions.Unkillable(target) == false)
                {
                    var pred = Spells.Q.GetPrediction(target);
                    {
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
            if (Spells.W.IsReady() && Config.MiscMenu["Wks"].Cast<CheckBox>().CurrentValue)
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.W.IsInRange(t)
                    && t.Health <= Damages.WDamage(t)
                    && !Extensions.Unkillable(t)), DamageType.Magical);

                if (target != null)
                {
                    Spells.W.Cast(target);
                }
            }
            if (Spells.E.IsReady() && Config.MiscMenu["Eks"].Cast<CheckBox>().CurrentValue)
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.E.IsInRange(t)
                    && t.Health <= Damages.EDamage(t)
                    && !Extensions.Unkillable(t)), DamageType.Magical);

                if (target != null)
                {
                    Spells.E.Cast(target);
                }
            }
        }
        #endregion

        #region AutoHarass
        public static void AutoHarass(EventArgs args)
        {
            if (Player.Instance.ManaPercent < Config.HarassMenu["autohrmng"].Cast<Slider>().CurrentValue) return;
            if (!Config.HarassMenu["keyharass"].Cast<KeyBind>().CurrentValue) return;
            if (Config.HarassMenu["useQhr"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                {
                    if (target != null && target.IsValid())
                    {
                        var pred = Spells.Q.GetPrediction(target);
                        Spells.Q.Cast(pred.CastPosition);
                    }
                    if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                    {
                        var pred = Spells.Q.GetPrediction(target);
                        var CollisionObject = pred.GetCollisionObjects<Obj_AI_Base>().ToList();
                        var CollisionObjectE = pred.GetCollisionObjects<Obj_AI_Base>().Where(x => x.HasBuff("RyzeE")).OrderBy(x => x.Distance(target)).FirstOrDefault();
                        if (!CollisionObject.Any())
                        {
                            Spells.Q.Cast(pred.CastPosition);
                        }
                        else
                        {
                            if (CollisionObjectE.Distance(target) <= 300)
                            {
                                Spells.Q.Cast(CollisionObjectE);
                            }
                        }
                    }
                }
            }
            if (Config.HarassMenu["useWhr"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                {
                    if (target != null && target.IsValid())
                    {
                        Spells.W.Cast(target);
                    }
                }
            }
            if (Config.HarassMenu["useEhr"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
            {
                if (Extensions.MinionHasEBuff != null)
                {
                    var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionHasEBuff.Position, true);
                    if (targetE != null && Extensions.MinionHasEBuff.Health <= Damages.EDamage(Extensions.MinionHasEBuff))
                    {
                        Spells.E.Cast(Extensions.MinionHasEBuff);
                    }
                }
                if (Extensions.MinionEDie != null)
                {
                    var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionEDie.Position, true);
                    {
                        if (targetE != null && Extensions.MinionEDie.Health <= Damages.EDamage(Extensions.MinionEDie))
                        {
                            Spells.E.Cast(Extensions.MinionEDie);
                        }
                    }
                }
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                {
                    if (target != null && target.IsValid())
                    {
                        Spells.E.Cast(target);
                    }
                }               
            }
            //if (Config.HarassMenu["useEQhr"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() && Spells.Q.IsLearned)
            //{
            //    var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            //    {
            //        if (target != null && target.IsValid())
            //        {
            //            var pred = Spells.Q.GetPrediction(target);
            //            if (pred.CollisionObjects != null)
            //            {
            //                var Obj = pred.CollisionObjects.Where(a => a.Distance(pred.UnitPosition) <= 300).FirstOrDefault();
            //                Spells.E.Cast(Obj);
            //            }
            //        }
            //    }
            //}
        }
        #endregion

        #region Gapclose
        public static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (Spells.W.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 100 || args.End.IsInRange(Player.Instance, Spells.W.Range))
                && sender.Spellbook.CastEndTime <= Spells.W.CastDelay
                && Config.MiscMenu["gapcloser"].Cast<CheckBox>().CurrentValue)
            {
                Spells.W.Cast(sender);
            }
            
        } 
        #endregion
    }
}
