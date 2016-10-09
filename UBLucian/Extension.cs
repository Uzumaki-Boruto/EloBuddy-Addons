using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UBLucian
{
    public static class Extension
    {
        public static string AddonName = "UBLucian";
        public static bool HasPassive;
        public static bool CanCastNextSpell(Menu menu)
        {
            return (!HasPassive && menu.Checked("passive")) || !menu.Checked("passive");
        }
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
            return target.IsInvulnerable;
        }
        public static bool IsActive(this Enum Flag)
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Flag);
        }
        public static void QExtend(this PredictionResult PredictionResult)
        {
            var Rectangle = new Geometry.Polygon.Rectangle(PredictionResult.UnitPosition, Player.Instance.Position, Spells.Q2.Width);
            var Collision = PredictionResult.CollisionObjects.OrderBy(x => x.Distance(Rectangle.Start) + x.Distance(Rectangle.End));           
            var QTarget = new Obj_AI_Base();
            if (Collision != null)
            {
                foreach (var obj in Collision)
                {
                    var Hitbox = new Geometry.Polygon.Rectangle(Player.Instance.Position.To2D(), Player.Instance.Position.Extend(obj.Position, Spells.Q2.Range), 75f);
                    if (Hitbox.IsInside(PredictionResult.CastPosition) && Spells.Q.IsInRange(obj))
                    {
                        QTarget = obj;
                        break;
                    }
                }
            }
            if (QTarget != null || QTarget != new Obj_AI_Base())
            {
                Spells.Q.Cast(QTarget);
            }
        }
        public static bool IsUnderEnemyTurret(this Vector3 Pos)
        {
            return EntityManager.Turrets.AllTurrets.Any((Obj_AI_Turret turret) => Player.Instance.Team != turret.Team && Pos.Distance(turret) <= turret.GetAutoAttackRange() && turret.IsValid);
        }
        public static bool IsUnderEnemyTurret(this Vector2 Pos)
        {
            return Pos.To3DWorld().IsUnderEnemyTurret();
        }
    }
}
