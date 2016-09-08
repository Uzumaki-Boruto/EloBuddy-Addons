using System.Drawing;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBSyndra
{
    class Config
    {
        public static Menu Menu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClear;
        public static Menu JungleClear;
        public static Menu Lasthit; 
        public static Menu MiscMenu;
        public static Menu DrawMenu;

        public static void Dattenosa()
        {
            // Menu
            Menu = MainMenu.AddMenu("UB Syndra", "UBSyndra");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");


            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.Add("Qcb", new CheckBox("Use Q"));
                ComboMenu.Add("Wcb", new CheckBox("Use W"));
                ComboMenu.Add("Ecb", new CheckBox("Use E", false));
                ComboMenu.Add("Ecbhit", new Slider("Auto E if can stun {0} enemies", 4, 2, 6));
                ComboMenu.Add("QEcb", new CheckBox("Use QE"));
                ComboMenu.Add("R", new CheckBox("Use R"));
                ComboMenu.AddLabel("It's not killsteal settings");
                foreach (var Enemy in EntityManager.Heroes.Enemies)
                {
                    ComboMenu.Add(Enemy.ChampionName, new CheckBox("R on " + Enemy.ChampionName));
                }
            }

            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.Add("Qhr", new CheckBox("Use Q"));
                HarassMenu.Add("Whr", new CheckBox("Use W"));
                HarassMenu.Add("Ehr", new CheckBox("Use E", false));
                HarassMenu.Add("QEhr", new CheckBox("Use QE", false));
                HarassMenu.Add("hr", new Slider("If my MP below {0}% stop use spell to harass", 50));
                HarassMenu.Add("keyharass", new KeyBind("Auto Harass", false, KeyBind.BindTypes.PressToggle, 'Z'));
                HarassMenu.Add("autohrmng", new Slider("Stop auto harass if my MP below {0}%", 80));
            }

            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.Add("Qlc", new CheckBox("Use Q", false));
                LaneClear.Add("Qlchit", new Slider("Q only hit {0}", 3, 1, 10));
                LaneClear.Add("Wlc", new CheckBox("Use W", false));
                LaneClear.Add("Wlchit", new Slider("W only hit {0}", 3, 1, 10));
                LaneClear.Add("lc", new Slider("If my MP below {0}% stop use spell to Lanclear", 50));
            }

            JungleClear = Menu.AddSubMenu("JungClear");
            {
                JungleClear.Add("Qjc", new CheckBox("Use Q"));
                JungleClear.Add("Wjc", new CheckBox("Use W"));
                JungleClear.Add("jc", new Slider("If my MP below {0}% stop use spell to Jungclear", 50));
            }

            Lasthit = Menu.AddSubMenu("Unkillable");
            {
                Lasthit.Add("Qlh", new CheckBox("Use Q"));
                Lasthit.Add("Wlh", new CheckBox("Use W"));
                Lasthit.Add("lh", new Slider("If my MP below {0}% stop", 20));
            }

            MiscMenu = Menu.AddSubMenu("Misc");
            {
                MiscMenu.AddGroupLabel("Killsteal");
                MiscMenu.Add("Qks", new CheckBox("Use Q to Killsteal"));
                MiscMenu.Add("Wks", new CheckBox("Use W to Killsteal"));
                MiscMenu.Add("Eks", new CheckBox("Use E to Killsteal"));
                MiscMenu.Add("Rks", new CheckBox("Use R to Killsteal"));
                foreach (var Enemy in EntityManager.Heroes.Enemies)
                {
                    MiscMenu.Add(Enemy.ChampionName, new CheckBox("R on " + Enemy.ChampionName));
                }
                MiscMenu.AddGroupLabel("Gapcloser");
                MiscMenu.Add("gapcloser", new CheckBox("Use E on Gapcloser"));
                MiscMenu.AddGroupLabel("Interrupt");
                MiscMenu.Add("interrupt", new CheckBox("Interrupt Spell"));
                MiscMenu.Add("interrupt.level", new ComboBox("Interrupt Level", 2, "Low", "Normal", "High"));

            }

            DrawMenu = Menu.AddSubMenu("Drawing");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawing"));
                DrawMenu.Add("drQ", new CheckBox("Draw Q"));
                DrawMenu.Add("drW", new CheckBox("Draw W"));
                DrawMenu.Add("drE", new CheckBox("Draw E"));
                DrawMenu.Add("drEQ", new CheckBox("Draw QE"));
                DrawMenu.Add("drBall", new CheckBox("Draw Ball"));
                DrawMenu.Add("drR", new CheckBox("Draw R"));
                DrawMenu.Add("drdamage", new CheckBox("Damage Indicator"));
                DrawMenu.Add("Color", new ColorPicker("Damage Indicator Color", Color.FromArgb(255, 255, 236, 0)));
            }
        }
    }
}
