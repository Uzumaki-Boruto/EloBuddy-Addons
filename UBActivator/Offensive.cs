using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;

namespace UBActivator
{
    class Offensive
    {
        public static void Ontick()
        {
            //if (Items.Tiamat == null && Items.Ravenous_Hydra == null) return;
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Count = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, 400).Count();
                if (Count >= Config.Offensive["TiamatLccount"].Cast<Slider>().CurrentValue)
                {
                    if (Items.Tiamat.IsOwned() && Items.Tiamat.IsReady()) Items.Tiamat.Cast();
                    else if (Items.Ravenous_Hydra.IsOwned() && Items.Ravenous_Hydra.IsReady()) Items.Ravenous_Hydra.Cast();
                    else if (Items.Titanic_Hydra.IsOwned() && Items.Titanic_Hydra.IsReady())
                    {
                        Items.Titanic_Hydra.Cast();
                        Orbwalker.ResetAutoAttack();
                    }
                }
            }
        }
        public static void OnTick2()
        {
            //if (Items.Tiamat == null && Items.Ravenous_Hydra == null) return;
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                var Count = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, 400).Count();
                var impMonster = ObjectManager.Get<Obj_AI_Minion>().Where(m => m.IsMonster && m.IsValidTarget(400) && Extensions.IsImportant(m)).OrderBy(x => x.MaxHealth).LastOrDefault();

                if (Count >= Config.Offensive["TiamatLccount"].Cast<Slider>().CurrentValue)
                {
                    if (Items.Tiamat.IsOwned() && Items.Tiamat.IsReady()) Items.Tiamat.Cast();
                    else if (Items.Ravenous_Hydra.IsOwned() && Items.Ravenous_Hydra.IsReady()) Items.Ravenous_Hydra.Cast();
                    else if (Items.Titanic_Hydra.IsOwned() && Items.Titanic_Hydra.IsReady())
                    {
                        Items.Titanic_Hydra.Cast();
                        Orbwalker.ResetAutoAttack();
                    }
                }
                if (impMonster != null)
                {
                    if (Player.Instance.Distance(impMonster) <= 400)
                    {
                        if (Items.Tiamat.IsOwned() && Items.Tiamat.IsReady()) Items.Tiamat.Cast();
                        else if (Items.Ravenous_Hydra.IsOwned() && Items.Ravenous_Hydra.IsReady()) Items.Ravenous_Hydra.Cast();
                        else if (Items.Titanic_Hydra.IsOwned() && Items.Titanic_Hydra.IsReady())
                        {
                            Items.Titanic_Hydra.Cast();
                            Orbwalker.ResetAutoAttack();
                        }
                    }
                }
            }
        }
        public static void OnTick3()
        {
            //if (Items.Tiamat == null && Items.Ravenous_Hydra == null && Items.Bilgewater_Cutlass == null && Items.Hextech_Gunblade == null && Items.Blade_Of_The_Ruined_King == null) return;
            var TiamatTarget = TargetSelector.GetTarget(400, DamageType.Physical);
            var CutlassTarget = TargetSelector.GetTarget(550, DamageType.Magical);
            var HextechTarget = TargetSelector.GetTarget(700, DamageType.Magical);
            if (TiamatTarget != null)
            {
                var distance = Player.Instance.Distance(TiamatTarget);
                if (Config.Offensive["Tiamat"].Cast<ComboBox>().CurrentValue > 0
                && TiamatTarget.IsValidTarget()
                && distance >= 400 * Config.Offensive["TiamatSlider"].Cast<Slider>().CurrentValue / 100)
                {
                    switch (Config.Offensive["Tiamat"].Cast<ComboBox>().CurrentValue)
                    {
                        case 1:
                            {
                                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                                {
                                    if (Items.Tiamat.IsOwned() && Items.Tiamat.IsReady()) Items.Tiamat.Cast();
                                    else if (Items.Ravenous_Hydra.IsOwned() && Items.Ravenous_Hydra.IsReady()) Items.Ravenous_Hydra.Cast();
                                }
                            }
                            break;
                        case 2:
                            {
                                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                                {
                                    if (Items.Tiamat.IsOwned() && Items.Tiamat.IsReady()) Items.Tiamat.Cast();
                                    else if (Items.Ravenous_Hydra.IsOwned() && Items.Ravenous_Hydra.IsReady()) Items.Ravenous_Hydra.Cast();
                                }
                            }
                            break;
                        case 3:
                            {
                                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                                {
                                    if (Items.Tiamat.IsOwned() && Items.Tiamat.IsReady()) Items.Tiamat.Cast();
                                    else if (Items.Ravenous_Hydra.IsOwned() && Items.Ravenous_Hydra.IsReady()) Items.Ravenous_Hydra.Cast();
                                }
                            }
                            break;
                        case 4:
                            {
                                if (Items.Tiamat.IsOwned() && Items.Tiamat.IsReady()) Items.Tiamat.Cast();
                                else if (Items.Ravenous_Hydra.IsOwned() && Items.Ravenous_Hydra.IsReady()) Items.Ravenous_Hydra.Cast();
                            }
                            break;
                    }
                }
            }
            if (CutlassTarget != null && CutlassTarget.IsValidTarget() && (Items.Bilgewater_Cutlass.IsOwned() || Items.Blade_Of_The_Ruined_King.IsOwned()))
            {
                if ((Config.Offensive["cbitem"].Cast<CheckBox>().CurrentValue
                    && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                    || !Config.Offensive["cbitem"].Cast<CheckBox>().CurrentValue)
                {
                    if (Player.Instance.HealthPercent <= Config.Offensive["MyHPT"].Cast<Slider>().CurrentValue
                        && CutlassTarget.HealthPercent <= Config.Offensive["TargetHPT"].Cast<Slider>().CurrentValue)
                    {
                        if (Items.Bilgewater_Cutlass.IsOwned() && Items.Bilgewater_Cutlass.IsReady()) Items.Bilgewater_Cutlass.Cast(CutlassTarget);
                        if (Items.Blade_Of_The_Ruined_King.IsOwned() && Items.Blade_Of_The_Ruined_King.IsReady()) Items.Blade_Of_The_Ruined_King.Cast(CutlassTarget);
                    }
                }
            }
            if (HextechTarget != null && HextechTarget.IsValidTarget() && Items.Hextech_Gunblade.IsOwned())
            {
                if ((Config.Offensive["cbitem"].Cast<CheckBox>().CurrentValue
                   && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                   || !Config.Offensive["cbitem"].Cast<CheckBox>().CurrentValue)
                {
                    if (Config.Offensive["HG"].Cast<CheckBox>().CurrentValue
                        && Player.Instance.HealthPercent <= Config.Offensive["MyHPT"].Cast<Slider>().CurrentValue
                        && CutlassTarget.HealthPercent <= Config.Offensive["TargetHPT"].Cast<Slider>().CurrentValue)
                    {
                        if (Items.Hextech_Gunblade.IsOwned() && Items.Hextech_Gunblade.IsReady()) Items.Hextech_Gunblade.Cast(HextechTarget);
                    }
                }
            }
            if (Player.Instance.CountEnemiesInRange(1000) >= 1 && Items.Youmuu_s_Ghostblade.IsOwned())
            {
                if ((Config.Offensive["cbmvitem"].Cast<CheckBox>().CurrentValue
                  && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                  || !Config.Offensive["cbmvitem"].Cast<CheckBox>().CurrentValue)
                {
                    if (Config.Offensive["Youmuu"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Items.Youmuu_s_Ghostblade.IsOwned() && Items.Youmuu_s_Ghostblade.IsReady()) Items.Youmuu_s_Ghostblade.Cast();
                    }
                }
            }

        
        }
        public static void OnTick4()
        {
            if (!Items.Hextech_Protobelt_01.IsOwned()) return;
            if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) return;
            if (Config.Offensive["Hextech01"].Cast<CheckBox>().CurrentValue) return;
            var target = TargetSelector.GetTarget(750, DamageType.Magical);
            if (target != null && target.IsValid)
            {
                var pred = Prediction.Position.PredictLinearMissile(target, 750, 70, 150, 1150, int.MaxValue, Player.Instance.Position);
                switch (Config.Offensive["Hextech01gap"].Cast<ComboBox>().CurrentValue)
                {
                    case 0:
                        break;
                    case 1:
                        {
                            if (Items.Hextech_Protobelt_01.IsOwned() && Items.Hextech_Protobelt_01.IsReady())
                            {
                                Items.Hextech_Protobelt_01.Cast(Player.Instance.Position.Extend(Game.CursorPos, 275).To3D());
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Items.Hextech_Protobelt_01.IsOwned() && Items.Hextech_Protobelt_01.IsReady())
                            {
                                if (!Player.Instance.IsInAutoAttackRange(target) && Player.Instance.Distance(pred.UnitPosition) <= Player.Instance.GetAutoAttackRange() + 325)
                                    Items.Hextech_Protobelt_01.Cast(Player.Instance.Position.Extend(pred.CastPosition, 275).To3D());
                            }
                        }
                        break;
                }
            }
        }
        public static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsAlly) return;
            if (Items.Hextech_GLP_800 == null && Items.Hextech_Protobelt_01 == null) return;
            {
                if (sender.IsValidTarget(200, true, Player.Instance.Position))
                {
                    if ((Config.Offensive["Hextech800style"].Cast<ComboBox>().CurrentValue == 1
                        || Config.Offensive["Hextech800style"].Cast<ComboBox>().CurrentValue == 3)
                        && Items.Hextech_GLP_800.IsOwned())
                    {
                        if (Items.Hextech_GLP_800.IsOwned() && Items.Hextech_GLP_800.IsReady())
                        {
                            Items.Hextech_GLP_800.Cast(e.End);
                        }
                    }
                    if (Config.Offensive["Hextech01agap"].Cast<CheckBox>().CurrentValue
                        && Items.Hextech_Protobelt_01.IsOwned())
                    {
                        if (Items.Hextech_Protobelt_01.IsOwned() && Items.Hextech_Protobelt_01.IsReady())
                        {
                            Items.Hextech_Protobelt_01.Cast(sender.Position.Extend(Player.Instance.Position, sender.Distance(Player.Instance) + 275).To3D());
                        }
                    }
                }
            }
        }
        public static void KillSteal()
        {
            //if (Items.Hextech_GLP_800 == null && Items.Hextech_Protobelt_01 == null && Items.Tiamat == null && Items.Ravenous_Hydra == null) return;
            var HextechTarget = TargetSelector.GetTarget(750, DamageType.Magical);
            var TiamatTarget = TargetSelector.GetTarget(400, DamageType.Physical);
            if (HextechTarget != null
                && Items.Hextech_GLP_800.IsOwned()
                && Config.Offensive["Hextech800style"].Cast<ComboBox>().CurrentValue >= 2)
            {
                var Damage = Player.Instance.CalculateDamageOnUnit(
                    HextechTarget, DamageType.Magical,
                    (new[] { 0f, 100f, 106f, 112f, 118f, 124f, 130f, 136f, 141f, 147f, 153f, 159f, 165f, 171f, 176f, 182f, 188f, 194f, 200f}[Player.Instance.Level]) + 0.35f * Player.Instance.TotalMagicalDamage);
                var Pred = Prediction.Position.PredictLinearMissile(HextechTarget, 750f, 60, 150, 1150, int.MaxValue);
                if (Items.Hextech_GLP_800.IsOwned() && Items.Hextech_GLP_800.IsReady() 
                && HextechTarget.Health <= Damage)
                {
                    Items.Hextech_GLP_800.Cast(Pred.CastPosition);
                }
            }
            if (HextechTarget != null
                && Items.Hextech_Protobelt_01.IsOwned()
                && Config.Offensive["Hextech01Ks"].Cast<CheckBox>().CurrentValue)
            {
                var Damage = Player.Instance.CalculateDamageOnUnit(
                    HextechTarget, DamageType.Magical,
                    (new[] { 0f, 75f, 79f, 83f, 88f, 92f, 97f, 101f, 106f, 110f, 115f, 119f, 124f, 128f, 132f, 137f, 141f, 146f, 150f }[Player.Instance.Level]) + 0.35f * Player.Instance.TotalMagicalDamage);
                var Pred = Prediction.Position.PredictLinearMissile(HextechTarget, 750, 60, 150, 1150, 0);
                if (Items.Hextech_Protobelt_01.IsOwned() && Items.Hextech_Protobelt_01.IsReady()
                && HextechTarget.Health <= Damage)
                {
                    Items.Hextech_Protobelt_01.Cast(Player.Instance.Position.Extend(Pred.CastPosition, 275).To3D());
                }
            }
            if (TiamatTarget != null && (Items.Tiamat.IsOwned() || Items.Ravenous_Hydra.IsOwned())
                && Config.Offensive["TiamatKs"].Cast<CheckBox>().CurrentValue)
            {
                var PercentDame = (100 - Player.Instance.Distance(TiamatTarget) / 10);
                var Damage = Player.Instance.CalculateDamageOnUnit(TiamatTarget, DamageType.Physical, Player.Instance.TotalAttackDamage * PercentDame, false);
                if (Items.Tiamat.IsOwned()
                && Items.Tiamat.IsReady()
                && TiamatTarget.Health <= Damage)
                {
                    Items.Tiamat.Cast();
                }
                if (Items.Ravenous_Hydra.IsOwned()
                && Items.Ravenous_Hydra.IsReady()
                && TiamatTarget.Health <= Damage)
                {
                    Items.Ravenous_Hydra.Cast();
                }
            }
        }
        public static void On_Unkillable_Minion(Obj_AI_Base unit, Orbwalker.UnkillableMinionArgs args)
        {
            if (Config.Offensive["TiamatLh"].Cast<CheckBox>().CurrentValue
                && (Items.Tiamat.IsOwned() || Items.Ravenous_Hydra.IsOwned())
                && unit.Health <= Player.Instance.TotalAttackDamage *0.8
                && unit.Distance(Player.Instance) <= 400)
            {
                if (Items.Tiamat.IsOwned() && Items.Tiamat.IsReady()) Items.Tiamat.Cast();
                else if (Items.Ravenous_Hydra.IsOwned() && Items.Ravenous_Hydra.IsReady()) Items.Ravenous_Hydra.Cast(); 
            }
        }
        public static void OnPostAttack(AttackableUnit target, EventArgs args)
        {
            var Tar = target as AIHeroClient;
            if (Tar == null) return;
            if (!Items.Titanic_Hydra.IsOwned() || !Items.Titanic_Hydra.IsReady()) return;
            if (Config.Offensive["styletitanic"].Cast<ComboBox>().CurrentValue == 0)
            {
                switch (Config.Offensive["Tiamat"].Cast<ComboBox>().CurrentValue)
                {
                    case 0:
                        break;
                    case 1:
                        {
                            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                            {
                                Items.Titanic_Hydra.Cast();
                                Orbwalker.ResetAutoAttack();
                                Player.IssueOrder(GameObjectOrder.AttackTo, target);
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                            {
                                Items.Titanic_Hydra.Cast();
                                Orbwalker.ResetAutoAttack();
                                Player.IssueOrder(GameObjectOrder.AttackTo, target);
                            }
                        }
                        break;
                    case 3:
                        {
                            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                            {
                                Items.Titanic_Hydra.Cast();
                                Orbwalker.ResetAutoAttack();
                                Player.IssueOrder(GameObjectOrder.AttackTo, target);
                            }
                        }
                        break;
                    case 4:
                        {
                            Items.Titanic_Hydra.Cast();
                            Orbwalker.ResetAutoAttack();
                            Player.IssueOrder(GameObjectOrder.AttackTo, target);
                        }
                        break;
                }
            }
        }
        public static void OnPreAttack(AttackableUnit target, EventArgs args)
        {
            var Tar = target as AIHeroClient;
            if (Tar == null) return;
            if (!Items.Titanic_Hydra.IsOwned() || !Items.Titanic_Hydra.IsReady()) return;
            if (Config.Offensive["styletitanic"].Cast<ComboBox>().CurrentValue == 1)
            {
                switch (Config.Offensive["Tiamat"].Cast<ComboBox>().CurrentValue)
                {
                    case 0:
                        break;
                    case 1:
                        {
                            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                            {
                                Items.Titanic_Hydra.Cast();
                                Orbwalker.ResetAutoAttack();
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                            {
                                Items.Titanic_Hydra.Cast();
                                Orbwalker.ResetAutoAttack();
                            }
                        }
                        break;
                    case 3:
                        {
                            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                            {
                                Items.Titanic_Hydra.Cast();
                                Orbwalker.ResetAutoAttack();
                            }
                        }
                        break;
                    case 4:
                        {
                            Items.Titanic_Hydra.Cast();
                            Orbwalker.ResetAutoAttack();
                            Player.IssueOrder(GameObjectOrder.AttackTo, target);
                        }
                        break;
                }
            }
        }
    }
}
