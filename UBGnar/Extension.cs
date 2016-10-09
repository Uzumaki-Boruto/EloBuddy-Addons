using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UBGnar
{
    static class Extension
    {
        public static string AddonName = "UBGnar";
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
            if (target.IsInvulnerable)
            {
                return true;
            }
            return target.HasUndyingBuff();
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
        public static bool IsUnderEnemyTurret(this Vector3 Pos)
        {
            return EntityManager.Turrets.AllTurrets.Any((Obj_AI_Turret turret) => Player.Instance.Team != turret.Team && Pos.Distance(turret) <= turret.GetAutoAttackRange() && turret.IsValid);
        }
        public static Vector3 EEndPred(this Vector3 point)
        {
            return Player.Instance.Position.Extend(point, Player.Instance.Distance(point) + Spells.ETiny.Range).To3DWorld();
        }
        public static bool HasWTinyBuff(this Obj_AI_Base target)
        {
            return target.HasBuff("GnarWproc");
        }
        public static bool IsTiny
        {
            get
            {
                return Player.Instance.Model == "Gnar";
            }
        }
        public static bool IsPrepareToBig
        {
            get
            {
                return IsTiny && (Player.Instance.MaxMana - Player.Instance.Mana == 0 || Player.Instance.HasBuff("gnartransformsoon"));
            }
        }
    }
}
