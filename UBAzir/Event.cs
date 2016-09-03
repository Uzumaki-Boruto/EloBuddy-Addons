using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Events;

namespace UBAzir
{
    class Event
    {
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
        public static bool HasSpellShield(AIHeroClient target)
        {
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "bansheesveil"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "SivirE"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "NocturneW"))
            {
                return true;
            }
            return target.HasBuffOfType(BuffType.SpellShield) || target.HasBuffOfType(BuffType.SpellImmunity);
        }
        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            var Value = Config.MiscMenu["interrupt.value"].Cast<ComboBox>().CurrentValue;
            var Danger = Value == 0 ? DangerLevel.High : Value == 1 ? DangerLevel.Medium : Value == 2 ? DangerLevel.Low : DangerLevel.High;
            if (sender.IsEnemy 
                && Config.MiscMenu["interrupter"].Cast<CheckBox>().CurrentValue
                && sender.IsValidTarget(Spells.R.Range - 20) 
                && e.DangerLevel == Danger)
            {
                SpecialVector.WhereCastR(sender, SpecialVector.I_want.All);
            }
        }
        public static void OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (Spells.R.IsReady()
                && sender != null
                && sender.IsEnemy
                && (sender.IsAttackingPlayer || Player.Instance.Distance(args.End) < 100 || args.End.IsInRange(Player.Instance, Spells.R.Range))
                && sender.Spellbook.CastEndTime <= Spells.R.CastDelay
                && Config.MiscMenu["gap"].Cast<CheckBox>().CurrentValue
                && Config.MiscMenu["gap" + sender.ChampionName].Cast<CheckBox>().CurrentValue)
            {
                switch (Config.MiscMenu["gap.1"].Cast<ComboBox>().CurrentValue)
                {
                    case 0:
                        {
                            if (args.Type == Gapcloser.GapcloserType.Targeted)
                            {
                                SpecialVector.WhereCastR(sender, SpecialVector.I_want.On_Gapclose, args.End, (int)sender.Spellbook.CastEndTime);
                            }
                        }
                        break;
                    case 1:
                        {
                            SpecialVector.WhereCastR(sender, SpecialVector.I_want.On_Gapclose, args.End, (int)sender.Spellbook.CastEndTime);
                        }
                        break;
                }
            }
        }
        public static void Obj_AI_Base_OnLevelUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args)
        {
            if (!sender.IsMe)
                return;
            var delay = Config.MiscMenu["upgradedelay"].Cast<Slider>().CurrentValue * 10;
            switch (Config.MiscMenu["upgrademode"].Cast<ComboBox>().CurrentValue)
            {
                case 0:
                    {
                        SpellSlot[] levels =
                         {
                             SpellSlot.Unknown, SpellSlot.W, SpellSlot.Q, SpellSlot.E, SpellSlot.Q, SpellSlot.Q,
                             SpellSlot.R, SpellSlot.Q, SpellSlot.W, SpellSlot.Q, SpellSlot.W, SpellSlot.R,
                             SpellSlot.W, SpellSlot.W, SpellSlot.E, SpellSlot.E, SpellSlot.R, SpellSlot.E, SpellSlot.E
                         };
                        if (Config.MiscMenu["upgrade"].Cast<CheckBox>().CurrentValue)
                            Core.DelayAction(() => Player.LevelSpell(levels[Player.Instance.Level]), delay);
                    }
                    break;
                case 1:
                    {
                        SpellSlot[] levels =
                         {
                             SpellSlot.Unknown, SpellSlot.W, SpellSlot.Q, SpellSlot.E, SpellSlot.W, SpellSlot.W,
                             SpellSlot.R, SpellSlot.W, SpellSlot.Q, SpellSlot.W, SpellSlot.Q, SpellSlot.R,
                             SpellSlot.Q, SpellSlot.Q, SpellSlot.E, SpellSlot.E, SpellSlot.R, SpellSlot.E, SpellSlot.E
                         };
                        if (Config.MiscMenu["upgrade"].Cast<CheckBox>().CurrentValue)
                            Core.DelayAction(() => Player.LevelSpell(levels[Player.Instance.Level]), delay);
                    }
                    break;
            }
        }
    }
}
