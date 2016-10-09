using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Drawing;

namespace UBBard
{
    class Config
    {
        public static Menu Menu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu Clear;
        public static Menu AutoHeal;
        public static Menu LasthitMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;

        public static void Dattenosa()
        {
            Menu = MainMenu.AddMenu("UB Bard", Extension.AddonName);
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");

            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.AddGroupLabel("Combo Settings");
                ComboMenu.Add("Q", new CheckBox("Use Q"));
                ComboMenu.Add("Qstun", new CheckBox("Only Q Stun"));
                ComboMenu.Add("R", new CheckBox("Use R"));
                ComboMenu.Add("Rhit", new Slider("Min {0} hit to cast R", 3, 1, 6));
                ComboMenu.Add("Rhitchance", new Slider("Hitchance", 75));
            }

            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.AddGroupLabel("Harass Settings");
                HarassMenu.Add("Q", new CheckBox("Use Q"));
                HarassMenu.Add("Qstun", new CheckBox("Only Q Stun"));
                HarassMenu.Add("hr", new Slider("If mana percent below {0}% stop harass", 50));
                HarassMenu.Add("keyharass", new KeyBind("Auto Harass", false, KeyBind.BindTypes.HoldActive));
                HarassMenu.Add("autohrmng", new Slider("If mana percent below {0}% stop auto harass", 85));
            }

            Clear = Menu.AddSubMenu("Clear");
            {
                Clear.AddGroupLabel("Laneclear Settings");
                Clear.Add("Q", new CheckBox("Use Q to laneclear", false));
                Clear.Add("lc", new Slider("If mana percent below {0}% stop use skill to laneclear", 50));
                Clear.AddGroupLabel("Jungleclear Settings");
                Clear.Add("Qjc", new CheckBox("Use Q to jungleclear"));
                Clear.Add("jc", new Slider("If mana percent below {0}% stop use skill to jungleclear", 50));

            }

            AutoHeal = Menu.AddSubMenu("Auto Heal");
            {
                AutoHeal.AddGroupLabel("W Setting");
                AutoHeal.Add("W", new CheckBox("Enable W"));
                AutoHeal.Add("Worder", new ComboBox("Order by", 2, "Most AP", "Most AD", "Least HP"/*, "Seclect Champion"*/));
                AutoHeal.Add("HP", new Slider("Health Percent", 25));
                foreach (var ally in EntityManager.Heroes.Allies)
                {
                    AutoHeal.Add(ally.ChampionName, new CheckBox("W on " + ally.ChampionName));
                }
            }

            MiscMenu = Menu.AddSubMenu("MiscMenu");
            {
                MiscMenu.AddGroupLabel("KillSteal Settings");
                MiscMenu.Add("Qks", new CheckBox("Use Q to KillSteal", false));
                MiscMenu.AddGroupLabel("Auto R Crossing Turret");
                MiscMenu.Add("R", new CheckBox("Auto R when your team crossing turret"));
                MiscMenu.AddGroupLabel("GapCloser");
                MiscMenu.Add("Qgap", new CheckBox("Use Q anti GapCloser <stun>"));
                MiscMenu.Add("Wgap", new CheckBox("Use W to anti GapCloser"));
                MiscMenu.AddGroupLabel("Interrupter");
                MiscMenu.Add("interrupt", new CheckBox("Use Q to Interrupt"));
                MiscMenu.Add("interrupt.level", new ComboBox("Danger Level", 2, "Low", "Normal", "High"));

            }

            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawing"));
                DrawMenu.Add("notif", new CheckBox("Enable notification"));
                DrawMenu.AddSeparator();
                DrawMenu.Add("Qdr", new CheckBox("Draw Q"));
                DrawMenu.Add("Wdr", new CheckBox("Draw W"));
                DrawMenu.Add("Rdr", new CheckBox("Draw R"));
                DrawMenu.Add("dmg", new CheckBox("Draw Damage Indicator"));
                var ColorPick = DrawMenu.Add("color", new ColorPicker("Damage Indicator Color", SaveColor.Load()));
                ColorPick.OnLeftMouseUp += delegate(Control sender, System.EventArgs args)
                {
                    SaveColor.Save(ColorPick.CurrentValue);
                };
            }
        }
    }
}
