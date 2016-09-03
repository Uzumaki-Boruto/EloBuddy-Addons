using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using UBSivir.Evade;

namespace UBSivir
{
    public static class _E
    {
        public static readonly Dictionary<string, List<Block>> BlockedSpells =
            new Dictionary<string, List<Block>>();


        static _E()
        {
            const SpellSlot N48 = (SpellSlot)48;

            var q = new Block(SpellSlot.Q);
            var w = new Block(SpellSlot.W);
            var e = new Block(SpellSlot.E);
            var r = new Block(SpellSlot.R);

            BlockedSpells.Add("Aatrox", new List<Block> { r });
            BlockedSpells.Add("Akali", new List<Block> { q, e, r });
            BlockedSpells.Add("Alistar", new List<Block> { q, w });
            BlockedSpells.Add("Anivia", new List<Block> { e });
            BlockedSpells.Add("Annie", new List<Block> { q, w, r });
            BlockedSpells.Add("Amumu", new List<Block> { r });
            BlockedSpells.Add("Ashe", new List<Block> { w });
            BlockedSpells.Add("Azir", new List<Block> { r });
            BlockedSpells.Add("Bard", new List<Block> { r });
            BlockedSpells.Add("Blitzcrank", new List<Block> { new Block("PowerFistAttack", "Power Fist", true), r });
            BlockedSpells.Add("Brand", new List<Block> { e, r });
            BlockedSpells.Add("Braum", new List<Block> { new Block("BraumBasicAttackPassiveOverride", "Stun", true) });
            BlockedSpells.Add("Caitlyn", new List<Block> { r });
            BlockedSpells.Add("Cassiopeia", new List<Block> { r });
            BlockedSpells.Add("Chogath", new List<Block> { r });
            BlockedSpells.Add("Darius", new List<Block> { q, new Block("DariusNoxianTacticsONHAttack", "Empowered W", true), e, r });
            BlockedSpells.Add("Diana", new List<Block> { e, r });          
            BlockedSpells.Add("Ekko", new List<Block> { new Block("EkkoEAttack", "Empowered E", true), new Block("ekkobasicattackp3", "Third Proc Passive", true) });
            BlockedSpells.Add("Elise", new List<Block> { q });
            BlockedSpells.Add("Evelynn", new List<Block> { e, r });
            BlockedSpells.Add("FiddleSticks", new List<Block> { q, w, e });
            BlockedSpells.Add("Fizz", new List<Block> { q, new Block("fizzjumptwo", "Second E") });
            BlockedSpells.Add("Gangplank", new List<Block> { q, new Block((SpellSlot)45) { Name = "Barrel Q" } });
            BlockedSpells.Add("Garen", new List<Block> { new Block("GarenQAttack", "Empowered Q", true), r });
            BlockedSpells.Add("Gnar", new List<Block> { new Block("GnarBasicAttack", "Empowered W", true)
                    {
                        BuffName = "gnarwproc",
                        IsPlayerBuff = true
                    }, r });
            BlockedSpells.Add("Gragas", new List<Block> { new Block("DrunkenRage", "Drunken Rage", true), r });
            BlockedSpells.Add("Hecarim", new List<Block> { new Block("hecarimrampattack", "Empowered E", true), r });
            BlockedSpells.Add("Illaoi", new List<Block> { new Block("illaoiwattack", "Empowered W", true), r });
            BlockedSpells.Add("Irelia", new List<Block> { q, e });
            BlockedSpells.Add("Janna", new List<Block> { w });
            BlockedSpells.Add("JarvanIV",new List<Block> { new Block("JarvanIVMartialCadenceAttack", "Martial Cadence", true), e , r });
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
            BlockedSpells.Add("Jhin", new List<Block> { q, new Block("JhinPassiveAttack", "4th", true) });
            BlockedSpells.Add("Kassadin", new List<Block> { q, new Block("KassadinBasicAttack3", "Empowered W", true) });
            BlockedSpells.Add("Katarina", new List<Block> { q, e });
            BlockedSpells.Add("Kayle", new List<Block> { q });
            BlockedSpells.Add("Kennen", new List<Block> { new Block("KennenMegaProc", "Empowered", true), w });
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
                    new Block("BlindMonkEOne", "First E"),
                    r
                });
            BlockedSpells.Add(
                "Leona", new List<Block> { new Block("LeonaShieldOfDaybreakAttack", "Stun Q", true) });
            BlockedSpells.Add("Lissandra", new List<Block> { new Block(N48) { Name = "R" } });
            BlockedSpells.Add("Lulu", new List<Block> { w });
            BlockedSpells.Add("Malphite", new List<Block> { q, r });
            BlockedSpells.Add("Malzahar", new List<Block> { e, r });
            BlockedSpells.Add("Maokai", new List<Block> { w });
            BlockedSpells.Add(
                "MasterYi", new List<Block> { q });
            BlockedSpells.Add("MissFortune", new List<Block> { q });
            BlockedSpells.Add(
                "Mordekaiser",
                new List<Block> { new Block("mordekaiserqattack2", "Empowered Q", true), e, r });
            BlockedSpells.Add("Nami", new List<Block> { w, r });
            BlockedSpells.Add(
                "Nasus", new List<Block> { new Block("NasusQAttack", "Empowered Q", true), w });
            BlockedSpells.Add(
                "Nautilus",
                new List<Block> { new Block("NautilusRavageStrikeAttack", "Empowered", true), e, r });
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
            BlockedSpells.Add("Nunu", new List<Block> { e });
            BlockedSpells.Add("Oriana", new List<Block> { w, r});
            BlockedSpells.Add("Pantheon", new List<Block> { q, w });
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
                    q,
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
            BlockedSpells.Add("Ryze", new List<Block> { w, e });
            BlockedSpells.Add("Sejuani", new List<Block> { r });
            BlockedSpells.Add("Shaco", new List<Block> { e });
            BlockedSpells.Add(
                "Shyvana", new List<Block> { new Block("ShyvanaDoubleAttackHit", "Empowered Q", true), r });
            BlockedSpells.Add("Singed", new List<Block> { e });
            BlockedSpells.Add("Sion", new List<Block> { q, r });
            BlockedSpells.Add("Shen", new List<Block> { e });
            BlockedSpells.Add("Skarner", new List<Block> { r });
            BlockedSpells.Add("Sona", new List<Block> { q, r });
            BlockedSpells.Add("Syndra", new List<Block> { r });
            BlockedSpells.Add("Swain", new List<Block> { q, e });
            BlockedSpells.Add(
                "Talon",
                new List<Block>
                {
                    new Block("TalonNoxianDiplomacyAttack", "Empowered Q", true),
                    w,
                    e
                });
            BlockedSpells.Add("Taric", new List<Block> { e });
            BlockedSpells.Add("Tahm Kench", new List<Block> { w });
            BlockedSpells.Add("Teemo", new List<Block> { q });
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
            BlockedSpells.Add("Veigar", new List<Block> { e, r });
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
            BlockedSpells.Add("Vladimir", new List<Block> { q });
            BlockedSpells.Add(
                "Volibear", new List<Block> { new Block("VolibearQAttack", "Empowered Q", true), w });
            BlockedSpells.Add("Warwick", new List<Block> { q });
            BlockedSpells.Add("Wukong", new List<Block> { new Block("WukongQAttack", "Empowered Q", true), e });
            BlockedSpells.Add(
                "XinZhao", new List<Block> { new Block("XinZhaoThrust3", "Empowered Q", true), e, r });
            BlockedSpells.Add("Yasuo", new List<Block> { new Block("yasuoq3", "Whirlwind Q"), e });
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
            BlockedSpells.Add("Zac", new List<Block> { w, r });
            BlockedSpells.Add("Ziggs", new List<Block> { r });
            BlockedSpells.Add("Zilean", new List<Block> { e });
            BlockedSpells.Add("Zyra", new List<Block> { r });

            var enemies = EntityManager.Heroes.Enemies;

            if (enemies.Any(o => o.ChampionName.Equals("Kalista")))
                Config.ShieldMenu.Add("Oathsworn", new CheckBox("Block Oathsworn Knockup (Kalista R)", true));

            foreach (var unit in enemies)
            {
                if (!_E.BlockedSpells.ContainsKey(unit.ChampionName))
                    continue;

                foreach (var spell in _E.BlockedSpells[unit.ChampionName])
                {
                    var slot = spell.Slot.Equals(48) ? SpellSlot.R : spell.Slot;
                    Config.ShieldMenu.Add(unit.ChampionName + spell.MenuName, new CheckBox(unit.ChampionName + " - " + spell.DisplayName, true));
                }
            }

            Game.OnUpdate += OnUpdate;
        }

        static void OnUpdate(EventArgs args)
        {           
            if (Config.ShieldMenu["blockSpellsE"].Cast<CheckBox>().CurrentValue == true || Spells.E.IsReady())
            foreach (var skillshot in Evade.Evade.GetSkillshotsAboutToHit(Player.Instance, (int)(Spells.E.CastDelay + Game.Ping /2f)))
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
                    var item = Config.ShieldMenu[enemy.ChampionName + spell.MenuName];

                    if (item == null || !item.Cast<CheckBox>().CurrentValue)
                        continue;

                    if (!spell.PassesSlotCondition(skillshot.SpellData.Slot))
                        continue;

                    if (!spell.PassesBuffCondition(enemy) || !spell.PassesModelCondition(enemy))
                        continue;
                    if (!spell.PassesSpellCondition(skillshot.SpellData.SpellName))
                        continue;
                    if (Config.ShieldMenu["blockSpellsE"].Cast<CheckBox>().CurrentValue == true)
                    {
                        Spells.E.Cast();
                    }
                }
            }
        }

        public static bool Contains(AIHeroClient unit, GameObjectProcessSpellCastEventArgs args)
        {
            var name = unit.ChampionName;
            var slot = args.Slot.Equals(48) ? SpellSlot.R : args.Slot;          
            if (args.SData.Name.Equals("KalistaRAllyDash") && Config.ShieldMenu["Oathsworn"].Cast<CheckBox>().CurrentValue)
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
                var item = Config.ShieldMenu[name + spell.MenuName];
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
                        Console.WriteLine(args.SData.Name);
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
                    Console.WriteLine(buff.Name + " " + buff.Count);
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
}
