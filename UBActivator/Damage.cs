using EloBuddy;
using EloBuddy.SDK;

namespace UBActivator
{
    class Damage
    {
        public static float HextechDamage(Obj_AI_Base target, HextechItem Item)
        {
            var DamageIce = Player.Instance.CalculateDamageOnUnit(
                target, DamageType.Magical,
                (new[] { 0f, 100f, 106f, 112f, 118f, 124f, 130f, 136f, 141f, 147f, 153f, 159f, 165f, 171f, 176f, 182f, 188f, 194f, 200f }[Player.Instance.Level]) + 0.35f * Player.Instance.TotalMagicalDamage);
            var DamageFire = Player.Instance.CalculateDamageOnUnit(
                target, DamageType.Magical,
                (new[] { 0f, 75f, 79f, 83f, 88f, 92f, 97f, 101f, 106f, 110f, 115f, 119f, 124f, 128f, 132f, 137f, 141f, 146f, 150f }[Player.Instance.Level]) + 0.35f * Player.Instance.TotalMagicalDamage);
            var DamageGunblade = Player.Instance.CalculateDamageOnUnit(
                target, DamageType.Magical, 250 + 0.3f * Player.Instance.TotalMagicalDamage);
            
            switch (Item)
            {
                case HextechItem.Ice:
                    return DamageIce;
                case HextechItem.Fire:
                    return DamageIce;
                case HextechItem.Gunblade:
                    return DamageIce;
                default:
                    return 0f;
            }
        }
        public static float CutlassDamage(Obj_AI_Base target)
        {
            var Damage = Player.Instance.CalculateDamageOnUnit(
                target, DamageType.Magical, 100f);
            return Damage;
        }
        public static float BladeDamage(Obj_AI_Base target)
        {
            var Damage = Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, target.MaxHealth * 0.1f);
            var Damage2 = Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, 100f);
            return Damage > Damage2 ? Damage : Damage2;
        }
    }
}
