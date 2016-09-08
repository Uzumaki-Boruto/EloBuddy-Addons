using System;
using System.Linq;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;


namespace UBVeigar
{
    public static class Extension
    {
        public struct ELocation
        {
            public int HitNumber;

            public Vector3 CastPosition;
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
        public static bool Immobilize(this AIHeroClient target)
        {
            if (target.HasBuffOfType(BuffType.Stun)
             || target.HasBuffOfType(BuffType.Snare)
             || target.HasBuffOfType(BuffType.Knockup)
             || target.HasBuffOfType(BuffType.Charm)
             || target.HasBuffOfType(BuffType.Fear)
             || target.HasBuffOfType(BuffType.Knockback)
             || target.HasBuffOfType(BuffType.Taunt)
             || target.HasBuffOfType(BuffType.Suppression)
             || target.HasBuffOfType(BuffType.Suppression)
             || target.IsRecalling())
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public static ELocation GetCircularBoxLocation(this Spell.Skillshot spell, IEnumerable<AIHeroClient> entities, float radius, int range, Vector2? sourcePosition = null)
        {
            AIHeroClient[] array = entities.ToArray<AIHeroClient>();
            switch (array.Length)
            {
                case 0:
                    return default(ELocation);
                case 1:
                    return new ELocation
                    {
                        CastPosition = array[0].ServerPosition,
                        HitNumber = 1
                    };
                default:
                    {
                        Vector2 startPos = sourcePosition ?? Player.Instance.ServerPosition.To2D();
                        int num = 0;
                        Vector2 vector = Vector2.Zero;
                        Vector2[] array2 = (from a in array
                                            select a.ServerPosition.To2D() into a
                                            where a.IsInRange(startPos, (float)range)
                                            select a).ToArray<Vector2>();
                        Vector2[] array3 = array2;
                        for (int i = 0; i < array3.Length; i++)
                        {
                            Vector2 pos = array3[i];
                            int num2 = array2.Count((Vector2 a) => a.IsInRange(pos, radius / 2f));
                            if (num2 >= num)
                            {
                                vector = pos;
                                num = num2;
                            }
                        }
                        return new ELocation
                        {
                            CastPosition = vector.To3DWorld(),
                            HitNumber = num
                        };
                    }
            }
        }
        public static bool ShouldWait
        {
            get
            {
                return Orbwalker.LaneClearMinionsList.Count(m => m.Health <= Damage.QDamage(m) + Config.LaneClear.GetValue("Q.waittime") * Player.Instance.GetAutoAttackDamage(m)) >= 2;
            }
        }
    }
}
