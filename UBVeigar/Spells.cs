using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;

namespace UBVeigar
{
    class Spells
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Targeted R { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Linear, 250, 2000, 70)
            {
                AllowedCollisionCount = 1,
            };

            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 1350, int.MaxValue, 112)
            {
                AllowedCollisionCount = int.MaxValue,
            };

            E = new Spell.Skillshot(SpellSlot.E, 700, SkillShotType.Circular, 500, int.MaxValue, 375)
            {
                AllowedCollisionCount = int.MaxValue,
            };

            R = new Spell.Targeted(SpellSlot.R, 650);
        }
        public static void ECast(Obj_AI_Base target)
        {
            var pred = E.GetPrediction(target);
            if (pred.UnitPosition.IsInRange(Player.Instance, E.Range + E.Radius) && E.IsReady())
            {
                var Location = Player.Instance.Position.Extend(pred.UnitPosition, Player.Instance.Distance(pred.UnitPosition) - E.Radius).To3DWorld();
                E.Cast(Location);
            }
        }
        public static void ECast(Vector3 Vector)
        {
            if (E.IsReady())
            {
                var Location = Player.Instance.Position.Extend(Vector, Player.Instance.Distance(Vector) - E.Radius).To3DWorld();
                E.Cast(Location);
            }
        }
    }
}
