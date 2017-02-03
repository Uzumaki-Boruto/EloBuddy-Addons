using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;

namespace UBNidalee
{
    class Cooldowncal
    {
        public static void SpellsOnUpdate(EventArgs args)
        {          
            Event.CD["Javelintoss"] = ((Event.TimeStore["Javelintoss"] - Game.Time) > 0)
                ? (Event.TimeStore["Javelintoss"] - Game.Time)
                : 0;

            Event.CD["Bushwhack"] = ((Event.TimeStore["Bushwhack"] - Game.Time) > 0)
                ? (Event.TimeStore["Bushwhack"] - Game.Time)
                : 0;

            Event.CD["Primalsurge"] = ((Event.TimeStore["Primalsurge"] - Game.Time) > 0)
                ? (Event.TimeStore["Primalsurge"] - Game.Time)
                : 0;

            Event.CD["Takedown"] = ((Event.TimeStore["Takedown"] - Game.Time) > 0)
               ? (Event.TimeStore["Takedown"] - Game.Time)
               : 0;

            Event.CD["Pounce"] = ((Event.TimeStore["Pounce"] - Game.Time) > 0)
                ? (Event.TimeStore["Pounce"] - Game.Time)
                : 0;

            Event.CD["Swipe"] = ((Event.TimeStore["Swipe"] - Game.Time) > 0)
                ? (Event.TimeStore["Swipe"] - Game.Time)
                : 0;

            Event.CD["Aspect"] = ((Event.TimeStore["Aspect"] - Game.Time) > 0)
                ? (Event.TimeStore["Aspect"] - Game.Time)
                : 0;
        }
        public static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe && args.SData.Name.ToLower() == "pounce")
            {
                var unit = args.Target as AIHeroClient;
                if (unit.IsValidTarget() && Event.IsPassive(unit))
                    Event.TimeStore["Pounce"] = Game.Time + args.SData.CooldownTime * 0.3f;
                else
                    Event.TimeStore["Pounce"] = Game.Time + args.SData.CooldownTime;
            }

            if (sender.IsMe && args.SData.Name.ToLower() == "swipe")
            {
                Event.TimeStore["Swipe"] = Game.Time + args.SData.CooldownTime;
            }

            if (sender.IsMe && args.SData.Name.ToLower() == "primalsurge")
            {
                Event.TimeStore["Primalsurge"] = Game.Time + args.SData.CooldownTime;
            }

            if (sender.IsMe && args.SData.Name.ToLower() == "bushwhack")
            {
                Event.TimeStore["Bushwhack"] = Game.Time + args.SData.CooldownTime;
            }

            if (sender.IsMe && args.SData.Name.ToLower() == "javelintoss")
            {
                Event.TimeStore["Javelintoss"] = Game.Time + args.SData.CooldownTime;
            }

            if (sender.IsMe && args.SData.Name.ToLower() == "takedown" /*&& Player.Instance.HasBuff("Takedown") && Orbwalker.IsAutoAttacking*/)
            {
                if (!Event.Humanform && !Player.Instance.HasBuff("Takedown"))
                {
                    Event.TimeStore["Takedown"] = Game.Time + args.SData.CooldownTime;
                }
            }
            if (sender.IsMe && args.SData.Name.ToLower() == "aspectofthecougar")
            {
                Event.TimeStore["Aspect"] = Game.Time + args.SData.CooldownTime;
            }
        }
    }
}
