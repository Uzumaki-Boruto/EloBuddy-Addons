using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UBBard
{
    class Spells
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Skillshot R { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Linear, 250, 1600, 65);

            W = new Spell.Skillshot(SpellSlot.W, 800, SkillShotType.Circular, 250, int.MaxValue, 100);

            E = new Spell.Skillshot(SpellSlot.E, 600, SkillShotType.Linear);

            R = new Spell.Skillshot(SpellSlot.R, 3400, SkillShotType.Circular, 500, int.MaxValue, 2100);
        }
    }
}
