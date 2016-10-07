using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace UBActivator
{
    class Ward
    {
        #region Extension
        public static float LastWard;
        public static float LastReveal;
        public static bool CanCastWard
        {
            get { return Game.Time - LastWard > 5.25f && IsReady; }
        }
        public static bool CanReveal
        {
            get { return Game.Time - LastReveal > 1.25f && IsReadyReveal; }
        }
        public static readonly Item[] ItemWards =
        {
            Items.Ruby_Sightstone, Items.Sightstone, Items.Eye_of_the_Equinox,
            Items.Eye_of_the_Oasis, Items.Eye_of_the_Watchers, Items.Farsight_Alteration,
            Items.Tracker_s_Knife, Items.Tracker_s_Knife_Enchantment_Runic_Echoes,
            Items.Tracker_s_Knife_Enchantment_Warrior, Items.Tracker_s_Knifee_Enchantment_Cinderhulk,
            Items.Warding_Totem, Items.Vision_Ward, 
        };
        public static readonly Item[] ItemReveal =
        {
            Items.Vision_Ward, Items.Oracle_Alteration
        };
        public static bool IsReadyReveal
        {
            get { return ItemReveal.Any(i => i.IsReady()); }
        }
        private static Item GetItemReveal
        {
            get { return ItemReveal.FirstOrDefault(i => i.IsReady()); }
        }
        public static bool IsReady
        {
            get { return ItemWards.Any(i => i.IsReady()); }
        }

        private static Item GetItem
        {
            get { return ItemWards.FirstOrDefault(i => i.IsReady()); }
        }
        #endregion

        #region Bush Reveal
        public static void OnTick()
        {
            if (!Config.Ward["enableward"].Cast<CheckBox>().CurrentValue || !Config.Ward["enablebrush"].Cast<CheckBox>().CurrentValue) return;
            if (!Items.Vision_Ward.IsOwned() && !Items.Ruby_Sightstone.IsOwned() && !Items.Sightstone.IsOwned() && !Items.Eye_of_the_Equinox.IsOwned()
                && !Items.Eye_of_the_Oasis.IsOwned() && !Items.Eye_of_the_Watchers.IsOwned() && !Items.Farsight_Alteration.IsOwned()
                && !Items.Warding_Totem.IsOwned() && !Items.Tracker_s_Knife.IsOwned() && !Items.Tracker_s_Knife_Enchantment_Runic_Echoes.IsOwned() 
                && !Items.Tracker_s_Knife_Enchantment_Warrior.IsOwned() && !Items.Tracker_s_Knifee_Enchantment_Cinderhulk.IsOwned()) return;
            foreach (var heros in EntityManager.Heroes.Enemies.Where(x => !x.IsDead && x.Distance(Player.Instance) < 1000))
            {
                var Path = heros.Path.LastOrDefault();
                var Time = Config.Ward["warddelay"].Cast<Slider>().CurrentValue;
                var Delay = Config.Ward["wardhuman"].Cast<CheckBox>().CurrentValue ?
                    new Random().Next(0, Time) :
                    Time;
                if (NavMesh.IsWallOfGrass(Path, 1))
                {
                    if (heros.Distance(Path) > 200) return;
                    if (NavMesh.IsWallOfGrass(Player.Instance.Position, 1) && Player.Instance.Distance(Path) < 200) return;

                    if (Player.Instance.Distance(Path) <= 600)
                    {
                        foreach (var obj in ObjectManager.Get<AIHeroClient>().Where(x => x.Name.ToLower().Contains("ward") && x.IsAlly && x.Distance(Path) < 300))
                        {
                            if (NavMesh.IsWallOfGrass(obj.Position, 1)) return;
                        }
                        var ward = GetItem;
                        if (ward != null && CanCastWard)
                        {
                            Core.DelayAction(() => ward.Cast(Path), Delay);
                            LastWard = Game.Time;
                        }
                    }
                }
            }
        }
        #endregion

        #region Reveal Kappa
        static readonly List<Stealth> SpellList = new List<Stealth>();
        public static float vaynebuff = 0f;
        public struct Stealth
        {
            public SpellSlot Slot;

            public Champion Hero;

            public String Name;
        } 
        public static void InitStealth()
        {
            SpellList.Add(new Stealth { Hero = Champion.Akali, Name = "akalismokebomb", Slot = SpellSlot.W });
            SpellList.Add(new Stealth { Hero = Champion.Shaco, Name = "deceive", Slot = SpellSlot.Q });
            SpellList.Add(new Stealth { Hero = Champion.Khazix, Name = "khazixr", Slot = SpellSlot.R });
            SpellList.Add(new Stealth { Hero = Champion.Khazix, Name = "khazixrlong", Slot = SpellSlot.R });
            SpellList.Add(new Stealth { Hero = Champion.Talon, Name = "talonshadowassault", Slot = SpellSlot.R });
            SpellList.Add(new Stealth { Hero = Champion.MonkeyKing, Name = "monkeykingdecoy", Slot = SpellSlot.W });
            SpellList.Add(new Stealth { Hero = Champion.Vayne, Name = "vaynetumble", Slot = SpellSlot.Q });
            SpellList.Add(new Stealth { Hero = Champion.Twitch, Name = "hideinshadows", Slot = SpellSlot.Q });
        }
        public static void OnTick2()
        {
            var vayne =
                EntityManager.Heroes.Enemies.FirstOrDefault(
                    v => v.Hero == Champion.Vayne && v.IsEnemy && v.Buffs.Any(b => b.Name.ToLower().Contains("vayneinquisition")));

            if (vayne != null)
            {
                vaynebuff = vayne.GetBuff("VayneInquisition").EndTime;
            }
        }
        public static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var Time = Config.Ward["warddelay"].Cast<Slider>().CurrentValue;
            var Delay = Config.Ward["wardhuman"].Cast<CheckBox>().CurrentValue ?
                new Random().Next(0, Time) :
                Time;
            if (!sender.IsEnemy || sender == null) return;

            if (SpellList.Any(spell => spell.Name == args.SData.Name.ToLower()))
            {
                if (Config.Ward[args.SData.Name].Cast<CheckBox>().CurrentValue)
                {
                    if (args.SData.Name.ToLower().Contains("vaynetumble") && Game.Time > vaynebuff)
                    {
                        return;
                    }

                    var reveal = GetItemReveal;
                    if (reveal != null && CanReveal)
                    {
                        Core.DelayAction(() => reveal.Cast(sender.Position), Delay);
                        LastReveal = Game.Time;
                    }
                }
            }
        }
        #endregion
    }
}
