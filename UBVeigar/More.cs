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

namespace UBVeigar
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Veigar") return;

            Config.Dattenosa();
            Spells.InitSpells();
            InitEvents();
        }
        private static void InitEvents()
        {
            if (Config.DrawMenu.Checked("notif") && Config.DrawMenu.Checked("draw"))
            {
                var notStart = new SimpleNotification("UBVeigar Load Status", "UBVeigar sucessfully loaded.");
                Notifications.Show(notStart, 5000);
            }

            Game.OnTick += GameOnTick;
            Game.OnTick += Mode.Get_AP;
            Game.OnTick += Mode.JungSteal;
            Game.OnUpdate += Mode.AutoHarass;
            Game.OnUpdate += Mode.Killsteal;

            Gapcloser.OnGapcloser += Mode.Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Mode.Interrupter_OnInterruptableSpell;

            Drawing.OnDraw += OnDraw;
            Drawing.OnEndScene += Damage.Damage_Indicator;
            

            Orbwalker.OnUnkillableMinion += Mode.On_Unkillable_Minion;

            AIHeroClient.OnBuffGain += Mode.AIHeroClient_OnBuffGain;

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
                Circle.Draw(Spells.W.IsLearned ? Color.Orange : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("drE"))
            {
                Circle.Draw(Spells.E.IsLearned ? Color.Cyan : Color.Zero, Spells.E.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("drR"))
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Green : Color.Zero, Spells.R.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("drEring") && Spells.E.IsInRange(Game.CursorPos))
            {
                var Ring = new Geometry.Polygon.Ring(Game.CursorPos, 300f, 375f);
                Ring.Draw(Colour.Cyan, 2);
            }
            if (Config.DrawMenu.Checked("drminion"))
            {
                var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Spells.Q.Range).Where(m => m.Health <= Damage.QDamage(m)).ToArray() ;
                if (minions != null)
                {
                    Circle.Draw(Color.HotPink, 50f, minions);
                }
            }
        }
    }
}
