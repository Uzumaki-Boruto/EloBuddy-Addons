using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UBZilean
{
    static class Extension
    {
        public static int GetValue(this Menu menu, string id, bool IsSlider = true)
        {
            if (IsSlider)
                return menu[id].Cast<Slider>().CurrentValue;
            else
                return menu[id].Cast<ComboBox>().CurrentValue;
        }
        public static bool Checked(this Menu menu, string id, bool IsCheckBox = true)
        {
            if (IsCheckBox)
                return menu[id].Cast<CheckBox>().CurrentValue;
            else
                return menu[id].Cast<KeyBind>().CurrentValue;
        }
        public static bool HasQBuff(this Obj_AI_Base target)
        {
            if (target.IsEnemy)
            {
                return target.HasBuff("ZileanQEnemyBomb");
            }
            else
            {
                return target.HasBuff("ZileanQAllyBomb");
            }
        }
        public static bool HasEBuff(this Obj_AI_Base target)
        {
            return target.HasBuff("TimeWarp");
        }
        public static bool Unkillable(this AIHeroClient target)
        {
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "UndyingRage"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "ChronoShift"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "JudicatorIntervention"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName.ToLower() == "kindredrnodeathbuff"))
            {
                return true;
            }
            if (target.HasBuffOfType(BuffType.Invulnerability))
            {
                return true;
            }
            return target.IsInvulnerable;
        }
        public static bool IsActive(this Enum Flag)
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Flag);
        }
        public static int GetPriority(this AIHeroClient champ, bool FromMenu = false)
        {
            var ChampionName = champ.ChampionName;
            string[] priorities1 =
            {
                "Alistar", "Amumu", "Bard", "Blitzcrank", "Braum", "Cho'Gath", "Dr. Mundo", "Garen", "Gnar",
                "Hecarim", "Janna", "Jarvan IV", "Leona", "Lulu", "Malphite", "Nami", "Nasus", "Nautilus", "Nunu",
                "Olaf", "Rammus", "Renekton", "Sejuani", "Shen", "Shyvana", "Singed", "Sion", "Skarner", "Sona",
                "Soraka", "Taric", "Thresh", "Volibear", "Warwick", "MonkeyKing", "Yorick", "Zac", "Zyra", "Tahm Kench"
            };

            string[] priorities2 =
            {
                "Aatrox", "Darius", "Elise", "Evelynn", "Galio", "Gangplank", "Gragas", "Irelia", "Jax",
                "Lee Sin", "Maokai", "Morgana", "Nocturne", "Pantheon", "Poppy", "Rengar", "Rumble", "Ryze", "Swain",
                "Trundle", "Tryndamere", "Udyr", "Urgot", "Vi", "XinZhao", "RekSai", "Kled", "Illaoi"
            };

            string[] priorities3 =
            {
                "Akali", "Diana", "Ekko", "Fiddlesticks", "Fiora", "Fizz", "Heimerdinger", "Jayce", "Kassadin",
                "Kayle", "Kha'Zix", "Lissandra", "Mordekaiser", "Nidalee", "Riven", "Shaco", "Vladimir", "Yasuo"
            };

            string[] priorities4 =
            {
                "Ahri", "Anivia", "Annie", "Ashe", "Azir", "Brand", "Caitlyn", "Cassiopeia", "Corki", "Draven",
                "Ezreal", "Graves", "Jinx", "Kalista", "Karma", "Karthus", "Katarina", "Kennen", "KogMaw", "Leblanc",
                "Lucian", "Lux", "Malzahar", "MasterYi", "MissFortune", "Orianna", "Quinn", "Sivir", "Syndra", "Talon",
                "Teemo", "Tristana", "TwistedFate", "Twitch", "Varus", "Vayne", "Veigar", "VelKoz", "Viktor", "Xerath",
                "Zed", "Ziggs", "Kindred", "Jhin", "Aurelion Sol", "Taliyah"
            };

            string priority5 = "Zilean";

            if (FromMenu)
            {
                return Config.RMenu.GetValue("priority" + ChampionName);
            }
            if (priorities1.Contains(ChampionName))
            {
                return 1;
            }
            if (priorities2.Contains(ChampionName))
            {
                return 2;
            }
            if (priorities3.Contains(ChampionName))
            {
                return 3;
            }
            if (priorities4.Contains(ChampionName))
            {
                return 4;
            }
            return priority5.Contains(ChampionName) ? 5 : 1;
        }
        public static AIHeroClient[] Superman
        {
            get
            {
                return EntityManager.Heroes.Allies.Where(x => Spells.Q.IsInRange(x) && x.HasQBuff()).ToArray();
            }
        }
        public static AIHeroClient[] Batman
        {
            get
            {
                return EntityManager.Heroes.Enemies.Where(x => Spells.Q.IsInRange(x) && x.HasQBuff()).ToArray();
            }
        }
        public static AIHeroClient[] League_Of_Bomber
        {
            get
            {
                return EntityManager.Heroes.AllHeroes.Where(x => Spells.Q.IsInRange(x) && x.HasQBuff()).ToArray();
            }
        }
    }
}
