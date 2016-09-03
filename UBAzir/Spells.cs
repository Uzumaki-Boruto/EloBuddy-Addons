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
        public static Spell.Skillshot Q_in_Flee { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot WLine { get; private set; }
        public static Spell.Targeted WFocus { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Active RFake { get; private set; }
        public static Spell.Skillshot R { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }
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
            Q_in_Flee = new Spell.Skillshot(SpellSlot.Q, 1800, SkillShotType.Linear, 250, 1200, 70)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            W = new Spell.Skillshot(SpellSlot.W, 450, SkillShotType.Circular, 250, int.MaxValue, 375);

            WLine = new Spell.Skillshot(SpellSlot.W, 425, SkillShotType.Linear, (int)Orbwalker.AttackDelay, int.MaxValue, 50)
            {
                AllowedCollisionCount = int.MaxValue
            };
            WFocus = new Spell.Targeted(SpellSlot.W, 375);
            E = new Spell.Skillshot(SpellSlot.E, 1100, SkillShotType.Linear, 250, 1000, 150);
            RFake = new Spell.Active(SpellSlot.R);
            R = new Spell.Skillshot(SpellSlot.R, 450, SkillShotType.Linear, 350, 1000, (new int[] { 0, 532, 665, 798 }[RFake.Level]))
            {
                AllowedCollisionCount = int.MaxValue
            };
            if (HasSpell("summonerdot"))
                Ignite = new Spell.Targeted(ObjectManager.Player.GetSpellSlotFromName("summonerdot"), 600);
            if (HasSpell("summonerflash"))
                Flash = new Spell.Skillshot(ObjectManager.Player.GetSpellSlotFromName("summonerflash"), 425, SkillShotType.Circular, 0, int.MaxValue, 300);
        }
    }
}
