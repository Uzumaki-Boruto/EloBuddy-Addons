using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;

namespace UBNidalee
{
    class Event
    {
        public static bool Humanform
        {
            get
            {
                return Player.Instance.Model == "Nidalee";
            }
        }
        public static bool IsPassive(Obj_AI_Base target)
        {
            if (target != null)
            {
                return target.HasBuff("nidaleepassivehunted");
            }
            else return false;
        }
        public static bool IsReady(float time, float extra = 0f)
        {
            return time <= extra;
        }       
        public static Dictionary<string, float> CD = new Dictionary<string, float>
        {          
            {"Javelintoss", 0},
            {"Bushwhack", 0},
            {"Primalsurge", 0},
            {"Takedown", 0},
            {"Pounce", 0},
            {"Swipe", 0},
            {"Aspect", 0}
        };
        public static Dictionary<string, float> TimeStore = new Dictionary<string, float>
        {            
            {"Javelintoss", 0},
            {"Bushwhack", 0},
            {"Primalsurge", 0},
            {"Takedown", 0},
            {"Pounce", 0},
            {"Swipe", 0},
            {"Aspect", 0}
        };
        private static List<AIHeroClient> Enemies = new List<AIHeroClient>();
        public static void AutoQ(EventArgs agrs)
        {
            foreach (
                      var enemy in
                          Enemies.Where(
                              enemy => enemy.IsValidTarget(Spells.Q.Range) && Immobilize(enemy)))
            {
                if (enemy != null)
                {
                    var pred = Spells.Q.GetPrediction(enemy);
                    Spells.Q.Cast(pred.UnitPosition);
                    return;
                }
            }
        }
        public static bool Immobilize(AIHeroClient target)
        {
            if (target.HasBuffOfType(BuffType.Stun)
             || target.HasBuffOfType(BuffType.Snare)
             || target.HasBuffOfType(BuffType.Knockup)
             || target.HasBuffOfType(BuffType.Charm)
             || target.HasBuffOfType(BuffType.Fear)
             || target.HasBuffOfType(BuffType.Knockback)
             || target.HasBuffOfType(BuffType.Taunt)
             || target.HasBuffOfType(BuffType.Suppression)
             || target.IsStunned
             && !target.IsRecalling())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Unkillable(AIHeroClient target)
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
        public static bool HasSpellShield(AIHeroClient target)
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
    }
}
