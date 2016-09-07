using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UBLucian
{
    class Spells
    {
        public static Spell.Targeted Q { get; private set; }
        public static Spell.Skillshot Q2 { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Skillshot R { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 750);

            Q2 = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 450, int.MaxValue, 75)
            {
                AllowedCollisionCount = int.MaxValue,
            }; 

            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Linear, 300, 1600, 80);

            E = new Spell.Skillshot(SpellSlot.E, 475, SkillShotType.Linear, 100, 1350)
            {
                AllowedCollisionCount = int.MaxValue,
            };

            R = new Spell.Skillshot(SpellSlot.R, 1200, SkillShotType.Circular, 250, 2800, 110);

        }
    }
}
