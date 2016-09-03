using System;
using System.Linq;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Notifications;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBAzir
{
    class Config
    {
        public static Menu Menu;
        public static Menu ComboMenu;
        public static Menu Insec;
        public static Menu HarassMenu;
        public static Menu LaneClear;
        public static Menu JungleClear;
        public static Menu LasthitMenu;
        public static Menu Flee;
        public static Menu MiscMenu;
        public static Menu DrawMenu;

        public static void Dattenosa()
        {
            // Menu
            Menu = MainMenu.AddMenu("UB Azir", "UBAzir");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");
            Menu.Add("mastery", new ComboBox("What is keystones that you're using in mastery?", 0, "None", "Thunderlord's Decree", "Deathfire Touch"));

            //ComboMenu
            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.AddGroupLabel("Combo Settings");
                ComboMenu.Add("Qcb", new CheckBox("Use Q"));
                ComboMenu.Add("Qcbbonus", new Slider("Q will cast over {0} distance", 120, 0, 200));
                ComboMenu.AddLabel("Higher for more distance behind target, but it reduce Q hitchance");
                ComboMenu.AddLabel("Lower for more hitchance, but hardly for some a.a from soldier");
                ComboMenu.Add("Wcb", new CheckBox("Use W"));
                ComboMenu.Add("Wunitcb", new Slider("Max of Sand Soldier", 2, 1, 3));
                ComboMenu.Add("Ecb", new CheckBox("Use E Logic"));
                foreach (var e in EntityManager.Heroes.Enemies.OrderBy(e => e.ChampionName))
                {
                    ComboMenu.Add(e.ChampionName, new CheckBox("Don't E to" + e.ChampionName));
                }
                ComboMenu.Add("Edanger", new Slider("Don't E if around target has {0} enemy", 2, 1, 4));
                ComboMenu.Add("Rcb", new CheckBox("Use R"));
                ComboMenu.Add("Rhitcb", new Slider("Use R if hit", 3, 1, 5));
                var TeamFight = ComboMenu.Add("teamfight", new KeyBind("You are in teamfight?", false, KeyBind.BindTypes.PressToggle, '.'));
                TeamFight.OnValueChange += delegate
                {
                    var On = new SimpleNotification("Teamfight Mode status :", "Activated. ");
                    var Off = new SimpleNotification("Teamfight Mode status", "Disable. ");

                    Notifications.Show(TeamFight.CurrentValue ? On : Off, 2000);
                };
                ComboMenu.Add("ig", new CheckBox("Use Ignite"));

            }
            //InsecMenu
            Insec = Menu.AddSubMenu("Insec Settings");
            {
                Insec.AddGroupLabel("Hotkeys");
                Insec.Add("normalInsec", new KeyBind("Do Normal Insec", false, KeyBind.BindTypes.HoldActive, 'H'));
                Insec.Add("godInsec", new KeyBind("Do God Insec", false, KeyBind.BindTypes.HoldActive, 'G'));
                Insec.AddGroupLabel("Settings");
                Insec.AddLabel("Normal Insec");
                Insec.Add("normal.1", new ComboBox("I want to insec target to", 4, "My Cursor", "My Ally", "My Turret", "My Last Postion", "Smart"));
                Insec.Add("allowfl", new CheckBox("Allow using Flash in Insec"));
                Insec.Add("flvalue", new Slider("Only flash if can be insec {0} enemies", 4, 2, 5));
                Insec.Add("normalgoto", new ComboBox("If can't insec, I want move to", 1, "None", "Cursor", "To enemy"));
                Insec.AddSeparator();
                Insec.AddLabel("God Insec");
                Insec.Add("god.1", new ComboBox("I want to insec target to", 4, "My Cursor", "My Ally", "My Turret", "My Last Postion", "Smart"));
                Insec.Add("god.2", new ComboBox("I want to move to", 0, "My Cursor", "My Ally", "My Turret"));
                Insec.Add("godgoto", new ComboBox("If can't insec, I want to move to", 1, "None", "Cursor", "To enemy"));
            }
            //HarassMenu
            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.AddGroupLabel("Harass Settings");
                HarassMenu.Add("Qhr", new CheckBox("Use Q"));
                HarassMenu.Add("Qhrbonus", new Slider("Q will cast over {0} distance", 120, 0, 200));
                HarassMenu.AddLabel("Higher for more distance behind target, but it reduce Q hitchance");
                HarassMenu.AddLabel("Lower for more hitchance, but hardly for some a.a from soldier");
                HarassMenu.Add("Whr", new CheckBox("Use W"));
                HarassMenu.Add("Wunithr", new Slider("Max of Sand Soldier", 1, 1, 3));
                HarassMenu.Add("HrManage", new Slider("If mana percent below {0} stop harass", 50));
                HarassMenu.AddGroupLabel("Auto Harass Settings");
                var AutoHarass = HarassMenu.Add("autokey", new KeyBind("Auto Harass", true, KeyBind.BindTypes.PressToggle, 'Z'));
                AutoHarass.OnValueChange += delegate
                {
                    var On = new SimpleNotification("Auto Harass status :", "Activated. ");
                    var Off = new SimpleNotification("Auto Harass status :", "Disable. ");

                    Notifications.Show(AutoHarass.CurrentValue ? On : Off, 2000);
                };
                HarassMenu.Add("Qauto", new CheckBox("Use Q", false));
                HarassMenu.Add("Wauto", new ComboBox("Use W", 1, "Off", "Only Enemy in range", "When spell ready"));
                HarassMenu.Add("a.auto", new CheckBox("Auto Attack target if possible"));
                HarassMenu.Add("aa.auto", new CheckBox("Auto Attack other object to hit target"));
                HarassMenu.Add("automng", new Slider("Stop Auto harass when my mana below {0}%", 75));
            }

            //LaneClear Menu
            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.AddGroupLabel("Laneclear Settings");
                LaneClear.Add("Qlc", new CheckBox("Use Q to laneclear", false));
                LaneClear.Add("Wlc", new CheckBox("Use W to laneclear"));
                LaneClear.Add("Wunitlc", new Slider("Max of Sand Soldier", 1, 1, 3));
                LaneClear.Add("LcManager", new Slider("If mana percent below {0} stop use skill to laneclear", 50));
            }
            //JungleClear Menu
            JungleClear = Menu.AddSubMenu("JungleClear");
            {
                JungleClear.AddGroupLabel("Jungleclear Settings");
                JungleClear.Add("Qjc", new CheckBox("Use Q to jungleclear"));
                JungleClear.Add("Wjc", new CheckBox("Use W to jungleclear"));
                JungleClear.Add("Wunitjc", new Slider("Max of Sand Soldier", 2, 1, 3));
                JungleClear.Add("JcManager", new Slider("If mana percent below {0} stop use skill to jungleclear", 50));
            }

            //LasthitMenu
            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.Add("Qlh", new CheckBox("Use Q on lasthit", false));
                LasthitMenu.Add("Qautolh", new CheckBox("Auto Q on Unkillable Minion"));
                LasthitMenu.Add("Wlh", new CheckBox("Use W on lasthit", false));
                LasthitMenu.Add("Wunitlh", new Slider("Max of Sand Soldier", 1, 1, 3));
                LasthitMenu.Add("LhManager", new Slider("If mana percent below {0} stop use skill to lasthit", 50));
            }
            //Flee
            Flee = Menu.AddSubMenu("Flee");
            {
                Flee.Add("flee", new ComboBox("Combo Flee (To Mouse)", 2, "W-E", "W-Q-E", "W-E-Q"));
                Flee.Add("fleedelay", new Slider("Delay", 250, 0, 500));
                Flee.AddLabel("More for distance");
                Flee.AddLabel("Lower if you see it cast Q too late");
            }
            //DrawMenu
            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.Add("Qdr", new CheckBox("Draw Q"));
                DrawMenu.Add("Wcastdr", new CheckBox("Draw W Cast range"));
                //DrawMenu.Add("Waadr", new CheckBox("Draw Sand Soldier Attack Range"));
                //DrawMenu.Add("timer", new CheckBox("Draw time on Sand Soldier"));
                DrawMenu.Add("Edr", new CheckBox("Draw E"));
                DrawMenu.Add("Rdr", new CheckBox("Draw R"));
                DrawMenu.Add("drawdamage", new CheckBox("Draw Damage Indicator"));
                DrawMenu.Add("Color", new ColorPicker("Damage Indicator Color", Color.FromArgb(255, 255, 236, 0)));
            }

            //MiscMenu          
            MiscMenu = Menu.AddSubMenu("MiscMenu");
            {
                MiscMenu.AddGroupLabel("Misc Settings");
                MiscMenu.AddLabel("Killsteal Settings");
                MiscMenu.Add("Qks", new CheckBox("Use Q to KS"));
                MiscMenu.Add("Wks", new CheckBox("Use W to KS"));
                MiscMenu.Add("Eks", new CheckBox("Use E to KS", false));
                MiscMenu.Add("Rks", new CheckBox("Use R to KS", false));
                MiscMenu.AddSeparator();

                MiscMenu.AddLabel("Interrupt Settings");
                MiscMenu.Add("interrupter", new CheckBox("Enable Interrupter"));
                MiscMenu.Add("interrupt.value", new ComboBox("Interrupter DangerLevel", 0, "High", "Medium", "Low"));
                MiscMenu.AddSeparator();

                MiscMenu.AddLabel("Anti Gapcloser Settings");
                MiscMenu.Add("gap", new CheckBox("Enable Anti Gapcloser"));
                foreach (var a in Gapcloser.GapCloserList)
                {
                    foreach (var e in EntityManager.Heroes.Enemies.OrderBy(e => e.ChampionName))
                    {
                        if (a.ChampName == e.ChampionName)
                            MiscMenu.Add("gap" + a.ChampName, new CheckBox("R when " + a.ChampName + " using " + a.SpellSlot + " to gapclose me"));
                    }
                }
                MiscMenu.Add("gap.1", new ComboBox("Use R when", 1, "Gapclose spell is targeted", "Both Skillshot & Targeted"));
                /*MiscMenu.AddLabel("Activator Item");
                MiscMenu.Add("item.1", new CheckBox("Auto use Bilgewater Cutlass"));
                MiscMenu.Add("item.1MyHp", new Slider("My HP less than {0}%", 95));
                MiscMenu.Add("item.1EnemyHp", new Slider("Enemy HP less than {0}%", 70));
                MiscMenu.Add("item.sep", new Separator());

                MiscMenu.Add("item.2", new CheckBox("Auto use Blade of the Ruined King"));
                MiscMenu.Add("item.2MyHp", new Slider("My HP less than {0}%", 80));
                MiscMenu.Add("item.2EnemyHp", new Slider("Enemy HP less than {0}%", 70));
                MiscMenu.Add("sep7", new Separator());*/

                MiscMenu.AddLabel("Mod Skin");
                MiscMenu.Add("Modskin", new CheckBox("Enable mod skin", false));
                MiscMenu.Add("Modskinid", new Slider("Mod Skin", 2, 0, 2));
                MiscMenu.AddSeparator();

                MiscMenu.AddLabel("Level up auto upgrade spell");
                MiscMenu.Add("upgrade", new CheckBox("Auto Upgrade when level up"));
                MiscMenu.Add("upgradedelay", new Slider("Delay {0}0 milisec", 50, 0, 200));
                MiscMenu.Add("upgrademode", new ComboBox("Upgrade Mode", 0, "R-Q-W-E", "R-W-Q-E"));
            }
        }
    }
}
