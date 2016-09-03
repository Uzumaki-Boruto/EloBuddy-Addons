using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace UBSivir
{
    class Event
    {
        public static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (sender.IsEnemy && !sender.IsMonster && !sender.IsMinion && sender.IsValidTarget(Spells.Q.Range))
            {
                if (sender.HasBuffOfType(BuffType.Stun)
                 || sender.HasBuffOfType(BuffType.Snare)
                 || sender.HasBuffOfType(BuffType.Knockup)
                 || sender.HasBuffOfType(BuffType.Charm)
                 || sender.HasBuffOfType(BuffType.Fear)
                 || sender.HasBuffOfType(BuffType.Knockback)
                 || sender.HasBuffOfType(BuffType.Taunt)
                 || sender.HasBuffOfType(BuffType.Suppression)
                 || sender.HasBuffOfType(BuffType.Disarm)
                 || sender.HasBuffOfType(BuffType.Flee)
                 || sender.HasBuffOfType(BuffType.Polymorph))
                {
                    Spells.Q.Cast(sender);
                }
            }
        }
        public static bool Unkillable(AIHeroClient target)
        {
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "UndyingRage"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "ChronoShift"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "JudicatorIntervention"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "kindredrnodeathbuff"))
            {
                return true;
            }
            if (target.HasBuffOfType(BuffType.Invulnerability))
            {
                return true;
            }
            return target.IsInvulnerable;
        }
        public static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.Slot == SpellSlot.W)
            {
                Orbwalker.ResetAutoAttack();
            }
        }
        public static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            var Tar = target as AIHeroClient;
            if (Tar == null || !Spells.W.IsReady() || !Player.Instance.IsInAutoAttackRange(Tar)) return;
            if (Config.ComboMenu["useWCombo"].Cast<CheckBox>().CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Spells.W.Cast();
                Orbwalker.ResetAutoAttack();
                Player.IssueOrder(GameObjectOrder.AttackTo, Tar);
            }
            if (Config.HarassMenu["useWHr"].Cast<CheckBox>().CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Spells.W.Cast();
                Orbwalker.ResetAutoAttack();
                Player.IssueOrder(GameObjectOrder.AttackTo, Tar);
            }
            if (Config.MiscMenu["autoW"].Cast<CheckBox>().CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.None))
            {
                Spells.W.Cast();
                Orbwalker.ResetAutoAttack();
                Player.IssueOrder(GameObjectOrder.AttackTo, Tar);
            }
        }
    }
}
