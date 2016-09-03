using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Notifications;
using SharpDX;
using Colour = System.Drawing.Color;

namespace UBRyze
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Ryze") return;

            var notStart = new SimpleNotification("UBRyze Load Status", "UBRyze sucessfully loaded.");
            Notifications.Show(notStart, 5000);

            Config.Dattenosa();
            Spells.InitSpells();
            InitEvents();
        }
        private static void InitEvents()
        {
            Game.OnTick += GameOnTick;
            Game.OnUpdate += Mode.AutoHarass;
            Game.OnUpdate += Mode.Killsteal;

            Gapcloser.OnGapcloser += Mode.Gapcloser_OnGapcloser;

            if (Config.DrawMenu["draw"].Cast<CheckBox>().CurrentValue)
            {
                Drawing.OnDraw += OnDraw;
                Drawing.OnEndScene += Damages.Damage_Indicator;
            }

            Orbwalker.OnUnkillableMinion += Mode.On_Unkillable_Minion;

            Obj_AI_Base.OnProcessSpellCast += Extensions.Obj_AI_Base_OnProcessSpellCast;
        }
              
        private static void GameOnTick(EventArgs args)
        {
            Orbwalker.ForcedTarget = null;
            if (Player.Instance.IsDead) return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            { Mode.Combo(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            { Mode.Harass(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            { Mode.LaneClear(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            { Mode.Lasthit(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            { Mode.JungleClear(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            { Mode.Flee(); }

        }
        private static void OnDraw(EventArgs args)
        {
            if (Config.DrawMenu["drQ"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.Q.IsLearned ? Color.HotPink : Color.Zero, Spells.Q.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu["drW"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W.IsLearned || Spells.E.IsLearned ? Color.Yellow : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu["drR"].Cast<CheckBox>().CurrentValue && Player.Instance.Level < 11)
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Green : Color.Zero, Spells.R.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu["drR"].Cast<CheckBox>().CurrentValue && Player.Instance.Level >= 11)
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Green : Color.Zero, Spells.R2.Range, Player.Instance.Position);
            }
        }
    }
}
