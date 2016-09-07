using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBLucian
{
    class Config
    {
        public static Menu Menu;
        public static Menu ESettings;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClear;
        public static Menu JungleClear;
        public static Menu LasthitMenu;
        public static Menu FleeMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;

        public static ComboBox WStyle;
        public static Slider EQHit, ERange;
        public static CheckBox ESafe, EWall, ECorrect, EQ, EPath, EKite, EGap, EGrass;

        public static void Dattenosa()
        {
            Menu = MainMenu.AddMenu("UB Lucian", "UBLucian");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");

            ESettings = Menu.AddSubMenu("E");
            {               
                ESettings.AddGroupLabel("E General Settings");
                ESafe = ESettings.Add("Esafe", new CheckBox("Safe Point Check E"));
                EWall = ESettings.Add("Ewall", new CheckBox("Check Wall"));
                ECorrect = ESettings.Add("Ecorrect", new CheckBox("Allow Correct to better"));
                EQ = ESettings.Add("EQ", new CheckBox("Try E to Q hit more enemy", false));
                EQHit = ESettings.Add("EQhit", new Slider("Try to E if next Q hit {0} champ", 4, 1, 5));
                ERange = ESettings.Add("Erange", new Slider("Range eliminate {0}", 35, 0, 75));
                ESettings.Add("label", new Label("The more value, the more easier for next a.a, the more dangerous, recommended 20~40"));
                EPath = ESettings.Add("Epath", new CheckBox("Anti Enemy Path"));
                EKite = ESettings.Add("Ekite", new CheckBox("Try to kite champ"));
                EGap = ESettings.Add("Etogap", new CheckBox("Use E to Gapclose taget"));
                EGrass = ESettings.Add("Egrass", new CheckBox("Try E to Grass"));
                ESettings.AddSeparator();
                ESettings.AddGroupLabel("E Style Settings");
                var EStyle = ESettings.Add("E", new ComboBox("E style", 1, "Don't use E", "Markman", "CursorPos", "Cursor (Smart)"));
                EStyle.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                {
                    switch (args.NewValue)
                    {
                        case 1:
                            {
                                ESafe.IsVisible = true;
                                ESafe.CurrentValue = true;
                                EWall.IsVisible = true;
                                EWall.CurrentValue = true;
                                ECorrect.IsVisible = true;
                                EQ.IsVisible = true;
                                EQHit.IsVisible = true;
                                ERange.IsVisible = true;
                                ESettings["label"].IsVisible = true;
                                EPath.CurrentValue = true;
                                EPath.IsVisible = true;
                                EKite.CurrentValue = true;
                                EKite.IsVisible = true;
                                EGap.IsVisible = true;
                                EGrass.IsVisible = false;

                            }
                            break;
                        case 2:
                            {
                                ESafe.IsVisible = false;
                                EWall.IsVisible = false;
                                ECorrect.IsVisible = false;
                                EQ.IsVisible = false;
                                EQHit.IsVisible = false;
                                ERange.IsVisible = false;
                                ESettings["label"].IsVisible = false;
                                EPath.IsVisible = false;
                                EKite.IsVisible = false;
                                EGap.IsVisible = false;
                                EGrass.IsVisible = false;
                            }
                            break;
                        case 3:
                            {
                                ESafe.CurrentValue = true;
                                EWall.CurrentValue = false;
                                ECorrect.IsVisible = true;
                                ECorrect.CurrentValue = true;
                                EQ.IsVisible = true;
                                EQHit.IsVisible = true;
                                ERange.IsVisible = false;
                                ESettings["label"].IsVisible = false;
                                EPath.IsVisible = false;
                                EKite.IsVisible = true;
                                EGap.IsVisible = true;
                                EGrass.IsVisible = true;
                            }
                            break;
                    }
                };
            }

            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.AddGroupLabel("Combo Settings");
                ComboMenu.Add("passive", new CheckBox("Passive Combo"));
                ComboMenu.Add("Q", new ComboBox("Q style", 3, "Don't use Q", "Q only", "Q Extend only", "Both"));
                ComboMenu.Add("W", new ComboBox("W style", 2, "Don't use W", "A.A Range", "If hit target"));
                ComboMenu.Add("E", new CheckBox("Use E"));
                ComboMenu.Add("R", new Slider("Only R if enemy hit {0}% damage"));
                ComboMenu.AddLabel("Slide to 0 and not use R");
            }

            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.AddGroupLabel("Harass Settings");
                HarassMenu.Add("passive", new CheckBox("Passive Harass"));
                HarassMenu.Add("Q", new ComboBox("Q style", 3, "Don't use Q", "Q only", "Q Extend only", "Both"));
                HarassMenu.Add("W", new CheckBox("Use W"));
                HarassMenu.Add("E", new CheckBox("Use E"));
                HarassMenu.Add("hr", new Slider("If mana percent below {0}% stop harass", 50));
                HarassMenu.Add("keyharass", new KeyBind("Auto Harass", false, KeyBind.BindTypes.HoldActive, 'Z'));
                HarassMenu.Add("autohrmng", new Slider("If mana percent below {0}% stop auto harass", 85));
            }

            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.AddGroupLabel("Laneclear Settings");
                LaneClear.Add("passive", new CheckBox("Passive LaneClear"));
                LaneClear.Add("Q", new CheckBox("Use Q to laneclear", false));
                LaneClear.Add("Qhit", new Slider("Only Use Q if hit {0} minion(s)", 6, 1, 15));
                LaneClear.Add("W", new CheckBox("Use W to laneclear", false));
                LaneClear.Add("E", new CheckBox("Use E to laneclear", false));
                LaneClear.Add("lc", new Slider("If mana percent below {0}% stop use skill to laneclear", 50));
            }

            JungleClear = Menu.AddSubMenu("JungleClear");
            {
                JungleClear.AddGroupLabel("Jungleclear Settings");
                JungleClear.Add("passive", new CheckBox("Passive jungleclear"));
                JungleClear.Add("Q", new CheckBox("Use Q to jungleclear"));
                JungleClear.Add("W", new CheckBox("Use W to jungleclear"));
                JungleClear.Add("E", new CheckBox("Use E to jungleclear"));
                JungleClear.Add("jc", new Slider("If mana percent below {0}% stop use skill to jungleclear", 50));
            }

            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.AddGroupLabel("Unkillable Minion Settings");
                LasthitMenu.Add("Q", new CheckBox("Use Q to lasthit"));
                LasthitMenu.Add("W", new CheckBox("Use W to lasthit"));
                LasthitMenu.Add("E", new CheckBox("Use E to lasthit", false));
                LasthitMenu.Add("lh", new Slider("If mana percent below {0}% stop use skill to lasthit", 50));
            }

            FleeMenu = Menu.AddSubMenu("Flee Setting");
            {
                var switchkey = FleeMenu.Add("switchkey", new KeyBind("Switch Key", false, KeyBind.BindTypes.HoldActive, 'S'));
                switchkey.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                {
                    if (args.NewValue)
                    {
                        var value = FleeMenu.GetValue("jumptype", false);
                        if (value == 2)
                        {
                            value = 0;
                        }
                        else
                        {
                            value++;
                        }
                    }
                };
                FleeMenu.Add("jumptype", new ComboBox("Jump Type", 0, "Don't E jump", "Flee Active", "Auto Jump"));
                FleeMenu.Add("jumpclickrange", new Slider("Range of Click", 50, 20, 100));
            }

            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawing"));
                DrawMenu.Add("notif", new CheckBox("Enable notification"));
                DrawMenu.AddSeparator();
                DrawMenu.Add("Qdr", new CheckBox("Draw Q"));
                DrawMenu.Add("Q2dr", new CheckBox("Draw Q Extend"));
                DrawMenu.Add("Wdr", new CheckBox("Draw W"));
                DrawMenu.Add("Edr", new CheckBox("Draw E"));
                DrawMenu.Add("Eposdr", new CheckBox("Draw best postion E"));
                DrawMenu.Add("Rdr", new CheckBox("Draw R"));
                DrawMenu.Add("dmg", new CheckBox("Draw Damage Indicator"));
                DrawMenu.Add("color", new ColorPicker("Damage Indicator Color", System.Drawing.Color.FromArgb(255, 255, 236, 0)));
                DrawMenu.Add("spot", new CheckBox("Draw Dash Spot"));
                DrawMenu.Add("spotcolor", new ColorPicker("Dash spot color", System.Drawing.Color.OrangeRed));
            }

            MiscMenu = Menu.AddSubMenu("MiscMenu");
            {
                MiscMenu.AddGroupLabel("KillSteal Settings");
                MiscMenu.Add("Qks", new CheckBox("Use Q to KillSteal"));
                MiscMenu.Add("Wks", new CheckBox("Use W to KillSteal"));
                MiscMenu.Add("Eks", new CheckBox("Use E to KillSteal", false));
                MiscMenu.Add("Rks", new ComboBox("Use R to KillSteal", 2, "Disable", "Always", "Out of Spells Range and a.a range"));
                MiscMenu.Add("Rkstick", new Slider("R Ticks KillSteal", 5, 1, 30));
                MiscMenu.AddGroupLabel("Orb cancel amination");
                MiscMenu.Add("Qcancel", new CheckBox("Cancel Q amination[BETA]"));
                MiscMenu.AddGroupLabel("GapCloser");
                MiscMenu.Add("Egap", new CheckBox("Use E to anti GapCloser"));             

            }
            //Need Improve
            //SemiPlayer = Menu.AddSubMenu("Semi Player");
            //{
            //    SemiPlayer.AddGroupLabel("Semi Player Setting");
            //    SemiPlayer.Add("semi", new KeyBind("Enable Semi Player", false, KeyBind.BindTypes.PressToggle, 'G'));
            //    SemiPlayer.Add("passive", new CheckBox("Passive Semi"));
            //    SemiPlayer.Add("Q", new ComboBox("Q style", 3, "Don't use Q", "Q only", "Q Extend only", "Both"));
            //    SemiPlayer.Add("W", new ComboBox("W style", 2, "Don't use W", "A.A Range", "If hit target"));
            //    SemiPlayer.Add("E", new CheckBox("Use E"));
            //}
        }
        public static void CorrectTheMenu()
        {
            switch (ESettings.GetValue("E", false))
            {
                case 1:
                    {
                        ESafe.IsVisible = true;
                        ESafe.CurrentValue = true;
                        EWall.IsVisible = true;
                        EWall.CurrentValue = true;
                        ECorrect.IsVisible = true;
                        EQ.IsVisible = true;
                        EQHit.IsVisible = true;
                        ERange.IsVisible = true;
                        ESettings["label"].IsVisible = true;
                        EPath.CurrentValue = true;
                        EPath.IsVisible = true;
                        EKite.CurrentValue = true;
                        EKite.IsVisible = true;
                        EGap.IsVisible = true;
                        EGrass.IsVisible = false;

                    }
                    break;
                case 0: case 2:
                    {
                        ESafe.IsVisible = false;
                        EWall.IsVisible = false;
                        ECorrect.IsVisible = false;
                        EQ.IsVisible = false;
                        EQHit.IsVisible = false;
                        ERange.IsVisible = false;
                        ESettings["label"].IsVisible = false;
                        EPath.IsVisible = false;
                        EKite.IsVisible = false;
                        EGap.IsVisible = false;
                        EGrass.IsVisible = false;
                    }
                    break;
                case 3:
                    {
                        ESafe.CurrentValue = true;
                        EWall.CurrentValue = false;
                        ECorrect.IsVisible = true;
                        ECorrect.CurrentValue = true;
                        EQ.CurrentValue = false;
                        EQ.IsVisible = true;
                        EQHit.IsVisible = true;
                        ERange.IsVisible = false;
                        ESettings["label"].IsVisible = false;
                        EPath.IsVisible = false;
                        EKite.IsVisible = true;
                        EGap.IsVisible = true;
                        EGrass.IsVisible = true;
                    }
                    break;
            }
        }
    }
}
