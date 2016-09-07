using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBZilean
{
    class Config
    {
        public static Menu Menu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClear;
        public static Menu JungleClear;
        public static Menu LasthitMenu;
        public static Menu GapCloser;
        public static Menu MiscMenu;
        public static Menu DrawMenu;

        public static void Dattenosa()
        {
            Menu = MainMenu.AddMenu("UB Zilean", "UBZilean");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");

            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.AddGroupLabel("Combo Settings");
                ComboMenu.Add("Q", new ComboBox("Q style", 2, "Don't use Q", "Q Normal", "Q Smart"));
                ComboMenu.Add("W", new CheckBox("Use W"));
                ComboMenu.Add("Wcast", new Slider("Don't W if Q will Ready in {0} seconds", 1, 0, 4));
                ComboMenu.Add("E", new ComboBox("E style", 3, "Don't use E", "E Slow", "E Speed", "Smart"));
                ComboMenu.AddGroupLabel("R Settings");
                ComboMenu.Add("R", new CheckBox("Use R"));
                foreach (var champ in EntityManager.Heroes.Allies)
                {
                    ComboMenu.Add("R" + champ.ChampionName, new CheckBox("Use on " + champ.ChampionName));
                    ComboMenu.Add("HP" + champ.ChampionName, new Slider("Use R if " + champ.ChampionName + "'s HP below {0}%", 20, 0, 100));
                    ComboMenu.AddSeparator();
                }
            }

            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.AddGroupLabel("Harass Settings");
                HarassMenu.Add("Q", new ComboBox("Q style", 1, "Don't use Q", "Q Normal", "Q Smart"));
                HarassMenu.Add("W", new CheckBox("Use W"));
                HarassMenu.Add("Wcast", new Slider("Don't W if Q will Ready in {0} seconds", 1, 0, 4));
                HarassMenu.Add("hr", new Slider("If mana percent below {0}% stop harass", 50));
                HarassMenu.Add("keyharass", new KeyBind("Auto Harass", false, KeyBind.BindTypes.HoldActive, 'Z'));
                HarassMenu.Add("autohrmng", new Slider("If mana percent below {0}% stop auto harass", 85));
            }

            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.AddGroupLabel("Laneclear Settings");
                LaneClear.Add("Q", new CheckBox("Use Q to laneclear", false));
                LaneClear.Add("Qhit", new Slider("Only Use Q if hit {0} minion(s)", 6, 1, 15));
                LaneClear.Add("W", new CheckBox("Use W to laneclear", false));
                LaneClear.Add("lc", new Slider("If mana percent below {0}% stop use skill to laneclear", 50));
            }

            JungleClear = Menu.AddSubMenu("JungleClear");
            {
                JungleClear.AddGroupLabel("Jungleclear Settings");
                JungleClear.Add("Q", new CheckBox("Use Q to jungleclear"));
                JungleClear.Add("W", new CheckBox("Use W to jungleclear"));
                JungleClear.Add("jc", new Slider("If mana percent below {0}% stop use skill to jungleclear", 50));
            }

            GapCloser = Menu.AddSubMenu("GapCloser");
            {
                GapCloser.AddGroupLabel("Anti GapCloser");
                GapCloser.Add("anti", new CheckBox("Use Q & W to Anti GapCloser"));
                GapCloser.Add("E", new ComboBox("E style", 3, "Don't use E", "E Slow", "E Speed", "Smart"));
                foreach (var champ in EntityManager.Heroes.Allies)
                {
                    GapCloser.Add("anti" + champ.ChampionName, new CheckBox("Active if target is " + champ.ChampionName));
                }
                GapCloser.AddSeparator();
                GapCloser.AddGroupLabel("GapCloser");
                GapCloser.AddLabel("Make sure your Q's combo style is smart");
                GapCloser.Add("gap", new CheckBox("Use Q on Allies's GapCloser"));
                GapCloser.Add("gapE", new CheckBox("Use E on Ally"));
                foreach (var gap in Gapcloser.GapCloserList)
                {
                    foreach (var ally in EntityManager.Heroes.Allies)
                    {
                        if (gap.ChampName == ally.ChampionName)
                            GapCloser.Add("gap" + gap.ChampName + gap.SpellSlot, new CheckBox("Q on " + gap.ChampName + " using " + gap.SpellSlot + " to gapclose enemy"));
                    }
                }
            }

            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawing"));
                DrawMenu.Add("notif", new CheckBox("Enable notification"));
                DrawMenu.AddSeparator();
                DrawMenu.Add("Qdr", new CheckBox("Draw Q + R"));
                DrawMenu.Add("Edr", new CheckBox("Draw E"));
                DrawMenu.Add("dmg", new CheckBox("Draw Damage Indicator"));
                DrawMenu.Add("color", new ColorPicker("Damage Indicator Color", System.Drawing.Color.FromArgb(255, 255, 236, 0)));
            }

            MiscMenu = Menu.AddSubMenu("MiscMenu");
            {
                MiscMenu.AddGroupLabel("KillSteal Settings");
                MiscMenu.Add("Qks", new CheckBox("Use Q to KillSteal", true));
                MiscMenu.Add("Wks", new CheckBox("Use W to KillSteal", false));
                MiscMenu.AddGroupLabel("Interrupter Settings");
                MiscMenu.Add("inter", new CheckBox("Interrupter"));
                MiscMenu.Add("value", new ComboBox("Danger Value", 0, "High", "Medium", "Low"));              
            }
        }
    }
}
