using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Notifications;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Events;


namespace UBActivator
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        static void Loading_OnLoadingComplete(EventArgs args)
        {          
            Spells.InitSpells();
            Config.Dattenosa();
            Items.InitItems();
            Zhonya.Initialize();
            if (Spells.Exhaust != null)
            Exhaust.Initialize();
            if (Spells.Barrier != null)
            Barrier.Initialize();
            InitEvent();
            var notStart = new SimpleNotification("UBActivator Load Status", "UBActivator sucessfully loaded.");
            Notifications.Show(notStart, 5000);
        }
        static void InitEvent()
        {
            if (Config.SkinSlider.CurrentValue != Player.Instance.SkinId && Config.Utility["eskin"].Cast<CheckBox>().CurrentValue)
            {
                Player.SetSkinId(Config.SkinSlider.CurrentValue);
            }
            if (Config.OnTickButton.CurrentValue == true)
            {
                Game.OnTick += _Game;
            }
            else if (Config.OnUpdateButton.CurrentValue == true)
            {
                Game.OnUpdate += _Game;
            }

            if (Config.OnTickButton.CurrentValue == true || Config.OnUpdateButton.CurrentValue == true)
            {
                Obj_AI_Base.OnBuffGain += Clean.OnBuffGain;
                Obj_AI_Base.OnBuffGain += Defense.OnBuffGain;

                Orbwalker.OnPostAttack += Offensive.OnPostAttack;
                Orbwalker.OnPreAttack += Offensive.OnPreAttack;

                Gapcloser.OnGapcloser += Offensive.Gapcloser_OnGapcloser;

                Interrupter.OnInterruptableSpell += Spells.Interrupter_OnInterruptableSpell;

                Obj_AI_Base.OnProcessSpellCast += Defense.OnProcessSpellCast;
                Obj_AI_Base.OnProcessSpellCast += Defensive.OnProcessSpellCast;

                Obj_AI_Base.OnBasicAttack += Defensive.Obj_AI_Base_OnBasicAttack;

            }
            else return;
        }
       
        static void _Game(EventArgs args)
        {
            Offensive.Ontick();
            Offensive.OnTick2();
            Offensive.OnTick3();
            Offensive.OnTick4();
            Offensive.KillSteal();
            Utility.Game_OnTick();
            Utility.OnTick();
            Combat.OnTick();
            Defensive.OnTick();
            Potions.OnTick();
            Spells.JungSteal();
            Spells.KillSteal();
            Spells.UseHeal();
            Ward.OnTick();
        }
        
    }
}
