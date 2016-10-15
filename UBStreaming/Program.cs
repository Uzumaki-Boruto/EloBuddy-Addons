using System;
using System.Linq;
using System.Runtime.InteropServices;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Color = System.Drawing.Color;
using SharpDX;

namespace UBStreaming
{
    class Program
    {
        private static float lastclick;
        private static readonly Random r = new Random();

        private static Menu Menu;

        private static AIHeroClient player
        {
            get { return Player.Instance; }
        }

        private static bool Pre;
        private static bool Stream
        {
            get { return (Menu["Menu"].Cast<KeyBind>().CurrentValue && Menu["Chat"].Cast<KeyBind>().CurrentValue) || CompleteTime > Game.Time - 5.5f; }
        }
        private static float CompleteTime;
        private static int RandomMin
        {
            get { return Menu["Randommin"].Cast<Slider>().CurrentValue; }
        }
        private static int RandomMax
        {
            get { return Menu["Randommax"].Cast<Slider>().CurrentValue; }
        }
        private static int RandomValue
        {
            get { return new Random().Next(RandomMin , RandomMax); }
        }
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadComplete;
        }

        static void OnLoadComplete(EventArgs args)
        {
            Orbwalker.OnPreAttack += Orbwalker_OnPreAttack;
            Orbwalker.OnPostAttack += AfterAttack;
            //Player.OnIssueOrder += OnIssueOrder;
            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnUpdate += GameOnUpdate;
            CompleteTime = Game.Time;

            Menu = MainMenu.AddMenu("UB Streaming", "UBStreaming");
            Menu.AddLabel("Make by Uzumaki Boruto");
            Menu.Add("Randommin", new Slider("Min random Delay per click {0} milisec", 150, 0, 400));
            Menu.Add("Randommax", new Slider("Max random Delay per click {0} milisec", 250, 0, 450));
            Menu.AddLabel("Note: Press Shift won't show menu if Both true");
            Menu.Add("Menu", new KeyBind("Show menu key", true, KeyBind.BindTypes.PressToggle, '.'));
            Menu.Add("Chat", new KeyBind("Please Don't Change this key", false, KeyBind.BindTypes.HoldActive, 16));
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Pre)
            {
                Drawing.DrawCircle(player.Position, player.GetAutoAttackRange() - 50f, Color.AliceBlue);
            }
        }

        private static void GameOnUpdate(EventArgs args)
        {
            if (Stream)
            {
                Hacks.DisableDrawings = true;
                Hacks.RenderWatermark = false;
            }
            if (!Stream)
            {
                Hacks.DisableDrawings = false;
                Hacks.RenderWatermark = true;
            }
            if (player.IsDead)
            {
                Pre = false;
            }
            Hacks.IngameChat = false;
            Hacks.DisableTextures = true;
            Hacks.TowerRanges = false;
        }
        private static void Orbwalker_OnPreAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            if (target != null && !target.IsDead && target.IsValidTarget(player.GetAutoAttackRange()))
            {
                Pre = true;
            }
        }
        private static void AfterAttack(AttackableUnit target, EventArgs args)
        {
            Pre = false;
        }
        private static void ShowClick(Vector3 position, ClickType type)
        {
            if (lastclick * 1000 + RandomValue / 1000 < Game.Time)
            {
                Hud.ShowClick(type, position);
                lastclick = Game.Time;
            }
        }

        private static void OnIssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs args)
        {
            if (sender.IsMe &&
                (args.Order == GameObjectOrder.MoveTo || args.Order == GameObjectOrder.AttackUnit ||
                 args.Order == GameObjectOrder.AttackTo))
            {
                var clickpos = args.TargetPosition;
                if (args.Order == GameObjectOrder.AttackUnit || args.Order == GameObjectOrder.AttackTo)
                {
                    Hud.ShowClick(ClickType.Attack, clickpos);
                }
                else
                {
                    ShowClick(clickpos, ClickType.Move);
                    Pre = false;
                }
            }
        }
    }
}
