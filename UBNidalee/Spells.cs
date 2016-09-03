using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UBNidalee
{
    class Spells
    {
        /// <summary>
        /// Javelin Toss
        /// </summary>
        public static Spell.Skillshot Q { get; private set; }

        /// <summary>
        /// Takedown
        /// </summary>
        public static Spell.Active Q2 { get; private set; }

        /// <summary>
        /// Bushwhack
        /// </summary>
        public static Spell.Skillshot W { get; private set; }

        /// <summary>
        /// Pounce
        /// </summary>
        public static Spell.Skillshot W2 { get; private set; }

        /// <summary>
        /// Pounce(With Passive)
        /// </summary>
        public static Spell.Skillshot W2p { get; private set; }

        /// <summary>
        /// Pouce for Auto system
        /// </summary>
        public static Spell.Skillshot W2f { get; private set; }

        /// <summary>
        /// Primal Surge
        /// </summary>
        public static Spell.Targeted E { get; private set; }

        /// <summary>
        /// Swipe
        /// </summary>
        public static Spell.Skillshot E2 { get; private set; }

        /// <summary>
        /// Aspect of the Cougar
        /// </summary>
        public static Spell.Active R { get; private set; }
        public static Spell.Targeted Smite { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }



        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1500, SkillShotType.Linear, 250, 1300, 55);
            Q2 = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 750, int.MaxValue, 40);
            W2 = new Spell.Skillshot(SpellSlot.W, 375, SkillShotType.Linear, 250, 1500, 75)
            {
                AllowedCollisionCount = int.MaxValue
            };
            W2p = new Spell.Skillshot(SpellSlot.W, 750, SkillShotType.Linear, 250, 1800, 75)
            {
                AllowedCollisionCount = int.MaxValue
            };
            W2f = new Spell.Skillshot(SpellSlot.W, 475, SkillShotType.Linear, 250, 1800, 75)
            {
                AllowedCollisionCount = int.MaxValue
            };
            E = new Spell.Targeted(SpellSlot.E, 600);
            E2 = new Spell.Skillshot(SpellSlot.E, 300, SkillShotType.Cone, 250, int.MaxValue, (int)(15.00 * Math.PI / 180.00));
            R = new Spell.Active(SpellSlot.R);
            var slot = Player.Instance.GetSpellSlotFromName("summonerdot");
            if (slot != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(slot, 600);
            }
            slot = Player.Instance.GetSpellSlotFromName("summonersmite");
            if (slot != SpellSlot.Unknown)
            {
                Smite = new Spell.Targeted(slot, 500);
            }
        }
    }
}
