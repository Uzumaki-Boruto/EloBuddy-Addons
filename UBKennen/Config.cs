using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Drawing;


namespace UBKennen
{
    public class Config
    {     
        public static Menu Menu { get; private set; }
        public static Menu ComboMenu { get; private set; }
        public static Menu HarassMenu { get; private set; }
        public static Menu LaneClear { get; private set; }
        public static Menu JungleClear { get; private set; }
        public static Menu LasthitMenu { get; private set; }
        public static Menu MiscMenu { get; private set; }
        public static Menu DrawMenu { get; private set; }     

        public static void Dattenosa()
        {
            // Menu
            Menu = MainMenu.AddMenu("UB Kennen", "UBKennen");          
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");

            //ComboMenu
            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.AddGroupLabel("Combo Settings");
                ComboMenu.Add("useQCombo", new CheckBox("Use Q"));
                ComboMenu.Add("focus", new CheckBox("Priority Q on enemy has Passive Buff"));
                ComboMenu.Add("useWCombo", new CheckBox("Use W"));
                ComboMenu.Add("WHitCombo", new Slider("Only Use W if hit {0} enemy", 1, 1, 5));
                ComboMenu.Add("useECombo", new CheckBox("Use E", false));                
                ComboMenu.Add("useRCombo", new CheckBox("Use R"));
                ComboMenu.Add("RHitCombo", new Slider("Least enemy to use R", 2, 1, 5));
                ComboMenu.AddSeparator();
            }

            //HarassMenu
            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.AddGroupLabel("Harass Settings");
                HarassMenu.Add("useQ", new CheckBox("Use Q"));
                HarassMenu.Add("useW", new CheckBox("Use W"));
                HarassMenu.Add("Whit", new Slider("W when hit {0} enemy", 1, 1, 5));
                HarassMenu.Add("HrEnergyManager", new Slider("If energy below {0} stop harass", 0, 0, 200));
            }

            //LaneJungleClear Menu
            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.AddGroupLabel("Laneclear Settings");
                LaneClear.Add("useQLc", new CheckBox("Use Q to laneclear", false));
                LaneClear.Add("useWLc", new CheckBox("Use W to laneclear", false));
                LaneClear.Add("WHitLc", new Slider("Only Use W if hit {0} minion(s)", 5, 1, 30));
                LaneClear.Add("useELc", new CheckBox("Use E to laneclear", false));
                LaneClear.Add("EnergyManager", new Slider("If energy below {0} stop use skill to laneclear", 0, 0, 200));
            }
            //JungleClear Menu
            JungleClear =Menu.AddSubMenu("JungleClear");
            {
                JungleClear.AddGroupLabel("Jungleclear Settings");
                JungleClear.Add("useQJc", new CheckBox("Use Q to jungleclear"));
                JungleClear.Add("useWJc", new CheckBox("Use W to jungleclear"));
                JungleClear.Add("useEJc", new CheckBox("Use E to jungleclear", false));
                JungleClear.Add("JcEnergyManager", new Slider("if energy below {0} stop Use skill to jungleclear", 0, 0, 200));
            }

            //LasthitMenu
            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.Add("useQLh", new CheckBox("Use Q to lasthit"));
                LasthitMenu.Add("useWLh", new CheckBox("Use W to lasthit"));
            }

            //DrawMenu
            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawings"));
                DrawMenu.Add("drawQ", new CheckBox("Draw Q"));
                DrawMenu.Add("drawW", new CheckBox("Draw W"));
                DrawMenu.Add("drawR", new CheckBox("Draw R"));
                DrawMenu.Add("Time", new CheckBox("Passive Timer", false));
                DrawMenu.Add("dmg", new CheckBox("Draw Damage Indicator"));
                DrawMenu.Add("Color", new ColorPicker("Damage Indicator Color", Color.FromArgb(255, 255, 236, 0)));
            }

            //MiscMenu          
            MiscMenu = Menu.AddSubMenu("MiscMenu");
            {
                MiscMenu.AddGroupLabel("Misc Settings");
                MiscMenu.AddLabel("Anti Gapcloser");
                MiscMenu.Add("useQAG", new CheckBox("Use Q to anti GapCloser"));
                MiscMenu.Add("useWAG", new CheckBox("Use W to anti Gapcloser"));
                MiscMenu.Add("useEAG", new CheckBox("Use E to anti Gapcloser"));

                MiscMenu.AddLabel("Killsteal Settings");
                MiscMenu.Add("useQKS", new CheckBox("Use Q to KS"));
                MiscMenu.Add("useWKS", new CheckBox("Use W to KS"));
            }         
        }                     
    }
}     
