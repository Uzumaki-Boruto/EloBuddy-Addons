using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Notifications;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;

namespace UBRyze
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Ryze") return;

            Config.Dattenosa();
            Spells.InitSpells();
            InitEvents();
        }
        private static void InitEvents()
        {
            if (Config.DrawMenu.Checked("draw") && Config.DrawMenu.Checked("notif"))
            {
                var notStart = new SimpleNotification("UBRyze Load Status", "UBRyze sucessfully loaded.");
                Notifications.Show(notStart, 5000);
            }
            Game.OnTick += GameOnTick;
            Game.OnTick += Spells.UpdateSpells;
            Game.OnTick += Mode.Zhonya;
            Game.OnUpdate += Mode.AutoHarass;
            Game.OnUpdate += Mode.Killsteal;

            Spellbook.OnCastSpell += delegate(Spellbook sender, SpellbookCastSpellEventArgs args)
            {
                if (!Config.Menu.Checked("human")) return;
                if (sender.Owner.IsMe)
                {
                    var Delay = Config.Menu.GetValue("style") == 0 ? Config.Menu.GetValue("delay1") : new Random().Next(Config.Menu.GetValue("delay1") * 10, Config.Menu.GetValue("delay2") * 10);
                    if (Extensions.LastCast * 1000 + Delay >= Game.Time)
                    {
                        args.Process = true;
                    }
                    else
                    {
                        args.Process = false;
                    }
                    //Will this work? Pray ( ͡° ͜ʖ ͡°)
                }
            };

            Gapcloser.OnGapcloser += Mode.Gapcloser_OnGapcloser;

            Drawing.OnDraw += OnDraw;
            Drawing.OnEndScene += Damages.Damage_Indicator;            

            Orbwalker.OnUnkillableMinion += Mode.On_Unkillable_Minion;

            Obj_AI_Base.OnProcessSpellCast += Extensions.Obj_AI_Base_OnProcessSpellCast;
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
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            { Mode.Lasthit(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            { Mode.JungleClear(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            { Mode.Flee(); }

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
                Circle.Draw(Spells.W.IsLearned || Spells.E.IsLearned ? Color.Yellow : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("drR"))
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Green : Color.Zero, Spells.R.Range, Player.Instance.Position);
            }
        }
    }
}
