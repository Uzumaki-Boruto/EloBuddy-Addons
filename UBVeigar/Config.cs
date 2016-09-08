using System.Drawing;
using EloBuddy.SDK;
using EloBuddy.SDK.Notifications;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBVeigar
{
    class Config
    {
        public static Menu Menu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClear;
        public static Menu JungleClear;
        public static Menu MiscMenu;
        public static Menu DrawMenu;

        public static void Dattenosa()
        {
            // Menu
            Menu = MainMenu.AddMenu("UB Veigar", "UBVeigar");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");


            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.Add("Qcb", new CheckBox("Use Q"));
                ComboMenu.Add("Wcb", new CheckBox("Use W"));
                ComboMenu.Add("W.cc", new CheckBox("W only on cc target"));
                ComboMenu.Add("Ecb", new CheckBox("Use E"));
                ComboMenu.Add("Ebox", new Slider("E if boxed {0} enemy", 3, 2, 5));
            }

            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.Add("Qhr", new CheckBox("Use Q"));
                HarassMenu.Add("Whr", new CheckBox("Use W"));
                HarassMenu.Add("Whr.cc", new CheckBox("W only on cc target"));
                HarassMenu.Add("Ehr", new CheckBox("Use E", false));
                HarassMenu.Add("hr", new Slider("If my MP below {0}% stop use spell to harass", 50));
                HarassMenu.Add("keyharass", new KeyBind("Auto Harass", false, KeyBind.BindTypes.PressToggle, 'Z'));
                HarassMenu.Add("autohrmng", new Slider("Stop auto harass if my MP  below {0}%", 80));
            }

            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.Add("Qlc", new CheckBox("Use Q"));
                LaneClear.Add("Wlc", new CheckBox("Use W", false));
                LaneClear.Add("lc", new Slider("If my MP below {0}% stop use spell to Lanclear", 30));
                LaneClear.Add("Whitlc", new Slider("Use W only hit {0} minion", 5, 1, 15));
                LaneClear.AddGroupLabel("Q Settings");
                LaneClear.Add("Q.attack", new CheckBox("Attack other"));
                LaneClear.Add("Q.wait", new CheckBox("Stop attacking minion"));
                LaneClear.Add("Q.waittime", new Slider("Attack wait", 2, 1, 5));
                LaneClear.Add("Q.unkill", new CheckBox("Auto Q on Unkillable minion"));
                LaneClear.Add("Q.farm", new CheckBox("Auto Q if got more than 2 AP"));
            }

            JungleClear = Menu.AddSubMenu("JungClear");
            {
                JungleClear.Add("Qjc", new CheckBox("Use Q"));
                JungleClear.Add("Qj.wait", new CheckBox("Wait if Jungmob getting low"));
                JungleClear.Add("Qjs", new CheckBox("Auto Q kill Jungle Mobs"));
                JungleClear.Add("Wjc", new CheckBox("Use W"));
                JungleClear.Add("jc", new Slider("If my MP below {0}% stop use spell to Jungclear", 50));
            }

            MiscMenu = Menu.AddSubMenu("Misc");
            {
                MiscMenu.AddGroupLabel("Killsteal");
                MiscMenu.Add("Qks", new CheckBox("Use Q to Killsteal"));
                MiscMenu.Add("Wks", new CheckBox("Use W to Killsteal"));
                MiscMenu.Add("Rks", new CheckBox("Use R to Killsteal"));
                foreach (var Enemy in EntityManager.Heroes.Enemies)
                {
                    MiscMenu.Add(Enemy.ChampionName, new CheckBox("R on " + Enemy.ChampionName));
                }
                MiscMenu.AddGroupLabel("Gapcloser");
                MiscMenu.Add("gapcloser", new CheckBox("Use E on Gapcloser"));
                MiscMenu.AddGroupLabel("Iterrupt");
                MiscMenu.Add("interrupt", new CheckBox("Iterupter Spell"));
                MiscMenu.Add("interrupt.level", new ComboBox("Danger Level", 2, "Low", "Normal", "High"));

            }

            DrawMenu = Menu.AddSubMenu("Drawing");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawing"));
                DrawMenu.Add("notif", new CheckBox("Enable Notifiaction"));
                DrawMenu.Add("drQ", new CheckBox("Draw Q"));
                DrawMenu.Add("drminion", new CheckBox("Draw killable minion with Q"));
                DrawMenu.Add("drW", new CheckBox("Draw W"));
                DrawMenu.Add("drE", new CheckBox("Draw E"));
                DrawMenu.Add("drEring", new CheckBox("Draw E Ring on Cursor", false));
                DrawMenu.Add("drR", new CheckBox("Draw R"));
                DrawMenu.Add("drdamage", new CheckBox("Damage Indicator"));
                DrawMenu.Add("Color", new ColorPicker("Damage Indicator Color", Color.FromArgb(255, 255, 236, 0)));
            }
        }
    }
}
