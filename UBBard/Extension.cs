using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UBBard
{
    static class Extension
    {
        public static string AddonName = "UBBard";
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
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "kindredrnodeathbuff"))
            {
                return true;
            }
            if (target.HasBuffOfType(BuffType.Invulnerability))
            {
                return true;
            }
            return target.IsInvulnerable || target.HasSpellShield();
        }
        public static bool HasSpellShield(this AIHeroClient target)
        {
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "bansheesveil"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "SivirE"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "NocturneW"))
            {
                return true;
            }
            return target.HasBuffOfType(BuffType.SpellShield) || target.HasBuffOfType(BuffType.SpellImmunity);
        }
        public static bool WillStun(this PredictionResult pred)
        {
            var EndPoint = Player.Instance.Position.Extend(pred.CastPosition, Spells.Q.Range).To3D();
            var Rectangle = new Geometry.Polygon.Rectangle(Player.Instance.Position, EndPoint, Spells.Q.Width);
            var Count = ObjectManager.Get<Obj_AI_Base>().Where(x => x.IsValid && Rectangle.IsInside(x)).Count();
            if (pred.CollisionObjects.Count() == 1)
            {
                return true;
            }
            if (Count >= 2)
            {
                return true;
            }
            var Distance = Spells.Q.Range - Player.Instance.Distance(pred.UnitPosition);
            float checkDistance = Distance / 50f;
            var Dir = (pred.UnitPosition - ObjectManager.Player.ServerPosition).Normalized();
            for (int i = 0; i < 50; i++)
            {
                Vector3 finalPosition = pred.UnitPosition + (Dir * checkDistance * i);
                if (finalPosition.IsWall())
                {
                    return true;
                }
            }
            return false;
        }
        public static bool WillStun(this AIHeroClient hero)
        {
            var pred = Spells.Q.GetPrediction(hero);
            return pred.WillStun();
        }
        public static Vector2[] Intersection_Of_2Circle(Vector2 center1, float radius1, Vector2 center2, float radius2)
        {
            var Distance = center1.Distance(center2);
            if (Distance > radius1 + radius2 || (Distance <= Math.Abs(radius1 - radius2)))
            {
                return new Vector2[] { };
            }

            var A = (radius1 * radius1 - radius2 * radius2 + Distance * Distance) / (2 * Distance);
            var H = (float)Math.Sqrt(radius1 * radius1 - A * A);
            var Direction = (center2 - center1).Normalized();
            var PA = center1 + A * Direction;
            var Loc1 = PA + H * Direction.Perpendicular();
            var Loc2 = PA - H * Direction.Perpendicular();
            return new[] { Loc1, Loc2 };
        }
        public static int GetPriority(this AIHeroClient champ, bool FromMenu = false)
        {
            var ChampionName = champ.ChampionName;
            string[] priorities1 =
            {
                "Alistar", "Amumu", "Bard", "Blitzcrank", "Braum", "Cho'Gath", "Dr. Mundo", "Garen", "Gnar",
                "Hecarim", "Janna", "Jarvan IV", "Leona", "Lulu", "Malphite", "Nami", "Nasus", "Nautilus", "Nunu",
                "Olaf", "Rammus", "Renekton", "Sejuani", "Shen", "Shyvana", "Singed", "Sion", "Skarner", "Sona",
                "Soraka", "Taric", "Thresh", "Volibear", "Warwick", "MonkeyKing", "Yorick", "Zac", "Zyra", "Zilean", "Tahm Kench"
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


            if (FromMenu)
            {
                return Config.AutoHeal.GetValue("priority" + ChampionName);
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
            return 1;
        }
    }
}
