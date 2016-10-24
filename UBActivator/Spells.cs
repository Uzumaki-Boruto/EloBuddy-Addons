using System.Linq;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Notifications;

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
            if (Ignite != null && Config.Spell["eIg"].Cast<CheckBox>().CurrentValue)
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
            if (Smite != null && Config.Spell["esmiteKs"].Cast<CheckBox>().CurrentValue && Extensions.CanUseOnChamp)
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
            if (Smite != null)
            {
                var Important = Config.Spell["esmite3r"].Cast<CheckBox>().CurrentValue;
                var minion = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(m => m != null && m.IsMonster && Extensions.IsImportant(m) && Smite.IsInRange(m));
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
                    if (Red.Health <= Player.Instance.GetSummonerSpellDamage(Red, DamageLibrary.SummonerSpells.Smite))
                    {
                        Smite.Cast(Red);
                    }
                }
                if (Blue != null && Blue.IsValid && Config.Spell["esmiteblue"].Cast<CheckBox>().CurrentValue)
                {
                    if (Blue.Health <= Player.Instance.GetSummonerSpellDamage(Blue, DamageLibrary.SummonerSpells.Smite))
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
                 && Player.Instance.CountEnemiesInRange(1200) >= 1)
                {
                    Heal.Cast();
                }
                foreach (var ally in EntityManager.Heroes.Allies.Where(a => !a.IsDead))
                {
                    if (EntityManager.Heroes.Allies.Where(a => !a.IsDead) != null && ally.ChampionName != Player.Instance.ChampionName)
                    {
                        if (Config.Spell["eHealAlly"].Cast<CheckBox>().CurrentValue
                            && ally.CountEnemiesInRange(1200) >= 1
                            && Player.Instance.Position.Distance(ally) <= 800
                            && Config.Spell["heal" + ally.ChampionName].Cast<CheckBox>().CurrentValue
                            && ally.HealthPercent <= Config.Spell["allyHPHeal"].Cast<Slider>().CurrentValue)
                        {
                            Heal.Cast();
                        }
                    }
                }
            }
        }
        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (Exhaust == null || !Exhaust.IsReady()) return;
            if (!Config.Spell["exhaust"].Cast<CheckBox>().CurrentValue || !Config.Spell["danger"].Cast<CheckBox>().CurrentValue) return;
            if (sender.IsEnemy && args.DangerLevel == DangerLevel.High)
            {
               if (Exhaust.IsInRange(sender))
               {
                   Exhaust.Cast(sender);
               }
            }
        }
    }
    public static class Exhaust
    {
        static Exhaust()
        {
            new Advance("Ahri", SpellSlot.R).Add();
            new Advance("Akali", SpellSlot.R).Add();
            new Advance("Annie", SpellSlot.R).Add();
            new Advance("Kennen", SpellSlot.R).Add();
            new Advance("Lucian", SpellSlot.R).Add();
            new Advance("Rengar", SpellSlot.R).Add();
            new Advance("Riven", SpellSlot.R).Add();
            new Advance("Syndra", SpellSlot.R).Add();
            new Advance("Veigar", SpellSlot.R).Add();
            new Advance("Viktor", SpellSlot.R).Add();
            new Advance("Yasuo", SpellSlot.R).Add();
            new Advance("Zed", SpellSlot.R).Add();

            var notif = new SimpleNotification("UBActivator Notification", "Detected Exhaust as a summoner spell.");
            Notifications.Show(notif, 5000);

            Config.Spell.AddGroupLabel("Exhaust Setting");
            Config.Spell.Add("exhaust", new CheckBox("Use Exhaust"));
            Config.Spell.Add("danger", new CheckBox("Use Exhaust on danger spell"));
            foreach (var Dangerous in Advance.GetDispellList().Where(d => EntityManager.Heroes.Enemies.Any(h => h.ChampionName == d.ChampionName)))
            {
                Config.Spell.Add("Exhaust" + Dangerous.ChampionName + Dangerous.Slot, new CheckBox("Exhaust " + Dangerous.ChampionName + " - " + Dangerous.Slot, true));
            }

            AIHeroClient.OnProcessSpellCast += AIHeroClient_OnProcessSpellCast;
        }

        static void AIHeroClient_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsAlly) return;
            var caster = sender as AIHeroClient;
            if (Config.Spell["Exhaust" + caster.ChampionName + args.Slot] != null && Config.Spell["Exhaust" + caster.ChampionName + args.Slot].Cast<CheckBox>().CurrentValue && Config.Spell["exhaust"].Cast<CheckBox>().CurrentValue)
            {
                if (Spells.Exhaust != null && Spells.Exhaust.IsReady())
                {
                    Spells.Exhaust.Cast(caster);
                }
            }
        }

        public static List<Advance> Dangerous
        {
            get { return Advance.GetDispellList(); }
        }

        public static void Initialize()
        {

        }
    }
    public static class Barrier
    {
        static Barrier()
        {
            new Advance("Ahri", SpellSlot.R).Add();
            new Advance("Akali", SpellSlot.R).Add();
            new Advance("Annie", SpellSlot.R).Add();
            new Advance("Kennen", SpellSlot.R).Add();
            new Advance("Rengar", SpellSlot.Q).Add();
            new Advance("Riven", SpellSlot.R).Add();
            new Advance("Syndra", SpellSlot.R).Add();
            new Advance("Veigar", SpellSlot.R).Add();
            new Advance("Viktor", SpellSlot.R).Add();
            new Advance("Yasuo", SpellSlot.R).Add();
            new Advance("Zed", SpellSlot.R).Add();

            var notif = new SimpleNotification("UBActivator Notification", "Detected Barrier as a summoner spell.");
            Notifications.Show(notif, 5000);

            Config.Spell.AddGroupLabel("Barrier Setting");
            foreach (var Dangerous in Advance.GetDispellList().Where(d => EntityManager.Heroes.Enemies.Any(h => h.ChampionName == d.ChampionName)))
            {
                Config.Spell.Add("Barrier" + Dangerous.ChampionName + Dangerous.Slot, new CheckBox("Barrier " + Dangerous.ChampionName + " - " + Dangerous.Slot, true));
            }

            AIHeroClient.OnProcessSpellCast += AIHeroClient_OnProcessSpellCast;
        }

        static void AIHeroClient_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var caster = sender as AIHeroClient;
            if (args.End.Distance(Player.Instance) <= 500 || (args.Target != null && args.Target.IsMe))
            {
                if (Config.Spell["Barrier" + caster.ChampionName + args.Slot].Cast<CheckBox>().CurrentValue)
                {
                    if (Spells.Barrier != null && Spells.Barrier.IsReady())
                    {
                        Spells.Barrier.Cast(caster);
                    }
                }
            }
        }

        public static List<Advance> Dangerous
        {
            get { return Advance.GetDispellList(); }
        }

        public static void Initialize()
        {

        }
    }

    public class Advance
    {
        private static readonly List<Advance> DispellList = new List<Advance>();
        public string ChampionName;
        public SpellSlot Slot;

        public Advance(string champName, SpellSlot slot)
        {
            ChampionName = champName;
            Slot = slot;
        }

        public void Add()
        {
            DispellList.Add(this);
        }

        public static List<Advance> GetDispellList()
        {
            return DispellList;
        }
    }
}
