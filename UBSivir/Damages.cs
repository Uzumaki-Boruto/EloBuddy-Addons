using EloBuddy;
using EloBuddy.SDK;

namespace UBSivir
{
    class Damages
    {   
        public static float QDamage(Obj_AI_Base target)
        {
            var damage = 0f;
            if (Spells.Q.IsReady())
                damage += ObjectManager.Player.GetSpellDamage(target, SpellSlot.Q);
            return damage;
        }      
    }
}
