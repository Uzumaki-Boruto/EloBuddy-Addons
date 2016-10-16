using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Notifications;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Events;
using SharpDX;
using Colour = System.Drawing.Color;

namespace UBAzir
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Azir") return;

            var notStart = new SimpleNotification("UBAzir Load Status", "UBAzir sucessfully loaded.");
            Notifications.Show(notStart, 7500);

            Config.Dattenosa();
            Spells.InitSpells();
            InitEvents();
        }

        private static void InitEvents()
        {
            Game.OnTick += GameOnTick;
            Game.OnTick += ObjManager.ManyThingInHere;
            Game.OnUpdate += Mode.KillSteal;
            Game.OnTick += Spells.UpdateSpells;
            Game.OnWndProc += Insec.Game_OnWndProc;
            //Game.OnUpdate += On_Update;
            //Game.OnNotify += _Insec.Notification;
            Drawing.OnDraw += OnDraw;
            Drawing.OnEndScene += Damages.Damage_Indicator;
            Orbwalker.OnUnkillableMinion += Mode.On_Unkillable_Minion;
            Gapcloser.OnGapcloser += Extension.OnGapCloser;
            //AIHeroClient.OnProcessSpellCast += delegate(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
            //{
            //    if (sender.IsMe)
            //        if (Config.Insec.Checked("godInsec", false))
            //        {
            //            if (args.Slot == SpellSlot.Q)
            //            {
            //                var CastR = 
            //                Player.CastSpell(SpellSlot.R, Insec.RPosGod, true);
            //            }
            //        }
            //};
            Dash.OnDash += delegate(Obj_AI_Base sender, Dash.DashEventArgs args)
            {
                if (!Spells.Q.IsReady()) return;
                if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Flee)
                {
                    var End = args.EndPos.Extend(Game.CursorPos, Spells.Q.Range).To3DWorld();
                    Core.DelayAction(() => Player.CastSpell(SpellSlot.Q, End, true), args.Duration - Game.Ping - 5);
                }
                if (Config.Insec.Checked("normalInsec", false))
                {
                    var End = Player.Instance.Position.Extend(args.EndPos, Player.Instance.Distance(args.EndPos) + Spells.Q.Range).To3DWorld();
                    Core.DelayAction(() => Player.CastSpell(SpellSlot.Q, End, true), args.Duration - Game.Ping - 5);
                }
                if (Config.Insec.Checked("godInsec", false) && TargetSelector.GetTarget(Spells.R.Width, DamageType.Mixed) != null)
                {
                    var End = args.EndPos.Extend(Game.CursorPos, args.EndPos.Distance(Game.CursorPos) + Spells.Q.Range).To3DWorld();
                    Player.CastSpell(SpellSlot.Q, End);
                }
            };
            Interrupter.OnInterruptableSpell += Extension.Interrupter_OnInterruptableSpell;
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
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            { Mode.Flee(Game.CursorPos); }

            if (Config.Insec["normalInsec"].Cast<KeyBind>().CurrentValue)
            { Insec.Do_Normal_Insec(); }
            if (Config.Insec["godInsec"].Cast<KeyBind>().CurrentValue)
            { Insec.Do_God_Insec(); }

            if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) && !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)
                && !Config.Insec["normalInsec"].Cast<KeyBind>().CurrentValue && !Config.Insec["godInsec"].Cast<KeyBind>().CurrentValue)
            { Mode.Auto_Harass(); }
        }
        private static void OnDraw(EventArgs args)
        {
            if (Config.DrawMenu.Checked("Q"))  
            {
                Circle.Draw(Spells.Q.IsLearned ? Color.HotPink : Color.Zero, Spells.Q.Range, Player.Instance.Position);
            }
            
            if (Config.DrawMenu.Checked("W"))    
            {
                Circle.Draw(Spells.W.IsLearned ? Color.Cyan : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }

            if (Config.DrawMenu.Checked("E"))
            {
                Circle.Draw(Spells.E.IsLearned ? Color.Orange : Color.Zero, Spells.E.Range, Player.Instance.Position);
            }

            if (Config.DrawMenu.Checked("R"))
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Yellow : Color.Zero, 325, Player.Instance.Position);
            }
            if (Config.DrawMenu.Checked("InsecPos") && (Insec.PositionSelected != new Vector3() || Insec.AllySelected != null))
            {
                if (Insec.AllySelected != null)
                {
                    Insec.AllySelected.Position.DrawCircle(60, Color.Red, 10);
                }
                else
                {
                    Insec.PositionSelected.DrawCircle(60, Color.Red, 10);
                }
            }
            if (Config.DrawMenu.Checked("InsecPos") && Insec.PositionGotoSelected != new Vector3())
            {
                Insec.PositionGotoSelected.DrawCircle(60, Color.Cyan, 10);
            }
        }
    }
}
