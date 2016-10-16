using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Events;

namespace UBAzir
{
    static class Extension
    {
        public static string AddonName = "UBAzir";
        public static int GetValue(this Menu menu, string id, bool IsSlider = true)
        {
            if (IsSlider)
                return menu[id].Cast<Slider>().CurrentValue;
            else
                return menu[id].Cast<ComboBox>().CurrentValue;
        }
        public static bool Checked(this Menu menu, string id, bool IsCheckBox = true)
        {
            if (IsCheckBox)
                return menu[id].Cast<CheckBox>().CurrentValue;
            else
                return menu[id].Cast<KeyBind>().CurrentValue;
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
            return target.IsInvulnerable || target.HasUndyingBuff();
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
            var Danger = Value == 2 ? DangerLevel.High : Value == 1 ? DangerLevel.Medium : Value == 0 ? DangerLevel.Low : DangerLevel.High;
            if (sender.IsEnemy 
                && Config.MiscMenu.Checked("interrupter")
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
                && (sender.Spellbook.CastEndTime - Game.Time) * 1000 <= Spells.E.CastDelay
                && Config.MiscMenu.Checked("gap")
                && Config.MiscMenu.Checked("gap" + sender.ChampionName))
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
    }
}
