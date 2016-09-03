using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace UBSivir
{
    class Mode
    {
        public static void Combo()
        {
            if (Config.ComboMenu["useQCombo"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Physical);
                if (target != null && target.IsValidTarget())
                {
                    var pred = Spells.Q.GetPrediction(target);
                    Spells.Q.Cast(pred.CastPosition);
                }
            }
            if (Config.ComboMenu["useRCombo"].Cast<CheckBox>().CurrentValue
                      && Spells.R.IsReady())
            {
                if (ObjectManager.Player.Position.CountAlliesInRange(1000) >= (Config.ComboMenu["RHitCombo"].Cast<Slider>().CurrentValue)
                  && ObjectManager.Player.Position.CountEnemiesInRange(2000) >= Config.ComboMenu["RHitCombo"].Cast<Slider>().CurrentValue)
                {
                    Spells.R.Cast();
                }
            }
        }
        //Harass
        public static void Harass()
        {
            if (Config.HarassMenu["useQHr"].Cast<CheckBox>().CurrentValue
                && Spells.Q.IsReady()
                && !Player.Instance.IsDashing()
                && Player.Instance.ManaPercent >= Config.HarassMenu["HrManage"].Cast<Slider>().CurrentValue
                && Config.HarassMenu["useQHr2"].Cast<CheckBox>().CurrentValue == false)
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Physical);
                if (target != null && target.IsValidTarget())
                {
                    var pred = Spells.Q.GetPrediction(target);
                    Spells.Q.Cast(pred.CastPosition);
                }
            }
            else if (Config.HarassMenu["useQHr"].Cast<CheckBox>().CurrentValue
                && Spells.QLine.IsReady()
                && !Player.Instance.IsDashing()
                && Player.Instance.ManaPercent >= Config.HarassMenu["HrManage"].Cast<Slider>().CurrentValue
                && Config.HarassMenu["useQhr2"].Cast<CheckBox>().CurrentValue == true)
            {
                var target = TargetSelector.GetTarget(Spells.QLine.Range, DamageType.Physical);
                if (target != null && target.IsValidTarget())
                {
                    var pred = Spells.Q.GetPrediction(target);
                    Spells.Q.Cast(pred.CastPosition);
                }
            }
        }
        //LaneClear
        public static void LaneClear()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLineFarmLocation(EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, ObjectManager.Player.Position, Spells.Q.Range), Spells.Q.Width, (int)Spells.Q.Range);
            if (Config.LaneClear["useQLc"].Cast<CheckBox>().CurrentValue
                && Player.Instance.ManaPercent >= Config.LaneClear["LcManager"].Cast<Slider>().CurrentValue
                && Spells.Q.IsReady())
            {
                Spells.Q.Cast(minions.CastPosition);
            }

            if (Config.LaneClear["useWLc"].Cast<CheckBox>().CurrentValue
                && Player.Instance.ManaPercent > Config.LaneClear["LcManager"].Cast<Slider>().CurrentValue
                && EntityManager.MinionsAndMonsters.GetLaneMinions().Count(x => x.Distance(ObjectManager.Player.Position) <= ObjectManager.Player.GetAutoAttackRange() + 250) >= Config.LaneClear["WhitLc"].Cast<Slider>().CurrentValue
                && Spells.W.IsReady())
            {
                Spells.W.Cast();
            }
            var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Physical);
            if (Config.LaneClear["autoWhr"].Cast<CheckBox>().CurrentValue
                && Player.Instance.ManaPercent > Config.LaneClear["LcManager"].Cast<Slider>().CurrentValue
                && Spells.W.IsReady()
                && target.CountEnemiesInRange(850) >= 1
                && target != null
                && target.IsValidTarget())
            {
                Spells.W.Cast();
            }

        }
        //Lasthit
        public static void Orbwalker_OnUnkillableMinion(Obj_AI_Base target, Orbwalker.UnkillableMinionArgs args)
        {
            if (Player.Instance.ManaPercent < Config.LasthitMenu["LhManager"].Cast<Slider>().CurrentValue) return;
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            if (Config.LasthitMenu["useQLh"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
            {
                if (args.RemainingHealth < Damages.QDamage(target) && target.IsValidTarget(Spells.Q.Range))
                {
                    Spells.Q.Cast(target);
                }
            }
            if (Config.LasthitMenu["useWLh"].Cast<CheckBox>().CurrentValue)
            {
                if (args.RemainingHealth < Player.Instance.GetAutoAttackDamage(target) && Player.Instance.IsInAutoAttackRange(target))
                {
                    Spells.W.Cast();
                    Player.IssueOrder(GameObjectOrder.AttackTo, target);
                }
            }
        }

        //JungleClear
        public static void JungleClear()
        {
            var monster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.Q.Range)).OrderBy(x => x.Health).FirstOrDefault();
            if (monster == null || !monster.IsValid) return;
            if (Orbwalker.IsAutoAttacking) return;
            Orbwalker.ForcedTarget = null;
            if (Config.JungleClear["useQJc"].Cast<CheckBox>().CurrentValue
                && monster.Health > (ObjectManager.Player.GetAutoAttackDamage(Player.Instance) * 2)
                && Player.Instance.ManaPercent >= Config.JungleClear["JcManager"].Cast<Slider>().CurrentValue
                && Spells.Q.IsReady())
            {
                Spells.Q.Cast(monster);
            }

            var wmonster = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsMonster && x.IsValidTarget(Spells.W.Range)).OrderBy(x => x.Health).FirstOrDefault();
            if (wmonster == null || !wmonster.IsValid) return;
            if (Orbwalker.IsAutoAttacking) return;
            Orbwalker.ForcedTarget = null;
            if (Config.JungleClear["useWJc"].Cast<CheckBox>().CurrentValue
                && Player.Instance.ManaPercent >= Config.JungleClear["JcManager"].Cast<Slider>().CurrentValue
                && Spells.W.IsReady()
                && wmonster.CountEnemiesInRange(600) >= Config.JungleClear["WhitJc"].Cast<Slider>().CurrentValue)
            {
                Spells.W.Cast();
            }
        }
        //KillSteal
        public static void Killsteal()
        {
            if (!Config.MiscMenu["useQKS"].Cast<CheckBox>().CurrentValue && !Spells.Q.IsReady()) return;
            {
                var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(t => t != null
                    && t.IsValidTarget()
                    && Spells.Q.IsInRange(t)
                    && t.Health <= Damages.QDamage(t)), DamageType.Physical);

                if (target != null && Event.Unkillable(target) == false)
                {
                    var pred = Spells.Q.GetPrediction(target);
                    {
                        Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
        }
    }
}
