using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UBAzir
{
    class Spells
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Skillshot R { get; private set; }
        public static Spell.Skillshot Flash { get; private set; }

        public static bool HasSpell(string s)
        {
            return Player.Spells.FirstOrDefault(o => o.SData.Name.Contains(s)) != null;
        }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 875, SkillShotType.Linear, 250, 1200, 70)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            W = new Spell.Skillshot(SpellSlot.W, 450, SkillShotType.Circular, 250, int.MaxValue, 375);
            E = new Spell.Skillshot(SpellSlot.E, 1100, SkillShotType.Linear, 250, 2000, 150)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            R = new Spell.Skillshot(SpellSlot.R, 450, SkillShotType.Linear, 500, 1000, 220)
            {
                AllowedCollisionCount = int.MaxValue
            };
            if (HasSpell("summonerflash"))
                Flash = new Spell.Skillshot(ObjectManager.Player.GetSpellSlotFromName("summonerflash"), 425, SkillShotType.Circular, 0, int.MaxValue, 300);
        }
        public static void UpdateSpells(EventArgs args)
        {
            var Width = new int[] { 0, 532, 665, 798 }[R.Level];
            if (R.Width < Width)
            {
                R.Width = Width;
                Flash.Width = R.Width;
            }
        }
    }
}
