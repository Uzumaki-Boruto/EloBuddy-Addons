using System;
using System.Linq;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
//using EloBuddy.SDK.Notifications;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UBAzir
{
    class Insec : UBAzir.SpecialVector
    {
        public static Obj_AI_Turret Turret = EntityManager.Turrets.Allies.FirstOrDefault(t => t.IsValidTarget(1250));
        public static AIHeroClient Ally = EntityManager.Heroes.Allies.FirstOrDefault(a => a.IsValidTarget(1000));

        #region Normal Insec
        public static void Do_Normal_Insec()
        {
            var normal = Config.Insec["normal.1"].Cast<ComboBox>().CurrentValue;
            var target = TargetSelector.GetTarget(1100, DamageType.Magical);
            var Objectnumber = Config.Insec["normalgoto"].Cast<ComboBox>().CurrentValue;
            var Object = Objectnumber == 0 ?
                Player.Instance.Position : Objectnumber == 1 ?
                Game.CursorPos : Objectnumber == 2 ? (target != null ? target.Position : Game.CursorPos) : Vector3.Zero;
            if (target != null && Spells.R.IsReady() && Player.Instance.Mana >= 270)
            {
                if (target.IsValidTarget(300))
                {
                    switch (normal)
                    {
                        case 0:
                            {
                                SpecialVector.WhereCastR(target, SpecialVector.I_want.Cursor);
                            }
                            break;
                        case 1:
                            {
                                SpecialVector.WhereCastR(target, SpecialVector.I_want.Turret);
                            }
                            break;
                        case 2:
                            {
                                SpecialVector.WhereCastR(target, SpecialVector.I_want.Ally);
                            }
                            break;
                        case 3:
                            {
                                SpecialVector.WhereCastR(target, SpecialVector.I_want.LastPostion);
                            }
                            break;
                        case 4:
                            {
                                SpecialVector.WhereCastR(target, SpecialVector.I_want.All);
                            }
                            break;
                    }
                }                
                if (target.IsValidTarget(1100))
                {
                    if (ObjManager.All_Basic_Is_Ready)
                    {
                        var pred = Prediction.Position.PredictUnitPosition(target, 400);
                        Mode.Flee(pred.To3D(), true);
                    }
                    else
                    {
                        Player.IssueOrder(GameObjectOrder.MoveTo, Object);
                    }
                }
                if (Config.Insec["normal"].Cast<KeyBind>().CurrentValue && Config.Insec["allowfl"].Cast<CheckBox>().CurrentValue && Spells.Flash != null)
                {
                    var unit = TargetSelector.GetTarget(Spells.Flash.Range, DamageType.Magical);
                    if (unit != null)
                    {
                        if (Spells.Flash.IsReady() && Spells.R.IsReady())
                        {
                            var PosAndHits = GetBestRPos(unit.Position.To2D());
                            if (PosAndHits.First().Value >= Config.Insec["flvalue"].Cast<Slider>().CurrentValue)
                            {
                                Spells.Flash.Cast(PosAndHits.First().Key.To3D());
                                switch (normal)
                                {
                                    case 0:
                                        {
                                            SpecialVector.WhereCastR(unit, SpecialVector.I_want.Cursor);
                                        }
                                        break;
                                    case 1:
                                        {
                                            SpecialVector.WhereCastR(unit, SpecialVector.I_want.Turret);
                                        }
                                        break;
                                    case 2:
                                        {
                                            SpecialVector.WhereCastR(unit, SpecialVector.I_want.Ally);
                                        }
                                        break;
                                    case 3:
                                        {
                                            SpecialVector.WhereCastR(unit, SpecialVector.I_want.LastPostion);
                                        }
                                        break;
                                    case 4:
                                        {
                                            SpecialVector.WhereCastR(unit, SpecialVector.I_want.All);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Player.IssueOrder(GameObjectOrder.MoveTo, Object);
                }
            }
            else
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, Object);
            }
        }
        #endregion

        #region God Insec
        public static void Do_God_Insec()
        {
            var target = TargetSelector.GetTarget(1100, DamageType.Magical);
            var god1 = Config.Insec["god.1"].Cast<ComboBox>().CurrentValue;
            var god2 = Config.Insec["god.2"].Cast<ComboBox>().CurrentValue;
            var CastQTo = new Vector3();
            var SoldierPos = Vector3.Zero;
            var Objectnumber = Config.Insec["godgoto"].Cast<ComboBox>().CurrentValue;
            var Object = Objectnumber == 0 ?
                Player.Instance.Position : Objectnumber == 1 ?
                Game.CursorPos : Objectnumber == 2 ? (target != null ? target.Position : Game.CursorPos) : Vector3.Zero;

            //var incapability = new SimpleNotification("UBAzir God Insec", "It isn't qualified to perform");
            if (target != null && target.IsValidTarget(300) && Spells.R.IsReady() && Player.Instance.Mana >= 270)
            {
                switch (god1)
                {
                    case 0:
                        {
                            SpecialVector.WhereCastR(target, SpecialVector.I_want.Cursor);
                        }
                        break;
                    case 1:
                        {
                            SpecialVector.WhereCastR(target, SpecialVector.I_want.Turret);
                        }
                        break;
                    case 2:
                        {
                            SpecialVector.WhereCastR(target, SpecialVector.I_want.Ally);
                        }
                        break;
                    case 3:
                        {
                            SpecialVector.WhereCastR(target, SpecialVector.I_want.LastPostion);
                        }
                        break;
                    case 4:
                        {
                            SpecialVector.WhereCastR(target, SpecialVector.I_want.All);
                        }
                        break;
                }
            }
            switch (god2)
            {
                case 0:
                    {
                        CastQTo = Player.Instance.Position.Extend(Game.CursorPos, Spells.Q.Range).To3D();
                    }
                    break;

                case 1:
                    {
                        var Ally = EntityManager.Heroes.Allies.OrderByDescending(a => a.CountEnemiesInRange(Spells.R.Range)).FirstOrDefault();
                        CastQTo = Ally != null && !Ally.IsDead ?
                        Player.Instance.Position.Extend(Ally.Position, Spells.Q.Range).To3D() :
                        Player.Instance.Position.Extend(Game.CursorPos, Spells.Q.Range).To3D();
                    }
                    break;
                case 2:
                    {
                        var Turret = EntityManager.Turrets.Allies.FirstOrDefault(t => t.IsValidTarget(1250));
                        CastQTo = Turret != null && Turret.IsDead ?
                        Player.Instance.Position.Extend(Turret.Position, Spells.Q.Range).To3D() : 
                        Player.Instance.Position.Extend(Game.CursorPos, Spells.Q.Range).To3D();
                    }
                    break;
            }
            if (target != null && target.IsValidTarget(300, false, ObjManager.Soldier_Nearest_Enemy))
            {
                if (Spells.R.IsReady())
                {
                    var pred = Spells.R.GetPrediction(target);                   
                    if (ObjManager.All_Basic_Is_Ready)
                    {
                        if (ObjManager.CountAzirSoldier == 0)
                        {
                            SpecialVector.WhereCastW(target, true);
                        }
                        if (ObjManager.CountAzirSoldier != 0)
                        {
                            if (pred.UnitPosition.Distance(ObjManager.Soldier_Nearest_Enemy) <= 300)
                            {
                                if (!SpecialVector.Between(target))
                                {
                                    if (ObjManager.CountAzirSoldier >= 0)
                                    {
                                        Spells.E.Cast(ObjManager.Soldier_Nearest_Enemy);
                                        var delay = Config.Flee["fleedelay"].Cast<Slider>().CurrentValue;
                                        var time = (Player.Instance.ServerPosition.Distance(ObjManager.Soldier_Nearest_Azir) / Spells.E.Speed) * (700 + delay - Game.Ping);
                                        Core.DelayAction(() => Spells.Q_in_Flee.Cast(CastQTo), (int)time);
                                    }
                                }
                                else
                                {
                                    Player.IssueOrder(GameObjectOrder.MoveTo, Object);
                                }
                            }
                            else
                            {
                                Player.IssueOrder(GameObjectOrder.MoveTo, Object);
                            }
                        }
                        else
                        {
                            Player.IssueOrder(GameObjectOrder.MoveTo, Object);
                        }
                    }                    
                    else
                    {
                        Player.IssueOrder(GameObjectOrder.MoveTo, Object);
                    }
                }
                else
                {
                    Player.IssueOrder(GameObjectOrder.MoveTo, Object);
                }
                #region Rework
                /*if (Spells.Q.IsReady())
                    {
                        if (Spells.R.IsReady())
                        {                                                      
                            
                            if (ObjManager.CountAzirSoldier < 1 && ObjManager.All_Basic_Is_Ready && Player.Instance.Mana > 370)
                            {
                                SpecialVector.WhereCastW(target);
                            }

                            if (ObjManager.CountAzirSoldier > 0 && target.Distance(Player.Instance) > 200)
                            {
                                if (ObjManager.Soldier_Nearest_Enemy != Vector3.Zero)
                                {
                                    SoldierPos = ObjManager.Soldier_Nearest_Enemy;
                                }

                                if (target.Position.IsInRange(SoldierPos, Spells.R.Width))
                                {
                                    var time = (Player.Instance.ServerPosition.Distance(ObjManager.Soldier_Nearest_Azir) / Spells.E.Speed) * (500 + Game.Ping);
                                    Spells.E.Cast(Player.Instance.Position.Extend(target, Spells.E.Range).To3D());
                                    Core.DelayAction(() => Spells.Q.Cast(Player.Instance.Position.Extend(CastQTo, Spells.Q.Range).To3D()), (int)time);
                                }
                                else
                                {
                                    Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                                }
                            }
                            else
                            {
                                Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                            } 
                        }
                        else
                        {
                            Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                        }
                    }
                    else
                    {
                        Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                    }*/
                #endregion
            }
            else
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, Object);
            }
        }
        #endregion

        #region Flash Logic

        static int CountRHits(Vector2 CastPosition)
        {
            int Hits = new int();

            foreach (Vector3 EnemyPos in GetEnemiesPosition())
            {
                var start = Player.Instance.Position.Extend(CastPosition, Player.Instance.Distance(CastPosition) - Spells.R.Range);
                var end = Player.Instance.Position.Extend(CastPosition, Player.Instance.Distance(CastPosition) + 300);
                var retangle = new Geometry.Polygon.Rectangle(start, end, Spells.R.Width);
                if (retangle.IsInside(EnemyPos)) Hits += 1;
            }

            return Hits;
        }
        static Dictionary<Vector2, int> GetBestRPos(Vector2 TargetPosition)
        {
            Dictionary<Vector2, int> PosAndHits = new Dictionary<Vector2, int>();

            List<Vector2> RPos = new List<Vector2>
            {
                new Vector2(TargetPosition.X - 250, TargetPosition.Y + 100),
                new Vector2(TargetPosition.X - 250, TargetPosition.Y),

                new Vector2(TargetPosition.X - 200, TargetPosition.Y + 300),
                new Vector2(TargetPosition.X - 200, TargetPosition.Y + 200),
                new Vector2(TargetPosition.X - 200, TargetPosition.Y + 100),
                new Vector2(TargetPosition.X - 200, TargetPosition.Y - 100),
                new Vector2(TargetPosition.X - 200, TargetPosition.Y),

                new Vector2(TargetPosition.X - 160, TargetPosition.Y - 160),

                new Vector2(TargetPosition.X - 100, TargetPosition.Y + 300),
                new Vector2(TargetPosition.X - 100, TargetPosition.Y + 200),
                new Vector2(TargetPosition.X - 100, TargetPosition.Y + 100),
                new Vector2(TargetPosition.X - 100, TargetPosition.Y + 250),
                new Vector2(TargetPosition.X - 100, TargetPosition.Y - 200),
                new Vector2(TargetPosition.X - 100, TargetPosition.Y - 100),
                new Vector2(TargetPosition.X - 100, TargetPosition.Y),

                new Vector2(TargetPosition.X, TargetPosition.Y + 300),
                new Vector2(TargetPosition.X, TargetPosition.Y + 270),
                new Vector2(TargetPosition.X, TargetPosition.Y + 200),
                new Vector2(TargetPosition.X, TargetPosition.Y + 100),

                new Vector2(TargetPosition.X, TargetPosition.Y),

                new Vector2(TargetPosition.X, TargetPosition.Y - 100),
                new Vector2(TargetPosition.X, TargetPosition.Y - 200),

                new Vector2(TargetPosition.X + 100, TargetPosition.Y),
                new Vector2(TargetPosition.X + 100, TargetPosition.Y - 100),
                new Vector2(TargetPosition.X + 100, TargetPosition.Y - 200),
                new Vector2(TargetPosition.X + 100, TargetPosition.Y + 100),
                new Vector2(TargetPosition.X + 100, TargetPosition.Y + 200),
                new Vector2(TargetPosition.X + 100, TargetPosition.Y + 250),
                new Vector2(TargetPosition.X + 100, TargetPosition.Y + 300),

                new Vector2(TargetPosition.X + 160, TargetPosition.Y - 160),

                new Vector2(TargetPosition.X + 200, TargetPosition.Y),
                new Vector2(TargetPosition.X + 200, TargetPosition.Y - 100),
                new Vector2(TargetPosition.X + 200, TargetPosition.Y + 100),
                new Vector2(TargetPosition.X + 200, TargetPosition.Y + 200),
                new Vector2(TargetPosition.X + 200, TargetPosition.Y + 300),

                new Vector2(TargetPosition.X + 250, TargetPosition.Y),
                new Vector2(TargetPosition.X + 250, TargetPosition.Y + 100),
            };

            foreach (Vector2 pos in RPos)
            {
                PosAndHits.Add(pos, CountRHits(pos));
            }

            Vector2 PosToGG = PosAndHits.First(pos => pos.Value == PosAndHits.Values.Max()).Key;
            int Hits = PosAndHits.First(pos => pos.Key == PosToGG).Value;

            return new Dictionary<Vector2, int>() { { PosToGG, Hits } };
        }
        public static List<Vector3> GetEnemiesPosition()
        {
            List<Vector3> Positions = new List<Vector3>();

            foreach (AIHeroClient Hero in EntityManager.Heroes.Enemies.Where(e => !e.IsDead && Player.Instance.Distance(e) <= 600))
            {
                Positions.Add(Prediction.Position.PredictUnitPosition(Hero, 400).To3D());
            }

            return Positions;
        }
        #endregion
    }
}
