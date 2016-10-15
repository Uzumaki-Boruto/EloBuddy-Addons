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

            //ComboMenu
            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.AddGroupLabel("Combo Settings");
                ComboMenu.Add("Q", new CheckBox("Use Q"));
                ComboMenu.Add("Qbonus", new Slider("Q will cast over {0} distance", 120, 0, 200));
                ComboMenu.AddLabel("Higher for more distance behind target, but it reduce Q hitchance");
                ComboMenu.AddLabel("Lower for more hitchance, but hardly for some a.a from soldier");
                ComboMenu.Add("W", new CheckBox("Use W"));
                ComboMenu.Add("Wunit", new Slider("Max of Sand Soldier", 2, 1, 3));
                ComboMenu.Add("E", new CheckBox("Use E Logic"));
                foreach (var e in EntityManager.Heroes.Enemies.OrderBy(e => e.ChampionName))
                {
                    ComboMenu.Add(e.ChampionName, new CheckBox("E to" + e.ChampionName));
                }
                ComboMenu.Add("Edanger", new Slider("Don't E if around target has {0} enemy", 2, 1, 4));
                ComboMenu.Add("R", new CheckBox("Use R"));
                ComboMenu.Add("Rhit", new Slider("Use R if hit", 3, 1, 5));
                #region Shouldn't release now
                //ComboMenu.AddGroupLabel("Style Setting");
                //var switchkey = ComboMenu.Add("switchkey", new KeyBind("Switch Key", false, KeyBind.BindTypes.HoldActive));
                //switchkey.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                //{
                //    if (args.NewValue)
                //    {
                //        var value = ComboMenu["combostyle"].Cast<Slider>();
                //        if (value.CurrentValue == value.MaxValue)
                //        {
                //            value.CurrentValue = 0;
                //        }
                //        else
                //        {
                //            value.CurrentValue++;
                //        }
                //    }
                //};
                //var ComboSLider = ComboMenu.Add("combostyle", new Slider("Combo Style: Normal Combo", 0, 0, 3));
                //ComboSLider.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                //{
                //    switch (args.NewValue)
                //    {
                //        case 0:
                //            {
                //                ComboSLider.DisplayName = "Combo Style: Normal Combo";
                //            }
                //            break;
                //        case 1:
                //            {
                //                ComboSLider.DisplayName = "Combo Style: Solo Combo";
                //            }
                //            break;
                //        case 2:
                //            {
                //                ComboSLider.DisplayName = "Combo Style: Teamfight Combo";
                //            }
                //            break;
                //        case 3:
                //            {
                //                ComboSLider.DisplayName = "Combo Style: Highlight Combo";
                //            }
                //            break;
                //    }
                //};
                #endregion 
            }
            //InsecMenu
            Insec = Menu.AddSubMenu("Insec Settings");
            {
                Insec.AddGroupLabel("Hotkeys");
                Insec.Add("normalInsec", new KeyBind("Do Normal Insec", false, KeyBind.BindTypes.HoldActive, 'H'));
                Insec.Add("godInsec", new KeyBind("Do God Insec", false, KeyBind.BindTypes.HoldActive, 'G'));
                Insec.AddSeparator();
                Insec.AddGroupLabel("Normal Insec");
                Insec.Add("normal.1", new ComboBox("I want to insec target to", 0, "My Cursor", "My Ally", "My Turret", "Select"));
                Insec.AddLabel("Pls double left-click on ground to seclect position");
                Insec.Add("allowfl", new CheckBox("Allow using Flash in Insec"));
                Insec.Add("flvalue", new Slider("Only flash if can be insec {0} enemies", 4, 2, 5));
                Insec.Add("normalgoto", new ComboBox("If can't insec, I want move to", 1, "None", "Cursor", "To enemy"));
                Insec.AddSeparator();
                Insec.AddGroupLabel("God Insec");
                Insec.Add("god.1", new ComboBox("I want to insec target to", 3, "My Cursor", "My Ally", "My Turret", "Select"));
                Insec.Add("god.2", new ComboBox("I want to move to", 3, "My Cursor", "My Ally", "My Turret", "Select"));
                Insec.AddLabel("Pls middle-click on ground to seclect position you want to go");
                Insec.Add("godgoto", new ComboBox("If can't insec, I want to move to", 1, "None", "Cursor", "To enemy"));
            }
            //HarassMenu
            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.AddGroupLabel("Harass Settings");
                HarassMenu.Add("Q", new CheckBox("Use Q"));
                HarassMenu.Add("Qbonus", new Slider("Q will cast over {0} distance", 120, 0, 200));
                HarassMenu.AddLabel("Higher for more distance behind target, but it reduce Q hitchance");
                HarassMenu.AddLabel("Lower for more hitchance, but hardly for some a.a from soldier");
                HarassMenu.Add("W", new CheckBox("Use W"));
                HarassMenu.Add("Wunit", new Slider("Max of Sand Soldier", 1, 1, 3));
                HarassMenu.Add("HrManage", new Slider("If mana percent below {0} stop harass", 50));
                HarassMenu.AddGroupLabel("Auto Harass Settings");
                var AutoHarass = HarassMenu.Add("autokey", new KeyBind("Auto Harass", true, KeyBind.BindTypes.PressToggle, 'Z'));
                AutoHarass.OnValueChange += delegate
                {
                    var On = new SimpleNotification("Auto Harass status :", "Activated. ");
                    var Off = new SimpleNotification("Auto Harass status :", "Disable. ");

                    Notifications.Show(AutoHarass.CurrentValue ? On : Off, 2000);
                };
                HarassMenu.Add("automng", new Slider("Stop Auto harass when my mana below {0}%", 75));
            }

            //LaneClear Menu
            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.AddGroupLabel("Laneclear Settings");
                LaneClear.Add("Q", new CheckBox("Use Q to laneclear", false));
                LaneClear.Add("W", new CheckBox("Use W to laneclear"));
                LaneClear.Add("Wunit", new Slider("Max of Sand Soldier", 1, 1, 3));
                LaneClear.Add("LcManager", new Slider("If mana percent below {0} stop use skill to laneclear", 50));
            }
            //JungleClear Menu
            JungleClear = Menu.AddSubMenu("JungleClear");
            {
                JungleClear.AddGroupLabel("Jungleclear Settings");
                JungleClear.Add("Q", new CheckBox("Use Q to jungleclear"));
                JungleClear.Add("W", new CheckBox("Use W to jungleclear"));
                JungleClear.Add("Wunit", new Slider("Max of Sand Soldier", 2, 1, 3));
                JungleClear.Add("JcManager", new Slider("If mana percent below {0} stop use skill to jungleclear", 50));
            }

            //LasthitMenu
            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.AddGroupLabel("Unkillable Minion");
                LasthitMenu.Add("Q", new CheckBox("Auto Q on Unkillable Minion"));
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
                DrawMenu.Add("Q", new CheckBox("Draw Q"));
                DrawMenu.Add("W", new CheckBox("Draw W"));
                DrawMenu.Add("E", new CheckBox("Draw E"));
                DrawMenu.Add("R", new CheckBox("Draw R"));
                DrawMenu.Add("InsecPos", new CheckBox("Draw your InsecPosition"));
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
                MiscMenu.Add("interrupt.value", new ComboBox("Interrupter DangerLevel", 2, "Low", "Medium", "High"));
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
            }
        }
    }
}
