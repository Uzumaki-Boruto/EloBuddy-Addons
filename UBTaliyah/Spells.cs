using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;


namespace UBTaliyah
{
    class Spells
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Skillshot R { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 250, 3600, 50)
            {
                AllowedCollisionCount = 0,
            };

            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 500, int.MaxValue, 80)
            {
                AllowedCollisionCount = int.MaxValue,
            };

            E = new Spell.Skillshot(SpellSlot.E, 600, SkillShotType.Cone, 250, 500, 40)
            {
                AllowedCollisionCount = int.MaxValue,
            };

            R = new Spell.Skillshot(SpellSlot.R, 3000, SkillShotType.Linear, 250, 2800, 110);
        }
        public static void Update(EventArgs args)
        {
            if (R.Level == 2 && R.Range < 4500)
            {
                R = new Spell.Skillshot(SpellSlot.R, 4500, SkillShotType.Linear, 250, 2800, 110);
            }
            if (R.Level == 3 && R.Range < 6000)
            {
                R = new Spell.Skillshot(SpellSlot.R, 6000, SkillShotType.Linear, 250, 2800, 110);
            }
        }
    } 
}
