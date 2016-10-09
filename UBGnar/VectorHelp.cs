using System;
using System.Linq;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace UBGnar
{
    static class VectorHelp
    {
        public static List<Vector3> GetWallAroundMe(float Range ,float length)
        {
            var PI2 = (float)Math.PI * 2;
            var Wall = new List<Vector3>();
            var position = Player.Instance.Position.To2D();
            for (var radius = 1; radius <= Range; radius += 100)
            {                
                var quality = (int)(radius / 7 + 11);                
                var rad = new Vector2(0, radius);
                for (var i = 0; i <= (int)(Math.Abs(quality * length / PI2)); i++)
                {
                    var Point = (position + rad).RotateAroundPoint(position, 0 + PI2 * i / quality * (length > 0 ? 1 : -1));
                    if (Point.IsBuilding() || Point.IsWall())
                    {
                        Wall.Add(Point.To3DWorld());
                    }
                    else continue;
                }
                if (Wall.Count >= Config.Menu.GetValue("st"))
                {
                    break;
                }
            }
            return Wall;                
        }
        public static Vector3 Parallel(this Vector2 start, Vector2 end, Vector2 source)
        {
            var line = new Geometry.Polygon.Line(start, end);
            var line2 = line.MovePolygon(Player.Instance.Position.To2D());
            return line2.Points.Last().To3DWorld();
        }
        public static void DrawArc(Vector2 position, float radius, System.Drawing.Color color, float startDegree, float length, float width = 0.6F, int quality = -1)
        {
            float PI2 = (float)Math.PI * 2;
            if (quality == -1)
            {
                quality = (int)(radius / 7 + 50);
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
