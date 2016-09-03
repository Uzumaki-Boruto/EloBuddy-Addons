using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;

namespace UBActivator
{
    class Spells
    {
        public static Spell.Targeted Smite { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }
        public static Spell.Targeted Exhaust { get; private set; }
        public static Spell.Active Heal { get; private set; }
        public static Spell.Active Barrier { get; private set; }
        public static Spell.Active Cleanse { get; private set; }
        public static Spell.Active Ghost { get; private set; }

        public static void InitSpells()
        {
            var slot = Player.Instance.GetSpellSlotFromName("summonerdot");
            if (slot != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(slot, 600);
            }
            slot = Player.Instance.GetSpellSlotFromName("summonersmite");
            if (slot != SpellSlot.Unknown)
            {
                Smite = new Spell.Targeted(slot, 500);
            }
            slot = Player.Instance.GetSpellSlotFromName("SummonerExhaust");
            if (slot != SpellSlot.Unknown)
            {
                Exhaust = new Spell.Targeted(slot, 650);
            }
            slot = Player.Instance.GetSpellSlotFromName("summonerheal");
            if (slot != SpellSlot.Unknown)
            {
                Heal = new Spell.Active(slot, 850);
            }
            slot = Player.Instance.GetSpellSlotFromName("SummonerBarrier");
            if (slot != SpellSlot.Unknown)
            {
                Barrier = new Spell.Active(slot);
            }
            slot = Player.Instance.GetSpellSlotFromName("SummonerBoost");
            if (slot != SpellSlot.Unknown)
            {
                Cleanse = new Spell.Active(slot);
            }
        }
        public static void KillSteal()
        {
            if (Config.Spell["eIg"].Cast<CheckBox>().CurrentValue && Ignite != null)
            {
                switch (Config.Spell["Igstyle"].Cast<ComboBox>().CurrentValue)
                {
                    case 0:
                        {
                            var target = EntityManager.Heroes.Enemies.Where(t =>
                                t.IsValidTarget(Ignite.Range) &&
                                t.Health <= Player.Instance.GetSummonerSpellDamage(t, DamageLibrary.SummonerSpells.Ignite)).FirstOrDefault();

                            if (target != null && Config.Spell["Ig" + target.ChampionName].Cast<CheckBox>().CurrentValue && Ignite.IsReady())
                            {
                                Ignite.Cast(target);
                            }
                        }
                        break;
                    case 1:
                        {
                            var target = EntityManager.Heroes.Enemies.Where(t =>
                                t.IsValidTarget(Ignite.Range) &&
                                t.Health <= Player.Instance.GetSummonerSpellDamage(t, DamageLibrary.SummonerSpells.Ignite) / 5).FirstOrDefault();

                            if (target != null && Config.Spell["Ig" + target.ChampionName].Cast<CheckBox>().CurrentValue && Ignite.IsReady())
                            {
                                Ignite.Cast(target);
                            }
                        }
                        break;
                }
            }
            if (Config.Spell["esmiteKs"].Cast<CheckBox>().CurrentValue && Smite != null && Extensions.CanUseOnChamp)
            {
                var target = EntityManager.Heroes.Enemies.Where(t =>
                            t.IsValidTarget(Smite.Range) &&
                            t.Health <= Player.Instance.GetSummonerSpellDamage(t, DamageLibrary.SummonerSpells.Smite)).FirstOrDefault();

                if (target != null && Config.Spell["Smite" + target.ChampionName].Cast<CheckBox>().CurrentValue && Smite.IsReady())
                {
                    Smite.Cast(target);
                }
            }
        }
        public static void JungSteal()
        {
            var Important = Config.Spell["esmite3r"].Cast<CheckBox>().CurrentValue;
            if (Spells.Smite != null)
            {
                var minion = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(m => m.IsMonster && m.IsValidTarget(Smite.Range) && Extensions.IsImportant(m));
                var Red = ObjectManager.Get<Obj_AI_Minion>().Where(r => r.IsMonster && r.IsValidTarget(Smite.Range) && r.Name.Contains("Red")).OrderBy(x => x.MaxHealth).LastOrDefault();
                var Blue = ObjectManager.Get<Obj_AI_Minion>().Where(b => b.IsMonster && b.IsValidTarget(Smite.Range) && b.Name.Contains("Blue")).OrderBy(x => x.MaxHealth).LastOrDefault();
                if (minion != null && minion.IsValid && Important)
                {
                    if (minion.Health <= Player.Instance.GetSummonerSpellDamage(minion, DamageLibrary.SummonerSpells.Smite))
                    {
                        Smite.Cast(minion);
                    }
                }
                if (Red != null && Red.IsValid && Config.Spell["esmitered"].Cast<CheckBox>().CurrentValue)
                {
                    if (Red.Health <= Player.Instance.GetSummonerSpellDamage(minion, DamageLibrary.SummonerSpells.Smite))
                    {
                        Smite.Cast(Red);
                    }
                }
                if (Blue != null && Blue.IsValid && Config.Spell["esmiteblue"].Cast<CheckBox>().CurrentValue)
                {
                    if (Blue.Health <= Player.Instance.GetSummonerSpellDamage(minion, DamageLibrary.SummonerSpells.Smite))
                    {
                        Smite.Cast(Blue);
                    }
                }
            }
            
        }
        public static void UseHeal()
        {
            if (Heal != null && Heal.IsReady())
            {
                if (Config.Spell["eHeal"].Cast<CheckBox>().CurrentValue
                 && Player.Instance.HealthPercent <= Config.Spell["myHPHeal"].Cast<Slider>().CurrentValue
                 && ObjectManager.Player.CountEnemiesInRange(1200) >= 1)
                {
                    Heal.Cast();
                }
                foreach (
                    var ally in EntityManager.Heroes.Allies.Where(a => !a.IsDead))
                {
                    if (ally != null)
                    {
                        if (Config.Spell["eHealAlly"].Cast<CheckBox>().CurrentValue
                            && ally.CountEnemiesInRange(1200) >= 1
                            && ObjectManager.Player.Position.Distance(ally) <= 800
                            && Config.Spell["heal" + ally.ChampionName].Cast<CheckBox>().CurrentValue
                            && ally.HealthPercent <= Config.Spell["allyHPHeal"].Cast<Slider>().CurrentValue)
                        {
                            Heal.Cast(ally);
                        }
                    }
                }
            }
        }
    }
}
