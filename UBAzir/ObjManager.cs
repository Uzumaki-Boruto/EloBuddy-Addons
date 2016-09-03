using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Linq;

namespace UBAzir
{
    public class ObjManager
    {
        public static Vector3 LastMyPos;
        public static bool All_Basic_Is_Ready = false;
        public static float Time;

        public static int CountAzirSoldier
        {
            get
            {
                return Orbwalker.AzirSoldiers.Count(s => s.IsAlly);
            }
        }
        public static Vector3 Soldier_Can_Cast_E { get; set; }
        public static Vector3 Soldier_Nearest_Enemy
        {
            get
            {
                var target = TargetSelector.GetTarget(1300, DamageType.Magical);
                var Soldier = Orbwalker.ValidAzirSoldiers.OrderBy(s => s.Distance(target)).FirstOrDefault();
                if (Soldier != null) return Soldier.Position;
                else return Vector3.Zero;
            }
        }
        /*public static Vector3 Soldier_Nearest_Minion
        {
            get
            {
                var minion = EntityManager.MinionsAndMonsters.Minions.Where(m => m.IsEnemy).FirstOrDefault();
                var Soldier = Orbwalker.ValidAzirSoldiers.OrderBy(s => s.Distance(minion)).FirstOrDefault();
                if (Soldier != null && minion.Distance(Soldier.Position) <= 375) return Soldier.Position;
                else return Vector3.Zero;
            }
        }*/
        public static Vector3 Soldier_Nearest_Azir
        {
            get
            {
                var Soldier = Orbwalker.AzirSoldiers.OrderBy(s => s.Distance(Player.Instance.Position)).FirstOrDefault();
                if (Soldier != null && Player.Instance.Distance(Soldier.Position) <= 1050) return Soldier.Position;
                else return Vector3.Zero;
            }
        }
        public static Vector3 SoldierPos
        {
            get
            {
                if (CountAzirSoldier < 1)
                    return Vector3.Zero;
                else return Orbwalker.AzirSoldiers.LastOrDefault().Position;
            }
        }
        /*public static List<Obj_AI_Minion> AzirSoldiers = new List<Obj_AI_Minion>();
        static void Obj_AI_Base_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name == "AzirSoldier" && sender.IsAlly)
            {
                AzirSoldiers.Add((Obj_AI_Minion) sender);
            }

            if (sender.Name == "Azir_Base_W_Soldier_Dissipate.troy" && AzirSoldiers.Count > 0)
            {
                AzirSoldiers.Remove(AzirSoldiers.Aggregate((curMin, x) => (curMin == null || x.Distance(sender.Position) < curMin.Distance(sender.Position) ? x : curMin)));
            }      
        }*/
        public static void GetMyPosBefore(EventArgs args)
        {
            if (Game.Time > Time + 1f)
            {
                LastMyPos = Player.Instance.Position;
                if (Spells.Q.IsReady() && Spells.W.IsReady() && Spells.E.IsReady())
                    All_Basic_Is_Ready = true;
                else All_Basic_Is_Ready = false;
                Time = Game.Time;
            }
        }
    }
}
