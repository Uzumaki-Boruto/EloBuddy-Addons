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
            Obj_AI_Base.OnProcessSpellCast += delegate(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
            {
                var Time = args.SData.CooldownTime;
                //Time always return base cooldown
            };
            Game.OnUpdate += Mode.AutoE;
            Game.OnUpdate += Event.AutoQ;
            Game.OnUpdate += Cooldowncal.SpellsOnUpdate;
            Game.OnUpdate += Mode.KillSteal;
            Game.OnUpdate += Mode.JungleSteal;
            Game.OnUpdate += Flee.JumpSystem;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
           
        }

        static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            if (Config.RMenu["Rform"].Cast<ComboBox>().CurrentValue != 0) return;
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo || Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.JungleClear)
            {
                if (Event.Humanform)
                {
                    if (!Spells.Q.IsReady() && (!Spells.W.IsReady() || !Config.WMenu["Wcb"].Cast<CheckBox>().CurrentValue) && Spells.R.IsReady())
                    {
                        Spells.R.Cast();
                        Orbwalker.ResetAutoAttack();
                    }
                }
                else
                {
                    if (!Spells.Q2.IsReady() && !Spells.W2.IsReady() && !Spells.E2.IsReady() && Spells.R.IsReady())
                    {
                        Spells.R.Cast();
                        Orbwalker.ResetAutoAttack();
                    }
                }
            }
        }
        private static void Draw()
        {
            Drawing.OnDraw += OnDrawHuman;
            Drawing.OnDraw += OnDrawHuman2;
            Drawing.OnDraw += OnDrawCougar;
            Drawing.OnDraw += OnDrawCougar2;
            if (Game.MapId == GameMapId.SummonersRift)
                Drawing.OnDraw += Flee.DrawJumpSpot;
            Drawing.OnEndScene += Drawings.Special_Draw;
            Drawing.OnEndScene += Damage.Damage_Indicator;
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
            //if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            //{ Flee.FleeMode(); }
        }
        private static void OnDrawHuman(EventArgs args)
        {
            if (Event.Humanform && Config.DrawMenu["drawQ"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.Q.IsReady() ? Color.HotPink : Color.Zero, Spells.Q.Range, Player.Instance.Position);
            }
            if (Event.Humanform && Config.DrawMenu["drawW"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W.IsReady() ? Color.Yellow : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }
            if (Event.Humanform && Config.DrawMenu["drawE"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.E.IsReady() ? Color.Cyan : Color.Zero, Spells.E.Range, Player.Instance.Position);
            }
            if (Event.Humanform && Config.DrawMenu["drawaa"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Color.DarkGoldenrod, 175, Player.Instance.Position);
            }           
        }
        private static void OnDrawHuman2(EventArgs args)
        {
            var other = Config.DrawMenu["drawform"].Cast<CheckBox>().CurrentValue;
            if (other && Event.Humanform && Config.DrawMenu["drawW2"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W2.IsLearned ? Color.DarkGoldenrod : Color.Zero, Spells.W2.Range, Player.Instance.Position);
            }
            if (other && Event.Humanform && Config.DrawMenu["drawW2p"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W2p.IsLearned ? Color.DarkGoldenrod : Color.Zero, Spells.W2p.Range, Player.Instance.Position);
            }
            if (other && Event.Humanform && Config.DrawMenu["drawE2"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.E2.IsLearned ? Color.DarkGoldenrod : Color.Zero, Spells.E2.Range, Player.Instance.Position);
            }
        }
        private static void OnDrawCougar(EventArgs args)
        {
            if (!Event.Humanform && Config.DrawMenu["drawW2"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W2.IsReady() ? Color.HotPink : Color.Zero, Spells.W2.Range, Player.Instance.Position);
            }
            if (!Event.Humanform && Config.DrawMenu["drawW2p"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W2p.IsReady() ? Color.Yellow : Color.Zero, Spells.W2p.Range, Player.Instance.Position);
            }
            if (!Event.Humanform && Config.DrawMenu["drawE2"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.E2.IsReady() ? Color.Cyan : Color.Zero, Spells.E2.Range, Player.Instance.Position);
            }
            if (!Event.Humanform && Config.DrawMenu["drawaa"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Color.Green, 550, Player.Instance.Position);
            }
        }
        private static void OnDrawCougar2(EventArgs args)
        {
            var other = Config.DrawMenu["drawform"].Cast<CheckBox>().CurrentValue;
            if (other && !Event.Humanform && Config.DrawMenu["drawQ"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.Q.IsLearned ? Color.Green : Color.Zero, Spells.Q.Range, Player.Instance.Position);
            }
            if (other && !Event.Humanform && Config.DrawMenu["drawW"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W.IsLearned ? Color.Green : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }
            if (other && !Event.Humanform && Config.DrawMenu["drawE"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.E.IsLearned ? Color.Green : Color.Zero, Spells.E.Range, Player.Instance.Position);
            }
        }
    }
}

