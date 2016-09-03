using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;


namespace UBSivir
{
    class Config
    {
        public static Menu Menu;
        public static Menu ComboMenu;
        public static Menu ShieldMenu;
        public static Menu ShieldMenu2;
        public static Menu HarassMenu;
        public static Menu LaneClear;
        public static Menu JungleClear;
        public static Menu LasthitMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;

        public static void Dattenosa()
        {
            // Menu
            Menu = MainMenu.AddMenu("UB Sivir", "UBSivir");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");

            //ComboMenu
            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.AddGroupLabel("Combo Settings");
                ComboMenu.Add("useQCombo", new CheckBox("Use Q"));
                ComboMenu.Add("useWCombo", new CheckBox("Use W"));
                ComboMenu.Add("useRCombo", new CheckBox("Use R"));
                ComboMenu.Add("RHitCombo", new Slider("Use R if buff {0} ally & you in combo mode", 4, 1, 4));
            }
            //Shield Menu
            ShieldMenu = Menu.AddSubMenu("ShieldMenu");
            {
                ShieldMenu.AddGroupLabel("Block Options");
                ShieldMenu.Add("blockSpellsE", new CheckBox("Auto-Block Spells (E)"));
                ShieldMenu.AddSeparator();

                ShieldMenu.AddGroupLabel("Enemies spells to block");
            }
            //Shield Menu2
            ShieldMenu2 = Menu.AddSubMenu("ShieldMenu2");
            {
                ShieldMenu2.AddGroupLabel("Core Options");
                ShieldMenu2.Add("BlockChalleningE", new CheckBox("Auto use E to Block Channeling Spells"));
                ShieldMenu2.AddSeparator();

                ShieldMenu2.AddGroupLabel("Enemies spells to block");
            }

            //HarassMenu
            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.AddGroupLabel("Harass Settings");
                HarassMenu.Add("useQHr", new CheckBox("Use Q"));
                HarassMenu.Add("useQHr2", new CheckBox("Only use Q if no minion collision"));
                HarassMenu.Add("useWHr", new CheckBox("Use W"));               
                HarassMenu.Add("HrManage", new Slider("If mana percent below {0} stop harass", 50));
            }

            //LaneClear Menu
            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.AddGroupLabel("Laneclear Settings");
                LaneClear.Add("useQLc", new CheckBox("Use Q to laneclear", false));
                LaneClear.Add("useWLc", new CheckBox("Use W to laneclear", false));
                LaneClear.Add("WHitLc", new Slider("Only Use W if hit {0} minion(s)", 6, 1, 30));
                LaneClear.Add("autoWHr", new CheckBox("Auto W if maybe hit enemy"));
                LaneClear.Add("LcManager", new Slider("If mana percent below {0} stop use skill to laneclear", 50));
            }
            //JungleClear Menu
            JungleClear = Menu.AddSubMenu("JungleClear");
            {
                JungleClear.AddGroupLabel("Jungleclear Settings");
                JungleClear.Add("useQJc", new CheckBox("Use Q to jungleclear"));
                JungleClear.Add("useWJc", new CheckBox("Use W to jungleclear"));
                JungleClear.Add("WHitJc", new Slider("Use W if hit {0} monster(s)", 2, 1, 4));               
                JungleClear.Add("JcManager", new Slider("If mana percent below {0} stop use skill to jungleclear", 50));
            }

            //LasthitMenu
            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.Add("useQLh", new CheckBox("Use Q to lasthit"));
                LasthitMenu.Add("useWLh", new CheckBox("Use W to lasthit"));
                LasthitMenu.Add("LhManager", new Slider("If mana percent below {0} stop use skill to lasthit", 50));
            }

            //DrawMenu
            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.Add("drawQ", new CheckBox("Draw Q", false));
                DrawMenu.Add("drawR", new CheckBox("Draw R", false));
            }

            //MiscMenu          
            MiscMenu = Menu.AddSubMenu("MiscMenu");
            {
                MiscMenu.AddGroupLabel("Misc Settings");
                MiscMenu.Add("useQKS", new CheckBox("Use Q to KillSteal"));
                MiscMenu.Add("AutoQ", new CheckBox("Auto Q if target in immobilize"));
                MiscMenu.Add("AutoW", new CheckBox("Auto reset auto attack"));
            }
        }
        public static bool BlockSpells
        {
            get { return ShieldMenu["blockSpellsE"].Cast<CheckBox>().CurrentValue; }
        }
    }
}
