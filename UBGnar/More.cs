using System;
using System.Linq;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Notifications;
using SharpDX;
using Colour = System.Drawing.Color;

namespace UBGnar
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Gnar") return;

            Config.Dattenosa();
            Spells.InitSpells();
            InitEvents();
        }
        private static void InitEvents()
        {
            if (Config.DrawMenu.Checked("notif") && Config.DrawMenu.Checked("draw"))
            {
                var notStart = new SimpleNotification("UBGnar Load Status", "UBGnar sucessfully loaded.");
                Notifications.Show(notStart, 5000);
            }
            Game.OnTick += GameOnTick;
            Game.OnUpdate += Mode.AutoHarass;
            Game.OnUpdate += Mode.Killsteal;
            Game.OnUpdate += Mode.CatchQ;

            Obj_GeneralParticleEmitter.OnCreate += Mode.Obj_GeneralParticleEmitter_OnCreate;
            Obj_GeneralParticleEmitter.OnDelete += Mode.Obj_GeneralParticleEmitter_OnDelete;
            MissileClient.OnCreate += Mode.MissileClient_OnCreate;
            MissileClient.OnDelete += Mode.MissileClient_OnDelete;

            Gapcloser.OnGapcloser += Mode.Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Mode.Interrupter_OnInterruptableSpell;

            Drawing.OnDraw += OnDraw;
            Drawing.OnEndScene += Damage.Damage_Indicator;

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
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            { Mode.Flee(); }

        }        
        private static void OnDraw(EventArgs args)
        {
            if (!Config.DrawMenu.Checked("draw")) return;
            if (Config.DrawMenu.Checked("Qdr"))
            {
                Circle.Draw(Spells.QTiny.IsLearned ? Color.HotPink : Color.Zero, Spells.QTiny.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("Wdr"))
            {
                Circle.Draw(Spells.WMega.IsLearned ? Color.Yellow : Color.Zero, Spells.WMega.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("Edr"))
            {
                Circle.Draw(Spells.ETiny.IsLearned ? Color.Cyan : Color.Zero, Spells.ETiny.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("Rdr"))
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Green : Color.Zero, Spells.R.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("timer"))
            {
                var Buff1 = Player.GetBuff("GnarTransformTired");
                var Buff2 = Player.GetBuff("gnartransform");
                if (Buff1 == null && Buff2 == null)
                {
                    var Fury = Player.Instance.Mana;
                    var Color = Fury > 95 ? Colour.Red : Fury > 80 ? Colour.OrangeRed : Fury > 25? Colour.Orange : Colour.Yellow;
                    VectorHelp.DrawArc(Player.Instance.Position.To2D(), 100, Color, 0, - Player.Instance.Mana / 15.5f, 3.5f);

                }
                else
                {
                    var EndTime = Buff1 != null ? Buff1.EndTime : Buff2.StartTime + 15f;
                    var PercentRemaining = (Game.Time - EndTime) / 15f * 100;
                    var Fury = Player.Instance.Mana;
                    var Color = Buff1 != null ? Colour.White : Fury > 95 ? Colour.Red : Fury > 80 ? Colour.OrangeRed : Fury > 25 ? Colour.Orange : Colour.Yellow;
                    VectorHelp.DrawArc(Player.Instance.Position.To2D(), 100, Color, 0, PercentRemaining / 15.5f, 3.5f);
                }
            }
            if (Config.DrawMenu.Checked("rpos") && !Spells.R.IsOnCooldown)
            {
                var WhereWall = new List<Vector3>();
                for (var i = 0; i <= 80; i++)
                {
                    WhereWall = VectorHelp.GetWallAroundMe(Spells.R.Range, i / 12.5f);
                }
                Circle.Draw(Color.Green, 40, WhereWall.ToArray());
            }
        }       
    }
}
