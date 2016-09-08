using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace UBLucian
{
    class Flee
    {
        public static List<Vector3[]> JumpSpots = new List<Vector3[]>();
        private static float lastMoveClick;
        private static float lastDistance;
        private static float currentDistance;
        private static bool jumped;
        private static Vector3 LastRightClick;

        public static Vector3 LastMoveCommand
        {
            get { return Player.Instance.Path.LastOrDefault(); }
        }
        public static Vector3[] GetJumpSpot()
        {
            return JumpSpots
                 .Where(x => LastMoveCommand.Distance(x[0]) <= Config.FleeMenu.GetValue("jumpclickrange"))
                 .OrderBy(x => Player.Instance.Distance(x[0]))
                .FirstOrDefault();
        }
        public static void JumpSystem(EventArgs args)
        {
            var Value = Config.FleeMenu.GetValue("jumptype", false);
            if (Value == 0) return;
            if ((Value == 1 && Orbwalker.ActiveModes.Flee.IsActive()) || Value == 2)
            {
                var spot = GetJumpSpot();
                if (Spells.E.IsReady()
                    && spot != null
                    && Environment.TickCount - lastMoveClick > 100)
                {
                    if (LastMoveCommand.Distance(spot[0]) <= Config.FleeMenu.GetValue("jumpclickrange") && Player.Instance.Distance(spot[0]) <= 10)
                    {
                        Spells.E.Cast(spot[1]);
                        jumped = true;
                    }
                    else if (Player.Instance.Distance(spot[0]) > 10 && Player.Instance.Distance(spot[0]) < Config.FleeMenu.GetValue("jumpclickrange"))
                    {
                        lastDistance = currentDistance;
                        currentDistance = Player.Instance.Distance(spot[0]);
                        if (lastDistance == currentDistance)
                        {
                            Spells.E.Cast(spot[1]);
                        }
                    }
                    else
                    {
                        Player.IssueOrder(GameObjectOrder.MoveTo, spot[0]);
                        lastMoveClick = Environment.TickCount;
                    }
                }
            }
        }

        public static void DrawJumpSpot(EventArgs agrs)
        {
            if (Config.DrawMenu.Checked("spot") && Config.DrawMenu.Checked("draw"))
            {
                foreach (var spot in JumpSpots.Where(s => Player.Instance.Distance(s[0]) <= 2000))
                {
                    Drawing.DrawCircle(spot[0], Config.FleeMenu.GetValue("jumpclickrange"), Config.DrawMenu["spotcolor"].Cast<ColorPicker>().CurrentValue);
                }
            }
        }
        #region InitSpots
        public static void InitSpots()
        {
            //blue side wolves - left wall (top)
            JumpSpots.Add(new[] { new Vector3(2809, 6936, 53), new Vector3(3058, 6960, 52) });
            JumpSpots.Add(new[] { new Vector3(3058, 6960, 52), new Vector3(2809, 6936, 53) });

            //blue side wolves - left wall (middle)
            JumpSpots.Add(new[] { new Vector3(2755, 6523, 57), new Vector3(3072, 6607, 51) });
            JumpSpots.Add(new[] { new Vector3(3072, 6607, 51), new Vector3(2755, 6523, 57) });

            //blue side wolves - left wall (bottom)
            JumpSpots.Add(new[] { new Vector3(3022, 6111, 57), new Vector3(3195, 6307, 52) });
            JumpSpots.Add(new[] { new Vector3(3195, 6307, 52), new Vector3(3022, 6111, 57) });

            //red side wolves - right wall (top)
            JumpSpots.Add(new[] { new Vector3(11817, 8903, 50), new Vector3(11513, 8762, 65) });
            JumpSpots.Add(new[] { new Vector3(11513, 8762, 65), new Vector3(11817, 8903, 50) });

            //red side wolves - right wall (middle)
            JumpSpots.Add(new[] { new Vector3(11755, 8206, 55), new Vector3(12095, 8281, 52) });
            JumpSpots.Add(new[] { new Vector3(12095, 8281, 52), new Vector3(11755, 8206, 55) });

            //red side wolves - right wall (bottom)
            JumpSpots.Add(new[] { new Vector3(12110, 7980, 53), new Vector3(12110, 7980, 53) });
            JumpSpots.Add(new[] { new Vector3(11767, 7900, 52), new Vector3(11767, 7900, 52) });

            //bottom bush wall (top)
            JumpSpots.Add(new[] { new Vector3(11354, 5511, 8), new Vector3(11647, 5452, 54) });
            JumpSpots.Add(new[] { new Vector3(11647, 5452, 54), new Vector3(11354, 5511, 8) });

            //bottom bush wall (middle)
            JumpSpots.Add(new[] { new Vector3(11725, 5120, 52), new Vector3(11428, 4984, -71) });
            JumpSpots.Add(new[] { new Vector3(11428, 4984, -71), new Vector3(11725, 5120, 52) });

            //bot bush wall (bottom)
            JumpSpots.Add(new[] { new Vector3(11697, 4614, -71), new Vector3(11960, 4802, 51) });
            JumpSpots.Add(new[] { new Vector3(11960, 4802, 51), new Vector3(11697, 4614, -71) });

            //top bush wall (top)
            JumpSpots.Add(new[] { new Vector3(3074, 10056, 54), new Vector3(3324, 10206, -65) });
            JumpSpots.Add(new[] { new Vector3(3324, 10206, -65), new Vector3(3074, 10056, 54) });

            //top bush wall (middle)
            JumpSpots.Add(new[] { new Vector3(3474, 9856, -65), new Vector3(3226, 9752, 52) });
            JumpSpots.Add(new[] { new Vector3(3226, 9752, 52), new Vector3(3474, 9856, -65) });

            //top bush wall (bottom)
            JumpSpots.Add(new[] { new Vector3(3478, 9422, 16), new Vector3(3224, 9440, 51) });
            JumpSpots.Add(new[] { new Vector3(3224, 9440, 51), new Vector3(3478, 9422, 16) });

            //mid wall - top side (top)
            JumpSpots.Add(new[] { new Vector3(6484, 8804, -71), new Vector3(6685, 9116, 49) });
            JumpSpots.Add(new[] { new Vector3(6685, 9116, 49), new Vector3(6484, 8804, -71) });

            //mid wall - top side (middle)
            JumpSpots.Add(new[] { new Vector3(6857, 8517, -71), new Vector3(7095, 8727, 52) });
            JumpSpots.Add(new[] { new Vector3(7095, 8727, 52), new Vector3(6857, 8517, -71) });

            //mid wall - top side (bottom)
            JumpSpots.Add(new[] { new Vector3(7174, 8256, -33), new Vector3(7422, 8406, 53) });
            JumpSpots.Add(new[] { new Vector3(7422, 8406, 53), new Vector3(7174, 8256, -33) });

            //mid wall - bot side (top)
            JumpSpots.Add(new[] { new Vector3(7714, 6544, -1), new Vector3(7378, 6298, 52) });
            JumpSpots.Add(new[] { new Vector3(7378, 6298, 52), new Vector3(7714, 6544, -1) });

            //mid wall - bot side (middle)
            JumpSpots.Add(new[] { new Vector3(8139, 6210, -71), new Vector3(7813, 5938, 52) });
            JumpSpots.Add(new[] { new Vector3(7813, 5938, 52), new Vector3(8139, 6210, -71) });

            //mid wall - bot side (bottom)
            JumpSpots.Add(new[] { new Vector3(8194, 5742, 42), new Vector3(8412, 6081, -71) });
            JumpSpots.Add(new[] { new Vector3(8412, 6081, -71), new Vector3(8194, 5742, 42) });

            //baron wall
            JumpSpots.Add(new[] { new Vector3(5774, 10656, 55), new Vector3(5474, 10656, -71) });
            JumpSpots.Add(new[] { new Vector3(5474, 10656, -71), new Vector3(5774, 10656, 55) });

            //baron entrance wall (left)
            JumpSpots.Add(new[] { new Vector3(4480, 10437, -71), new Vector3(4292, 10199, -71) });
            JumpSpots.Add(new[] { new Vector3(4292, 10199, -71), new Vector3(4480, 10437, -71) });

            //baron entrance wall (right)
            JumpSpots.Add(new[] { new Vector3(5083, 9998, -71), new Vector3(4993, 9706, -70) });
            JumpSpots.Add(new[] { new Vector3(4993, 9706, -70), new Vector3(5083, 9998, -71) });

            //dragon wall
            JumpSpots.Add(new[] { new Vector3(9322, 4358, -71), new Vector3(9072, 4208, 53) });
            JumpSpots.Add(new[] { new Vector3(9072, 4208, 53), new Vector3(9322, 4358, -71) });

            //dragon entrance wall (left)
            JumpSpots.Add(new[] { new Vector3(9751, 4884, -71), new Vector3(9803, 5249, -68) });
            JumpSpots.Add(new[] { new Vector3(9803, 5249, -68), new Vector3(9751, 4884, -71) });

            //dragon entrance wall (right)
            JumpSpots.Add(new[] { new Vector3(10375, 4441, -71), new Vector3(10643, 4641, -68) });
            JumpSpots.Add(new[] { new Vector3(10643, 4641, -68), new Vector3(10375, 4441, -71) });

            //top golllems wall
            JumpSpots.Add(new[] { new Vector3(6543, 12054, 56), new Vector3(6553, 11666, 53) });
            JumpSpots.Add(new[] { new Vector3(6553, 11666, 53), new Vector3(6543, 12054, 56) });

            //bot gollems wall
            JumpSpots.Add(new[] { new Vector3(8250, 2894, 51), new Vector3(8222, 3158, 51) });
            JumpSpots.Add(new[] { new Vector3(8222, 3158, 51), new Vector3(8250, 2894, 51) });

            //blue side bot tribush wall (left)
            JumpSpots.Add(new[] { new Vector3(9505, 2756, 49), new Vector3(9535, 3203, 55) });
            JumpSpots.Add(new[] { new Vector3(9535, 3203, 55), new Vector3(9505, 2756, 49) });

            //blue side bot tribush wall (middle)
            JumpSpots.Add(new[] { new Vector3(9815, 2673, 49), new Vector3(9862, 3111, 58) });
            JumpSpots.Add(new[] { new Vector3(9862, 3111, 58), new Vector3(9815, 2673, 49) });

            //blue side bot tribush wall (right)
            JumpSpots.Add(new[] { new Vector3(10259, 2925, 49), new Vector3(10046, 2675, 49) });
            JumpSpots.Add(new[] { new Vector3(10046, 2675, 49), new Vector3(10259, 2925, 49) });

            //red side toplane tribush wall (right)
            JumpSpots.Add(new[] { new Vector3(5269, 11725, 57), new Vector3(5363, 12185, 56) });
            JumpSpots.Add(new[] { new Vector3(5363, 12185, 56), new Vector3(5269, 11725, 57) });

            //red side toplane tribush wall (middle)
            JumpSpots.Add(new[] { new Vector3(4993, 11836, 57), new Vector3(5110, 12210, 56) });
            JumpSpots.Add(new[] { new Vector3(5110, 12210, 56), new Vector3(4993, 11836, 57) });

            //red side toplane tribush wall (left)
            JumpSpots.Add(new[] { new Vector3(4605, 11970, 57), new Vector3(4825, 12307, 56) });
            JumpSpots.Add(new[] { new Vector3(4825, 12307, 56), new Vector3(4605, 11970, 57) });

            //blue side razorbeak wall
            JumpSpots.Add(new[] { new Vector3(7372, 5858, 52), new Vector3(7174, 5608, 58) });
            JumpSpots.Add(new[] { new Vector3(7174, 5608, 58), new Vector3(7372, 5858, 52) });

            //blue side blue buff wall (right)
            JumpSpots.Add(new[] { new Vector3(3802, 7743, 52), new Vector3(3856, 7412, 51) });
            JumpSpots.Add(new[] { new Vector3(3856, 7412, 51), new Vector3(3802, 7743, 52) });

            //blue side blue buff wall (left)
            JumpSpots.Add(new[] { new Vector3(3437, 7398, 52), new Vector3(3422, 7759, 53) });
            JumpSpots.Add(new[] { new Vector3(3422, 7759, 53), new Vector3(3437, 7398, 52) });

            //blue side blue buff - right wall
            JumpSpots.Add(new[] { new Vector3(4124, 8022, 50), new Vector3(4382, 8149, 48) });
            JumpSpots.Add(new[] { new Vector3(4382, 8149, 48), new Vector3(4124, 8022, 50) });

            //blue side rock between blue buff/baron (left)
            JumpSpots.Add(new[] { new Vector3(4664, 8652, -10), new Vector3(4662, 8896, -69) });
            JumpSpots.Add(new[] { new Vector3(4662, 8896, -69), new Vector3(4664, 8652, -10) });

            //blue side rock between blue buff/baron (right)
            JumpSpots.Add(new[] { new Vector3(3737, 9233, -8), new Vector3(4074, 9322, -67) });
            JumpSpots.Add(new[] { new Vector3(4074, 9322, -67), new Vector3(3737, 9233, -8) });

            //red side blue buff wall (left)
            JumpSpots.Add(new[] { new Vector3(11040, 7179, 51), new Vector3(10904, 7521, 52) });
            JumpSpots.Add(new[] { new Vector3(10904, 7521, 52), new Vector3(11040, 7179, 51) });

            //red side blue buff wall (right)
            JumpSpots.Add(new[] { new Vector3(11458, 7155, 52), new Vector3(11449, 7517, 52) });
            JumpSpots.Add(new[] { new Vector3(11449, 7517, 52), new Vector3(11458, 7155, 52) });

            //red side rock between blue buff/dragon (left)
            JumpSpots.Add(new[] { new Vector3(10185, 6286, 29), new Vector3(10189, 5922, -71) });
            JumpSpots.Add(new[] { new Vector3(10189, 5922, -71), new Vector3(10185, 6286, 29) });

            //red side rock between blue buff/dragon (right)
            JumpSpots.Add(new[] { new Vector3(10665, 5662, -68), new Vector3(11049, 5660, -22) });
            JumpSpots.Add(new[] { new Vector3(11049, 5660, -22), new Vector3(10665, 5662, -68) });

            //blue side top tribush wall (top)
            JumpSpots.Add(new[] { new Vector3(2537, 9674, 54), new Vector3(2800, 9596, 52) });
            JumpSpots.Add(new[] { new Vector3(2800, 9596, 52), new Vector3(2537, 9674, 54) });

            //blue side top tribush wall (bottom)
            JumpSpots.Add(new[] { new Vector3(2874, 9306, 51), new Vector3(2598, 9272, 52) });
            JumpSpots.Add(new[] { new Vector3(2598, 9272, 52), new Vector3(2874, 9306, 51) });

            //blue side wolves - right wall (bottom)
            JumpSpots.Add(new[] { new Vector3(4644, 5876, 51), new Vector3(4772, 5636, 50) });
            JumpSpots.Add(new[] { new Vector3(4772, 5636, 50), new Vector3(4644, 5876, 51) });

            //blue side wolves - right wall (top)
            JumpSpots.Add(new[] { new Vector3(4938, 6062, 51), new Vector3(4869, 6452, 51) });
            JumpSpots.Add(new[] { new Vector3(4869, 6452, 51), new Vector3(4938, 6062, 51) });

            //blue razorbeak - left wall
            JumpSpots.Add(new[] { new Vector3(6199, 5286, 49), new Vector3(5998, 5536, 52) });
            JumpSpots.Add(new[] { new Vector3(5998, 5536, 52), new Vector3(6199, 5286, 49) });

            //red side bottom tribush wall (bottom)
            JumpSpots.Add(new[] { new Vector3(12327, 5243, 52), new Vector3(12027, 5265, 54) });
            JumpSpots.Add(new[] { new Vector3(12027, 5265, 54), new Vector3(12327, 5243, 52) });

            //red side bottom tribush wall (top)
            JumpSpots.Add(new[] { new Vector3(11969, 5480, 55), new Vector3(12343, 5498, 53) });
            JumpSpots.Add(new[] { new Vector3(12343, 5498, 53), new Vector3(11969, 5480, 55) });

            //red side razorbeak - rightdown wall
            JumpSpots.Add(new[] { new Vector3(8646, 9635, 50), new Vector3(8831, 9384, 52) });
            JumpSpots.Add(new[] { new Vector3(8831, 9384, 52), new Vector3(8646, 9635, 50) });

            //red side wolves - left wall (top)
            JumpSpots.Add(new[] { new Vector3(10193, 9052, 50), new Vector3(10061, 9282, 52) });
            JumpSpots.Add(new[] { new Vector3(10061, 9282, 52), new Vector3(10193, 9052, 50) });

            //red side wolves - left wall (bottom)
            JumpSpots.Add(new[] { new Vector3(9967, 8429, 65), new Vector3(9856, 8831, 50) });
            JumpSpots.Add(new[] { new Vector3(9856, 8831, 50), new Vector3(9967, 8429, 65) });

            //red size razorbeak - right wall
            JumpSpots.Add(new[] { new Vector3(8066, 9796, 51), new Vector3(8369, 9807, 50) });
            JumpSpots.Add(new[] { new Vector3(8369, 9807, 50), new Vector3(8066, 9796, 51) });

            //blue side base wall (right)
            JumpSpots.Add(new[] { new Vector3(4463, 3260, 96), new Vector3(4780, 3460, 51) });
            JumpSpots.Add(new[] { new Vector3(4780, 3460, 51), new Vector3(4463, 3260, 96) });

            //blue side base wall (left)
            JumpSpots.Add(new[] { new Vector3(3085, 4539, 96), new Vector3(3182, 4917, 54) });
            JumpSpots.Add(new[] { new Vector3(3182, 4917, 54), new Vector3(3085, 4539, 96) });

            //red side base wall (right)
            JumpSpots.Add(new[] { new Vector3(11735, 10430, 91), new Vector3(11621, 10092, 52) });
            JumpSpots.Add(new[] { new Vector3(11621, 10092, 52), new Vector3(11735, 10430, 91) });

            //red base wall (left)
            JumpSpots.Add(new[] { new Vector3(10321, 11664, 91), new Vector3(9999, 11554, 52) });
            JumpSpots.Add(new[] { new Vector3(9999, 11554, 52), new Vector3(10321, 11664, 91) });

            /////////////////////////////////////////////////////////////////////////////////////////

            //gravestone right (south)
            JumpSpots.Add(new[] { new Vector3(3855, 2303, 96), new Vector3(4195, 2462, 96) });
            JumpSpots.Add(new[] { new Vector3(4195, 2462, 96), new Vector3(3855, 2303, 96) });

            //extra port right (south)
            JumpSpots.Add(new[] { new Vector3(5000, 2622, 51), new Vector3(4661, 2479, 96) });
            JumpSpots.Add(new[] { new Vector3(4661, 2479, 96), new Vector3(5000, 2622, 51) });

            //near turret 3 bottom (south) 
            JumpSpots.Add(new[] { new Vector3(5058, 1906, 52), new Vector3(4693, 1953, 96) });
            JumpSpots.Add(new[] { new Vector3(4693, 1953, 96), new Vector3(5058, 1906, 52) });

            //near turret 3 middle (right, south)
            JumpSpots.Add(new[] { new Vector3(4270, 3614, 96), new Vector3(4628, 3726, 51) });
            JumpSpots.Add(new[] { new Vector3(4628, 3726, 51), new Vector3(4270, 3614, 96) });

            //near turret 3 middle (left, south)
            JumpSpots.Add(new[] { new Vector3(3484, 4352, 96), new Vector3(3568, 4716, 54) });
            JumpSpots.Add(new[] { new Vector3(3568, 4716, 54), new Vector3(3484, 4352, 96) });

            //gravestone left (south)
            JumpSpots.Add(new[] { new Vector3(2409, 3875, 96), new Vector3(2496, 4257, 96) });
            JumpSpots.Add(new[] { new Vector3(2496, 4257, 96), new Vector3(2409, 3875, 96) });

            //exta port left (south)
            JumpSpots.Add(new[] { new Vector3(2384, 4696, 96), new Vector3(2372, 5064, 53) });
            JumpSpots.Add(new[] { new Vector3(2372, 5064, 53), new Vector3(2384, 4696, 96) });

            //near turret 3 top (south)
            JumpSpots.Add(new[] { new Vector3(1787, 4731, 96), new Vector3(1792, 5101, 53) });
            JumpSpots.Add(new[] { new Vector3(1792, 5101, 53), new Vector3(1787, 4731, 96) });

            //opposite wolf area (south)
            JumpSpots.Add(new[] { new Vector3(3123, 5852, 57), new Vector3(3477, 5964, 52) });
            JumpSpots.Add(new[] { new Vector3(3477, 5964, 52), new Vector3(3123, 5852, 57) });

            //wolf area (right, south)
            JumpSpots.Add(new[] { new Vector3(4024, 6408, 52), new Vector3(4402, 6292, 51) });
            JumpSpots.Add(new[] { new Vector3(4402, 6292, 51), new Vector3(4024, 6408, 52) });

            //wolf area (left, south)
            JumpSpots.Add(new[] { new Vector3(3773, 6689, 52), new Vector3(3700, 7088, 50) });
            JumpSpots.Add(new[] { new Vector3(3700, 7088, 50), new Vector3(3773, 6689, 52) });

            //frog area (south)
            JumpSpots.Add(new[] { new Vector3(2027, 7903, 50), new Vector3(2218, 8238, 52) });
            JumpSpots.Add(new[] { new Vector3(2218, 8238, 52), new Vector3(2027, 7903, 50) });

            //frog area (south)
            JumpSpots.Add(new[] { new Vector3(2014, 8604, 52), new Vector3(1652, 8665, 53) });
            JumpSpots.Add(new[] { new Vector3(1652, 8665, 53), new Vector3(2014, 8604, 52) });

            //frog area (south)
            JumpSpots.Add(new[] { new Vector3(2102, 8945, 52), new Vector3(1744, 9038, 53) });
            JumpSpots.Add(new[] { new Vector3(1744, 9038, 53), new Vector3(2102, 8945, 52) });

            //frog area (south)
            JumpSpots.Add(new[] { new Vector3(1956, 9419, 53), new Vector3(2190, 9014, 52) });
            JumpSpots.Add(new[] { new Vector3(2190, 9014, 52), new Vector3(1956, 9419, 53) });

            //Dragon area near mid brush            
            JumpSpots.Add(new[] { new Vector3(8820, 5080, 52), new Vector3(8976, 5440, -71) });
            JumpSpots.Add(new[] { new Vector3(8976, 5440, -71), new Vector3(8820, 5080, 52) });

            //Dragon area oposite red brush
            JumpSpots.Add(new[] { new Vector3(9086, 4716, 52), new Vector3(9440, 4770, -71) });
            JumpSpots.Add(new[] { new Vector3(9440, 4770, -71), new Vector3(9086, 4716, 52) });

            //Dragon area
            JumpSpots.Add(new[] { new Vector3(9722, 3458, 54), new Vector3(9828, 3838, -71) });
            JumpSpots.Add(new[] { new Vector3(9828, 3838, -71), new Vector3(9722, 3458, 54) });

            //Dragon area
            JumpSpots.Add(new[] { new Vector3(9376, 3680, 62), new Vector3(9554, 4042, -62) });
            JumpSpots.Add(new[] { new Vector3(9554, 4042, -62), new Vector3(9376, 3680, 62) });

            //Krug wall (south)
            JumpSpots.Add(new[] { new Vector3(9316, 3150, 55), new Vector3(9264, 2780, 49) });
            JumpSpots.Add(new[] { new Vector3(9264, 2780, 49), new Vector3(9316, 3150, 55) });

            //Krug area (south)
            JumpSpots.Add(new[] { new Vector3(8569, 2858, 51), new Vector3(8586, 3244, 54) });
            JumpSpots.Add(new[] { new Vector3(8586, 3244, 54), new Vector3(8569, 2858, 51) });

            //Turret 2 bottom (south)
            JumpSpots.Add(new[] { new Vector3(7564, 1728, 49), new Vector3(7612, 2100, 51) });
            JumpSpots.Add(new[] { new Vector3(7612, 2100, 51), new Vector3(7564, 1728, 49) });

            //Oposite Krug area (south)
            JumpSpots.Add(new[] { new Vector3(8343, 1735, 49), new Vector3(8300, 2104, 51) });
            JumpSpots.Add(new[] { new Vector3(8300, 2104, 51), new Vector3(8343, 1735, 49) });

            //Oposite Krug area (south)
            JumpSpots.Add(new[] { new Vector3(8584, 2094, 51), new Vector3(8585, 1718, 49) });
            JumpSpots.Add(new[] { new Vector3(8585, 1718, 49), new Vector3(8584, 2094, 51) });

            //Oposite Krug area (south)
            JumpSpots.Add(new[] { new Vector3(8796, 1726, 49), new Vector3(9000, 2076, 59) });
            JumpSpots.Add(new[] { new Vector3(9000, 2076, 59), new Vector3(8796, 1726, 49) });

            //Wall near turret 3 bottom (south)
            JumpSpots.Add(new[] { new Vector3(9470, 2102, 49), new Vector3(9710, 2390, 49) });
            JumpSpots.Add(new[] { new Vector3(9710, 2390, 49), new Vector3(9470, 2102, 49) });

            //Wall near turret 3 bottom (south)
            JumpSpots.Add(new[] { new Vector3(10496, 2190, 49), new Vector3(10550, 1796, 49) });
            JumpSpots.Add(new[] { new Vector3(10550, 1796, 49), new Vector3(10496, 2190, 49) });

            //Between turret 3 and turret 2 bottom (south)
            JumpSpots.Add(new[] { new Vector3(5874, 2012, 52), new Vector3(5598, 1756, 51) });
            JumpSpots.Add(new[] { new Vector3(5598, 1756, 51), new Vector3(5874, 2012, 52) });
            //---------------------------------------------------------------------------------------------//

            //gravestone right (north)
            JumpSpots.Add(new[] { new Vector3(12462, 11116, 91), new Vector3(12345, 10762, 91) });
            JumpSpots.Add(new[] { new Vector3(12345, 10762, 91), new Vector3(12462, 11116, 91) });

            //extra port right (north)
            JumpSpots.Add(new[] { new Vector3(12422, 10256, 91), new Vector3(12360, 9888, 52) });
            JumpSpots.Add(new[] { new Vector3(12360, 9888, 52), new Vector3(12422, 10256, 91) });

            //near turret 3 bottom (north)
            JumpSpots.Add(new[] { new Vector3(12964, 9856, 48), new Vector3(13018, 10234, 91) });
            JumpSpots.Add(new[] { new Vector3(13018, 10234, 91), new Vector3(12964, 9856, 48) });

            //near turret 3 middle (right, north)
            JumpSpots.Add(new[] { new Vector3(11080, 10251, 52), new Vector3(11190, 10640, 91) });
            JumpSpots.Add(new[] { new Vector3(11190, 10640, 91), new Vector3(11080, 10251, 52) });

            //near turret 3 middle (left, north)
            JumpSpots.Add(new[] { new Vector3(10525, 11300, 91), new Vector3(10163, 11231, 51) });
            JumpSpots.Add(new[] { new Vector3(10163, 11231, 51), new Vector3(10525, 11300, 91) });

            //gravestone left (north)
            JumpSpots.Add(new[] { new Vector3(10734, 12502, 91), new Vector3(11095, 12593, 91) });
            JumpSpots.Add(new[] { new Vector3(11095, 12593, 91), new Vector3(10734, 12502, 91) });

            //exta port left (north)
            JumpSpots.Add(new[] { new Vector3(10100, 12418, 91), new Vector3(9777, 12439, 52) });
            JumpSpots.Add(new[] { new Vector3(9777, 12439, 52), new Vector3(10100, 12418, 91) });

            //near turret 3 top (north)
            JumpSpots.Add(new[] { new Vector3(9732, 13056, 50), new Vector3(10092, 13019, 95) });
            JumpSpots.Add(new[] { new Vector3(10092, 13019, 95), new Vector3(9732, 13056, 50) });

            //opposite wolf area (north)
            JumpSpots.Add(new[] { new Vector3(12014, 8604, 52), new Vector3(11662, 8474, 57) });
            JumpSpots.Add(new[] { new Vector3(11662, 8474, 57), new Vector3(12014, 8604, 52) });

            //wolf area (right, north)
            JumpSpots.Add(new[] { new Vector3(11000, 8147, 63), new Vector3(11086, 7771, 52) });
            JumpSpots.Add(new[] { new Vector3(11086, 7771, 52), new Vector3(11000, 8147, 63) });

            //wolf area (left, north)
            JumpSpots.Add(new[] { new Vector3(10772, 8456, 63), new Vector3(10390, 8512, 64) });
            JumpSpots.Add(new[] { new Vector3(10390, 8512, 64), new Vector3(10772, 8456, 63) });

            //frog area (north)
            JumpSpots.Add(new[] { new Vector3(12164, 6874, 52), new Vector3(12300, 7234, 52) });
            JumpSpots.Add(new[] { new Vector3(12300, 7234, 52), new Vector3(12164, 6874, 52) });

            //frog area (north)
            JumpSpots.Add(new[] { new Vector3(12530, 6682, 52), new Vector3(12664, 7048, 52) });
            JumpSpots.Add(new[] { new Vector3(12664, 7048, 52), new Vector3(12530, 6682, 52) });

            //frog area (north)
            JumpSpots.Add(new[] { new Vector3(12772, 6408, 52), new Vector3(13156, 6420, 55) });
            JumpSpots.Add(new[] { new Vector3(13156, 6420, 55), new Vector3(12772, 6408, 52) });

            //frog area (north)
            JumpSpots.Add(new[] { new Vector3(12306, 5850, 59), new Vector3(11930, 5856, 50) });
            JumpSpots.Add(new[] { new Vector3(11930, 5856, 50), new Vector3(12306, 5850, 59) });

            //frog area (north)
            JumpSpots.Add(new[] { new Vector3(11772, 6558, 52), new Vector3(11646, 6180, 51) });
            JumpSpots.Add(new[] { new Vector3(11646, 6180, 51), new Vector3(11772, 6558, 52) });

            //Between turret 3 and turret 2 top (north)
            JumpSpots.Add(new[] { new Vector3(8922, 12856, 56), new Vector3(9150, 13147, 53) });
            JumpSpots.Add(new[] { new Vector3(9150, 13147, 53), new Vector3(8922, 12856, 56) });

            //Wall oposite extra port left (north)
            JumpSpots.Add(new[] { new Vector3(8699, 11860, 56), new Vector3(8826, 12234, 56) });
            JumpSpots.Add(new[] { new Vector3(8826, 12234, 56), new Vector3(8699, 11860, 56) });

            //Wall oposite extra port left (north)
            JumpSpots.Add(new[] { new Vector3(8944, 11514, 57), new Vector3(9300, 11632, 52) });
            JumpSpots.Add(new[] { new Vector3(9300, 11632, 52), new Vector3(8944, 11514, 57) });

            //Wall between red and bird area (north)
            JumpSpots.Add(new[] { new Vector3(8190, 10746, 50), new Vector3(8562, 10648, 51) });
            JumpSpots.Add(new[] { new Vector3(8562, 10648, 51), new Vector3(8190, 10746, 50) });

            //Near turret 2 top (north)
            JumpSpots.Add(new[] { new Vector3(7102, 12824, 56), new Vector3(7236, 13184, 53) });
            JumpSpots.Add(new[] { new Vector3(7236, 13184, 53), new Vector3(7102, 12824, 56) });

            //Oposite Krug area (north)
            JumpSpots.Add(new[] { new Vector3(6402, 12828, 55), new Vector3(6396, 13202, 53) });
            JumpSpots.Add(new[] { new Vector3(6396, 13202, 53), new Vector3(6402, 12828, 55) });

            //Oposite Krug area (north)
            JumpSpots.Add(new[] { new Vector3(6084, 13188, 53), new Vector3(5926, 12826, 53) });
            JumpSpots.Add(new[] { new Vector3(5926, 12826, 53), new Vector3(6084, 13188, 53) });

            //Wall near turret 1 top (north)
            JumpSpots.Add(new[] { new Vector3(5130, 12480, 56), new Vector3(5352, 12790, 53) });
            JumpSpots.Add(new[] { new Vector3(5352, 12790, 53), new Vector3(5130, 12480, 56) });

            //Krug area (north)
            JumpSpots.Add(new[] { new Vector3(6204, 12148, 56), new Vector3(5840, 12236, 56) });
            JumpSpots.Add(new[] { new Vector3(5840, 12236, 56), new Vector3(6204, 12148, 56) });

            //Krug area (north)
            JumpSpots.Add(new[] { new Vector3(6274, 12006, 56), new Vector3(6184, 11632, 57) });
            JumpSpots.Add(new[] { new Vector3(6184, 11632, 57), new Vector3(6274, 12006, 56) });

            //Red area (north)
            JumpSpots.Add(new[] { new Vector3(6566, 11284, 56), new Vector3(6824, 11006, 56) });
            JumpSpots.Add(new[] { new Vector3(6824, 11006, 56), new Vector3(6566, 11284, 56) });

            //Bird area (north)
            JumpSpots.Add(new[] { new Vector3(7822, 9306, 52), new Vector3(7534, 9032, 53) });
            JumpSpots.Add(new[] { new Vector3(7534, 9032, 53), new Vector3(7822, 9306, 52) });

            //baron area
            JumpSpots.Add(new[] { new Vector3(5854, 9472, -71), new Vector3(6016, 9784, 52) });
            JumpSpots.Add(new[] { new Vector3(6016, 9784, 52), new Vector3(5854, 9472, -71) });

            //baron area
            JumpSpots.Add(new[] { new Vector3(5452, 11174, 57), new Vector3(5206, 10928, -71) });
            JumpSpots.Add(new[] { new Vector3(5206, 10928, -71), new Vector3(5452, 11174, 57) });

            //baron area
            JumpSpots.Add(new[] { new Vector3(5805, 10213, 54), new Vector3(5442, 10322, -71) });
            JumpSpots.Add(new[] { new Vector3(5442, 10322, -71), new Vector3(5805, 10213, 54) });
        }
        #endregion
    }
}