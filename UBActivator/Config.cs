using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

namespace UBActivator
{
    class Config
    {
        public static Menu Menu { get; private set; }
        public static Menu Potions { get; private set; }
        public static Menu Offensive { get; private set; }
        public static Menu Defensive { get; private set; }
        public static Menu Combat { get; private set; }
        public static Menu Clean { get; private set; }
        public static Menu Spell { get; private set; }
        public static Menu Ward { get; private set; }
        public static Menu Utility { get; private set; }
        public static int[] SkillOrder;
        public static CheckBox OnTickButton;
        public static CheckBox OnUpdateButton;
        public static CheckBox RandomButton;
        public static CheckBox OmenButton;
        public static CheckBox GloryButton;
        public static CheckBox TargetButton;
        public static CheckBox WardButton;
        public static CheckBox CleanDelay;
        public static Slider SkinSlider;
        public static Slider CleanSlider;
        public static Slider WardSlider;
        public static Slider RandomSlider;
        public static Slider FOTMSlider;
        public static Slider SolariSlider;
        public static Slider SerSlider;
        public static Label SerLabel;
        public static Label FOTMLabel;
        public static Label SolariLabel;

        public static void Dattenosa()
        {
            Menu = MainMenu.AddMenu("UBActivator", "UB Activator");
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");
            Menu.AddLabel("Global Settings");
            Menu.AddLabel("Must F5 to take effect");
            Menu.AddLabel("Uncheck both won't load anything");
            OnTickButton = Menu.Add("Ontick", new CheckBox("Use Game.OnTick (More fps)"));
            OnTickButton.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
            {
                if (args.NewValue)
                {
                    OnUpdateButton.CurrentValue = false;
                    return;
                }
                if (!OnUpdateButton.CurrentValue)
                {
                    OnTickButton.CurrentValue = true;
                }
            };
            OnUpdateButton = Menu.Add("OnUpdate", new CheckBox("Use Game.OnUpdate (Faster rection)", false));
            OnUpdateButton.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
            {
                if (args.NewValue)
                {
                    OnTickButton.CurrentValue = false;
                    return;
                }
                if (!OnTickButton.CurrentValue)
                {
                    OnUpdateButton.CurrentValue = true;
                }
            };

            /*
            Menu.AddLabel("Global Settings");
            Menu.AddLabel("Must F5 to take effect");          
            Menu.Add("Potions", new ComboBox("Potions Menus will be", 1, "Simple", "Details"));
            Menu.Add("Offensive", new ComboBox("Offensive Menus will be", 0, "Simple", "Details"));
            Menu.Add("Defensive", new ComboBox("Defensive Menus will be", 0, "Simple", "Details"));
            Menu.Add("Clean", new ComboBox("Clean Menus will be", 0, "Simple", "Details"));
            Menu.Add("Spell", new ComboBox("Spell Menus will be", 0, "Simple", "Details"));
            Menu.Add("Ward", new ComboBox("Ward Menus will be", 0, "Simple", "Details"));
            Menu.Add("Utility", new ComboBox("Utility Menus will be", 0, "Simple", "Details"));*/


            Potions = Menu.AddSubMenu("Potions Config");
            {
                Potions.Add("ePotions", new CheckBox("Enable Auto Potion"));
                Potions.Add("preHPrecall", new CheckBox("Prevent using Potion if recalling"));
                Potions.Add("inshopHP", new CheckBox("Prevent using Potions in shop range"));
                Potions.Add("predHP", new CheckBox("Using Predition", false));
                Potions.AddSeparator();

                Potions.Add("HP", new CheckBox("Health Potion"));
                Potions.Add("HPH", new Slider("HP below {0}% use Potion", 65, 0, 100));
                Potions.AddSeparator();

                Potions.Add("Biscuit", new CheckBox("Biscuit"));
                Potions.Add("BiscuitH", new Slider("HP below {0}% use Potion", 60, 0, 100));
                Potions.AddSeparator();

                Potions.Add("RP", new CheckBox("Refillable Potion"));
                Potions.Add("RPH", new Slider("HP below {0}% use Potion", 65, 0, 100));
                Potions.AddSeparator();

                Potions.Add("CP", new CheckBox("Corrupting Potion"));
                Potions.Add("MPCP", new CheckBox("Prevent use Potion if the restore MP more than max MP"));
                Potions.Add("CPH", new Slider("HP below {0}% use Potion", 65, 0, 100));
                Potions.AddSeparator();

                Potions.Add("HTP", new CheckBox("Hunters Potion"));
                Potions.Add("MPHTP", new CheckBox("Prevent use Potion if the restore MP more than max MP"));
                Potions.Add("HTPH", new Slider("HP below {0}% use Potion", 75, 0, 100));
            }

            Offensive = Menu.AddSubMenu("Offensive Config");
            {
                Offensive.AddGroupLabel("Targeted Item");
                TargetButton = Offensive.Add("cbitem", new CheckBox("Now is Auto use (Check to change)", false));
                TargetButton.OnValueChange += TargetButton_OnValueChange;
                Offensive.Add("BC", new CheckBox("Use Bilgewater Cutlass"));
                Offensive.Add("Bork", new CheckBox("Use Blade of Ruined King"));
                Offensive.Add("HG", new CheckBox("Use Hextech Gunblade"));
                Offensive.Add("MyHPT", new Slider("My HP", 80));
                Offensive.Add("TargetHPT", new Slider("Target HP", 80));
                Offensive.AddGroupLabel("AOE Item");
                Offensive.AddLabel("Versus Champion");
                Offensive.Add("Tiamat", new ComboBox("Use Tiamat/Hydra/Titanic", 4, "None", "Harass Only", "Combo Only", "Both", "Auto Use"));
                Offensive.Add("TiamatSlider", new Slider("{0}% damage to use Tiamat/Hydra", 75));
                Offensive.Add("styletitanic", new ComboBox("Use Titanic Hydra:", 0, "After Attack", "Before Attack"));
                Offensive.Add("TiamatKs", new CheckBox("Use to Tiamat/Hydra Killsteal"));
                Offensive.AddLabel("Versus Minion");
                Offensive.Add("TiamatLc", new CheckBox("Use Tiamat/Hydra/Titanic on LaneClear"));
                Offensive.Add("TiamatLccount", new Slider("Use only around me ≥ {0} minion(s)", 4, 1, 10));
                Offensive.Add("TiamatLh", new CheckBox("Use on Unkillable minion"));
                Offensive.AddLabel("Versus Monster");
                Offensive.Add("TiamatJc", new CheckBox("Use on JungClear"));
                Offensive.Add("TiamatJccount", new Slider("Use only around me ≥ {0} monster(s), use anyway with Baron/Dragon/Herald", 2, 1, 4));
                Offensive.AddGroupLabel("Movement Item");
                Offensive.Add("cbmvitem", new CheckBox("Only use item on Combo Flag"));
                Offensive.Add("Youmuu", new CheckBox("Youmuu's Ghostblade"));
                Offensive.AddSeparator();
                Offensive.Add("Hextech01", new CheckBox("Use HT_Protobelt_01"));
                Offensive.Add("Hextech01Ks", new CheckBox("Use HT_Protobelt_01 on KillSteal"));
                Offensive.Add("Hextech01gap", new ComboBox("Use HT_Protobelt_01 on GapCloser", 2, "None", "To Mouse", "To Target"));
                Offensive.Add("Hextech01agap", new CheckBox("Use HT_Protobelt_01 on anti GapCloser", false));
                Offensive.AddSeparator();
                Offensive.Add("Hextech800style", new ComboBox("Use HT_GLP_800 On:", 3, "None", "Only Anti GapCloser", "Only KillSteal", " Both"));

            }

            Defensive = Menu.AddSubMenu("Defensive Config");
            {
                if (!Extensions.ChampNoMana)
                {
                    Defensive.AddGroupLabel("Seraph's Embrace Setting");
                    Defensive.Add("Ser", new CheckBox("Use Seraph's Embrace"));
                    Defensive.Add("Seratt", new CheckBox("Block basic attack & spells"));
                    SerSlider = Defensive.Add("Sershield", new Slider("Use if block {0}% shield value", 60, 0, 100));
                    SerSlider.OnValueChange += SerSlider_OnValueChange;
                    SerLabel = Defensive.Add("SerLabel", new Label("This mean Use Seraph if the basic attack or spell ≥ " + Math.Round((150 + 0.2 * Player.Instance.Mana) * 60 / 100) + " damage"));
                    Defensive.AddSeparator();
                }
                Defensive.AddGroupLabel("Face of The Moutain Setting");
                Defensive.Add("Face", new CheckBox("Use Face Of The Mountain"));
                Defensive.Add("Faceatt", new CheckBox("Block basic attack & spells"));
                FOTMSlider = Defensive.Add("Faceshield", new Slider("Use if block {0}% shield value", 60, 0, 100));
                FOTMSlider.OnValueChange += FOTMSlider_OnValueChange;
                FOTMLabel = Defensive.Add("FOTMLabel", new Label("This mean Use FOTM if the basic attack or spell ≥ " + Math.Round(Player.Instance.MaxHealth * 0.1 * 60 / 100) + " damage"));
                foreach (var Ally in EntityManager.Heroes.Allies)
                {
                    Defensive.Add("Face" + Ally.ChampionName, new CheckBox("Use FOTM on " + Ally.ChampionName));
                }
                Defensive.AddSeparator();
                Defensive.AddGroupLabel("Solari Setting");
                Defensive.Add("Solari", new CheckBox("Use Solari"));
                SolariSlider = Defensive.Add("Solarishield", new Slider("Use if block {0}% shield value", 60, 0, 100));
                SolariSlider.OnValueChange += SolariSlider_OnValueChange;
                SolariLabel = Defensive.Add("SolariLabel", new Label("This mean Use Solari if the basic attack or spell ≥ " + (75 + 15 * Player.Instance.Level) * 60 / 100 + " damage"));
                foreach (var Ally in EntityManager.Heroes.Allies)
                {
                    Defensive.Add("Solari" + Ally.ChampionName, new CheckBox("Use Solari on " + Ally.ChampionName));
                }
                Defensive.AddSeparator();
                Defensive.AddGroupLabel("Zhonya Customize");
                Defensive.Add("predtimebool", new CheckBox("Using Prediction to use Zhonya"));
                Defensive.Add("predtime", new Slider("Prediction in {0}0 ms", 70, 1));
                Defensive.Add("percentusagebool", new CheckBox("Percent Health Usage"));
                Defensive.Add("percentusage", new Slider("Use Zhonya if my HP below {0}%", 15));
                Defensive.AddGroupLabel("Block Danger Spells by Zhonya");
                Defensive.Add("enableblock", new CheckBox("Enable deny spell in spells list below", false));
                Defensive.AddSeparator();
            }

            Combat = Menu.AddSubMenu("Combat Item");
            {
                Combat.Add("Randuin", new CheckBox("Use Randuin"));
                OmenButton = Combat.Add("RanduinCb", new CheckBox("Use on Combo only(Check to change)"));
                OmenButton.OnValueChange += OmenButton_OnValueChange;
                Combat.Add("Randuincount", new Slider("Use Randuin On {0} Enemies", 2, 1, 5));
                Combat.AddSeparator();
                Combat.Add("Glory", new CheckBox("Auto use Righteous Glory"));
                GloryButton = Combat.Add("GloryCb", new CheckBox("Use on Combo only(Check to change)"));
                GloryButton.OnValueChange += GloryButton_OnValueChange;
                Combat.Add("Glorycountally", new Slider("Use it if buff {0} ally", 3, 1, 4));

            }

            Clean = Menu.AddSubMenu("Cleanse");
            {
                Clean.Add("enableQSS", new CheckBox("Use QSS/Mercurial"));
                Clean.Add("enableCleanse", new CheckBox("Use Cleanse (Spell)"));
                Clean.Add("enableMikael", new CheckBox("Use Mikael on Ally"));
                foreach (var ally in EntityManager.Heroes.Allies)
                {
                    if (ally.ChampionName != Player.Instance.ChampionName)
                        Clean.Add("mikael" + ally.ChampionName, new CheckBox("Use on " + ally.ChampionName));
                }
                Clean.AddGroupLabel("Auto QSS if :");
                Clean.Add("Airbone", new CheckBox("Airbone"));
                Clean.Add("Blind", new CheckBox("Blind", false));
                Clean.Add("Charm", new CheckBox("Charm"));
                Clean.Add("Fear", new CheckBox("Fear"));
                Clean.Add("Nearsight", new CheckBox("Nearsight", false));
                Clean.Add("Polymorph", new CheckBox("Polymorph"));
                Clean.Add("Taunt", new CheckBox("Taunt"));
                Clean.Add("Slow", new CheckBox("Slow", false));
                Clean.Add("Stun", new CheckBox("Stun"));
                Clean.Add("Snare", new CheckBox("Root"));
                Clean.Add("Suppression", new CheckBox("Suppression"));
                Clean.Add("Silence", new CheckBox("Silence", false));
                Clean.AddSeparator();
                CleanDelay = Clean.Add("random", new CheckBox("Use Random delay value"));
                CleanDelay.OnValueChange += CleanDelay_OnValueChange;
                CleanSlider = Clean.Add("CCDelay", new Slider("Delay", 250, 0, 1000));
                Clean.Add("EnemyManager", new CheckBox("Use QSS despite of no enemy around", false));
            }

            Spell = Menu.AddSubMenu("Spells");
            {
                Spell.AddGroupLabel("Smite");
                Spell.Add("esmite3r", new CheckBox("Use smite to Jungle Steal"));
                Spell.AddLabel("Only work on Baron/ Dragon/ Hearald");
                Spell.Add("esmitered", new CheckBox("JungleSteal Red buff", false));
                Spell.Add("esmiteblue", new CheckBox("JungleSteal Blue buff", false));
                Spell.Add("esmiteKs", new CheckBox("Use Smite to Killsteal"));
                foreach (var enemy in EntityManager.Heroes.Enemies)
                {
                    Spell.Add("Smite" + enemy.ChampionName, new CheckBox("Use Smite on " + enemy.ChampionName));
                }
                Spell.AddGroupLabel("Ignite");
                Spell.Add("eIg", new CheckBox("Use Ig to KillSteal"));
                Spell.Add("Igstyle", new ComboBox("Damage Calculator:", 0, "Full Damage", "First Tick"));
                foreach (var enemy in EntityManager.Heroes.Enemies)
                {
                    Spell.Add("Ig" + enemy.ChampionName, new CheckBox("Use Ignite on " + enemy.ChampionName));
                }
                Spell.AddGroupLabel("Heal");
                Spell.Add("eHeal", new CheckBox("Use Heal"));
                Spell.Add("myHPHeal", new Slider("Use Heal if my HP below {0}%", 30));
                Spell.Add("eHealAlly", new CheckBox("Use Heal on Ally"));
                foreach (var ally in EntityManager.Heroes.Allies)
                {
                    if (ally.ChampionName != Player.Instance.ChampionName)
                        Spell.Add("heal" + ally.ChampionName, new CheckBox("Heal " + ally.ChampionName));
                }
                Spell.Add("allyHPHeal", new Slider("If ally's HP blow {0}% use Heal", 15));
                Spell.AddGroupLabel("Ghost");
                Spell.Add("Ghost", new CheckBox("Use Ghost"));

            }

            Ward = Menu.AddSubMenu("Auto Reveal");
            {
                Ward.Add("enableward", new CheckBox("Enable Reveal"));
                Ward.Add("enablebrush", new CheckBox("Anti Bush"));
                foreach (var champ in EntityManager.Heroes.Enemies)
                {
                    if (champ.ChampionName == "Akali")
                        Ward.Add("akali", new CheckBox("Anti Akali"));
                    if (champ.ChampionName == "Rengar")
                        Ward.Add("rengar", new CheckBox("Anti Rengar"));
                    if (champ.ChampionName == "Shaco")
                        Ward.Add("shaco", new CheckBox("Anti Shaco"));
                    if (champ.ChampionName == "Wukong")
                        Ward.Add("shaco", new CheckBox("Anti Wukong", false));
                    //if (champ.ChampionName == "Leblanc")
                    //    Ward.Add("leblanc", new CheckBox("Anti Leblanc"));
                    if (champ.ChampionName == "KhaZix")
                        Ward.Add("khazik", new CheckBox("Anti KhaZix"));
                    if (champ.ChampionName == "Twitch")
                        Ward.Add("twitch", new CheckBox("Anti Twitch"));
                    if (champ.ChampionName == "Vayne")
                        Ward.Add("vayne", new CheckBox("Anti Vayne"));
                }
                WardButton = Ward.Add("wardhuman", new CheckBox("Use random value", false));
                WardButton.OnValueChange += WardButton_OnValueChange;
                WardSlider = Ward.Add("warddelay", new Slider("Delay", 500, 0, 2000));

            }

            Utility = Menu.AddSubMenu("Other Settings");
            {
                Utility.AddGroupLabel("Remind Trinkets");
                Utility.Add("remind", new CheckBox("Remind me to change Trinkets"));
                Utility.AddGroupLabel("Auto Tear");
                Utility.Add("etear", new CheckBox("Auto stack Tear"));
                Utility.AddGroupLabel("Mod Skin");
                Utility.Add("eskin", new CheckBox("Enable Modskin", false));
                SkinSlider = Utility.Add("skin", new Slider("Drag for skin", 0, 0, 15));
                SkinSlider.OnValueChange += SkinSlider_OnValueChange;
                Utility.AddSeparator();
                Utility.AddGroupLabel("Auto Level Up");
                Utility.Add("lvl", new CheckBox("Enable Auto Level UP"));
                RandomButton = Utility.Add("lvlrandom", new CheckBox("Use Random Value", false));
                RandomButton.OnValueChange += RandomButton_OnValueChange;
                RandomSlider = Utility.Add("lvldelay", new Slider("Delay", 500, 0, 2000));

                #region Champion Skill Order
                switch (Player.Instance.ChampionName)
                {
                    case "Aatrox":
                        SkillOrder = new[] { 2, 1, 3, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                        break;

                    case "Ahri":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Akali":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Alistar":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Amumu":
                        SkillOrder = new[] { 3, 2, 1, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                        break;

                    case "Anivia":
                        SkillOrder = new[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Annie":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Ashe":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "AurelionSol":
                        SkillOrder = new[] { 1, 2, 2, 3, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                        break;

                    case "Azir":
                        SkillOrder = new[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 3, 2, 3, 4, 3, 3 };
                        break;

                    case "Blitzcrank":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Brand":
                        SkillOrder = new[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                        break;

                    case "Braum":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Caitlyn":
                        SkillOrder = new[] { 2, 1, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Cassiopeia":
                        SkillOrder = new[] { 1, 3, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Chogath":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Corki":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Darius":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Diana":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "DrMundo":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Draven":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Elise":
                        Extensions.ROff = -1;
                        SkillOrder = new[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Evelynn":
                        SkillOrder = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Ezreal":
                        SkillOrder = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Ekko":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "FiddleSticks":
                        SkillOrder = new[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                        break;

                    case "Fiora":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Fizz":
                        SkillOrder = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Galio":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Gangplank":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Garen":
                        SkillOrder = new[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Gnar":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Gragas":
                        SkillOrder = new[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Graves":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Hecarim":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Heimerdinger":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Illaoi":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Irelia":
                        SkillOrder = new[] { 1, 3, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                        break;

                    case "Janna":
                        SkillOrder = new[] { 3, 1, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                        break;

                    case "JarvanIV":
                        SkillOrder = new[] { 3, 1, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Jax":
                        SkillOrder = new[] { 3, 2, 1, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                        break;

                    case "Jayce":
                        Extensions.ROff = -1;
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 2, 1, 3, 1, 3, 1, 3, 3, 2, 2, 3, 2, 2 };
                        break;

                    case "Jhin":
                        SkillOrder = new[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Jinx":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Karma":
                        Extensions.ROff = -1;
                        SkillOrder = new[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Karthus":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Kassadin":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Katarina":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Kalista":
                        SkillOrder = new[] { 3, 1, 2, 3, 3, 4, 1, 3, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Kayle":
                        SkillOrder = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Kennen":
                        SkillOrder = new[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                        break;

                    case "Khazix":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Kindred":
                        SkillOrder = new[] { 2, 1, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "KogMaw":
                        SkillOrder = new[] { 2, 3, 1, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                        break;

                    case "Leblanc":
                        SkillOrder = new[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                        break;

                    case "LeeSin":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Leona":
                        SkillOrder = new[] { 3, 1, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                        break;

                    case "Lissandra":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Lucian":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Lulu":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 1, 4, 2, 2 };
                        break;

                    case "Lux":
                        SkillOrder = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Malphite":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Malzahar":
                        SkillOrder = new[] { 2, 3, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Maokai":
                        SkillOrder = new[] { 3, 1, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "MasterYi":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "MissFortune":
                        SkillOrder = new[] { 1, 2, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                        break;

                    case "Mordekaiser":
                        SkillOrder = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Morgana":
                        SkillOrder = new[] { 1, 2, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                        break;
                    case "Nami":
                        SkillOrder = new[] { 2, 1, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Nasus":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Nautilus":
                        SkillOrder = new[] { 3, 2, 1, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                        break;

                    case "Nidalee":
                        Extensions.ROff = -1;
                        SkillOrder = new[] { 2, 1, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Nocturne":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2 };
                        break;

                    case "Nunu":
                        SkillOrder = new[] { 1, 3, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                        break;

                    case "Olaf":
                        SkillOrder = new[] { 2, 1, 3, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Orianna":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Pantheon":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Poppy":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Quinn":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Rammus":
                        SkillOrder = new[] { 2, 1, 3, 2, 3, 4, 2, 3, 3, 3, 4, 2, 2, 1, 1, 4, 1, 1 };
                        break;

                    case "Renekton":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Rengar":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Riven":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Rumble":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "RekSai":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Ryze":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 1, 3, 3, 2, 2, 2, 2 };
                        break;

                    case "Sejuani":
                        SkillOrder = new[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 3, 4, 3, 3, 3, 1, 4, 1, 1 };
                        break;

                    case "Shaco":
                        SkillOrder = new[] { 2, 1, 3, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Shen":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Shyvana":
                        SkillOrder = new[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                        break;

                    case "Singed":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Sion":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Sivir":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Skarner":
                        SkillOrder = new[] { 1, 2, 3, 3, 3, 4, 3, 3, 1, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Sona":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Soraka":
                        SkillOrder = new[] { 1, 2, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                        break;

                    case "Swain":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Syndra":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Talon":
                        SkillOrder = new[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                        break;

                    case "Taric":
                        SkillOrder = new[] { 3, 2, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "TahmKench":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Teemo":
                        SkillOrder = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Thresh":
                        SkillOrder = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Tristana":
                        SkillOrder = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Trundle":
                        SkillOrder = new[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Tryndamere":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "TwistedFate":
                        SkillOrder = new[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 4, 3, 3, 4, 3, 3 };
                        break;

                    case "Twitch":
                        SkillOrder = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Udyr":
                        SkillOrder = new[] { 4, 2, 3, 4, 4, 1, 4, 2, 4, 2, 2, 2, 3, 3, 3, 3, 1, 1 };
                        break;

                    case "Urgot":
                        SkillOrder = new[] { 3, 1, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Varus":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Vayne":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Veigar":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Velkoz":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Vi":
                        SkillOrder = new[] { 3, 1, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Viktor":
                        SkillOrder = new[] { 1, 3, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Vladimir":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Volibear":
                        SkillOrder = new[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                        break;

                    case "Warwick":
                        SkillOrder = new[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "MonkeyKing":
                        SkillOrder = new[] { 3, 1, 2, 1, 1, 4, 3, 1, 3, 1, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Xerath":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "XinZhao":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        break;

                    case "Yasuo":
                        SkillOrder = new[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Yorick":
                        SkillOrder = new[] { 2, 3, 1, 3, 3, 4, 3, 2, 3, 1, 4, 2, 1, 2, 1, 4, 2, 1 };
                        break;

                    case "Zac":
                        SkillOrder = new[] { 2, 1, 3, 3, 1, 4, 3, 1, 3, 1, 4, 3, 1, 2, 2, 4, 2, 2 };
                        break;

                    case "Zed":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 3, 1, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Ziggs":
                        SkillOrder = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;

                    case "Zilean":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;
                    case "Zyra":
                        SkillOrder = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                        break;
                }
                #endregion

                for (var i = 1; i <= 18; i++)
                {
                    Utility.Add(i.ToString() + Player.Instance.ChampionName, new ComboBox("Level " + i, SkillOrder[i - 1], "None", "Q", "W", "E", "R"));
                }
            }            
        }

        #region Value Change
        static void SkinSlider_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            if (ObjectManager.Player.SkinId != args.NewValue)
            {
                if (Config.Utility["eskin"].Cast<CheckBox>().CurrentValue)
                {
                    Player.Instance.SetSkinId(args.NewValue);
                }
            }

        }
        static void CleanDelay_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            switch (args.NewValue)
            {
                case true:
                    {
                        CleanSlider.DisplayName = "Max Value for Random Delay";
                    }
                    break;
                case false:
                    {
                        CleanSlider.DisplayName = "Delay for auto Cleanse";
                    }
                    break;
            }
        }
        static void SerSlider_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            SerLabel.DisplayName = "This mean Use Solari if the basic attack or spell ≥ " + Math.Round((150 + 0.2 * Player.Instance.Mana) * args.NewValue / 100) + " damage";
        }
        static void FOTMSlider_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            FOTMLabel.DisplayName = "This mean Use FOTM if the basic attack or spell ≥ " + Math.Round(Player.Instance.MaxHealth * 0.1 * args.NewValue / 100) + " damage";
        }
        static void SolariSlider_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            SolariLabel.DisplayName = "This mean Use Solari if the basic attack or spell ≥ " + (75 + 15 * Player.Instance.Level) * args.NewValue / 100 + " damage";
        }
        private static void GloryButton_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            switch (args.NewValue)
            {
                case true:
                    {
                        GloryButton.DisplayName = "Use on Combo only (Check to change)";
                    }
                    break;
                case false:
                    {
                        GloryButton.DisplayName = "Now is Auto use (Check to change)";
                    }
                    break;
            }
        }

        private static void OmenButton_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            switch (args.NewValue)
            {
                case true:
                    {
                        OmenButton.DisplayName = "Use on Combo only (Check to change)";
                    }
                    break;
                case false:
                    {
                        OmenButton.DisplayName = "Now is Auto use (Check to change)";
                    }
                    break;
            }
        }

        private static void TargetButton_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            switch (args.NewValue)
            {
                case true:
                    {
                        TargetButton.DisplayName = "Use on Combo only (Check to change)";
                    }
                    break;
                case false:
                    {
                        TargetButton.DisplayName = "Now is Auto use (Check to change)";
                    }
                    break;
            }
        }
        static void WardButton_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            switch (args.NewValue)
            {
                case true:
                    {
                        WardSlider.DisplayName = "Max Value for Random Delay";
                    }
                    break;
                case false:
                    {
                        WardSlider.DisplayName = "Delay for auto reveal";
                    }
                    break;
            }
        }
        private static void RandomButton_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            switch (args.NewValue)
            {
                case true:
                    {
                        RandomSlider.DisplayName = "Max Value for Random Delay";
                    }
                    break;
                case false:
                    {
                        RandomSlider.DisplayName = "Delay for auto skill up";
                    }
                    break;
            }
        }
        #endregion
    }
}
