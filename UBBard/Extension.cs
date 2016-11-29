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
            var Count = ObjectManager.Get<Obj_AI_Base>().Where(x => x.IsValidTarget() && Rectangle.IsInside(x)).Count();
            if (pred.CollisionObjects.Any())
            {
                return true;
            }
            if (Count >= 2)
            {
                return true;
            }
            var Distance = Spells.Q.Range - Player.Instance.Distance(pred.UnitPosition);
            float checkDistance = Distance / 50f;
            for (int i = 0; i <= 50; i++)
            {
                Vector3 finalPosition = Player.Instance.Position.Extend(pred.UnitPosition, Player.Instance.Distance(pred.UnitPosition) + i * checkDistance).To3DWorld();
                if (finalPosition.IsWall() || finalPosition.IsBuilding())
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
    }
}
