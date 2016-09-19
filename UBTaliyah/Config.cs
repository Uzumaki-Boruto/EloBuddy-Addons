using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;


namespace UBTaliyah
{
    class Config
    {
        public static Menu Menu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClear;
        public static Menu JungleClear;
        public static Menu LasthitMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;

        public static void Dattenosa()
        {
            Menu = MainMenu.AddMenu("UB Taliyah", "UBTaliyah");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");

            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.AddGroupLabel("Combo Settings");
                ComboMenu.Add("Q", new ComboBox("Q style", 2, "Don't use Q", "Q Full Only", "Both"));
                ComboMenu.Add("W", new ComboBox("W style", 3, "Don't use W", "Pull", "Push", "Smart"));
                ComboMenu.Add("E", new CheckBox("Use E"));
            }

            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.AddGroupLabel("Harass Settings");
                HarassMenu.Add("Q", new ComboBox("Q style", 2, "Don't use Q", "Q Full Only", "Both"));
                HarassMenu.Add("W", new ComboBox("W style", 3, "Don't use W", "Pull", "Push", "Smart"));
                HarassMenu.Add("E", new CheckBox("Use E"));
                HarassMenu.Add("hr", new Slider("If mana percent below {0}% stop harass", 50));
                HarassMenu.Add("keyharass", new KeyBind("Auto Harass", false, KeyBind.BindTypes.HoldActive, 'Z'));
                HarassMenu.Add("autohrmng", new Slider("If mana percent below {0}% stop auto harass", 85));
            }

            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.AddGroupLabel("Laneclear Settings");
                LaneClear.Add("Q", new CheckBox("Use Q to laneclear", false));
                LaneClear.Add("W", new CheckBox("Use W to laneclear", false));
                LaneClear.Add("E", new CheckBox("Use E to laneclear", false));
                LaneClear.Add("hit", new Slider("Minions around to cast", 5, 1, 15));
                LaneClear.Add("lc", new Slider("If mana percent below {0}% stop use skill to laneclear", 50));
            }

            JungleClear = Menu.AddSubMenu("JungleClear");
            {
                JungleClear.AddGroupLabel("Jungleclear Settings");
                JungleClear.Add("Q", new CheckBox("Use Q to jungleclear"));
                JungleClear.Add("W", new CheckBox("Use W to jungleclear"));
                JungleClear.Add("E", new CheckBox("Use E to jungleclear"));
                JungleClear.Add("jc", new Slider("If mana percent below {0}% stop use skill to jungleclear", 50));
            }

            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.AddGroupLabel("Unkillable Minion Settings");
                LasthitMenu.Add("Q", new CheckBox("Use Q to lasthit", false));
                LasthitMenu.Add("W", new CheckBox("Use W to lasthit", false));
                LasthitMenu.Add("E", new CheckBox("Use E to lasthit", false));
                LasthitMenu.Add("lh", new Slider("If mana percent below {0}% stop use skill to lasthit", 50));
            }

            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawing"));
                DrawMenu.Add("notif", new CheckBox("Enable notification"));
                DrawMenu.AddSeparator();
                DrawMenu.Add("Qdr", new CheckBox("Draw Q"));
                DrawMenu.Add("Wdr", new CheckBox("Draw W"));
                DrawMenu.Add("Edr", new CheckBox("Draw E"));
                DrawMenu.Add("Rdr", new CheckBox("Draw R"));
                DrawMenu.Add("dmg", new CheckBox("Draw Damage Indicator"));
                DrawMenu.Add("color", new ColorPicker("Damage Indicator Color", System.Drawing.Color.FromArgb(255, 255, 236, 0)));
            }

            MiscMenu = Menu.AddSubMenu("MiscMenu");
            {
                MiscMenu.AddGroupLabel("KillSteal Settings");
                MiscMenu.Add("Qks", new CheckBox("Use Q to KillSteal"));
                MiscMenu.Add("Wks", new CheckBox("Use W to KillSteal"));
                MiscMenu.Add("Eks", new CheckBox("Use E to KillSteal"));
                MiscMenu.AddGroupLabel("GapCloser");
                MiscMenu.Add("gap", new CheckBox("Use W to anti GapCloser"));

            }
        }
    }
}
