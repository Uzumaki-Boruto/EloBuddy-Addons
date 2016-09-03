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
            Game.OnTick += ObjManager.GetMyPosBefore;
            Game.OnUpdate += Mode.KillSteal;
            //Game.OnUpdate += On_Update;
            //Game.OnNotify += _Insec.Notification;
            Drawing.OnDraw += OnDraw;
            Drawing.OnEndScene += Damages.Damage_Indicator;
            Orbwalker.OnUnkillableMinion += Mode.On_Unkillable_Minion;
            Gapcloser.OnGapcloser += Event.OnGapCloser;
            Interrupter.OnInterruptableSpell += Event.Interrupter_OnInterruptableSpell;
            Obj_AI_Base.OnLevelUp +=  Event.Obj_AI_Base_OnLevelUp;
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
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            { Mode.Lasthit(); }
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

            if (ObjectManager.Player.SkinId != Config.MiscMenu["Modskinid"].Cast<Slider>().CurrentValue)
            {
                if (Config.MiscMenu["Modskin"].Cast<CheckBox>().CurrentValue)
                {
                    Player.SetSkinId(Config.MiscMenu["Modskinid"].Cast<Slider>().CurrentValue);
                    foreach (var Soldier in ObjectManager.Get<Obj_AI_Minion>().Where(o => o.IsValid && o.BaseSkinName == "AzirSoldier"))
                    {
                        Soldier.SetSkinId(Config.MiscMenu["Modskinid"].Cast<Slider>().CurrentValue);
                    }
                }
            }

            //Insec.Do_Flash_Insec();
        }
        private static void OnDraw(EventArgs args)
        {
            if (Config.DrawMenu["Qdr"].Cast<CheckBox>().CurrentValue)  
            {
                Circle.Draw(Spells.Q.IsLearned ? Color.HotPink : Color.Zero, Spells.Q.Range, Player.Instance.Position);
            }
            
            if (Config.DrawMenu["Wcastdr"].Cast<CheckBox>().CurrentValue)    
            {
                Circle.Draw(Spells.W.IsLearned ? Color.Cyan : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }

            if (Config.DrawMenu["Edr"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.E.IsLearned ? Color.Orange : Color.Zero, Spells.E.Range, Player.Instance.Position);
            }

            if (Config.DrawMenu["Rdr"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Yellow : Color.Zero, 325, Player.Instance.Position);
            }
            
        }
    }
}
