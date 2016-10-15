using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UBActivator
{
    #region Zhonya
    public static class Zhonya
    {
        public static readonly Dictionary<string, List<Block>> BlockedSpells =
            new Dictionary<string, List<Block>>();

        static Zhonya()
        {
            const SpellSlot N48 = (SpellSlot)48;

            var q = new Block(SpellSlot.Q);
            var w = new Block(SpellSlot.W);
            var e = new Block(SpellSlot.E);
            var r = new Block(SpellSlot.R);

            #region Block List
            BlockedSpells.Add("Aatrox", new List<Block> { r });
            BlockedSpells.Add("Akali", new List<Block> { r });
            BlockedSpells.Add("Alistar", new List<Block> { q, w });
            BlockedSpells.Add("Anivia", new List<Block> { e });
            BlockedSpells.Add("Annie", new List<Block> { q, w, r });
            BlockedSpells.Add("Amumu", new List<Block> { r });
            BlockedSpells.Add("Azir", new List<Block> { r });
            BlockedSpells.Add("Bard", new List<Block> { r });
            BlockedSpells.Add("Blitzcrank", new List<Block> { new Block("PowerFistAttack", "Power Fist", true), r });
            BlockedSpells.Add("Brand", new List<Block> { r });
            BlockedSpells.Add("Braum", new List<Block> { new Block("BraumBasicAttackPassiveOverride", "Stun", true) });
            BlockedSpells.Add("Caitlyn", new List<Block> { r });
            BlockedSpells.Add("Cassiopeia", new List<Block> { r });
            BlockedSpells.Add("Chogath", new List<Block> { r });
            BlockedSpells.Add("Darius", new List<Block> { new Block("DariusNoxianTacticsONHAttack", "Empowered W", true), e, r });
            BlockedSpells.Add("Diana", new List<Block> { e, r });
            BlockedSpells.Add("Ekko", new List<Block> { new Block("EkkoEAttack", "Empowered E", true), new Block("ekkobasicattackp3", "Third Proc Passive", true) });
            BlockedSpells.Add("FiddleSticks", new List<Block> { q });
            BlockedSpells.Add("Fizz", new List<Block> { q, new Block("fizzjumptwo", "Second E") });
            BlockedSpells.Add("Garen", new List<Block> { new Block("GarenQAttack", "Empowered Q", true), r });
            BlockedSpells.Add("Gnar", new List<Block> { new Block("GnarBasicAttack", "Empowered W", true)
                    {
                        BuffName = "gnarwproc",
                        IsPlayerBuff = true
                    }, r });
            BlockedSpells.Add("Gragas", new List<Block> { r });
            BlockedSpells.Add("Hecarim", new List<Block> { new Block("hecarimrampattack", "Empowered E", true), r });
            BlockedSpells.Add("Illaoi", new List<Block> { new Block("illaoiwattack", "Empowered W", true), r });
            BlockedSpells.Add("Irelia", new List<Block> { q, e });
            BlockedSpells.Add("JarvanIV", new List<Block> { r });
            BlockedSpells.Add(
                "Jax",
                new List<Block>
                {
                    new Block("JaxBasicAttack", "Empowered", true)
                    {
                        BuffName = "JaxEmpowerTwo",
                        IsSelfBuff = true
                    },
                    q
                    //new BlockedSpell(SpellSlot.E) { BuffName = "JaxCounterStrike", IsSelfBuff = true }
                });
            BlockedSpells.Add("Jayce", new List<Block>
                {
                    new Block("JayceToTheSkies", "Hammer Q"),
                    new Block("JayceThunderingBlow", "Hammer E")
                });
            BlockedSpells.Add("Jhin", new List<Block> { new Block("JhinPassiveAttack", "4th", true) });
            BlockedSpells.Add("Kassadin", new List<Block> {  new Block("KassadinBasicAttack3", "Empowered W", true) });
            BlockedSpells.Add("Katarina", new List<Block> { e });
            BlockedSpells.Add("Kayle", new List<Block> { q });
            BlockedSpells.Add("Kennen", new List<Block> { new Block("KennenMegaProc", "Empowered", true), w, r });
            BlockedSpells.Add("Khazix", new List<Block> { q });
            BlockedSpells.Add("Kindred", new List<Block> { e });
            BlockedSpells.Add("Leblanc", new List<Block> { q, new Block("LeblancChaosOrbM", "Block RQ"), w, new Block("LeblancSlideM", "Block RW") });
            BlockedSpells.Add(
                "LeeSin",
                new List<Block>
                {
                    new Block("blindmonkqtwo", "Second Q")
                    {
                        BuffName = "blindmonkqonechaos",
                        IsPlayerBuff = true
                    },
                    r
                });
            BlockedSpells.Add(
                "Leona", new List<Block> { new Block("LeonaShieldOfDaybreakAttack", "Stun Q", true) });
            BlockedSpells.Add("Lissandra", new List<Block> { new Block(N48) { Name = "R" } });
            BlockedSpells.Add("Lulu", new List<Block> { w });
            BlockedSpells.Add("Malphite", new List<Block> { r });
            BlockedSpells.Add("Malzahar", new List<Block> { r });
            BlockedSpells.Add("Maokai", new List<Block> { w });
            BlockedSpells.Add(
                "MasterYi", new List<Block> { q });
            BlockedSpells.Add(
                "Mordekaiser",
                new List<Block> { new Block("mordekaiserqattack2", "Empowered Q", true), r });
            BlockedSpells.Add("Nami", new List<Block> { r });
            BlockedSpells.Add(
                "Nasus", new List<Block> { new Block("NasusQAttack", "Empowered Q", true), w });
            BlockedSpells.Add(
                "Nautilus",
                new List<Block> { new Block("NautilusRavageStrikeAttack", "Empowered", true), r });
            BlockedSpells.Add(
                "Nidalee",
                new List<Block>
                {
                    new Block("NidaleeTakedownAttack", "Cougar Q", true) { ModelName = "nidalee_cougar" },
                    new Block(SpellSlot.W)
                    {
                        ModelName = "nidalee_cougar",
                        BuffName = "nidaleepassivehunted",
                        IsPlayerBuff = true,
                        Name = "Cougar W"
                    }
                });
            BlockedSpells.Add("Nocturne", new List<Block> { r });
            BlockedSpells.Add("Oriana", new List<Block> { w, r });
            BlockedSpells.Add("Pantheon", new List<Block> { w });
            BlockedSpells.Add(
                "Poppy",
                new List<Block>
                {
                    new Block("PoppyPassiveAttack", "Passive Attack", true)
                    {
                        BuffName = "poppypassivebuff",
                        IsSelfBuff = true
                    },
                    e
                });
            BlockedSpells.Add(
                "Quinn", new List<Block> { e });
            BlockedSpells.Add("Rammus", new List<Block> { e });
            BlockedSpells.Add(
                "RekSai",
                new List<Block>
                {
                    new Block("reksaiwburrowed", "W"),
                    new Block("reksaie", "E") { UseContains = false }
                });
            BlockedSpells.Add(
                "Renekton",
                new List<Block>
                {                   
                    new Block("RenektonExecute", "Empowered W", true),
                    new Block("RenektonSuperExecute", "Fury Empowered W", true)
                });
            BlockedSpells.Add(
                "Rengar", new List<Block> { new Block("RengarBasicAttack", "Empowered Q", true) });
            BlockedSpells.Add(
                "Riven",
                new List<Block>
                {
                    new Block(SpellSlot.Q) { Name = "Third Q", BuffName = "RivenTriCleave", IsSelfBuff = true },
                    w, 
                    new Block("rivenizunablade", "Second R")
                    {
                        BuffName = "RivenFengShuiEngine",
                        IsSelfBuff = true,
                    },
                });
            BlockedSpells.Add("Ryze", new List<Block> { w });
            BlockedSpells.Add("Sejuani", new List<Block> { r });
            BlockedSpells.Add(
                "Shyvana", new List<Block> { new Block("ShyvanaDoubleAttackHit", "Empowered Q", true), r });
            BlockedSpells.Add("Singed", new List<Block> { e });
            BlockedSpells.Add("Sion", new List<Block> { q, r });
            BlockedSpells.Add("Shen", new List<Block> { e });
            BlockedSpells.Add("Skarner", new List<Block> { r });
            BlockedSpells.Add("Sona", new List<Block> { r });
            BlockedSpells.Add("Syndra", new List<Block> { r });
            BlockedSpells.Add(
                "Talon",
                new List<Block>
                {
                    new Block("TalonNoxianDiplomacyAttack", "Empowered Q", true),
                    w,
                    e
                });
            BlockedSpells.Add("Tahm Kench", new List<Block> { w });
            BlockedSpells.Add("Thresh", new List<Block> { e });
            BlockedSpells.Add("Tristana", new List<Block> { e, r });
            BlockedSpells.Add(
                "Trundle", new List<Block> { new Block("TrundleQ", "Empowered Q", true), r });
            BlockedSpells.Add(
                "TwistedFate", new List<Block> { new Block("goldcardpreattack", "Gold Card", true), new Block("redcardpreattack", "Red Card", true) });
            BlockedSpells.Add("Udyr", new List<Block> { new Block("UdyrBearAttack", "Bear", true) });
            BlockedSpells.Add("Urgot", new List<Block> { r });
            BlockedSpells.Add(
                "Vayne",
                new List<Block>
                {
                    new Block("VayneBasicAttack", "Silver Bolts")
                    {
                        IsAutoAttack = true,
                        IsPlayerBuff = true,
                        BuffName = "vaynesilvereddebuff",
                        BuffCount = 2
                    },
                    e
                });
            BlockedSpells.Add("Veigar", new List<Block> { r });
            BlockedSpells.Add(
                "Vi",
                new List<Block>
                {
                    new Block("ViBasicAttack", "Empowered W")
                    {
                        IsAutoAttack = true,
                        BuffName = "viwproc",
                        IsPlayerBuff = true,
                        BuffCount = 2
                    },
                    new Block("ViEAttack", "Empowered E") { IsAutoAttack = true },
                    r
                });
            BlockedSpells.Add(
                "Viktor",
                new List<Block> { q, r });
            BlockedSpells.Add(
                "Volibear", new List<Block> { new Block("VolibearQAttack", "Empowered Q", true)});
            BlockedSpells.Add("Wukong", new List<Block> { new Block("WukongQAttack", "Empowered Q", true), e });
            BlockedSpells.Add(
                "XinZhao", new List<Block> { new Block("XinZhaoThrust3", "Empowered Q", true), e, r });
            BlockedSpells.Add("Yasuo", new List<Block> { new Block("yasuoq3", "Whirlwind Q")});
            BlockedSpells.Add(
                "Yorick",
                new List<Block>
                {
                    new Block("yorickbasicattack", "Empowered Q")
                    {
                        IsAutoAttack = true,
                        BuffName = "YorickSpectral",
                        IsSelfBuff = true
                    },
                    e
                });
            BlockedSpells.Add("Zac", new List<Block> { r });
            BlockedSpells.Add("Ziggs", new List<Block> { r });
            BlockedSpells.Add("Zilean", new List<Block> { e });
            BlockedSpells.Add("Zyra", new List<Block> { r });
            #endregion

            var enemies = EntityManager.Heroes.Enemies;

            if (enemies.Any(o => o.ChampionName.Equals("Kalista")))
                Config.Defensive.Add("Oathsworn", new CheckBox("Block Oathsworn Knockup (Kalista R)", true));

            foreach (var unit in enemies)
            {
                if (!Zhonya.BlockedSpells.ContainsKey(unit.ChampionName))
                    continue;

                foreach (var spell in Zhonya.BlockedSpells[unit.ChampionName])
                {
                    var slot = spell.Slot.Equals(48) ? SpellSlot.R : spell.Slot;
                    Config.Defensive.Add(unit.ChampionName + spell.MenuName, new CheckBox(unit.ChampionName + " - " + spell.DisplayName, false));
                }
            }

            Game.OnUpdate += OnUpdate;
        }

        static void OnUpdate(EventArgs args)
        {
            if (Config.Defensive["enableblock"].Cast<CheckBox>().CurrentValue && Items.Zhonya_Hourglass.IsOwned() && Items.Zhonya_Hourglass.IsReady())
                foreach (var skillshot in Evade.Evade.GetSkillshotsAboutToHit(Player.Instance, (int)(Game.Ping)))
                {
                    var enemy = skillshot.Unit as AIHeroClient;

                    if (enemy == null)
                        continue;

                    var spells = new List<Block>();

                    BlockedSpells.TryGetValue(enemy.ChampionName, out spells);


                    if (spells == null || spells.Count == 0)
                        continue;

                    foreach (var spell in spells)
                    {
                        var item = Config.Defensive[enemy.ChampionName + spell.MenuName];

                        if (item == null || item.Cast<ComboBox>().CurrentValue != 1)
                            continue;

                        if (!spell.PassesSlotCondition(skillshot.SpellData.Slot))
                            continue;

                        if (!spell.PassesBuffCondition(enemy) || !spell.PassesModelCondition(enemy))
                            continue;
                        if (!spell.PassesSpellCondition(skillshot.SpellData.SpellName))
                            continue;
                        if (Config.Defensive["enableblock"].Cast<CheckBox>().CurrentValue)
                        {
                            if (Items.Zhonya_Hourglass.IsOwned() && Items.Zhonya_Hourglass.IsReady())
                                Items.Zhonya_Hourglass.Cast();
                        }
                    }
                }
            var time = Config.Defensive["predtime"].Cast<Slider>().CurrentValue * 10;
            var PredictionHealth = Prediction.Health.GetPrediction(Player.Instance, time);
            if (Config.Defensive["predtimebool"].Cast<CheckBox>().CurrentValue)
            {
                if (PredictionHealth <= 0)
                {
                    if (Items.Zhonya_Hourglass.IsOwned() && Items.Zhonya_Hourglass.IsReady())
                        Items.Zhonya_Hourglass.Cast();
                }
            }
            if (Config.Defensive["percentusagebool"].Cast<CheckBox>().CurrentValue)
            {
                if (Player.Instance.HealthPercent < Config.Defensive["percentusage"].Cast<Slider>().CurrentValue)
                {
                    if (Items.Zhonya_Hourglass.IsOwned() && Items.Zhonya_Hourglass.IsReady())
                        Items.Zhonya_Hourglass.Cast();
                }
            }
        }

        public static bool Contains(AIHeroClient unit, GameObjectProcessSpellCastEventArgs args)
        {
            var name = unit.ChampionName;
            var slot = args.Slot.Equals(48) ? SpellSlot.R : args.Slot;
            if (args.SData.Name.Equals("KalistaRAllyDash") && Config.Defensive["Oathsworn"].Cast<CheckBox>().CurrentValue)
            {
                return true;
            }
            var spells = new List<Block>();
            BlockedSpells.TryGetValue(name, out spells);

            if (spells == null || spells.Count == 0)
            {
                return false;
            }

            foreach (var spell in
                spells)
            {
                var item = Config.Defensive[name + spell.MenuName];
                if (item == null || !item.Cast<CheckBox>().CurrentValue)
                {
                    continue;
                }

                if (!spell.PassesModelCondition(unit))
                {
                    continue;
                }

                if (!spell.PassesSpellCondition(args.SData.Name))
                {
                    continue;
                }

                if (!spell.PassesBuffCondition(unit))
                {
                    continue;
                }

                if (!spell.PassesSlotCondition(args.Slot))
                {
                    continue;
                }

                if (spell.IsAutoAttack)
                {
                    if (spell.SpellName.Equals("-1"))
                    {
                    }

                    var condition = true;

                    if (unit.ChampionName.Equals("Gnar"))
                    {
                        var buff = ObjectManager.Player.Buffs.FirstOrDefault(b => b.Name.Equals("gnarwproc"));
                        condition = buff != null && buff.Count == 2;
                    }
                    else if (unit.ChampionName.Equals("Rengar"))
                    {
                        condition = unit.Mana.Equals(5);
                    }

                    if (condition)
                    {
                        return true;
                    }

                    continue;
                }

                if (name.Equals("Riven") && args.SData.Name == "RivenTriCleave")
                {
                    return unit.GetBuffCount("RivenTriCleave").Equals(2);
                }

                return true;
            }

            return false;
        }


        public static void Initialize()
        {

        }
    }
    public class Block
    {
        public int BuffCount;
        public string BuffName;
        public string DisplayName;
        public bool Enabled;
        public bool IsAutoAttack;
        public bool IsPlayerBuff;
        public bool IsSelfBuff;
        public string MenuName;
        public string ModelName;
        public string Name;
        public SpellSlot Slot = SpellSlot.Unknown;
        public string SpellName;
        public bool UseContains = true;

        public Block(string spellName, string displayName, bool isAutoAttack = false, bool enabled = true)
        {
            SpellName = spellName.ToLower();
            Name = displayName;
            IsAutoAttack = isAutoAttack;
            SetMenuData();
        }

        public Block(SpellSlot slot)
        {
            Slot = slot;
            SetMenuData();
        }

        public bool HasBuffCondition
        {
            get { return !string.IsNullOrWhiteSpace(BuffName); }
        }

        public bool HasModelCondition
        {
            get { return !string.IsNullOrWhiteSpace(ModelName); }
        }

        public bool HasSpellCondition
        {
            get { return !string.IsNullOrWhiteSpace(SpellName); }
        }

        public bool HasSlotCondition
        {
            get { return !Slot.Equals(SpellSlot.Unknown); }
        }

        private void SetMenuData()
        {
            var menuName = "";
            var display = "Block ";

            if (HasSpellCondition)
            {
                menuName += SpellName;
            }
            else if (!Slot.Equals(SpellSlot.Unknown))
            {
                menuName += Slot;
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                display += " " + Name;
            }
            else if (!Slot.Equals(SpellSlot.Unknown))
            {
                display += Slot;
            }

            if (IsAutoAttack)
            {
                display += " AA";
            }

            MenuName = menuName;
            DisplayName = display;
        }

        public bool PassesModelCondition(AIHeroClient hero)
        {
            return !HasModelCondition || hero.BaseSkinName.Equals(ModelName);
        }

        public bool PassesBuffCondition(AIHeroClient hero)
        {
            if (!HasBuffCondition)
            {
                return true;
            }

            var unit = IsSelfBuff ? hero : ObjectManager.Player;

            if (BuffName.Equals("-1"))
            {
                foreach (var buff in unit.Buffs)
                {
                }
            }
            return BuffCount == 0 ? unit.HasBuff(BuffName) : unit.GetBuffCount(BuffName).Equals(BuffCount);
        }

        public bool PassesSlotCondition(SpellSlot slot)
        {
            return !HasSlotCondition || Slot.Equals(slot);
        }

        public bool PassesSpellCondition(string spell)
        {
            spell = spell.ToLower();
            if (UseContains)
            {
                return !HasSpellCondition || spell.ToLower().Contains(SpellName);
            }
            return !HasSpellCondition || spell.ToLower().Equals(SpellName);
        }
    }
    class Defense
    {
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var unit = sender as AIHeroClient;
            if (unit == null || !unit.IsValid)
            {
                return;
            }

            if (!unit.IsEnemy || !Items.Zhonya_Hourglass.IsOwned() || !Items.Zhonya_Hourglass.IsReady())
            {
                return;
            }

            // spell handled by evade
            if (UBActivator.Evade.SpellDatabase.GetByName(args.SData.Name) != null && !Config.Defensive["enableblock"].Cast<CheckBox>().CurrentValue)
                return;

            if (!Zhonya.Contains(unit, args))
                return;

            if (args.End.Distance(Player.Instance) == 0)
                return;

            var type = args.SData.TargettingType;

            if (unit.ChampionName.Equals("Caitlyn") && args.Slot == SpellSlot.R)
            {
                Core.DelayAction(() => Items.Zhonya_Hourglass.Cast(),
                    (int)(args.Start.Distance(Player.Instance) / args.SData.MissileSpeed * 1000) -
                    (int)(args.End.Distance(Player.Instance) / args.SData.MissileSpeed) - 500);
            }
            if (unit.ChampionName.Equals("Zyra"))
            {
                Core.DelayAction(() => Items.Zhonya_Hourglass.Cast(),
                    (int)(args.Start.Distance(Player.Instance) / args.SData.MissileSpeed * 1000) -
                    (int)(args.End.Distance(Player.Instance) / args.SData.MissileSpeed) - 500);
            }
            if (args.End.Distance(Player.Instance) < 250)
            {
                if (unit.ChampionName.Equals("Ashe"))
                {
                    Core.DelayAction(() => Items.Zhonya_Hourglass.Cast(),
                        (int)(args.Start.Distance(Player.Instance) / args.SData.MissileSpeed * 1000) -
                        (int)args.End.Distance(Player.Instance));
                    return;
                }
                else if (unit.ChampionName.Equals("Varus") || unit.ChampionName.Equals("TahmKench") ||
                         unit.ChampionName.Equals("Lux"))
                {
                    Core.DelayAction(() => Items.Zhonya_Hourglass.Cast(),
                        (int)(args.Start.Distance(Player.Instance) / args.SData.MissileSpeed * 1000) -
                        (int)(args.End.Distance(Player.Instance) / args.SData.MissileSpeed) - 500);
                }
                else if (unit.ChampionName.Equals("Amumu"))
                {
                    if (sender.Distance(Player.Instance) < 1100)
                        Core.DelayAction(() => Items.Zhonya_Hourglass.Cast(),
                            (int)(args.Start.Distance(Player.Instance) / args.SData.MissileSpeed * 1000) -
                            (int)(args.End.Distance(Player.Instance) / args.SData.MissileSpeed) - 500);
                }
            }

            if (args.Target != null && type.Equals(SpellDataTargetType.Unit))
            {
                if (!args.Target.IsMe ||
                    (args.Target.Name.Equals("Barrel") && args.Target.Distance(Player.Instance) > 200 &&
                     args.Target.Distance(Player.Instance) < 400))
                {
                    return;
                }

                if ((unit.ChampionName.Equals("Nautilus") || unit.ChampionName.Equals("Veigar") || unit.ChampionName.Equals("Syndra") ||
                    (unit.ChampionName.Equals("Caitlyn"))) && args.Slot.Equals(SpellSlot.R))
                {
                    var d = unit.Distance(Player.Instance);
                    var travelTime = d / args.SData.MissileSpeed;
                    var delay = travelTime * 1000 + 1150;
                    Core.DelayAction(() => Items.Zhonya_Hourglass.Cast(), (int)delay);
                    return;
                }
                Items.Zhonya_Hourglass.Cast();
            }

            if (type.Equals(SpellDataTargetType.Unit))
            {
                if (unit.ChampionName.Equals("Bard") && args.End.Distance(Player.Instance) < 300)
                {
                    Core.DelayAction(() => Items.Zhonya_Hourglass.Cast(), 400 + (int)(unit.Distance(Player.Instance) / 7f));
                }
                else if (unit.ChampionName.Equals("Riven") && args.End.Distance(Player.Instance) < 260)
                {
                    Items.Zhonya_Hourglass.Cast();
                }
                else
                {
                    Items.Zhonya_Hourglass.Cast();
                }
            }
            else if (type.Equals(SpellDataTargetType.LocationAoe) &&
                     args.End.Distance(Player.Instance) < args.SData.CastRadius)
            {
                // annie moving tibbers
                if (unit.ChampionName.Equals("Annie") && args.Slot.Equals(SpellSlot.R))
                {
                    return;
                }
                Items.Zhonya_Hourglass.Cast();
            }
            else if (type.Equals(SpellDataTargetType.Cone) &&
                     args.End.Distance(Player.Instance) < args.SData.CastRadius)
            {
                Items.Zhonya_Hourglass.Cast();
            }
            else if (type.Equals(SpellDataTargetType.SelfAoe) || type.Equals(SpellDataTargetType.Self))
            {
                var d = args.End.Distance(Player.Instance.ServerPosition);
                var p = args.SData.CastRadius > 5000 ? args.SData.CastRange : args.SData.CastRadius;
                if (d < p)
                    Items.Zhonya_Hourglass.Cast();
            }
        }
        public static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (!sender.IsMe) return;
            if (Config.Defensive["enableblock"].Cast<CheckBox>().CurrentValue) return;
            if (args.Buff.Name.Contains("vladimirhemoplaguedebuff")
             || args.Buff.Name.Contains("karthusfallenone")
             || args.Buff.Name.Contains("soulshackles")
             || args.Buff.Name.Contains("zedultexecute")
             || args.Buff.Name.Contains("fizzmarinerdoombomb"))
            {
                if (Items.Zhonya_Hourglass.IsOwned() && Items.Zhonya_Hourglass.IsReady())
                    Items.Zhonya_Hourglass.Cast();
            }
        }
    }
    #endregion

    #region Other Defense Item
    class Defensive
    {
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is AIHeroClient))
            {
                return;
            }
            var target = (AIHeroClient)args.Target;

            if ((sender is AIHeroClient) && sender.IsEnemy && target != null && target.IsAlly)
            {
                var caster = (AIHeroClient)sender;
                if (target.IsValidTarget(Items.Face_of_the_moutain.Range)
                    && Config.Defensive["Face"].Cast<CheckBox>().CurrentValue)
                {
                    var ShieldValue = Player.Instance.MaxHealth * 0.1 * Config.FOTMSlider.CurrentValue / 100;
                    if (caster.GetSpellDamage(target, args.Slot) >= ShieldValue)
                    {
                        if (Config.Defensive["Face" + target.ChampionName].Cast<CheckBox>().CurrentValue)
                        {
                            if (Items.Face_of_the_moutain.IsOwned() && Items.Face_of_the_moutain.IsReady())
                            {
                                Items.Face_of_the_moutain.Cast(target);
                            }
                        }
                    }
                }

                if (target.IsValidTarget(Items.Locket_of_the_iron_Solari.Range)
                    && Config.Defensive["Solari"].Cast<CheckBox>().CurrentValue)
                {
                    var ShieldValue = (75 + 15 * Player.Instance.Level) * Config.SolariSlider.CurrentValue / 100;
                    if (caster.GetSpellDamage(target, args.Slot) >= ShieldValue)
                    {
                        if (Config.Defensive["Solari" + target.ChampionName].Cast<CheckBox>().CurrentValue)
                        {
                            if (Items.Locket_of_the_iron_Solari.IsOwned() && Items.Locket_of_the_iron_Solari.IsReady())
                            {
                                Items.Locket_of_the_iron_Solari.Cast();
                            }
                        }
                    }
                }

                if (!Extensions.ChampNoMana)
                {
                    if (target.IsMe && Config.Defensive["Ser"].Cast<CheckBox>().CurrentValue)
                    {
                        var ShieldValue = (150 + 0.2 * Player.Instance.Mana) * Config.SerSlider.CurrentValue / 100;
                        if (caster.GetSpellDamage(target, args.Slot) >= ShieldValue)
                        {
                            if (Items.Seraph_s_Embrace.IsOwned() && Items.Seraph_s_Embrace.IsReady())
                            {
                                Items.Seraph_s_Embrace.Cast();
                            }
                        }
                    }
                }
            }
        }
        public static void Obj_AI_Base_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is AIHeroClient))
            {
                return;
            }
            var target = (AIHeroClient)args.Target;

            if ((sender is AIHeroClient || sender is Obj_AI_Turret) && sender.IsEnemy && target != null && target.IsAlly)
            {
                if (target.IsValidTarget(Items.Face_of_the_moutain.Range)
                    && Config.Defensive["Face"].Cast<CheckBox>().CurrentValue)
                {
                    var ShieldValue = Player.Instance.MaxHealth * 0.1 * Config.FOTMSlider.CurrentValue / 100;
                    if (sender.GetAutoAttackDamage(target, true) >= ShieldValue)
                    {
                        if (Config.Defensive["Face" + target.ChampionName].Cast<CheckBox>().CurrentValue)
                        {
                            if (Items.Face_of_the_moutain.IsOwned() && Items.Face_of_the_moutain.IsReady())
                            {
                                Items.Face_of_the_moutain.Cast(target);
                            }
                        }
                    }
                }

                if (target.IsValidTarget(Items.Locket_of_the_iron_Solari.Range)
                    && Config.Defensive["Solari"].Cast<CheckBox>().CurrentValue)
                {
                    var ShieldValue = (75 + 15 * Player.Instance.Level) * Config.SolariSlider.CurrentValue / 100;
                    if (sender.GetAutoAttackDamage(target, true) >= ShieldValue)
                    {
                        if (Config.Defensive["Solari" + target.ChampionName].Cast<CheckBox>().CurrentValue)
                        {
                            if (Items.Locket_of_the_iron_Solari.IsOwned() && Items.Locket_of_the_iron_Solari.IsReady())
                            {
                                Items.Locket_of_the_iron_Solari.Cast();
                            }
                        }
                    }
                }

                if (!Extensions.ChampNoMana)
                {
                    if (target.IsMe && Config.Defensive["Ser"].Cast<CheckBox>().CurrentValue)
                    {
                        var ShieldValue = (150 + 0.2 * Player.Instance.Mana) * Config.SerSlider.CurrentValue / 100;
                        if (sender.GetAutoAttackDamage(target, true) >= ShieldValue)
                        {
                            if (Items.Seraph_s_Embrace.IsOwned() && Items.Seraph_s_Embrace.IsReady())
                            {
                                Items.Seraph_s_Embrace.Cast();
                            }
                        }
                    }
                }
            }
        }
        public static void OnTick()
        {
            if (!Extensions.ChampNoMana && Config.Defensive["Ser"].Cast<CheckBox>().CurrentValue)
            {
                var predMissingHP = Player.Instance.Health - Prediction.Health.GetPrediction(Player.Instance, 2500);
                var ShieldValue = (150 + 0.2 * Player.Instance.Mana) * Config.SerSlider.CurrentValue / 100;
                if (predMissingHP >= ShieldValue || Player.Instance.HealthPercent <= 20 || Prediction.Health.GetPrediction(Player.Instance, 2500) <= 0)
                {
                    if (Items.Seraph_s_Embrace.IsOwned() && Items.Seraph_s_Embrace.IsReady())
                    {
                        Items.Seraph_s_Embrace.Cast();
                    }
                }
            }
            if (Items.Face_of_the_moutain.IsOwned() || Items.Locket_of_the_iron_Solari.IsOwned())
            {
                foreach (var Ally in EntityManager.Allies.Where(x => x.IsInRange(Player.Instance, Items.Face_of_the_moutain.Range) && Config.Defensive["Face" + x.BaseSkinName].Cast<CheckBox>().CurrentValue))
                {
                    if (Ally.IsValid && Config.Defensive["Face"].Cast<CheckBox>().CurrentValue)
                    {
                        var predMissingHP = Ally.Health - Prediction.Health.GetPrediction(Ally, 2500);
                        var ShieldValue = Player.Instance.MaxHealth * 0.1 * Config.FOTMSlider.CurrentValue / 100;
                        if (predMissingHP >= ShieldValue || Ally.HealthPercent <= 20 || Prediction.Health.GetPrediction(Player.Instance, 2500) <= 0)
                        {
                            if (Items.Face_of_the_moutain.IsOwned() && Items.Face_of_the_moutain.IsReady())
                            {
                                Items.Face_of_the_moutain.Cast(Ally);
                            }
                        }

                    }
                }
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    if (Player.Instance.CountAlliesInRange(Items.Locket_of_the_iron_Solari.Range) >= 4 && Player.Instance.CountEnemiesInRange(1300) > 0)
                    {
                        if (Items.Locket_of_the_iron_Solari.IsOwned() && Items.Locket_of_the_iron_Solari.IsReady())
                        {
                            Items.Locket_of_the_iron_Solari.Cast();
                        }
                    }
                }
            }
        }
    }

    #endregion
}
