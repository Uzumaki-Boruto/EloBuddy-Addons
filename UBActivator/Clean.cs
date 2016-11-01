using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace UBActivator
{
    class Clean
    {
        public static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            //if (Items.Quicksilver_Sash == null && Items.Mercurial_Scimitar == null && Items.Mikaels_Crucible == null && Spells.Cleanse == null) return;
            if (!sender.IsAlly) return;
            if (sender is AIHeroClient)
            {
                var Type = args.Buff.Type;
                var Duration = (args.Buff.EndTime - Game.Time) * 1000;
                var Noenemy = Config.Clean["EnemyManager"].Cast<CheckBox>().CurrentValue;
                var Time = Config.CleanSlider.CurrentValue;
                var Delay = Config.CleanDelay.CurrentValue ?
                    new Random().Next(0, Time) :
                    Time;

                var MinDurCC = Config.Clean["CCDelay"].Cast<Slider>().CurrentValue;
                //CC
                if ((Type == BuffType.Blind && Config.Clean["Blind"].Cast<CheckBox>().CurrentValue)
                || (Type == BuffType.Charm && Config.Clean["Charm"].Cast<CheckBox>().CurrentValue)
                || (Type == BuffType.Fear && Config.Clean["Fear"].Cast<CheckBox>().CurrentValue)
                || (Type == BuffType.NearSight && Config.Clean["Nearsight"].Cast<CheckBox>().CurrentValue)
                || (Type == BuffType.Polymorph && Config.Clean["Polymorph"].Cast<CheckBox>().CurrentValue)
                || (Type == BuffType.Taunt && Config.Clean["Taunt"].Cast<CheckBox>().CurrentValue)
                || (Type == BuffType.Slow && Config.Clean["Slow"].Cast<CheckBox>().CurrentValue)
                || (Type == BuffType.Stun && Config.Clean["Stun"].Cast<CheckBox>().CurrentValue)
                || (Type == BuffType.Snare && Config.Clean["Snare"].Cast<CheckBox>().CurrentValue)
                || (Type == BuffType.Suppression && Config.Clean["Suppression"].Cast<CheckBox>().CurrentValue)
                || (Type == BuffType.Silence && Config.Clean["Silence"].Cast<CheckBox>().CurrentValue))
                {
                    if (sender.IsMe)
                    {
                        if (((Noenemy == false && ObjectManager.Player.CountEnemiesInRange(1500) > 0) || Noenemy == true) && Duration >= MinDurCC)
                        {
                            if (Config.Clean["enableCleanse"].Cast<CheckBox>().CurrentValue)
                            {
                                if (Spells.Cleanse != null && Spells.Cleanse.IsReady())
                                {
                                    Core.DelayAction(() => Spells.Cleanse.Cast(), Delay);
                                }
                            }
                            if (Config.Clean["enableQSS"].Cast<CheckBox>().CurrentValue)
                            {
                                if (Items.Quicksilver_Sash.IsOwned() && Items.Quicksilver_Sash.IsReady())
                                {
                                    Core.DelayAction(() => Items.Quicksilver_Sash.Cast(), Delay);
                                }

                                if (Items.Mercurial_Scimitar.IsOwned() && Items.Mercurial_Scimitar.IsReady())
                                {
                                    Core.DelayAction(() => Items.Mercurial_Scimitar.Cast(), Delay);
                                }
                                else return;
                            }
                        }
                    }
                    if (sender.IsAlly && !sender.IsMe && !sender.IsMinion)
                    {
                        var stuner = sender as AIHeroClient;
                        if (!Config.Clean["enableMikael"].Cast<CheckBox>().CurrentValue) return;
                        if (!Config.Clean["mikael" + stuner.ChampionName].Cast<CheckBox>().CurrentValue) return;
                        if (Items.Mikaels_Crucible.IsOwned() && Items.Mikaels_Crucible.IsReady())
                        {
                            Core.DelayAction(() => Items.Mikaels_Crucible.Cast(stuner), Delay);
                        }
                    }
                }
                else if ((Type == BuffType.Knockup || Type == BuffType.Knockback) && Config.Clean["Airbone"].Cast<CheckBox>().CurrentValue)
                {
                    if (sender.IsMe)
                    {
                        if (((Noenemy == false && ObjectManager.Player.CountEnemiesInRange(1500) > 0) || Noenemy == true) && Duration >= MinDurCC)
                        {
                            if (!Config.Clean["enableQSS"].Cast<CheckBox>().CurrentValue) return;

                            if (Items.Quicksilver_Sash.IsOwned() && Items.Quicksilver_Sash.IsReady())
                            {
                                Core.DelayAction(() => Items.Quicksilver_Sash.Cast(), Delay);
                            }

                            if (Items.Mercurial_Scimitar.IsOwned() && Items.Mercurial_Scimitar.IsReady())
                            {
                                Core.DelayAction(() => Items.Mercurial_Scimitar.Cast(), Delay);
                            }
                        }
                    }
                }
            }
        }
    }
}
