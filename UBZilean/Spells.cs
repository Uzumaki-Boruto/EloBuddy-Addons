using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UBZilean
{
    class Spells
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Targeted R { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Circular, 350, 2000, 100)
            {
                AllowedCollisionCount = int.MaxValue,
            };

            W = new Spell.Active(SpellSlot.W);

            E = new Spell.Targeted(SpellSlot.E, 750);

            R = new Spell.Targeted(SpellSlot.R, 900);
        }
    }
}
