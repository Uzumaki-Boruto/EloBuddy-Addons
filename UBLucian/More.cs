using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Notifications;
using SharpDX;

namespace UBLucian
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Lucian") return;

            Config.Dattenosa();
            Spells.InitSpells();
            Flee.InitSpots();
            InitEvents();
            Config.CorrectTheMenu();
        }
        private static void InitEvents()
        {
            if (Config.DrawMenu.Checked("notif") && Config.DrawMenu.Checked("draw"))
            {
                var notStart = new SimpleNotification("UBLucian Load Status", "UBLucian sucessfully loaded.");
                Notifications.Show(notStart, 5000);
            }

            Game.OnTick += GameOnTick;
            Game.OnUpdate += Mode.AutoHarass;
            Game.OnUpdate += Mode.Killsteal;
            Game.OnUpdate += Flee.JumpSystem;
            //Game.OnWndProc += Flee.Game_OnWndProc;

            Gapcloser.OnGapcloser += Mode.Gapcloser_OnGapcloser;

            if (Game.MapId == GameMapId.SummonersRift)
                    Drawing.OnDraw += Flee.DrawJumpSpot;
            Drawing.OnDraw += OnDraw;
            Drawing.OnEndScene += Damages.Damage_Indicator;            

            Orbwalker.OnUnkillableMinion += Mode.On_Unkillable_Minion;
            Orbwalker.OnPostAttack += Mode.Orbwalker_OnPostAttack;

            Obj_AI_Base.OnProcessSpellCast += Mode.Obj_AI_Base_OnProcessSpellCast;
            Obj_AI_Base.OnBuffGain += Mode.Obj_AI_Base_OnBuffGain;
            Obj_AI_Base.OnBuffLose += Mode.Obj_AI_Base_OnBuffLose;
            
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
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            { Mode.JungleClear(); }
            //if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            //{ Mode.Flee(); }

        }
        private static void OnDraw(EventArgs args)
        {
            if (!Config.DrawMenu.Checked("draw")) return;
            if (Config.DrawMenu.Checked("Qdr"))
            {
                Circle.Draw(Spells.Q.IsLearned ? Color.HotPink : Color.Zero, Spells.Q.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("Q2dr"))
            {
                Circle.Draw(Spells.Q2.IsLearned ? Color.HotPink : Color.Zero, Spells.Q2.Range, Player.Instance.Position);
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
            if (Config.DrawMenu.Checked("Eposdr") && _E_.BestEPos != null)
            {
                Circle.Draw(Spells.E.IsReady() ? Color.OrangeRed : Color.Zero, Player.Instance.BoundingRadius, 3f, _E_.BestEPos);
            }
        }
    }
}
