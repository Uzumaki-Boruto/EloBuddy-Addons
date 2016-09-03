using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Notifications;
using SharpDX;

namespace UBSivir
{
    class More
    {
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Sivir") return;

            var notStart = new SimpleNotification("UBSivir Load Status", "UBSivir sucessfully loaded.");
            Notifications.Show(notStart, 5000);

            Config.Dattenosa();
            _E.Initialize();
            _E_Advance.Initialize();
            Spells.InitSpells();

            InitEvents();
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            Obj_AI_Base.OnProcessSpellCast += Event.Obj_AI_Base_OnProcessSpellCast;
            Obj_AI_Base.OnBuffGain += Event.Obj_AI_Base_OnBuffGain;

            Orbwalker.OnUnkillableMinion += Mode.Orbwalker_OnUnkillableMinion;
            Orbwalker.OnPostAttack += Event.Orbwalker_OnPostAttack;
        }
        private static void InitEvents()
        {           
            Game.OnTick += GameOnTick;
            Game.OnUpdate += On_Update;
            Drawing.OnDraw += OnDraw;          
        }
        private static void On_Update(EventArgs args)
        {         
            if (Spells.Q.IsReady())
            {
                Mode.Killsteal();
            }          
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
        }
        private static void OnDraw(EventArgs args)
        {
            if (Config.DrawMenu["drawQ"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.Q.IsLearned ? Color.HotPink : Color.Zero , Spells.Q.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu["drawR"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Yellow : Color.Zero, Spells.R.Range, Player.Instance.Position);
            }
        }
        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var unit = sender as AIHeroClient;
            if (unit == null || !unit.IsValid)
            {
                return;
            }

            if (!unit.IsEnemy || !Config.BlockSpells || !Spells.E.IsReady())
            {
                return;
            }

            // spell handled by evade
            if (UBSivir.Evade.SpellDatabase.GetByName(args.SData.Name) != null && !Config.BlockSpells)
                return;

            if (!_E.Contains(unit, args))
                return;

            if (args.End.Distance(Player.Instance) == 0)
                return;

            var type = args.SData.TargettingType;

            if (unit.ChampionName.Equals("Caitlyn") && args.Slot == SpellSlot.Q)
            {
                Core.DelayAction(() => Spells.E.Cast(),
                    (int)(args.Start.Distance(Player.Instance) / args.SData.MissileSpeed * 1000) -
                    (int)(args.End.Distance(Player.Instance) / args.SData.MissileSpeed) - 500);
            }
            if (unit.ChampionName.Equals("Zyra"))
            {
                Core.DelayAction(() => Spells.E.Cast(),
                    (int)(args.Start.Distance(Player.Instance) / args.SData.MissileSpeed * 1000) -
                    (int)(args.End.Distance(Player.Instance) / args.SData.MissileSpeed) - 500);
            }
            if (args.End.Distance(Player.Instance) < 250)
            {
                if (unit.ChampionName.Equals("Bard") && args.End.Distance(Player.Instance) < 300)
                {
                    Core.DelayAction(() => Spells.E.Cast(), (int)(unit.Distance(Player.Instance) / 7f) + 400);
                }
                else if (unit.ChampionName.Equals("Ashe"))
                {
                    Core.DelayAction(() => Spells.E.Cast(),
                        (int)(args.Start.Distance(Player.Instance) / args.SData.MissileSpeed * 1000) -
                        (int)args.End.Distance(Player.Instance));
                    return;
                }
                else if (unit.ChampionName.Equals("Varus") || unit.ChampionName.Equals("TahmKench") ||
                         unit.ChampionName.Equals("Lux"))
                {
                    Core.DelayAction(() => Spells.E.Cast(),
                        (int)(args.Start.Distance(Player.Instance) / args.SData.MissileSpeed * 1000) -
                        (int)(args.End.Distance(Player.Instance) / args.SData.MissileSpeed) - 500);
                }
                else if (unit.ChampionName.Equals("Amumu"))
                {
                    if (sender.Distance(Player.Instance) < 1100)
                        Core.DelayAction(() => Spells.E.Cast(),
                            (int)(args.Start.Distance(Player.Instance) / args.SData.MissileSpeed * 1000) -
                            (int)(args.End.Distance(Player.Instance) / args.SData.MissileSpeed) - 500);
                }
            }

            if (args.Target != null && type.Equals(SpellDataTargetType.Unit))
            {
                if (!args.Target.IsMe ||
                    (args.Target.Name.Equals("Barrel") && args.Target.Distance(Player.Instance) > 200 &&
                     args.Target.Distance(Player.Instance) < 400))
                {
                    return;
                }

                if (unit.ChampionName.Equals("Nautilus") ||
                    (unit.ChampionName.Equals("Caitlyn") && args.Slot.Equals(SpellSlot.R)))
                {
                    var d = unit.Distance(Player.Instance);
                    var travelTime = d / args.SData.MissileSpeed;
                    var delay = travelTime * 1000 - Spells.E.CastDelay + 1150;
                    Console.WriteLine("TT: " + travelTime + " " + delay);
                    Core.DelayAction(() => Spells.E.Cast(), (int)delay);
                    return;
                }
                Spells.E.Cast();
            }

            if (type.Equals(SpellDataTargetType.Unit))
            {
                if (unit.ChampionName.Equals("Bard") && args.End.Distance(Player.Instance) < 300)
                {
                    Core.DelayAction(() => Spells.E.Cast(), 400 + (int)(unit.Distance(Player.Instance) / 7f));
                }
                else if (unit.ChampionName.Equals("Riven") && args.End.Distance(Player.Instance) < 260)
                {
                    Spells.E.Cast();
                }
                else
                {
                    Spells.E.Cast();
                }
            }
            else if (type.Equals(SpellDataTargetType.LocationAoe) &&
                     args.End.Distance(Player.Instance) < args.SData.CastRadius)
            {
                // annie moving tibbers
                if (unit.ChampionName.Equals("Annie") && args.Slot.Equals(SpellSlot.R))
                {
                    return;
                }
                Spells.E.Cast();
            }
            else if (type.Equals(SpellDataTargetType.Cone) &&
                     args.End.Distance(Player.Instance) < args.SData.CastRadius)
            {
                Spells.E.Cast();
            }
            else if (type.Equals(SpellDataTargetType.SelfAoe) || type.Equals(SpellDataTargetType.Self))
            {
                var d = args.End.Distance(Player.Instance.ServerPosition);
                var p = args.SData.CastRadius > 5000 ? args.SData.CastRange : args.SData.CastRadius;
                if (d < p)
                    Spells.E.Cast();
            }
        }
    }
}
