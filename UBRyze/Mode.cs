using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using System;
using System.Linq;

namespace UBRyze
{
    class Mode
    {
        #region Combo
        public static void Do_Damage_Combo(Menu mode)
        {
            if (!Extensions.CanNextSpell) return;
            if (mode.Checked("W") && Spells.W.IsReady() && !Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                {
                    if (target != null && target.IsValid() && !target.HasBuff("RyzeE"))
                    {
                        Spells.W.Cast(target);
                    }
                }
            }
            if (mode.Checked("Q") && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                {
                    if (target != null && target.IsValid())
                    {
                        var pred = Spells.Q.GetPrediction(target);
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
            if (mode.Checked("E") && Spells.E.IsReady() && !Spells.Q.IsReady()
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
                        if (targetE != null)
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
        public static void Do_Flee_Combo(Menu mode)
        {
            if (!Extensions.CanNextSpell) return;
            if (mode.Checked("W") && Spells.W.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Magical);
                {
                    if (target != null && target.IsValid() && target.HasBuff("RyzeE"))
                    {
                        Spells.W.Cast(target);
                    }
                }
            }
            if (mode.Checked("Q") && Spells.Q.IsReady() && (Player.Instance.HasBuff("RyzeQIconFullCharge") || Player.Instance.HasBuff("RyzeQIconNoCharge")))
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                {
                    if (target != null && target.IsValid())
                    {
                        var pred = Spells.Q.GetPrediction(target);
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
            if (mode.Checked("E") && Spells.E.IsReady() && !Spells.Q.IsReady())
            {
                if (Extensions.MinionEDie != null)
                {
                    var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionEDie.Position, true);
                    {
                        if (targetE != null)
                        {
                            Spells.E.Cast(Extensions.MinionEDie);
                        }
                    }
                }
                if (Extensions.MinionHasEBuff != null)
                {
                    var targetE = TargetSelector.GetTarget(300, DamageType.Magical, Extensions.MinionHasEBuff.Position, true);
                    if (targetE != null)
                    {
                        Spells.E.Cast(Extensions.MinionHasEBuff);
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
        #endregion 

        #region Combo
        public static void Combo()
        {
            switch (Config.ComboMenu.GetValue("combostyle", false))
            {
                case 0:
                    {
                        Do_Damage_Combo(Config.ComboMenu);
                    }
                    break;
                case 1:
                    {
                        Do_Flee_Combo(Config.ComboMenu);
                    }
                    break;
                case 2:
                    {
                        if (Player.Instance.HealthPercent <= Config.ComboMenu.GetValue("hpcbsmart"))
                        {
                            Do_Flee_Combo(Config.ComboMenu);
                        }
                        else
                        {
                            Do_Damage_Combo(Config.ComboMenu);
                        }
                    }
                    break;
            }
        }
        #endregion

        #region Harass
        public static void Harass()
        {
            if (Player.Instance.ManaPercent < Config.HarassMenu.GetValue("hr")) return;
            Do_Damage_Combo(Config.HarassMenu);
        }
        #endregion

        #region LaneClear
        public static void LaneClear()
        {
            if (Player.Instance.ManaPercent < Config.LaneClear.GetValue("lc")) return;
            if (!Config.LaneClear.Checked("logiclc"))
            {
                if (Config.LaneClear.Checked("Q") && Spells.Q.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.Q.Range) && Spells.Q.IsInRange(m)).FirstOrDefault();
                    {
                        if (minion != null)
                        {
                            Spells.Q.Cast(minion);
                        }
                    }
                }
                if (Config.LaneClear.Checked("W") && Spells.W.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.W.Range) && Spells.W.IsInRange(m)).FirstOrDefault();
                    {
                        if (minion != null)
                        {
                            Spells.W.Cast(minion);
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
            }
            else
            {
                var WorstPos = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy), 300f, (int)Spells.E.Range);
                var minionnear = EntityManager.MinionsAndMonsters.EnemyMinions.OrderBy(m => m.Distance(WorstPos.CastPosition)).FirstOrDefault();
                if (Config.LaneClear.Checked("Q") && Spells.Q.IsReady())
                {
                    if (Extensions.CountminionEbuff >= Config.LaneClear.GetValue("Qlc"))
                    {
                        var minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy).Where(m => m.HasBuff("RyzeE")).FirstOrDefault();
                        Spells.Q.Cast(minion);
                    }
                }
                if (Config.LaneClear.Checked("W") && Spells.W.IsReady())
                {
                    if (Extensions.MinionHasEBuff != null && Extensions.MinionHasEBuff.Health <= Damages.WDamage(Extensions.MinionHasEBuff))
                    {
                        Spells.W.Cast(Extensions.MinionHasEBuff);
                    }
                }
                if (Config.LaneClear.Checked("E") && Spells.E.IsReady())
                {
                    if (Extensions.MinionHasEBuff != null)
                    {
                        Spells.E.Cast(Extensions.MinionHasEBuff);
                    }
                    if (Extensions.MinionEDie != null)
                    {
                        Spells.E.Cast(Extensions.MinionEDie);
                    }  
                    if (Extensions.MinionHasEBuff == null && Extensions.MinionEDie == null)
                    {
                        Spells.E.Cast(minionnear);
                    }                               
                }
            }
        }
        #endregion

        #region JungleClear
        public static void JungleClear()
        {
            if (Player.Instance.ManaPercent < Config.JungleClear.GetValue("jc")) return;
            if (Config.JungleClear.Checked("Q") && Spells.Q.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster == null || !monster.IsValid) return;
                if (Orbwalker.IsAutoAttacking) return;
                Orbwalker.ForcedTarget = null;
                if (Config.JungleClear.Checked("Q") && Spells.Q.IsReady())
                {
                    Spells.Q.Cast(monster);
                }
            }
            if (Config.JungleClear.Checked("E") && Spells.E.IsReady())
            {
                var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x != null && x.IsMonster && x.IsValidTarget(Spells.W.Range)).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (monster != null)
                {
                    Spells.E.Cast(monster);
                }
            }
            if (Config.JungleClear.Checked("W") && Spells.W.IsReady())
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
            if (Player.Instance.ManaPercent >= Config.LasthitMenu.GetValue("lh"))
            {
                if (Config.LaneClear.Checked("Q") && Spells.Q.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.Q.Range) && m.Health <= Damages.QDamage(m)).FirstOrDefault();
                    if (minion != null)
                    {
                        Spells.Q.Cast(minion);
                    }
                }
                if (Config.LaneClear.Checked("W") && Spells.W.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Spells.W.Range) && m.Health <= Damages.WDamage(m)).FirstOrDefault();
                    if (minion != null)
                    {
                        Spells.W.Cast(minion);
                    }
                }
                if (Config.LaneClear.Checked("E") && Spells.E.IsReady())
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
            if (Config.ComboMenu.Checked("useflee"))
            {
                Do_Flee_Combo(Config.ComboMenu);
            }
        }
        #endregion

        #region Zhonya & Flee
        public static void Zhonya(EventArgs args)
        {
            if (Config.AutoMenu.Checked("R", false) && Spells.R.IsReady() && Spells.Zhonya.IsOwned() && Spells.Zhonya.IsReady())
            {
                var NearestTurret = EntityManager.Turrets.Allies.Where(x => !x.IsDead).OrderBy(x => x.Distance(Player.Instance.Position)).FirstOrDefault();
                if (Spells.R.IsInRange(NearestTurret))
                {
                    if (Spells.R.Cast(NearestTurret))
                    Spells.Zhonya.Cast();
                }
                else 
                {
                    if(Spells.R.Cast(Player.Instance.Position.Extend(NearestTurret, Spells.R.Range).To3DWorld()))
                    Spells.Zhonya.Cast();
                }
            }
        }
        #endregion

        #region On_Unkillable_Minion
        public static void On_Unkillable_Minion(Obj_AI_Base unit, Orbwalker.UnkillableMinionArgs args)
        {
            if (Config.LasthitMenu.GetValue("unkillmanage") > Player.Instance.ManaPercent || unit == null
                || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) return;
            if(unit.Health <= Damages.QDamage(unit) && Spells.Q.IsReady() && Config.LasthitMenu.Checked("Qlh"))
            {
                Spells.Q.Cast(unit);
            }
            if (unit.Health <= Damages.WDamage(unit) && Spells.W.IsReady() && Config.LasthitMenu.Checked("Wlh"))
            {
                Spells.W.Cast(unit);
            }
            if (unit.Health <= Damages.EDamage(unit) && Spells.E.IsReady() && Config.LasthitMenu.Checked("Elh"))
            {
                Spells.E.Cast(unit);
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
                    && t.Health <= Damages.QDamage(t)), DamageType.Magical);

                if (target != null && Extensions.Unkillable(target) == false)
                {
                    var pred = Spells.Q.GetPrediction(target);
                    {
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
            if (Spells.W.IsReady() && Config.MiscMenu.Checked("Wks"))
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
            if (Spells.E.IsReady() && Config.MiscMenu.Checked("Eks"))
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
            if (Player.Instance.ManaPercent < Config.HarassMenu.GetValue("autohrmng")) return;
            if (!Config.HarassMenu.Checked("keyharass", false)) return;
            Do_Damage_Combo(Config.HarassMenu);
        }
        #endregion

        #region Gapclose
        public static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (Spells.W.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 225 /*|| args.End.IsInRange(Player.Instance, Spells.W.Range)*/)
                && (sender.Spellbook.CastEndTime - Game.Time) * 1000 <= Spells.E.CastDelay
                && Config.MiscMenu.Checked("gapcloser"))
            {
                Spells.W.Cast(sender);
            }        
        } 
        #endregion
    }
}
