using System;
using EloBuddy;
using EloBuddy.SDK.Spells;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UBGnar
{
    class Spells
    {
        public static Spell.Skillshot QTiny { get; private set; }
        public static Spell.Skillshot ETiny { get; private set; }

        public static Spell.Skillshot QMega { get; private set; }
        public static Spell.Skillshot WMega { get; private set; }
        public static Spell.Skillshot EMega { get; private set; }


        public static Spell.Skillshot R { get; private set; }

        public static void InitSpells()
        {
            QTiny = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 1200, 60, DamageType.Physical)
            {
                AllowedCollisionCount = Config.Menu.Checked("Q") ? int.MaxValue : 0,
            };
            ETiny = new Spell.Skillshot(SpellSlot.E, 475, SkillShotType.Circular, 0, 2100, 150, DamageType.Physical);

            QMega = new Spell.Skillshot(SpellSlot.Q, 1150, SkillShotType.Linear, 250, 1200, 80, DamageType.Physical);
            WMega = new Spell.Skillshot(SpellSlot.W, 550, SkillShotType.Linear, 500, int.MaxValue, 80, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            EMega = new Spell.Skillshot(SpellSlot.E, 475, SkillShotType.Circular, 0, 2100, 300, DamageType.Physical);

            R = new Spell.Skillshot(SpellSlot.R, 420, SkillShotType.Circular, 500, int.MaxValue);
        }
    }
}
