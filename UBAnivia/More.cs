using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Notifications;
using SharpDX;

namespace UBAnivia
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Anivia") return;

            Config.Dattenosa();
            Spells.InitSpells();
            InitEvents();
        }
        private static void InitEvents()
        {
            if (Config.DrawMenu.Checked("notif") && Config.DrawMenu.Checked("draw"))
            {
                var notStart = new SimpleNotification("UBAnivia Load Status", "UBAnivia sucessfully loaded.");
                Notifications.Show(notStart, 5000);
            }

            Game.OnTick += GameOnTick;
            Game.OnUpdate += Mode.AutoHarass;
            Game.OnUpdate += Mode.Killsteal;
            Game.OnUpdate += Extension.MissileTracker;

            Gapcloser.OnGapcloser += Mode.Gapcloser_OnGapcloser;

            Interrupter.OnInterruptableSpell += Mode.Interrupter_OnInterruptableSpell;

            Drawing.OnDraw += OnDraw;
            Drawing.OnEndScene += Damages.Damage_Indicator;

            Orbwalker.OnUnkillableMinion += Mode.On_Unkillable_Minion;

            Obj_AI_Base.OnProcessSpellCast += Extension.Obj_AI_Base_OnProcessSpellCast;
            Obj_AI_Base.OnCreate += Extension.Obj_AI_Base_OnCreate;
            Obj_AI_Base.OnDelete += Extension.Obj_AI_Base_OnDelete;

            AIHeroClient.OnBuffGain += Extension.Obj_AI_Base_OnBuffGain;
        }

        private static void GameOnTick(EventArgs args)
        {
            if (Player.Instance.IsDead) return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            { Mode.Combo(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            { Mode.Harass(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            { Mode.LaneClear(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            { Mode.JungleClear(); }
        }
        private static void OnDraw(EventArgs args)
        {
            if (!Config.DrawMenu.Checked("draw")) return;
            if (Config.DrawMenu.Checked("drQ"))
            {
                Circle.Draw(Spells.Q.IsLearned ? Color.HotPink : Color.Zero, Spells.Q.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("drW"))
            {
                Circle.Draw(Spells.W.IsLearned ? Color.Yellow : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("drE"))
            {
                Circle.Draw(Spells.E.IsLearned ? Color.Cyan : Color.Zero, Spells.E.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("drR"))
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Green : Color.Zero, Spells.R.Range, Player.Instance.Position);
            }
        }
    }
}
