using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Notifications;
using SharpDX;
using Colour = System.Drawing.Color;


namespace UBNidalee
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Nidalee") return;

            var notStart = new SimpleNotification("UBNidalee Load Status", "UBNidalee sucessfully loaded.");
            Notifications.Show(notStart, 5000);

            Config.Dattenosa();
            Spells.InitSpells();
            Items.InitItems();
            Flee.InitSpots();
            SmiteManager.Init();
            //Flee.InitPoints();
            InitEvents();
            Draw();
            Drawings.ImgData();
        }
        private static void InitEvents()
        {
            Game.OnTick += GameOnTick;
            Obj_AI_Base.OnProcessSpellCast += Cooldowncal.Obj_AI_Base_OnProcessSpellCast;
            Game.OnUpdate += Mode.AutoE;
            Game.OnUpdate += Event.AutoQ;
            Game.OnUpdate += Cooldowncal.SpellsOnUpdate;
            Game.OnUpdate += Mode.KillSteal;
            Game.OnUpdate += Mode.JungleSteal;
            Game.OnUpdate += Flee.JumpSystem;
           
        }
        private static void Draw()
        {
            Drawing.OnDraw += OnDrawHuman;
            Drawing.OnDraw += OnDrawHuman2;
            Drawing.OnDraw += OnDrawCougar;
            Drawing.OnDraw += OnDrawCougar2;
            Drawing.OnDraw += WDraw;
            if (Game.MapId == GameMapId.SummonersRift)
                Drawing.OnDraw += Flee.DrawJumpSpot;
            Drawing.OnEndScene += Drawings.Special_Draw;
            Drawing.OnEndScene += Drawings.Special_Draw_2;
            Drawing.OnEndScene += Drawings.Special_Draw_3;
            Drawing.OnEndScene += Damage.Damage_Indicator;
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
            //{ Flee.FleeMode(); }
        }
        private static void OnDrawHuman(EventArgs args)
        {
            if (Event.Humanform() && Config.DrawMenu["drawQ"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.Q.IsReady() ? Color.HotPink : Color.Zero, Spells.Q.Range, Player.Instance.Position);
            }
            if (Event.Humanform() && Config.DrawMenu["drawW"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W.IsReady() ? Color.Yellow : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }
            if (Event.Humanform() && Config.DrawMenu["drawE"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.E.IsReady() ? Color.Cyan : Color.Zero, Spells.E.Range, Player.Instance.Position);
            }
            if (Event.Humanform() && Config.DrawMenu["drawaa"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Color.DarkGoldenrod, 175, Player.Instance.Position);
            }           
        }
        private static void OnDrawHuman2(EventArgs args)
        {
            var other = Config.DrawMenu["drawform"].Cast<CheckBox>().CurrentValue;
            if (other && Event.Humanform() && Config.DrawMenu["drawW2"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W2.IsLearned ? Color.DarkGoldenrod : Color.Zero, Spells.W2.Range, Player.Instance.Position);
            }
            if (other && Event.Humanform() && Config.DrawMenu["drawW2p"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W2p.IsLearned ? Color.DarkGoldenrod : Color.Zero, Spells.W2p.Range, Player.Instance.Position);
            }
            if (other && Event.Humanform() && Config.DrawMenu["drawE2"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.E2.IsLearned ? Color.DarkGoldenrod : Color.Zero, Spells.E2.Range, Player.Instance.Position);
            }
        }
        private static void OnDrawCougar(EventArgs args)
        {
            if (!Event.Humanform() && Config.DrawMenu["drawW2"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W2.IsReady() ? Color.HotPink : Color.Zero, Spells.W2.Range, Player.Instance.Position);
            }
            if (!Event.Humanform() && Config.DrawMenu["drawW2p"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W2p.IsReady() ? Color.Yellow : Color.Zero, Spells.W2p.Range, Player.Instance.Position);
            }
            if (!Event.Humanform() && Config.DrawMenu["drawE2"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.E2.IsReady() ? Color.Cyan : Color.Zero, Spells.E2.Range, Player.Instance.Position);
            }
            if (!Event.Humanform() && Config.DrawMenu["drawaa"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Color.Green, 550, Player.Instance.Position);
            }
        }
        private static void OnDrawCougar2(EventArgs args)
        {
            var other = Config.DrawMenu["drawform"].Cast<CheckBox>().CurrentValue;
            if (other && !Event.Humanform() && Config.DrawMenu["drawQ"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.Q.IsLearned ? Color.Green : Color.Zero, Spells.Q.Range, Player.Instance.Position);
            }
            if (other && !Event.Humanform() && Config.DrawMenu["drawW"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W.IsLearned ? Color.Green : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }
            if (other && !Event.Humanform() && Config.DrawMenu["drawE"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.E.IsLearned ? Color.Green : Color.Zero, Spells.E.Range, Player.Instance.Position);
            }
        }
        private static void WDraw(EventArgs args)
        {
            if (Config.WMenu["Ws"].Cast<KeyBind>().CurrentValue && Event.Humanform())
            {
                Circle.Draw(Color.White, 400, Game.CursorPos);
            }
        }
    }
}

