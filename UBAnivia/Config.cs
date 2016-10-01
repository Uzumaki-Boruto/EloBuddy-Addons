using System.Drawing;
using EloBuddy.SDK.Notifications;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBAnivia
{
    class Config
    {
        public static Menu Menu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClear;
        public static Menu JungleClear;
        public static Menu LasthitMenu;
        public static Menu AutoMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;
        public static ComboBox AutoBox;

        public static void Dattenosa()
        {
            Menu = MainMenu.AddMenu("UB Anivia", "UBAnivia");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");


            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.Add("Q", new CheckBox("Use Q"));
                ComboMenu.Add("Qcb", new Slider("Min {0}% hit Q", 80));
                ComboMenu.Add("W", new CheckBox("Use W"));
                ComboMenu.Add("E", new CheckBox("Use E"));
                ComboMenu.Add("R", new CheckBox("Use R"));
                ComboMenu.AddSeparator();
                //ComboMenu.Add("Qcb", new CheckBox("Only Q on CC target"));
                //ComboMenu.AddLabel("Include slow");
                ComboMenu.Add("chill", new CheckBox("Use E only target is Chills"));
            }

            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.Add("Q", new CheckBox("Use Q"));
                HarassMenu.Add("Qhr", new Slider("Min {0}% hit Q", 80));
                HarassMenu.Add("E", new CheckBox("Use E"));
                HarassMenu.Add("R", new CheckBox("Use R"));
                HarassMenu.Add("hr", new Slider("If my MP below {0}% stop use spell to harass", 50));
                var HarassKey = HarassMenu.Add("keyharass", new KeyBind("Auto Harass", false, KeyBind.BindTypes.PressToggle, 'Z'));
                HarassKey.OnValueChange += delegate
                {
                    var On = new SimpleNotification("Auto Harass status:", "Activated. ");
                    var Off = new SimpleNotification("Auto Harass status:", "Disable. ");

                    Notifications.Show(HarassKey.CurrentValue ? On : Off, 2000);
                };
                HarassMenu.Add("autohrmng", new Slider("Stop auto harass if my MP  below {0}%", 80));
            }

            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.Add("Q", new CheckBox("Use Q", false));
                LaneClear.Add("E", new CheckBox("Use E", false));
                LaneClear.Add("R", new CheckBox("Use R"));
                LaneClear.Add("Rlc", new Slider("Use R only hit {0} minion", 5, 1, 15));
                LaneClear.Add("lc", new Slider("If my MP below {0}% stop use spell to Lanclear", 50));
            }

            JungleClear = Menu.AddSubMenu("JungClear");
            {
                JungleClear.Add("Q", new CheckBox("Use Q"));
                JungleClear.Add("E", new CheckBox("Use E"));
                JungleClear.Add("R", new CheckBox("Use R"));
                JungleClear.Add("jc", new Slider("If my MP below {0}% stop use spell to Jungclear", 50));
            }

            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.AddGroupLabel("Use spell on Unkillable minnion");
                LasthitMenu.Add("Qlh", new CheckBox("Use Q"));
                LasthitMenu.Add("Elh", new CheckBox("Use E"));
                LasthitMenu.Add("Rlh", new CheckBox("Use R"));
                LasthitMenu.Add("manage", new Slider("Stop this if my MP below {0}%", 15));
            }

            MiscMenu = Menu.AddSubMenu("Misc");
            {
                MiscMenu.AddGroupLabel("Killsteal");
                MiscMenu.Add("Qks", new CheckBox("Use Q to Killsteal"));
                MiscMenu.Add("Eks", new CheckBox("Use E to Killsteal"));
                MiscMenu.Add("Rks", new CheckBox("Use R to Killsteal"));
                MiscMenu.AddGroupLabel("Damage calculator");
                MiscMenu.Add("dmg", new Slider("R Tick Damage", 2, 1, 8));
                MiscMenu.AddGroupLabel("Antigapcloser");
                MiscMenu.Add("Qgap", new CheckBox("Use Q on Gapcloser"));
                MiscMenu.Add("Wgap", new CheckBox("Use W on Gapcloser"));
                MiscMenu.AddGroupLabel("Interrupt");
                MiscMenu.Add("Qinter", new CheckBox("Use Q on interrupt"));
                MiscMenu.Add("Winter", new CheckBox("Use W on interrupt"));
                MiscMenu.Add("level", new ComboBox("Interrupt Level", 0, "High", "Medium", "Low"));
                MiscMenu.AddGroupLabel("Auto on CC");
                MiscMenu.Add("Qcc", new CheckBox("Auto Q on CC"));
                MiscMenu.Add("Rcc", new CheckBox("Auto R on CC"));
                MiscMenu.AddGroupLabel("Auto Spells");
                MiscMenu.Add("Eauto", new CheckBox("Auto E on chilled target"));
                MiscMenu.Add("Rauto", new CheckBox("Auto R when cast E"));
                MiscMenu.AddGroupLabel("Auto turn off R");
                MiscMenu.Add("turnoffR", new CheckBox("Auto Turn off R"));
                MiscMenu.AddGroupLabel("Safe with W");
                MiscMenu.Add("safeW", new CheckBox("Auto W to keep me safe"));

            }

            DrawMenu = Menu.AddSubMenu("Drawing");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawing"));
                DrawMenu.Add("notif", new CheckBox("Enable notification"));
                DrawMenu.AddSeparator();
                DrawMenu.Add("drQ", new CheckBox("Draw Q"));
                DrawMenu.Add("drW", new CheckBox("Draw W"));
                DrawMenu.Add("drE", new CheckBox("Draw E"));
                DrawMenu.Add("drR", new CheckBox("Draw R"));
                DrawMenu.Add("dmg", new CheckBox("Damage Indicator"));
                DrawMenu.Add("Color", new ColorPicker("Damage Indicator Color", Color.FromArgb(255, 255, 236, 0)));
            }
        }   
    }
}
