using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UBAnivia
{
    class Spells
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Active QActive { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Skillshot R { get; private set; }
        public static Spell.Skillshot RMax { get; private set; }
        public static Spell.Active ROff { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1150, SkillShotType.Linear, 250, 850, 110)
            {
                    AllowedCollisionCount = int.MaxValue
            };

            QActive = new Spell.Active(SpellSlot.Q, 230, DamageType.Magical);

            W = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Circular, 600, int.MaxValue, 100);

            E = new Spell.Targeted(SpellSlot.E, 650);

            R = new Spell.Skillshot(SpellSlot.R, 750, SkillShotType.Circular, 50, int.MaxValue, 200);

            RMax = new Spell.Skillshot(SpellSlot.R, 750, SkillShotType.Circular, 3000, int.MaxValue, 400);

            ROff = new Spell.Active(SpellSlot.R, 750, DamageType.Magical);

        }
    }
}
