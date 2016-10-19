using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;


namespace UBKennen
{
    class Mode
    {
        //Combo
        public static void Combo()
        {
            var Champ = EntityManager.Heroes.Enemies.Where(x => x.IsValid && Spells.Q.IsInRange(x) && x.HasBuff("kennenmarkofstorm"));
            var Focus = Config.ComboMenu["focus"].Cast<CheckBox>().CurrentValue ? Champ != null ? Champ.OrderBy(x => TargetSelector.GetPriority(x)).LastOrDefault() : null : null; 
            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (Config.ComboMenu["useQCombo"].Cast<CheckBox>().CurrentValue
                && Spells.Q.IsReady())
            {
                if (target != null && Focus == null && target.IsValidTarget())
                {
                    var pred = Spells.Q.GetPrediction(target);
                    Spells.Q.Cast(pred.CastPosition);
                }
                if (Focus != null)
                {
                    var pred = Spells.Q.GetPrediction(Focus);
                    if (pred.CollisionObjects.Count() == 0)
                    {
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }

            if (Config.ComboMenu["useWCombo"].Cast<CheckBox>().CurrentValue
                 && Spells.W.IsReady())
            {
                var Target = EntityManager.Heroes.Enemies.Count(x => x.IsValid && Spells.W.IsInRange(x) && x.HasBuff("kennenmarkofstorm"));
                if (Target >= Config.ComboMenu["WhitCombo"].Cast<Slider>().CurrentValue)
                {
                    Spells.W.Cast();
                }
            }
            if (Config.ComboMenu["useECombo"].Cast<CheckBox>().CurrentValue
                 && Spells.E.IsReady() && target.CountEnemiesInRange((Player.Instance.MoveSpeed) * 2) > 0)
            {
                Spells.E.Cast();
            }

            if (Config.ComboMenu["useRCombo"].Cast<CheckBox>().CurrentValue
                      && Spells.R.IsReady())
            {
                var Count = EntityManager.Heroes.Enemies.Count(x => x.IsValid && Spells.R.IsInRange(x));
                if (Count >= Config.ComboMenu["RhitCombo"].Cast<Slider>().CurrentValue)
                {
                    Spells.R.Cast();
                }
            }
        }
        //Harass
        public static void Harass()
        {
            if (Player.Instance.Mana <= Config.LaneClear["HrEnergyManager"].Cast<Slider>().CurrentValue) return;
            if (Config.HarassMenu["useQ"].Cast<CheckBox>().CurrentValue
                && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Physical);
                if (target != null && target.IsValidTarget())
                {
                    var pred = Spells.Q.GetPrediction(target);
                    if (pred.CollisionObjects.Count() == 0)
                    {
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
            if (Spells.W.IsReady())
            {
                var Target = EntityManager.Heroes.Enemies.Count(x => x.IsValid && Spells.W.IsInRange(x) && x.HasBuff("kennenmarkofstorm"));
                if (Target >= Config.HarassMenu["Whit"].Cast<Slider>().CurrentValue)
                {
                    Spells.W.Cast();
                }
            }
        }
        //LaneClear
        public static void LaneClear()
        {
            var minion = Orbwalker.LaneClearMinionsList.FirstOrDefault();
            if (minion != null) return;
            if (Config.LaneClear["useQLc"].Cast<CheckBox>().CurrentValue
              && Player.Instance.Mana > Config.LaneClear["EnergyManager"].Cast<Slider>().CurrentValue
              && Spells.Q.IsReady())
            {
                Spells.Q.Cast(minion);
            }
            var Whit = EntityManager.MinionsAndMonsters.EnemyMinions.Count(x => x.IsValid && Spells.W.IsInRange(x) && x.HasBuff("kennenmarkofstorm"));
            if (Config.LaneClear["useWLc"].Cast<CheckBox>().CurrentValue
                && Player.Instance.Mana > Config.LaneClear["EnergyManager"].Cast<Slider>().CurrentValue
                && Spells.W.IsReady()
                && Whit >= Config.LaneClear["WhitLc"].Cast<Slider>().CurrentValue)
            {
                Spells.W.Cast();
            }

            if (Config.LaneClear["useELc"].Cast<CheckBox>().CurrentValue
               && Player.Instance.Mana > Config.LaneClear["EnergyManager"].Cast<Slider>().CurrentValue)
            {
                var Eminion = Orbwalker.LaneClearMinion;
                if (Spells.E.IsReady() && Eminion != null)
                {
                    Spells.E.Cast();
                }
            }
        }
        //Lasthit
        public static void Orbwalker_OnUnkillableMinion(Obj_AI_Base target, Orbwalker.UnkillableMinionArgs args)
        {
            if (target == null || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) return;
            if (args.RemainingHealth <= Damages.QDamage(target) && Config.LasthitMenu["useQLh"].Cast<CheckBox>().CurrentValue)
            {
                Spells.Q.Cast(target);
            }
            if (args.RemainingHealth <= Damages.WDamage(target) && Config.LasthitMenu["useWLh"].Cast<CheckBox>().CurrentValue && target.HasBuff("kennenmarkofstorm"))
            {
                Spells.W.Cast();
            }
        }

        //JungleClear
        public static void JungleClear()
        {
            var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.Health).LastOrDefault();
            if (monster == null || !monster.IsValid) return;
            if (Orbwalker.IsAutoAttacking) return;
            Orbwalker.ForcedTarget = null;
            if (Config.JungleClear["useQJc"].Cast<CheckBox>().CurrentValue
                && Player.Instance.Mana > Config.JungleClear["JcEnergyManager"].Cast<Slider>().CurrentValue
                && Spells.Q.IsReady())
            {
                Spells.Q.Cast(monster);
            }

            var wmonster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.W.Range)).OrderBy(x => x.Health).LastOrDefault();
            if (wmonster == null || !wmonster.IsValid) return;
            if (Orbwalker.IsAutoAttacking) return;
            Orbwalker.ForcedTarget = null;
            if (Config.JungleClear["useWJc"].Cast<CheckBox>().CurrentValue
                && Player.Instance.ManaPercent > Config.JungleClear["JcEnergyManager"].Cast<Slider>().CurrentValue
                && Spells.W.IsReady()
                && wmonster.HasBuff("kennenmarkofstorm"))
            {
                if (Player.Instance.Mana > Config.JungleClear["JcEnergyManager"].Cast<Slider>().CurrentValue)
                {
                    Spells.W.Cast();
                }
            }

            if (Config.JungleClear["useEJc"].Cast<CheckBox>().CurrentValue
               && Player.Instance.Mana > Config.JungleClear["JcEnergyManager"].Cast<Slider>().CurrentValue)
            {
                if (Spells.E.IsReady())
                {
                    Spells.E.Cast();
                }
            }
        }
        public static void Killsteal()
        {
            if (Config.MiscMenu["useQKS"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.Q.IsInRange(t)
                    && t.Health <= Damages.QDamage(t)), DamageType.Magical);

                if (target != null)
                {
                    var pred = Spells.Q.GetPrediction(target);
                    {
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
            if (Config.MiscMenu["useWKS"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady())
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && t.HasBuff("kennenmarkofstorm")
                    && Spells.W.IsInRange(t)
                    && t.Health <= Damages.WDamage(t)), DamageType.Magical);

                if (target != null)
                {
                    Spells.W.Cast();
                }
            }
        }
        public static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (Spells.E.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 100 || args.End.IsInRange(Player.Instance, 100))
                && Config.MiscMenu["useEAG"].Cast<CheckBox>().CurrentValue)
            {
                Spells.E.Cast(sender);
            }
            if (Spells.Q.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 100 || args.End.IsInRange(Player.Instance, Spells.W.Range))
                && Config.MiscMenu["useQAG"].Cast<CheckBox>().CurrentValue)
            {
                Spells.Q.Cast(args.End);
            }
            if (Spells.W.IsReady()
                && sender != null
                && sender.IsEnemy
                && sender.IsValid
                && sender.HasBuff("kennenmarkofstorm")
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 100 || args.End.IsInRange(Player.Instance, Spells.W.Range))
                && Config.MiscMenu["useWAG"].Cast<CheckBox>().CurrentValue)
            {
                Spells.W.Cast();
            }
        }
    }
}
