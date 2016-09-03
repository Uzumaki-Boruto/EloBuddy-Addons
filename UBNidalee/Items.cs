using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBNidalee
{
    class Items
    {
        public static Item Zhonya { get; private set; }
        public static Item Seraph { get; private set; }
        public static Item Cutlass { get; private set; }
        public static Item Hextech { get; private set; }
        public static Item Omen { get; private set; }
        public static Item Glory { get; private set; }
        public static Item Solari { get; private set; }

        public static void InitItems()
        {
            Zhonya = new Item(ItemId.Zhonyas_Hourglass);
            Seraph = new Item(ItemId.Seraphs_Embrace);
            Cutlass = new Item(ItemId.Bilgewater_Cutlass);
            Hextech = new Item(ItemId.Hextech_Gunblade);
            Omen = new Item(ItemId.Randuins_Omen); 
            Glory = new Item(ItemId.Righteous_Glory);
            Solari = new Item(ItemId.Locket_of_the_Iron_Solari);
           
            Game.OnTick += OnTick;
            Game.OnUpdate += OnUpdate;
        }

        private static void OnUpdate(EventArgs args)
        {
            if (Zhonya.IsOwned())
            {
                UseItem1(Zhonya);
            }
            if (Seraph.IsOwned())
            {
                UseItem2(Seraph);
            }
        }
        private static void OnTick(EventArgs args)
        {
            if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) return;

            var target = Orbwalker.LastTarget as AIHeroClient;
            if (target == null) return;
           
            if (Cutlass.IsOwned())
            {
                UseItem3(Cutlass, target);
            }
            if (Hextech.IsOwned())
            {
                UseItem4(Hextech, target);
            }
            if (Omen.IsOwned())
            {
                UseItem5(Omen);
            }
            if (Glory.IsOwned())
            {
                UseItem6(Glory);
            }
            if (Solari.IsOwned())
            {
                UseItem7(Solari);
            }
        }

        private static void UseItem1(Item item)
        {
            if (!(Player.Instance.CountEnemiesInRange(1500) >= 1)
                || !Config.ItemMenu["item1"].Cast<CheckBox>().CurrentValue
                || !(Config.ItemMenu["item1MyHp"].Cast<Slider>().CurrentValue < Player.Instance.HealthPercent))
            {
                return;
            }

            var slot = Player.Instance.InventoryItems.FirstOrDefault(x => x.Id == item.Id);
            if (slot != null && Player.GetSpell(slot.SpellSlot).IsReady)
            {
                Player.CastSpell(slot.SpellSlot);
            }
        }
        private static void UseItem2(Item item)
        {
            if (!(Player.Instance.CountEnemiesInRange(1000) >= 1)
                || !Config.ItemMenu["item2"].Cast<CheckBox>().CurrentValue
                || Config.ItemMenu["item2MyHp"].Cast<Slider>().CurrentValue < Player.Instance.HealthPercent)
            {
                return;
            }

            var slot = Player.Instance.InventoryItems.FirstOrDefault(x => x.Id == item.Id);
            if (slot != null && Player.GetSpell(slot.SpellSlot).IsReady)
            {
                Player.CastSpell(slot.SpellSlot);
            }
        }
        private static void UseItem3(Item item, AIHeroClient target)
        {
            if (!target.IsValidTarget(550)
                || !Config.ItemMenu["item3"].Cast<CheckBox>().CurrentValue
                || Config.ItemMenu["item34EnemyHP"].Cast<Slider>().CurrentValue < target.HealthPercent)
            {
                return;
            }

            var slot1 = Player.Instance.InventoryItems.FirstOrDefault(x => x.Id == item.Id);
            if (slot1 != null && Player.GetSpell(slot1.SpellSlot).IsReady)
            {
                Player.CastSpell(slot1.SpellSlot, target);
            }
        }
        private static void UseItem4(Item item, AIHeroClient target)
        {
            if (!target.IsValidTarget(700)
                || !Config.ItemMenu["item4"].Cast<CheckBox>().CurrentValue
                || Config.ItemMenu["item34EnemyHP"].Cast<Slider>().CurrentValue < target.HealthPercent)
            {
                return;
            }

            var slot1 = Player.Instance.InventoryItems.FirstOrDefault(x => x.Id == item.Id);
            if (slot1 != null && Player.GetSpell(slot1.SpellSlot).IsReady)
            {
                Player.CastSpell(slot1.SpellSlot, target);
            }
        }
        private static void UseItem5(Item item)
        {
            if (!(Player.Instance.CountEnemiesInRange(500) >= Config.ItemMenu["item5Enemy"].Cast<Slider>().CurrentValue)
                || !Config.ItemMenu["item5"].Cast<CheckBox>().CurrentValue)
            {
                return;
            }

            var slot = Player.Instance.InventoryItems.FirstOrDefault(x => x.Id == item.Id);
            if (slot != null && Player.GetSpell(slot.SpellSlot).IsReady)
            {
                Player.CastSpell(slot.SpellSlot);
            }
        }
        private static void UseItem6(Item item)
        {
            if (!(Player.Instance.CountEnemiesInRange(Player.Instance.MoveSpeed * 2) >= 1)
                || !Config.ItemMenu["item6"].Cast<CheckBox>().CurrentValue
                || Config.ItemMenu["item6Ally"].Cast<Slider>().CurrentValue > Player.Instance.CountAlliesInRange(500))
            {
                return;
            }

            var slot = Player.Instance.InventoryItems.FirstOrDefault(x => x.Id == item.Id);
            if (slot != null && Player.GetSpell(slot.SpellSlot).IsReady)
            {
                Player.CastSpell(slot.SpellSlot);
            }
        }
        private static void UseItem7(Item item)
        {
            if (Config.ItemMenu["item7Ally"].Cast<Slider>().CurrentValue > Player.Instance.CountAlliesInRange(600)
                || !Config.ItemMenu["item7"].Cast<CheckBox>().CurrentValue)
            {
                return;
            }

            var slot = Player.Instance.InventoryItems.FirstOrDefault(x => x.Id == item.Id);
            if (slot != null && Player.GetSpell(slot.SpellSlot).IsReady)
            {
                Player.CastSpell(slot.SpellSlot);
            }
        }
    }
}
