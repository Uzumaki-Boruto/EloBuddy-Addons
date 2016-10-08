using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Notifications;
using SharpDX;
using Colour = System.Drawing.Color;

namespace UBSyndra
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Syndra") return;

            var notStart = new SimpleNotification("UBSyndra Load Status", "UBSyndra sucessfully loaded.");
            Notifications.Show(notStart, 5000);

            Config.Dattenosa();
            Spells.InitSpells();
            InitEvents();

        }
        private static void InitEvents()
        {
            Game.OnTick += GameOnTick;
            Game.OnTick += Spells.UpdateSpells;
            Game.OnUpdate += Mode.AutoHarass;
            Game.OnUpdate += Mode.Killsteal;

            Gapcloser.OnGapcloser += Mode.Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Mode.Interrupter_OnInterruptableSpell;
          
            Drawing.OnDraw += OnDraw;
            Drawing.OnEndScene += Damage.Damage_Indicator;
            

            Orbwalker.OnUnkillableMinion += Mode.On_Unkillable_Minion;

            Obj_AI_Base.OnProcessSpellCast += Spells.Obj_AI_Base_OnProcessSpellCast;
            //Obj_AI_Base.OnCreate += BallManager.GameObject_OnCreate;
            //Obj_AI_Base.OnDelete += BallManager.GameObject_OnDelete;

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
                Circle.Draw(Spells.E.IsLearned ? Color.SkyBlue : Color.Zero, Spells.E.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("drEQ"))
            {
                Circle.Draw(Spells.Q.IsLearned && Spells.E.IsLearned ? Color.Yellow : Color.Zero, Spells.QE.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("drR"))
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Green : Color.Zero, Spells.R.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("drBall") && Spells.E.IsReady())
            {
                foreach (var Ball in BallManager.Balls.Where(x => Spells.E.IsInRange(x)))
                {
                    var Rectangle = new Geometry.Polygon.Rectangle(Player.Instance.Position, Player.Instance.Position.Extend(Ball.Position, Spells.QE.Range).To3DWorld(), Spells.QE.Width);
                    Rectangle.Draw(Colour.Cyan);
                }
            }
        }
    }
}
