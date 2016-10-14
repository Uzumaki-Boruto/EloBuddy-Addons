using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UBAzir
{
    class SpecialVector
    {
        public static AIHeroClient EnemyTarget;
        public enum I_want
        {
            Cursor, Ally, Turret, LastPostion, All, On_Gapclose
        }
        public static I_want Iwant;
        public static void WhereCastQ(Obj_AI_Base enemy, int BonusRange)
        {
            var MyPos = Player.Instance.Position;
            var Path = Spells.Q.GetPrediction(enemy).CastPosition;
            if (!Player.Instance.IsInAutoAttackRange(enemy))
            {
                if (Player.Instance.Distance(Path) >= Player.Instance.Distance(enemy))
                {
                    var Pos = MyPos.Extend(Path, Player.Instance.Distance(Path) + BonusRange).To3DWorld();
                    if (Spells.Q.IsInRange(Pos))
                    {
                        Spells.Q.Cast(Pos);
                    }
                    else
                    {
                        Spells.Q.Cast(Path);
                    }
                }
                else
                {
                    Spells.Q.Cast(Path);
                }
            }            
        }
        public static void WhereCastW(Obj_AI_Base enemy, int max)
        {
            var prediction = Spells.Q.GetPrediction(enemy);
            if (ObjManager.CountAzirSoldier == 0 || (Spells.Q.IsReady() && ObjManager.CountAzirSoldier <= max))
            {
                Spells.W.Cast(Player.Instance.Position.Extend(prediction.CastPosition, Spells.W.Range).To3DWorld());
            }
            else
            {
                if (Spells.W.IsInRange(prediction.CastPosition))
                {
                    Spells.W.Cast(prediction.CastPosition);
                }
            }
        }
        public static void WhereCastR(Obj_AI_Base enemy, Vector3 Seclect)
        {
            if (enemy != null)
            {
                var pred = Spells.R.GetPrediction(enemy);
                if (pred.UnitPosition.IsInRange(Player.Instance, 300) && enemy.IsValid)
                {
                    Spells.R.Cast(Player.Instance.Position.Extend(Seclect, Spells.R.Range).To3DWorld());
                }
            }
        }
        public static void WhereCastR(Obj_AI_Base enemy, I_want want, Vector3 From = new Vector3(), int delay = 0)
        {
            var MyPos = Player.Instance.Position;
            var Turret = Insec.Turret;
            var Allies = Insec.Ally;
            var MyPosBefore = ObjManager.LastMyPos;
            if (enemy != null)
            {
                var pred = Spells.R.GetPrediction(enemy);
                if (pred.UnitPosition.IsInRange(Player.Instance, 300) && enemy.IsValid)
                {
                    if (want == I_want.Cursor)
                    {
                        var Pos = MyPos.Extend(Game.CursorPos, 400).To3D();
                        Spells.R.Cast(Pos);
                    }
                    if (want == I_want.Turret)
                    {
                        if (Turret != null && !Turret.IsDead && Turret.IsValid)
                        {
                            var Pos = MyPos.Extend(Turret.Position, 400).To3D();
                            Spells.R.Cast(Pos);
                        }
                        if (Turret == null || Turret.IsDead || !Turret.IsValid)
                        {
                            var Pos = MyPos.Extend(Game.CursorPos, 400).To3D();
                            Spells.R.Cast(Pos);
                        }
                    }
                    if (want == I_want.Ally)
                    {
                        if (Allies != null && !Allies.IsDead)
                        {
                            var Pos = MyPos.Extend(Allies.Position, 400).To3D();
                            Spells.R.Cast(Pos);
                        }
                        if (Allies == null || Allies.IsDead)
                        {
                            var Pos = MyPos.Extend(Game.CursorPos, 400).To3D();
                            Spells.R.Cast(Pos);
                        }
                    }
                    if (want == I_want.LastPostion)
                    {
                        Spells.R.Cast(ObjManager.LastMyPos);
                    }
                    if (want == I_want.All)
                    {
                        if (Turret != null && !Turret.IsDead && Turret.IsValid)
                        {
                            var Pos = MyPos.Extend(Turret.Position, 400).To3D();
                            Spells.R.Cast(Pos);
                        }
                        if (Turret == null || Turret.IsDead || !Turret.IsValid)
                        {
                            if (Allies != null && !Allies.IsDead)
                            {
                                var Pos = MyPos.Extend(Allies.Position, 400).To3D();
                                Spells.R.Cast(Pos);
                            }
                            if (Allies == null || Allies.IsDead)
                            {
                                var Pos = MyPos.Extend(Game.CursorPos, 400).To3D();
                                Spells.R.Cast(Pos);
                            }
                        }
                    }
                }
                if (!enemy.IsInRange(Player.Instance, 300) && pred.UnitPosition.IsInRange(Player.Instance, Spells.R.Range))
                {
                    Spells.R.Cast(pred.CastPosition);
                }
            }
            if (want == I_want.On_Gapclose)
            {
                if (Turret != null && !Turret.IsDead && Turret.IsValid)
                {
                    var Pos = From.Extend(Turret, MyPos.Distance(From) + Spells.R.Range).To3D();
                    Core.DelayAction(() =>Spells.R.Cast(Pos), delay);
                }
                if (Turret == null || Turret.IsDead || !Turret.IsValid)
                {
                    if (Allies != null && !Allies.IsDead)
                    {
                        var Pos = From.Extend(Allies.Position, 400).To3D();
                        Core.DelayAction(() => Spells.R.Cast(Pos), delay);
                    }
                    if (Allies == null || Allies.IsDead)
                    {
                        var Pos = MyPos.Extend(From, 400).To3D();
                        Core.DelayAction(() => Spells.R.Cast(Pos), delay);
                    }
                }
            }
        }
        public static void AttackOtherObject()
        {
            if (ObjManager.Soldier_Nearest_Enemy != Vector3.Zero)
            {
                var target = TargetSelector.SelectedTarget != null &&
                             TargetSelector.SelectedTarget.Distance(ObjManager.Soldier_Nearest_Enemy) < 450
                    ? TargetSelector.SelectedTarget
                    : TargetSelector.GetTarget(425, DamageType.Magical, ObjManager.Soldier_Nearest_Enemy);

                if (!target.IsValidTarget())
                    return;

                var TargetPos = target.Position;
                var minions =
                    EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.Distance(ObjManager.Soldier_Nearest_Enemy) <= 375);
                var monsters =
                    EntityManager.MinionsAndMonsters.Monsters.Where(m => m.Distance(ObjManager.Soldier_Nearest_Enemy) <= 375);
                var champs = EntityManager.Heroes.Enemies.Where(c => c.Distance(ObjManager.Soldier_Nearest_Enemy) <= 375);
                {
                    foreach (var minion in from minion in minions
                                           let polygon = new Geometry.Polygon.Rectangle(
                                               (Vector2)ObjManager.Soldier_Nearest_Enemy,
                                               ObjManager.Soldier_Nearest_Enemy.Extend(minion.Position, 425), 50f)
                                           where polygon.IsInside(TargetPos)
                                           select minion)

                        if (minion != null)
                        {
                            Orbwalker.ForcedTarget = minion;
                        }
                        else
                        {
                            Orbwalker.ForcedTarget = TargetSelector.SelectedTarget;
                        }

                    foreach (var monster in from monster in monsters
                                            let polygon = new Geometry.Polygon.Rectangle(
                                                (Vector2)ObjManager.Soldier_Nearest_Enemy,
                                               ObjManager.Soldier_Nearest_Enemy.Extend(monster.Position, 425), 50f)
                                            where polygon.IsInside(TargetPos)
                                            select monster)
                        if (monster != null)
                        {
                            Orbwalker.ForcedTarget = monster;
                        }
                        else
                        {
                            Orbwalker.ForcedTarget = TargetSelector.SelectedTarget;
                        }

                    foreach (var champ in from champ in champs
                                          let polygon = new Geometry.Polygon.Rectangle(
                                              (Vector2)ObjManager.Soldier_Nearest_Enemy,
                                               ObjManager.Soldier_Nearest_Enemy.Extend(champ.Position, 425), 50f)
                                          where polygon.IsInside(TargetPos)
                                          select champ)
                    {
                        if (champ != null)
                        {
                            Orbwalker.ForcedTarget = champ;
                        }
                        else
                        {
                            Orbwalker.ForcedTarget = TargetSelector.SelectedTarget;
                        }
                    }
                }
            }
        }
        internal static bool Between(AIHeroClient target, Vector3 CheckPoint)
        {
            return new Geometry.Polygon.Rectangle(Player.Instance.Position, CheckPoint, target.BoundingRadius).IsInside(target);
        }       
    }
}
