using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Notifications;
using SharpDX;

namespace UBTaliyah
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Taliyah") return;

            Config.Dattenosa();
            Spells.InitSpells();
            InitEvents();
        }
        private static void InitEvents()
        {
            if (Config.DrawMenu.Checked("notif") && Config.DrawMenu.Checked("draw"))
            {
                var notStart = new SimpleNotification("UBTaliyah Load Status", "UBTaliyah sucessfully loaded.");
                Notifications.Show(notStart, 5000);
            }

            Game.OnTick += GameOnTick;
            Game.OnTick += Spells.Update;
            Game.OnUpdate += Mode.AutoHarass;
            Game.OnUpdate += Mode.Killsteal;

            GameObject.OnCreate += Obj_Manager.Obj_GeneralParticleEmitter_OnCreate;
            GameObject.OnDelete += Obj_Manager.Obj_GeneralParticleEmitter_OnDelete;

            Gapcloser.OnGapcloser += Mode.Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Mode.Interrupter_OnInterruptableSpell;

            Drawing.OnDraw += OnDraw;
            Drawing.OnEndScene += Damages.Damage_Indicator;            

            Orbwalker.OnUnkillableMinion += Mode.On_Unkillable_Minion;
            
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
            if (Config.DrawMenu.Checked("Qdr"))
            {
                Circle.Draw(Spells.Q.IsLearned ? Color.HotPink : Color.Zero, Spells.Q.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("Wdr"))
            {
                Circle.Draw(Spells.W.IsLearned ? Color.Yellow : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("Edr"))
            {
                Circle.Draw(Spells.E.IsLearned ? Color.AliceBlue : Color.Zero, Spells.E.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("Rdr"))
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Green : Color.Zero, Spells.R.Range, Player.Instance.Position);
            }
        }
    }
}
