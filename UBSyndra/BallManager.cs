using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace UBSyndra
{
    class BallManager
    {
        public static Obj_AI_Base[] Balls
        {
            get
            {
                return ObjectManager.Get<Obj_AI_Base>().Where(a => a.Name == "Seed" && a.IsValid && a.IsAlly && !a.IsDead).ToArray();
            }
        }
        //public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        //{
        //    if (!(sender is Obj_AI_Minion) || !sender.IsAlly || sender.Name != "Seed")
        //        return;
        //    else
        //    {
        //        var ball = sender as Obj_AI_Minion;
        //        Balls.Add(ball);
        //    }
        //}
        //public static void GameObject_OnDelete(GameObject sender, EventArgs args)
        //{
        //    if (!(sender is Obj_AI_Minion) || !sender.IsAlly || sender.Name != "Seed")
        //        return;
        //    else
        //    {
        //        var ball = sender as Obj_AI_Minion;
        //        Balls.Remove(ball);
        //    }
        //}
        public static Obj_AI_Base Get_Grab_Shit()
        {
            var minion = ObjectManager.Get<Obj_AI_Minion>().Where(x => x.IsValidTarget(Spells.W.Range) && !x.IsAlly).OrderBy(x => x.Distance(Player.Instance)).FirstOrDefault();
            if (Balls.Any())
            {
                return Balls.Where(x => Spells.W.IsInRange(x)).OrderBy(x => x.Distance(Player.Instance)).FirstOrDefault();
            }
            else if (minion != null)
            {
                return minion;
            }
            else
            {
                return null;
            }
        }
        public static void AutoE(EventArgs agrs)
        {
            if (Balls.Any() && Spells.E.IsReady())
            {
                foreach (var ball in Balls)
                {
                    var Vector = Player.Instance.Position.Extend(ball, Spells.QE.Range);
                    var Rectangle = new Geometry.Polygon.Rectangle(Player.Instance.Position.To2D(), Vector, Spells.QE.Width);
                    var Count = EntityManager.Heroes.Enemies.Count(x => x.IsValid() && !x.IsDead && Rectangle.IsInside(x));
                    if (Count >= Config.ComboMenu.GetValue("Ecbhit") && Spells.E.IsInRange(ball))
                    {
                        Spells.E.Cast(ball);
                    }
                }
            }
        }
    }
}
