using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.ThirdParty.Glide;
using System;
using System.Linq;
using SharpDX;

namespace UBBard
{
    class Mode
    {
        #region Combo
        public static void Combo()
        {
            if (Config.ComboMenu.Checked("Q") && Spells.Q.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (Target != null && Target.IsValid())
                {
                    var pred = Spells.Q.GetPrediction(Target);
                    if (!Config.ComboMenu.Checked("Qstun") || pred.WillStun())
                    {
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
            if (Config.ComboMenu.Checked("R") && Spells.R.IsReady())
            {
                Spells.R.CastIfItWillHit(Config.ComboMenu.GetValue("Rhit"), Config.ComboMenu.GetValue("Rhitchance"));
            }
        }
        #endregion 

        #region Harass
        public static void Harass()
        {
            if (Config.HarassMenu.Checked("Q") && Spells.Q.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (Target != null && Target.IsValid())
                {
                    var pred = Spells.Q.GetPrediction(Target);
                    if (!Config.HarassMenu.Checked("Qstun") || pred.WillStun())
                    {
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
        }
        #endregion 

        #region Clear
        public static void LaneClear()
        {
            if (Player.Instance.ManaPercent < Config.Clear.GetValue("lc")) return;
            if (Config.Clear.Checked("Q") && Spells.Q.IsReady())
            {
                var minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Spells.Q.Range);
                if (minion != null)
                {
                    Spells.Q.Cast(minion.First());
                }
            }
        }
        public static void JungleClear()
        {
            if (Player.Instance.ManaPercent < Config.Clear.GetValue("jc")) return;
            if (Config.Clear.Checked("Qjc") && Spells.Q.IsReady())
            {
                var monster = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, Spells.Q.Range).OrderByDescending(x => x.MaxHealth);
                if (monster != null)
                {
                    Spells.Q.Cast(monster.First());
                }
            }
        }
        #endregion 

        #region Flee
        public static void Flee()
        {
            if ((Game.CursorPos.IsWall() || Game.CursorPos.IsBuilding()) && Spells.E.IsInRange(Game.CursorPos))
            {
                Spells.E.Cast(Game.CursorPos);
            }
        }
        #endregion 

        #region AutoW
        public static void AutoW(EventArgs args)
        {
            if (!Spells.W.IsReady() || Player.Instance.IsRecalling() || !Config.AutoHeal.Checked("W")) return;
            var Allies = EntityManager.Heroes.Allies.Where(a => !a.IsDead && !a.IsZombie && a.IsValid && Spells.W.IsInRange(a) && a.HealthPercent <= Config.AutoHeal.GetValue("HP")
            && Config.AutoHeal.Checked(a.ChampionName));
            
            if (Allies.Count() != 0)
            {
                switch (Config.AutoHeal.GetValue("Worder", false))
                {
                    case 0:
                        {
                            Spells.W.Cast(Allies.OrderByDescending(x => x.TotalMagicalDamage).FirstOrDefault());
                        }
                        break;
                    case 1:
                        {
                            Spells.W.Cast(Allies.OrderByDescending(x => x.TotalAttackDamage).FirstOrDefault());
                        }
                        break;
                    case 2:
                        {
                            Spells.W.Cast(Allies.OrderBy(x => x.Health).FirstOrDefault().Position);
                        }
                        break;
                }
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
                    {
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
        }
        #endregion 

        #region AutoHarass
        public static void AutoHarass(EventArgs args)
        {
            if (Player.Instance.ManaPercent < Config.HarassMenu.GetValue("autohrmng")) return;
            if (!Config.HarassMenu.Checked("keyharass", false)) return;
            if (Config.HarassMenu.Checked("Q") && Spells.Q.IsReady())
            {
                var Target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (Target != null && Target.IsValid())
                {
                    var pred = Spells.Q.GetPrediction(Target);
                    if (!Config.HarassMenu.Checked("Qstun") || pred.WillStun())
                    {
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
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
                && sender.IsValidTarget(Spells.Q.Range)
                && e.DangerLevel == Danger
                && Spells.Q.IsReady())
            {
                var pred = Spells.Q.GetPrediction(sender);
                if (pred.WillStun())
                {
                    Spells.Q.Cast(pred.CastPosition);
                }
            }
        }
        #endregion

        #region AutoR
        public static void Obj_AI_Turret_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsAlly) return;
            var target = args.Target as AIHeroClient;
            if (sender is Obj_AI_Turret && Spells.R.IsInRange(sender) && target != null && target.IsAlly)
            {
                if (Config.MiscMenu.Checked("R") && Spells.R.IsReady())
                {
                    Spells.R.Cast(sender.Position);
                }
            }
        }
        #endregion

        #region Gapcloser
        public static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 225 || args.End.IsInRange(Player.Instance, Spells.Q.Range))
                && (sender.Spellbook.CastEndTime - Game.Time) * 1000 <= Spells.Q.CastDelay)
            {
                if (sender.WillStun() && Spells.Q.IsReady())
                {
                    if (Config.MiscMenu.Checked("Qgap"))
                        Spells.Q.Cast(args.End);
                }
                else
                {
                    var Ally = EntityManager.Heroes.Allies.Where(x => !x.IsDead && Spells.W.IsInRange(x)).OrderBy(x => x.Distance(args.End)).First();
                    if (Ally.Distance(args.End) < 225)
                        Spells.W.Cast(Ally);
                }
            }
        }
        #endregion

    }
}
