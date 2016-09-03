using System.Drawing;
using EloBuddy.SDK;
using EloBuddy.SDK.Notifications;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBRyze
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
            // Menu
            Menu = MainMenu.AddMenu("UB Ryze", "UBRyze");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");


            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.Add("useQcb", new CheckBox("Use Q"));
                ComboMenu.Add("useWcb", new CheckBox("Use W"));
                ComboMenu.Add("useEcb", new CheckBox("Use E"));
                ComboMenu.Add("useRcb", new CheckBox("Use R", false));
                ComboMenu.AddSeparator();
                ComboMenu.Add("combostyle", new ComboBox("Combo Style", 2, "Full Damage", "Shield & Movement speed", "Smart"));
                ComboMenu.Add("hpcbsmart", new Slider("If my HP below {0}% use Q to get Ms & shield (Smart)", 30));
                ComboMenu.Add("disatt", new CheckBox("Disable auto attack", false));
                ComboMenu.Add("logicdisatt", new CheckBox("Logic Disable A.A"));
                ComboMenu.AddSeparator();
                ComboMenu.Add("useflee", new CheckBox("Allow use Flee Combo"));
                ComboMenu.AddLabel("Flee combo is always use Q to get Shield & MS");
                ComboMenu.AddLabel("Active this by press your flee key");

            }

            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.Add("useQhr", new CheckBox("Use Q"));
                HarassMenu.Add("useWhr", new CheckBox("Use W"));
                HarassMenu.Add("useEhr", new CheckBox("Use E"));
                //HarassMenu.Add("useQEhr", new CheckBox("Use E on minion that colision in Q Width"));
                HarassMenu.Add("hrmanage", new Slider("If my MP below {0}% stop use spell to harass", 50));
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
                LaneClear.Add("useQlc", new CheckBox("Use Q", false));
                LaneClear.Add("useWlc", new CheckBox("Use W", false));
                LaneClear.Add("useElc", new CheckBox("Use E", false));
                LaneClear.Add("lcmanage", new Slider("If my MP below {0}% stop use spell to Lanclear", 50));
                LaneClear.Add("logiclc", new CheckBox("Enable Smart Lanclear[BETA]", false));
                LaneClear.AddGroupLabel("Smart Laneclear Settings");
                LaneClear.Add("Qlc", new Slider("Use Q only {0} minion has buff of E", 5, 1, 15));
                LaneClear.Add("Elc", new CheckBox("Enable E Logic cast"));
            }

            JungleClear = Menu.AddSubMenu("JungClear");
            {
                JungleClear.Add("useQjc", new CheckBox("Use Q"));
                JungleClear.Add("onlyQjc", new CheckBox("Only Q if Target has E buff"));
                JungleClear.Add("useWjc", new CheckBox("Use W"));
                JungleClear.Add("useEjc", new CheckBox("Use E"));
                JungleClear.Add("jcmanage", new Slider("If my MP below {0}% stop use spell to Jungclear", 50));
            }

            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.AddLabel("This only work if you can kill minion wwith spell");
                LasthitMenu.Add("useQlh", new CheckBox("Use Q"));
                LasthitMenu.Add("useWlh", new CheckBox("Use W"));
                LasthitMenu.Add("useElh", new CheckBox("Use E"));
                LasthitMenu.Add("lhmanage", new Slider("If my MP below {0}% stop use spell to Lasthit", 50));
                LasthitMenu.AddGroupLabel("Use spell on Unkillable minnion");
                LasthitMenu.Add("Qlh", new CheckBox("Use Q"));
                LasthitMenu.Add("Wlh", new CheckBox("Use W"));
                LasthitMenu.Add("Elh", new CheckBox("Use E"));
                LasthitMenu.Add("unkillmanage", new Slider("Stop this if my MP below {0}%", 15));
            }

            AutoMenu = Menu.AddSubMenu("AutoMenu");
            {
                AutoBox = AutoMenu.Add("autofl", new ComboBox("Auto W when flash", 1, "None", "W", "E + W"));
                AutoBox.OnValueChange += AutoBox_OnValueChange;
                //AutoMenu.AddSeparator();
                //AutoMenu.Add("Rzh", new Slider("Auto use Zhonya & R to your nearesr Nexus if around you >= {0}", 5, 1, 6));
                //AutoMenu.Add("Rzhe", new Slider("Get enemy around you {0}0 distance", 50, 0, 150));
                //AutoMenu.Add("Rzha", new Slider("Get ally around you {0}0 distance", 100, 0, 150));
                //AutoMenu.AddLabel("This mean if x enemy around you and no ally around, you will R into nearest Turret");
            }

            MiscMenu = Menu.AddSubMenu("Misc");
            {
                MiscMenu.AddGroupLabel("Killsteal");
                MiscMenu.Add("Qks", new CheckBox("Use Q to Killsteal"));
                MiscMenu.Add("Wks", new CheckBox("Use W to Killsteal"));
                MiscMenu.Add("Eks", new CheckBox("Use E to Killsteal"));
                MiscMenu.AddGroupLabel("Damage calculator");
                MiscMenu.Add("dmg", new ComboBox("How to damage cal?", 0, "Basic Combo(QWE)", "Highest Damage you can take"));
                MiscMenu.Add("gapcloser", new CheckBox("Use W on Gapcloser"));

            }

            DrawMenu = Menu.AddSubMenu("Drawing");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawing"));
                DrawMenu.Add("drQ", new CheckBox("Draw Q"));
                DrawMenu.Add("drW", new CheckBox("Draw W + E"));
                DrawMenu.Add("drR", new CheckBox("Draw R"));
                DrawMenu.Add("drdamage", new CheckBox("Damage Indicator"));
                DrawMenu.Add("Color", new ColorPicker("Damage Indicator Color", Color.FromArgb(255, 255, 236, 0)));
            }
        }

        static void AutoBox_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            switch (args.NewValue)
            {
                case 0:
                    {
                        AutoBox.DisplayName = "You turn off flash auto";
                    }
                    break;
                case 1:
                    {
                        AutoBox.DisplayName = "Auto use W after flash";
                    }
                    break;
                case 2:
                    {
                        AutoBox.DisplayName = "Auto use E + W after flash";
                    }
                    break;
            }
        }

    }
}
