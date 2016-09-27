using System;
using System.Linq;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace UBActivator
{
    class Utility
    {
        public static int[] Level = { 0, 0, 0, 0 };
        public static readonly Item[] ItemTear =
        {
            Items.Tear_of_the_Goddess, Items.Archangel_Staff, Items.Manamune,
        };
        public static float LastStack;
        public static float LastRemind;
        public static bool IsReady
        {
            get { return ItemTear.Any(i =>i.IsOwned() && Game.Time - LastStack > 1.5f); }
        }
        public static void OnTick()
        {
            if (Player.Instance.IsInShopRange() && Config.Utility["etear"].Cast<CheckBox>().CurrentValue)
            {
                var Q = new Spell.Skillshot(SpellSlot.Q, 500, EloBuddy.SDK.Enumerations.SkillShotType.Linear);
                var E = new Spell.Skillshot(SpellSlot.W, 500, EloBuddy.SDK.Enumerations.SkillShotType.Linear);
                var W = new Spell.Skillshot(SpellSlot.E, 500, EloBuddy.SDK.Enumerations.SkillShotType.Linear);
                var R = new Spell.Skillshot(SpellSlot.R, 300, EloBuddy.SDK.Enumerations.SkillShotType.Circular);
                #region Champion
                switch (Player.Instance.ChampionName)
                {
                    case "Ahri":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W);
                                LastStack = Game.Time;
                            }
                            if (IsReady && E.IsReady())
                            {
                                Player.CastSpell(SpellSlot.E, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Anivia":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && R.IsReady())
                            {
                                Player.CastSpell(SpellSlot.R, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Annie":
                        {
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && E.IsReady())
                            {
                                Player.CastSpell(SpellSlot.E);
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Aurelion Sol":
                        {
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W);
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Azir":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W);
                                LastStack = Game.Time;
                            }
                            if (IsReady && E.IsReady())
                            {
                                Player.CastSpell(SpellSlot.E);
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Brand":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Cassiopeia":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "ChoGath":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Corki":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && E.IsReady())
                            {
                                Player.CastSpell(SpellSlot.E);
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Diana":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W);
                                LastStack = Game.Time;
                            }
                            if (IsReady && E.IsReady())
                            {
                                Player.CastSpell(SpellSlot.E);
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Ekko":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && E.IsReady())
                            {
                                Player.CastSpell(SpellSlot.E, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Ezreal":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && E.IsReady())
                            {
                                Player.CastSpell(SpellSlot.E, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Karthus":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && E.IsReady())
                            {
                                Player.CastSpell(SpellSlot.E);
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Kassadin":
                        {
                            if (IsReady && W.IsReady())
                            {
                                Player.CastSpell(SpellSlot.W);
                                LastStack = Game.Time;
                            }
                            if (IsReady && E.IsReady())
                            {
                                Player.CastSpell(SpellSlot.E, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                            if (IsReady && R.IsReady())
                            {
                                Player.CastSpell(SpellSlot.R, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    case "Ryze":
                        {
                            if (IsReady && Q.IsReady())
                            {
                                Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 200).To3D());
                                LastStack = Game.Time;
                            }
                        }
                        break;
                    default:
                        break;
                #endregion
                }
            }
            if (Player.Instance.IsInShopRange() && Player.Instance.Level >= 9 && Player.Instance.Level < 12 && Config.Utility["remind"].Cast<CheckBox>().CurrentValue)
            {
                if (Game.Time - LastRemind > 15f)
                {
                    Chat.Print("Change your Trinket");
                    LastRemind = Game.Time;
                }
            }
        }      
        public static void Game_OnTick()
        {
            if (!Config.Level["lvl"].Cast<CheckBox>().CurrentValue) return;
            var QLevel = Player.GetSpell(SpellSlot.Q).Level + Extensions.QOff;
            var WLevel = Player.GetSpell(SpellSlot.W).Level + Extensions.WOff;
            var ELevel = Player.GetSpell(SpellSlot.E).Level + Extensions.EOff;
            var RLevel = Player.GetSpell(SpellSlot.R).Level + Extensions.ROff;

            Level = new[] { 0, 0, 0, 0 };

            for (var i = 1; i <= Player.Instance.Level; i++)
            {
                switch (Config.Level[i.ToString() + Player.Instance.ChampionName].Cast<ComboBox>().CurrentValue)
                {
                    case 0:
                        break;
                    case 1:
                        Level[0] += 1;
                        break;
                    case 2:
                        Level[1] += 1;
                        break;
                    case 3:
                        Level[2] += 1;
                        break;
                    case 4:
                        Level[3] += 1;
                        break;
                }
            }

            if (QLevel < Level[0])
            {
                LevelUp(SpellSlot.Q);
            }

            if (WLevel < Level[1])
            {
                LevelUp(SpellSlot.W);
            }

            if (ELevel < Level[2])
            {
                LevelUp(SpellSlot.E);
            }

            if (RLevel < Level[3])
            {
                LevelUp(SpellSlot.R);
            }
        }
        public static void LevelUp(SpellSlot slot)
        {
            var Time = Config.Level["lvldelay"].Cast<Slider>().CurrentValue;
            var Delay = Config.Level["lvlrandom"].Cast<CheckBox>().CurrentValue ?
                new Random().Next(0, Time) :
                Time;
            if (Player.Instance.Spellbook.CanSpellBeUpgraded(slot))
            Core.DelayAction(() => Player.LevelSpell(slot), Delay);
        }
    }
}
