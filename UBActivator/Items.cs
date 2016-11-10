using EloBuddy;
using EloBuddy.SDK;

namespace UBActivator
{
    class Items
    {
        public static Item Archangel_Staff { get; private set; }
        public static Item Bilgewater_Cutlass { get; private set; }
        public static Item Blade_Of_The_Ruined_King { get; private set; }
        public static Item Banner_of_Command { get; private set; }
        public static Item Corrupting_Potion { get; private set; }
        public static Item Elixir_of_Iron { get; private set; }
        public static Item Elixir_of_Sorcery { get; private set; }
        public static Item Elixir_of_Wrath { get; private set; }
        public static Item Eye_of_the_Equinox { get; private set; }
        public static Item Eye_of_the_Oasis { get; private set; }
        public static Item Eye_of_the_Watchers { get; private set; }
        public static Item Face_of_the_moutain{ get; private set; }
        public static Item Farsight_Alteration { get; private set; }
        public static Item Frost_Queen_s_Claim { get; private set; }
        public static Item HealthPotion { get; private set; }
        public static Item Hextech_GLP_800 { get; private set; }
        public static Item Hextech_Gunblade { get; private set; }
        public static Item Hextech_Protobelt_01 { get; private set; }
        public static Item Hunter_s_Potion { get; private set; }
        public static Item Locket_of_the_iron_Solari { get; private set; }
        public static Item Manamune { get; private set; }
        public static Item Mercurial_Scimitar { get; private set; }
        public static Item Mikaels_Crucible { get; private set; }
        public static Item Ohmwrecker { get; private set; }
        public static Item Oracle_Alteration { get; private set; }
        public static Item Quicksilver_Sash { get; private set; }
        public static Item Randuin_s_Omen { get; private set; }
        public static Item Ravenous_Hydra { get; private set; } 
        public static Item Refillable_Potion { get; private set; }
        public static Item Righteous_Glory { get; private set; }
        public static Item Ruby_Sightstone { get; private set; }
        public static Item Sightstone { get; private set; }
        public static Item Talisman_of_Ascension { get; private set; }
        public static Item Tear_of_the_Goddess { get; private set; }
        public static Item The_Black_Spear { get; private set; } 
        public static Item Tiamat { get; private set; }
        public static Item Titanic_Hydra { get; private set; }
        public static Item Total_Biscuit_of_Rejuvenation { get; private set; }
        public static Item Tracker_s_Knife { get; private set; }
        public static Item Tracker_s_Knifee_Enchantment_Cinderhulk { get; private set; }
        public static Item Tracker_s_Knife_Enchantment_Runic_Echoes { get; private set; }
        public static Item Tracker_s_Knife_Enchantment_Warrior { get; private set; }
        public static Item Seraph_s_Embrace { get; private set; }
        public static Item Sweeping_Lens { get; private set; }
        public static Item Control_Ward { get; private set; }
        public static Item Warding_Totem { get; private set; }
        public static Item Youmuu_s_Ghostblade { get; private set; }
        public static Item Zz_Rot_Portal { get; private set; }
        public static Item Zhonya_Hourglass { get; private set; }
        
        public static void InitItems()
        {
            Archangel_Staff = new Item(ItemId.Archangels_Staff);
            Bilgewater_Cutlass = new Item(ItemId.Bilgewater_Cutlass);
            Blade_Of_The_Ruined_King = new Item(ItemId.Blade_of_the_Ruined_King);
            Banner_of_Command = new Item(ItemId.Banner_of_Command);
            Corrupting_Potion = new Item(ItemId.Corrupting_Potion);
            Elixir_of_Iron = new Item(ItemId.Elixir_of_Iron);
            Elixir_of_Sorcery = new Item(ItemId.Elixir_of_Sorcery);
            Elixir_of_Wrath = new Item(ItemId.Elixir_of_Wrath);
            Eye_of_the_Equinox = new Item(ItemId.Eye_of_the_Equinox);
            Eye_of_the_Oasis = new Item(ItemId.Eye_of_the_Oasis, 600f);
            Eye_of_the_Watchers = new Item(ItemId.Eye_of_the_Watchers);
            Face_of_the_moutain = new Item(ItemId.Face_of_the_Mountain, 600);
            Farsight_Alteration = new Item(ItemId.Farsight_Alteration);
            Frost_Queen_s_Claim = new Item(ItemId.Frost_Queens_Claim);
            HealthPotion = new Item(ItemId.Health_Potion);
            Hextech_GLP_800 = new Item(ItemId.Hextech_GLP_800);
            Hextech_Gunblade = new Item(ItemId.Hextech_Gunblade);
            Hextech_Protobelt_01 = new Item(ItemId.Hextech_Protobelt_01);
            Hunter_s_Potion = new Item(ItemId.Hunters_Potion);
            Locket_of_the_iron_Solari = new Item(ItemId.Locket_of_the_Iron_Solari, 600);
            Manamune = new Item(ItemId.Manamune);
            Mercurial_Scimitar = new Item(ItemId.Mercurial_Scimitar);
            Mikaels_Crucible = new Item(ItemId.Mikaels_Crucible);
            Ohmwrecker = new Item(ItemId.Ohmwrecker);
            Oracle_Alteration = new Item(ItemId.Oracle_Alteration);
            Quicksilver_Sash = new Item(ItemId.Quicksilver_Sash);
            Randuin_s_Omen = new Item(ItemId.Randuins_Omen);
            Ravenous_Hydra = new Item(ItemId.Ravenous_Hydra);
            Refillable_Potion = new Item(ItemId.Refillable_Potion);
            Righteous_Glory = new Item(ItemId.Righteous_Glory);
            Ruby_Sightstone = new Item(ItemId.Ruby_Sightstone);
            Sightstone = new Item(ItemId.Sightstone);
            Talisman_of_Ascension = new Item(ItemId.Talisman_of_Ascension);
            Tear_of_the_Goddess = new Item(ItemId.Tear_of_the_Goddess);
            The_Black_Spear = new Item(ItemId.The_Black_Spear);
            Tiamat = new Item(ItemId.Tiamat);
            Titanic_Hydra = new Item(ItemId.Titanic_Hydra);
            Total_Biscuit_of_Rejuvenation = new Item(ItemId.Total_Biscuit_of_Rejuvenation);
            Tracker_s_Knife = new Item(ItemId.Trackers_Knife);
            Tracker_s_Knife_Enchantment_Runic_Echoes = new Item(ItemId.Trackers_Knife_Enchantment_Runic_Echoes);
            Tracker_s_Knife_Enchantment_Warrior = new Item(ItemId.Trackers_Knife_Enchantment_Warrior);
            Tracker_s_Knifee_Enchantment_Cinderhulk = new Item(ItemId.Trackers_Knife_Enchantment_Cinderhulk);
            Seraph_s_Embrace = new Item(ItemId.Seraphs_Embrace);
            Sweeping_Lens = new Item(ItemId.Sweeping_Lens_Trinket);
            Control_Ward = new Item(ItemId.Control_Ward);
            Warding_Totem = new Item(ItemId.Warding_Totem_Trinket);
            Youmuu_s_Ghostblade = new Item(ItemId.Youmuus_Ghostblade);
            Zz_Rot_Portal = new Item(ItemId.ZzRot_Portal);
            Zhonya_Hourglass = new Item(ItemId.Zhonyas_Hourglass);
        }
    }
}
