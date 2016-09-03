using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UBSivir
{
    public static class _E_Advance
    {
        static _E_Advance()
        {
            new Advance("Vladimir", "vladimirhemoplaguedebuff", SpellSlot.R, 4000).Add();
            new Advance("Tristana", "tristanaechargesound", SpellSlot.E, 4000).Add();
            new Advance("Karma", "karmaspiritbind", SpellSlot.W, 2000).Add();
            new Advance("Karthus", "karthusfallenone", SpellSlot.R, 3000).Add();
            new Advance("Leblanc", "leblancsoulshackle", SpellSlot.E, 1500).Add();
            new Advance("Leblanc", "leblancsoulshackler", SpellSlot.R, 1500).Add();
            new Advance("Morgana", "soulshackles", SpellSlot.R, 3000).Add();
            new Advance("Zed", "zedultexecute", SpellSlot.R, 3000).Add();
            new Advance("Fizz", "fizzmarinerdoombomb", SpellSlot.R, 1500).Add();

            foreach (var dispell in Advance.GetDispellList().Where(d => EntityManager.Heroes.Enemies.Any(h => h.ChampionName.Equals(d.ChampionName))))
            {
                Config.ShieldMenu2.Add(dispell.ChampionName + dispell.Slot, new CheckBox(dispell.ChampionName + " - " + dispell.Slot, true));
            }

            Game.OnUpdate += OnUpdate;
        }
        static void OnUpdate(EventArgs args)
        {
            if (!Spells.E.IsReady() || !Config.ShieldMenu2["BlockChalleningE"].Cast<CheckBox>().CurrentValue)
                return;

            foreach (var dispell in Dispells.Where(
                d =>
                    Player.HasBuff(d.BuffName) &&
                    Config.ShieldMenu2[d.ChampionName + d.Slot] != null &&
                    Config.ShieldMenu2[d.ChampionName + d.Slot].Cast<CheckBox>().CurrentValue
                ))
            {
                var buff = Player.GetBuff(dispell.BuffName);
                if (buff == null || !buff.IsValid || !buff.IsActive)
                    continue;
                var milisec = buff.EndTime - Game.Time;
                if (milisec < 0.4f)
                {
                    Spells.E.Cast();                  
                }
            }
        }

        public static List<Advance> Dispells
        {
            get { return Advance.GetDispellList(); }
        }

        public static void Initialize()
        {

        }
    }

    public class Advance
    {
        private static readonly List<Advance> DispellList = new List<Advance>();
        public string BuffName;
        public string ChampionName;
        public int Offset;
        public SpellSlot Slot;

        public Advance(string champName, string buff, SpellSlot slot, int offset = 0)
        {
            ChampionName = champName;
            BuffName = buff;
            Slot = slot;
            Offset = offset;
        }

        public void Add()
        {
            DispellList.Add(this);
        }

        public static List<Advance> GetDispellList()
        {
            return DispellList;
        }
    }
}
