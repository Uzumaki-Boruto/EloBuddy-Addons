using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System;

namespace UBRyze
{
    class Spells
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Targeted W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Skillshot R { get; private set; }

        public static Item Zhonya = new Item(ItemId.Zhonyas_Hourglass);

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 250, 1700, 70);     

            W = new Spell.Targeted(SpellSlot.W, 600);
            
            E = new Spell.Targeted(SpellSlot.E, 600);

            R = new Spell.Skillshot(SpellSlot.R, 1750, SkillShotType.Circular, 2250, int.MaxValue, 475);
                           
        }

        public static void UpdateSpells(EventArgs args)
        {
            if (R.Level == 2 && R.Range == 1750)
            {
                R = new Spell.Skillshot(SpellSlot.R, 3000, SkillShotType.Circular, 2250, int.MaxValue, 475);
            }
        }

    }
}
