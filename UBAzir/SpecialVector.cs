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
        public static void WhereCastQ(Obj_AI_Base enemy, bool forcedEnemy = false)
        {
            var EnemyPos = enemy.ServerPosition;
            var MyPos = Player.Instance.Position;
            var Combo = Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
            var Harass = Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
            var QBonusCombo = Config.ComboMenu["Qcbbonus"].Cast<Slider>().CurrentValue;
            var QBonusHarrass = Config.HarassMenu["Qhrbonus"].Cast<Slider>().CurrentValue;
            var Bonus = Combo ? QBonusCombo : Harass ? QBonusHarrass : QBonusCombo;
            var Path = enemy.Path.LastOrDefault();
            var Soldier = Orbwalker.AzirSoldiers.LastOrDefault(s => s.IsAlly);
            if (Path != null)
            {
                if (MyPos.Distance(Path) >= MyPos.Distance(Soldier))
                {
                    if (enemy != null && enemy.IsValid && !enemy.IsInRange(ObjManager.Soldier_Nearest_Enemy, 375))
                    {
                        var Pos = MyPos.Extend(EnemyPos.Extend(Path, 100).To3D(), MyPos.Distance(EnemyPos) + (float)Bonus).To3D();
                        Spells.Q.Cast(Pos);
                        Orbwalker.ResetAutoAttack();
                    }
                    if (enemy != null && enemy.IsValid && !enemy.IsInRange(ObjManager.Soldier_Nearest_Enemy, 375) && !enemy.IsInRange(Player.Instance, Spells.Q.Range - Bonus))
                    {
                        var Pos = MyPos.Extend(EnemyPos.Extend(Path, 100).To3D() , Spells.Q.Range).To3D();
                        Spells.Q.Cast(Pos);
                        Orbwalker.ResetAutoAttack();
                    }
                }
                if (MyPos.Distance(Path) < MyPos.Distance(Soldier))
                {
                    if (enemy != null && enemy.IsValid && !enemy.IsInRange(ObjManager.Soldier_Nearest_Enemy, 375) && enemy.IsInRange(Player.Instance, Spells.Q.Range))
                    {
                        Spells.Q.Cast(enemy);
                        Orbwalker.ResetAutoAttack();
                    }
                }
            }
        }
        public static void WhereCastW(Obj_AI_Base enemy, bool forcedEnemy = false)
        {
            var prediction = Spells.Q.GetPrediction(enemy);

            if (forcedEnemy)
                if (Spells.W.Range + Spells.W.Radius <= Player.Instance.Distance(prediction.CastPosition))
                    return;

            var endPoint = Player.Instance.ServerPosition.To2D()
                .Extend(prediction.CastPosition.To2D(), Spells.Q.Range);

            if ((prediction.HitChance == HitChance.High || prediction.HitChance == HitChance.Collision || prediction.HitChance == HitChance.Medium) &&
                prediction.UnitPosition.To2D().Distance(Player.Instance.ServerPosition.To2D(), endPoint, false) <
                        Spells.Q.Width + enemy.BoundingRadius)
            {

                if (prediction.CastPosition.Distance(Player.Instance) <= Spells.W.Range)
                    Spells.W.Cast(prediction.CastPosition);
                else
                    Spells.W.Cast(Player.Instance.ServerPosition.To2D().Extend(prediction.CastPosition.To2D(), (float)(Spells.W.Range)).To3D());
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
                    : TargetSelector.GetTarget(Spells.WLine.Range, DamageType.Magical, ObjManager.Soldier_Nearest_Enemy);

                if (!target.IsValidTarget())
                    return;

                var TargetPos = target.Position;
                var minions =
                    EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.Distance(ObjManager.Soldier_Nearest_Enemy) <= Spells.WFocus.Range);
                var monsters =
                    EntityManager.MinionsAndMonsters.Monsters.Where(m => m.Distance(ObjManager.Soldier_Nearest_Enemy) <= Spells.WFocus.Range);
                var champs = EntityManager.Heroes.Enemies.Where(c => c.Distance(ObjManager.Soldier_Nearest_Enemy) <= Spells.WFocus.Range);
                {
                    foreach (var minion in from minion in minions
                                           let polygon = new Geometry.Polygon.Rectangle(
                                               (Vector2)ObjManager.Soldier_Nearest_Enemy,
                                               ObjManager.Soldier_Nearest_Enemy.Extend(minion.Position, Spells.WLine.Range), 50f)
                                           where polygon.IsInside(TargetPos)
                                           select minion)
                    {
                        Player.IssueOrder(GameObjectOrder.AttackUnit, minion);
                    }

                    foreach (var monster in from monster in monsters
                                            let polygon = new Geometry.Polygon.Rectangle(
                                                (Vector2)ObjManager.Soldier_Nearest_Enemy,
                                               ObjManager.Soldier_Nearest_Enemy.Extend(monster.Position, Spells.WLine.Range), 50f)
                                            where polygon.IsInside(TargetPos)
                                            select monster)
                    {
                        Player.IssueOrder(GameObjectOrder.AttackUnit, monster);
                    }

                    foreach (var champ in from champ in champs
                                          let polygon = new Geometry.Polygon.Rectangle(
                                              (Vector2)ObjManager.Soldier_Nearest_Enemy,
                                               ObjManager.Soldier_Nearest_Enemy.Extend(champ.Position, Spells.WLine.Range), 50f)
                                          where polygon.IsInside(TargetPos)
                                          select champ)
                    {
                        if (!Orbwalker.CanAutoAttack || Orbwalker.IsAutoAttacking)
                        {
                            return;
                        }
                        else
                        {
                            Player.IssueOrder(GameObjectOrder.AttackUnit, champ);
                        }
                    }
                }
            }
        }
        internal static bool Between(AIHeroClient target)
        {
            return
                Orbwalker.AzirSoldiers.Select(soldier => new Geometry.Polygon.Rectangle(Player.Instance.Position, soldier.Position, target.BoundingRadius))
                .Any(rectangle => rectangle.IsInside(target));
        }       
    }
}
