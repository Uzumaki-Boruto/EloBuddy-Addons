using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;
using System.Linq;

namespace UBKennen
{
    static class Extension
    {
        public static bool Unkillable(AIHeroClient target)
        {
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "UndyingRage"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "ChronoShift"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "JudicatorIntervention"))
            {
                return true;
            }
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "kindredrnodeathbuff"))
            {
                return true;
            }
            if (target.HasBuffOfType(BuffType.Invulnerability))
            {
                return true;
            }
            return target.IsInvulnerable;
        }
        public static void DrawArc(this Vector2 position, float radius, System.Drawing.Color color, float startDegree, float length, float width = 0.6F, int quality = -1)
        {
            float PI2 = (float)Math.PI * 2;
            if (quality == -1)
            {
                quality = (int)(radius / 7 + 100);
            }
            var points = new Vector3[(int)(Math.Abs(quality * length / PI2) + 1)];
            var rad = new Vector2(0, radius);

            for (var i = 0; i <= (int)(Math.Abs(quality * length / PI2)); i++)
            {
                points[i] = (position + rad).RotateAroundPoint(position, startDegree + PI2 * i / quality * (length > 0 ? 1 : -1)).To3D((int)Player.Instance.Position.Z);
            }
            Line.DrawLine(color, width, points);
        }
    }
}
