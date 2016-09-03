using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UBRyze
{
    class Spells
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Targeted W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Skillshot R { get; private set; }
        public static Spell.Skillshot R2 { get; private set; }

        public static int[] Range = { 1500, 3000 };


        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 250, 1700, 70);      

            W = new Spell.Targeted(SpellSlot.W, 600);
            
            E = new Spell.Targeted(SpellSlot.E, 600);

            R = new Spell.Skillshot(SpellSlot.R, 1500, SkillShotType.Circular, 2250, int.MaxValue, 475);

            R2 = new Spell.Skillshot(SpellSlot.R, 3000, SkillShotType.Circular, 2250, int.MaxValue, 475);
                           
        }
    }
}
