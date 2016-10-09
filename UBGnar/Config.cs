using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBGnar
{
    class Config
    {
        public static Menu Menu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClear;
        public static Menu JungleClear;
        public static Menu LasthitMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;

        public static void Dattenosa()
        {
            Menu = MainMenu.AddMenu("UB Gnar", "UBGnar");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");
            Menu.Add("Q", new CheckBox("New Q collision for Gnar [F5]", false));
            Menu.Add("st", new Slider("R Load position", 10, 0, 20));
            Menu.AddLabel("The more R load, the more accuracy, but lower fps");

            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.AddGroupLabel("MiniGnar Settings");
                ComboMenu.Add("Q", new CheckBox("Use Q"));
                ComboMenu.Add("W", new CheckBox("Focus W"));
                ComboMenu.Add("Etiny", new CheckBox("Don't E to turret"));
                ComboMenu.Add("Etiny2", new CheckBox("Don't E maybe to turret", false));
                ComboMenu.Add("E", new ComboBox("E style", 2, "Don't E", "GapClose", "Position Kite", "Target Escape"));
                ComboMenu.AddGroupLabel("BigGnar Settings");
                ComboMenu.Add("Qbig", new CheckBox("Use Q"));
                ComboMenu.Add("Wbig", new CheckBox("Use W"));
                ComboMenu.Add("Ebig", new CheckBox("Use E"));
                ComboMenu.Add("Ebig1", new CheckBox("Don't E to Turret", false));
                ComboMenu.AddGroupLabel("R Settings");
                ComboMenu.Add("R", new CheckBox("Use R"));
                ComboMenu.Add("unit", new Slider("R only hit", 1, 1, 5));
                var stun = ComboMenu.Add("Rstun", new CheckBox("Stun Only"));
                var label = ComboMenu.Add("label", new Label("Contact me to ask for your suggestion about this addon"));
                label.IsVisible = false;               
                stun.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                {
                    if (args.NewValue)
                    {
                        stun.DisplayName = "You can't untick it kappa";
                        stun.CurrentValue = false;
                        label.IsVisible = true;
                    }
                };
                //var all = ComboMenu.Add("Rally", new CheckBox("R Ally"));
                //stun.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                //{
                //    if (args.NewValue)
                //    {
                //        all.CurrentValue = false;
                //        return;
                //    }
                //    if (!all.CurrentValue)
                //    {
                //        stun.CurrentValue = true;
                //    }
                //};
                //all.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                //{
                //    if (args.NewValue)
                //    {
                //        stun.CurrentValue = false;
                //        return;
                //    }
                //    if (!stun.CurrentValue)
                //    {
                //        all.CurrentValue = true;
                //    }
                //};
                ComboMenu.AddGroupLabel("Orb Take Q");
                ComboMenu.Add("takeQ", new CheckBox("Take Q [BETA]", false));
            }

            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.AddGroupLabel("MiniGnar Settings");
                HarassMenu.Add("Q", new CheckBox("Use Q"));
                HarassMenu.Add("W", new CheckBox("Focus W"));
                HarassMenu.Add("Etiny", new CheckBox("Don't E to turret"));
                HarassMenu.Add("Etiny2", new CheckBox("Don't E maybe to turret", false));
                HarassMenu.Add("E", new ComboBox("E style", 2, "Don't E", "GapClose", "Position Kite", "Target Escape"));
                HarassMenu.AddGroupLabel("BigGnar Settings");
                HarassMenu.Add("Qbig", new CheckBox("Use Q"));
                HarassMenu.Add("Wbig", new CheckBox("Use W"));
                HarassMenu.Add("Ebig", new CheckBox("Use E"));
                HarassMenu.Add("Ebig1", new CheckBox("Don't E to Turret", false));
                HarassMenu.AddGroupLabel("AutoHarass Settings");
                var key = HarassMenu.Add("keyharass", new KeyBind("Auto harass key", false, KeyBind.BindTypes.PressToggle));

            }

            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.AddGroupLabel("MiniGnar Settings");
                LaneClear.Add("Q", new CheckBox("Use Q", false));
                LaneClear.Add("E", new CheckBox("Use E", false));
                LaneClear.AddGroupLabel("BigGnar Settings");
                LaneClear.Add("Qbig", new CheckBox("Use Q", false));
                LaneClear.Add("Wbig", new CheckBox("Use W", false));
                LaneClear.Add("Ebig", new CheckBox("Use E", false));
            }

            JungleClear = Menu.AddSubMenu("JungleClear");
            {
                JungleClear.AddGroupLabel("MiniGnar Settings");
                JungleClear.Add("Q", new CheckBox("Use Q", false));
                JungleClear.Add("E", new CheckBox("Use E", false));
                JungleClear.AddGroupLabel("BigGnar Settings");
                JungleClear.Add("Qbig", new CheckBox("Use Q", false));
                JungleClear.Add("Wbig", new CheckBox("Use W", false));
                JungleClear.Add("Ebig", new CheckBox("Use E", false));
            }

            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.AddGroupLabel("Unkillable Minion Settings");
                LasthitMenu.Add("Q", new CheckBox("Use Q to lasthit"));
                LasthitMenu.Add("E", new CheckBox("Use E to lasthit", false));
            }

            MiscMenu = Menu.AddSubMenu("MiscMenu");
            {
                MiscMenu.AddGroupLabel("KillSteal Settings");
                MiscMenu.Add("Qks", new CheckBox("Use Q to KillSteal"));
                MiscMenu.Add("Wks", new CheckBox("Use W to KillSteal"));
                MiscMenu.Add("Eks", new CheckBox("Use E to KillSteal"));
                MiscMenu.Add("Ett", new CheckBox("Prevent E to Turret"));
                MiscMenu.Add("Rks", new CheckBox("Use E to KillSteal", false));
                MiscMenu.AddGroupLabel("GapCloser");
                MiscMenu.Add("gap", new CheckBox("Use W to anti GapCloser"));
                MiscMenu.AddGroupLabel("Interrupter");
                MiscMenu.Add("Winterrupt", new CheckBox("Use W to Interrupt"));
                MiscMenu.Add("Rinterrupt", new CheckBox("Use R to Interrupt"));
                MiscMenu.Add("interrupt.level", new ComboBox("Danger Level", 2, "Low", "Normal", "High"));

            }

            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawing"));
                DrawMenu.Add("notif", new CheckBox("Enable notification"));
                DrawMenu.AddSeparator();
                DrawMenu.Add("Qdr", new CheckBox("Draw Q"));
                DrawMenu.Add("Wdr", new CheckBox("Draw W"));
                DrawMenu.Add("Edr", new CheckBox("Draw E"));
                DrawMenu.Add("Rdr", new CheckBox("Draw R"));
                DrawMenu.Add("rpos", new CheckBox("Draw Wall"));
                DrawMenu.Add("timer", new CheckBox("Draw Timer Passive"));
                DrawMenu.Add("dmg", new CheckBox("Draw Damage Indicator"));
                var ColorPick = DrawMenu.Add("Color", new ColorPicker("Damage Indicator Color", SaveColor.Load()));
                ColorPick.OnLeftMouseUp += delegate(Control sender, System.EventArgs args)
                {
                    SaveColor.Save(ColorPick.CurrentValue);
                };
            }
        }
    }
}
