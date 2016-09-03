using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Notifications;

namespace UBNidalee
{
    class Config
    {
        public static Menu Menu;
        public static Menu QMenu;
        public static Menu WMenu;
        public static Menu EMenu;
        public static Menu RMenu;
        public static Menu ItemMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;

        public static void Dattenosa()
        {
            // Menu
            Menu = MainMenu.AddMenu("UB Nidalee", "UBNidalee");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");

            //Q
            QMenu = Menu.AddSubMenu("Q Settings");
            {
                QMenu.AddGroupLabel("Combo Settings");
                QMenu.AddLabel("Human Q");
                QMenu.Add("Qcb", new CheckBox("Use Q"));
                QMenu.Add("hitQcb", new Slider("Q hit chance", 75));
                //QMenu.Add("Qrange", new Slider("Lower for more accuracy, Higher for range (66 recommended)", 66)); 
                QMenu.Add("autoQ", new CheckBox("Auto Q if target in immobilize"));
                QMenu.AddLabel("Cougar Q");
                QMenu.Add("Q2cb", new CheckBox("Use Q"));
                QMenu.Add("Q2aa", new CheckBox("Use Q to reset a.a"));
                QMenu.AddGroupLabel("Harass Settings");
                QMenu.Add("Qhr", new CheckBox("Use Q (human)"));
                QMenu.Add("Qmodehr", new ComboBox("Mode for Q", 0, "Harass Mode", "Auto"));
                QMenu.Add("hitQhr", new Slider("Q hit chance", 75));
                QMenu.Add("Qmnghr", new Slider("If below {0}% mana, stop use Q", 50));
                QMenu.AddGroupLabel("JungleClear Settings");
                QMenu.Add("Qjc", new CheckBox("Use Q (human)"));
                QMenu.Add("Qmngjc", new Slider("If below {0}% mana, stop use Q", 20));
                QMenu.Add("Q2jc", new CheckBox("Use Q (Cougar, reset a.a)"));
                QMenu.AddGroupLabel("Flee");
                QMenu.Add("Qfl", new CheckBox("Use Q (human) on flee"));
                QMenu.AddGroupLabel("Killsteal Settings");
                QMenu.Add("Qks", new CheckBox("Enable"));
                QMenu.AddGroupLabel("Jungsteal Settings");
                QMenu.Add("Qjs", new CheckBox("Use Q Human to jungsteal"));
            }
            //W
            WMenu = Menu.AddSubMenu("W Settings");
            {
                WMenu.AddGroupLabel("Combo Settings");
                WMenu.AddLabel("Human W");
                WMenu.Add("Wcb", new CheckBox("Use W"));
                WMenu.AddLabel("Cougar W");
                WMenu.Add("W2cb", new ComboBox("Use W", 3, "Off", "Use W always", "Only Target has passive", "Smart"));
                WMenu.AddGroupLabel("Lane & Jung Clear Settings");
                WMenu.AddLabel("Human W");
                WMenu.Add("Wjc", new CheckBox("Use W in JungleClear"));
                WMenu.Add("Wmng", new Slider("If below {0}% mana, stop use W", 20));
                WMenu.AddLabel("Cougar W");
                WMenu.Add("W2lc", new CheckBox("Use W in LaneClear"));
                WMenu.Add("W2jc", new CheckBox("Use W in JungleClear"));
                WMenu.AddGroupLabel("Flee & Sight");
                WMenu.Add("Ws", new KeyBind("Use W (Human) to get sight", false, KeyBind.BindTypes.HoldActive, 'W'));
                WMenu.Add("Wfl", new CheckBox("Use W (Human) in Flee"));
                WMenu.Add("W2fl", new CheckBox("Use W (Cougar) in Flee"));
                WMenu.AddGroupLabel("Killsteal");
                WMenu.Add("W2ks", new CheckBox("Use W (Cougar) to KS"));

            }
            //E
            EMenu = Menu.AddSubMenu("E Settings");
            {
                EMenu.AddGroupLabel("E Human Setting");
                EMenu.AddLabel("Main Settings");
                EMenu.Add("E", new CheckBox("Auto use E"));
                EMenu.Add("Emode", new ComboBox("Mode for E", 0, "LeastHP", "CarryHP", "AP HP", "SelfHP"));
                EMenu.Add("Emng", new Slider("Stop use E if your mana below {0}%", 50));
                EMenu.Add("Emng2", new Slider("Don't use E if target's HP more than {0}%", 70));
                EMenu.AddLabel("White list to E");
                foreach (var a in EntityManager.Heroes.Allies.OrderBy(a => a.ChampionName))
                {
                    EMenu.Add(a.ChampionName, new CheckBox("Heal " + a.ChampionName));
                }
                EMenu.AddGroupLabel("Cougar E");
                EMenu.Add("E2cb", new CheckBox("Use E in Combo"));
                EMenu.AddGroupLabel("Lane & Jung Clear Settings");
                EMenu.AddLabel("Cougar E");
                EMenu.Add("E2lc", new CheckBox("Use E in LaneClear"));
                EMenu.Add("E2jc", new CheckBox("Use E in JungleClear"));
                EMenu.AddGroupLabel("Killsteal");
                EMenu.Add("E2ks", new CheckBox("Use E (Cougar) to KS"));
            }

            //R
            RMenu = Menu.AddSubMenu("R Settings");
            {
                RMenu.AddGroupLabel("R Setting");
                RMenu.Add("R", new CheckBox("Use R to reset a.a (logic)"));
                RMenu.Add("Rform", new ComboBox("I want change form when: ", 0 ,"My human spell is cooldown", "Has a passive target around me", "I want change form myself"));
                RMenu.Add("Rformjc", new CheckBox("Logic R in JungleClear"));
            }

            //ItemMenu
            ItemMenu = Menu.AddSubMenu("Item Menu");
            {
                ItemMenu.Add("item1", new CheckBox("Auto use Zhonya's Hourglass"));
                ItemMenu.Add("item.1MyHp", new Slider("My HP less than {0}%", 50));
                ItemMenu.AddSeparator();

                ItemMenu.Add("item2", new CheckBox("Auto use Seraph's Embrace"));
                ItemMenu.Add("item2MyHp", new Slider("My HP less than {0}%", 75));
                ItemMenu.AddSeparator();

                ItemMenu.Add("item3", new CheckBox("Auto use Bilgewater Cutlass"));
                ItemMenu.Add("item4", new CheckBox("Auto use Hextech Gunblade"));
                ItemMenu.Add("item34EnemyHP", new Slider("Enemy HP less than {0}%", 75));
                ItemMenu.AddSeparator();

                ItemMenu.Add("item5", new CheckBox("Auto use Randuin's Omen"));
                ItemMenu.Add("item5Enemy", new Slider("Enemy around to use", 3, 1, 5));
                ItemMenu.AddSeparator();

                ItemMenu.Add("item6", new CheckBox("Auto use Righteous Glory (Logic)"));
                ItemMenu.Add("item6Ally", new Slider("Use it if buff {0} ally", 3, 1, 4));
                ItemMenu.AddSeparator();

                ItemMenu.Add("item7", new CheckBox("Auto use Locket of the Iron Solari"));
                ItemMenu.Add("item7Ally", new Slider("Use it if buff {0} ally", 4, 1, 4));
            }
            //MiscMenu          
            MiscMenu = Menu.AddSubMenu("MiscMenu");
            {
                MiscMenu.AddGroupLabel("Misc Settings");
                MiscMenu.AddLabel("Ignite");
                MiscMenu.Add("ig", new CheckBox("Use Ignite"));
                MiscMenu.AddLabel("Smite");
                MiscMenu.Add("smcb", new CheckBox("Use Smite in Combo"));
                MiscMenu.Add("sm", new CheckBox("Use Smite to killsteal"));
                MiscMenu.Add("smjc", new CheckBox("Smite on Important buff"));
                MiscMenu.AddLabel("Auto Jump System");
                var JumpKey = MiscMenu.Add("jump", new KeyBind("Enable Auto jump system", true, KeyBind.BindTypes.PressToggle, 'G'));
                JumpKey.OnValueChange += delegate
                {
                    var On = new SimpleNotification("Auto Jump System :", "Activated. ");
                    var Off = new SimpleNotification("Auto Jump System:", "Disable. ");

                    Notifications.Show(JumpKey.CurrentValue ? On : Off, 2000);
                };
            }

            //DrawMenu
            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.AddGroupLabel("Drawings settings");
                DrawMenu.Add("drawform", new CheckBox("Draw ability range other form", false));
                DrawMenu.Add("drawaa", new CheckBox("Draw other form A.A range", false));
                DrawMenu.Add("drawjump", new CheckBox("Draw Jump spot"));
                DrawMenu.Add("formdraw", new CheckBox("Draw ability cooldown other form"));
                DrawMenu.Add("Xbonus", new Slider("X", 500, 0, 1000));
                DrawMenu.Add("Ybonus", new Slider("Y", 500, 0, 1000));
                DrawMenu.Add("drawdamage", new CheckBox("Damage Indicator"));
                DrawMenu.AddGroupLabel("Human Form");
                DrawMenu.Add("drawQ", new CheckBox("Draw Q"));
                DrawMenu.Add("drawW", new CheckBox("Draw W"));
                DrawMenu.Add("drawE", new CheckBox("Draw E"));
                DrawMenu.AddGroupLabel("Cougar Form");
                DrawMenu.Add("drawW2", new CheckBox("Draw W"));
                DrawMenu.Add("drawW2p", new CheckBox("Draw W (passive)", false));
                DrawMenu.Add("drawE2", new CheckBox("Draw E"));

            }
        }
    }
}