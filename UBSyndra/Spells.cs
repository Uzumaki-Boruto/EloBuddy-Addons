using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UBSyndra
{
    class Spells
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Skillshot QE { get; private set; }
        public static Spell.Targeted R { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 800, SkillShotType.Circular, 500, int.MaxValue, 125)
            {
                AllowedCollisionCount = int.MaxValue,
            };

            W = new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Circular, 350, 1600, 140)
            {
                AllowedCollisionCount = int.MaxValue,
            };

            E = new Spell.Skillshot(SpellSlot.E, 700, SkillShotType.Cone, 250, 2500, 22)
            {
                AllowedCollisionCount = int.MaxValue,
            };

            R = new Spell.Targeted(SpellSlot.R, 675);

            QE = new Spell.Skillshot(SpellSlot.E, 1200, SkillShotType.Linear, 800, 2500, 65)
            {
                AllowedCollisionCount = int.MaxValue,
            };
        }
        public static void UpdateSpells(EventArgs args)
        {
            if (R.Level == 3 && R.Range == 675)
            {
                R = new Spell.Targeted(SpellSlot.R, 750);
            }
            if (E.Level == 5 && E.Width == 22)
            {
                E = new Spell.Skillshot(SpellSlot.E, 700, SkillShotType.Cone, 250, 2500, 33)
                {
                    AllowedCollisionCount = int.MaxValue,
                };
            }
        }
        public static void CastQ(PredictionResult pred)
        {
            if (Q.IsReady())
            {
                Q.Cast(pred.CastPosition);
            }
        }
        public static void Grab()
        {
            var spell = Player.GetSpell(SpellSlot.W).ToggleState;
            if (W.IsReady() && spell == 1 && BallManager.Get_Grab_Shit() != null)
            {
                W.Cast(BallManager.Get_Grab_Shit());
            }
        }
        public static void CastE(PredictionResult pred)
        {
            if (E.IsReady())
            {
                E.Cast(pred.CastPosition);
            }
        }
        public static void ComboEQ()
        {
            var target = TargetSelector.GetTarget(Spells.QE.Range, DamageType.Magical);
            if (target != null && Q.IsReady())
            {
                Extension.QEcomboing = true;
                var pred = QE.GetPrediction(target);
                if (E.IsInRange(pred.UnitPosition))
                {
                    Q.Cast(Player.Instance.Position.Extend(pred.CastPosition, Player.Instance.Distance(pred.CastPosition) - 20f).To3DWorld());
                }
                else
                {
                    Q.Cast(Player.Instance.Position.Extend(pred.CastPosition, Spells.E.Range - 50f).To3DWorld());
                }
            }
        }
        public static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.Slot == SpellSlot.Q && Extension.QEcomboing)
            {
                Spells.E.Cast(args.End);
            }
            if (args.Slot == SpellSlot.W && Extension.QEcomboing && E.IsInRange(args.End))
            {
                Spells.E.Cast(args.End);
            }
            if (args.Slot == SpellSlot.E)
            {
                Extension.QEcomboing = false;
            }
        }
    }
}
