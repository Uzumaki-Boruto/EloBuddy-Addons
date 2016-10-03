using System;
using System.IO;
using System.Drawing;


namespace UBSyndra
{
    class SaveColor
    {
        public static Color Load()
        {
            Color color = new Color();

            var UBConfig = EloBuddy.Sandbox.SandboxConfig.DataDirectory + @"\UBSeries\";
            var addonFile = UBConfig + @"\" + Extension.AddonName + ".txt";

            if (!Directory.Exists(UBConfig))
            {
                Save(System.Drawing.Color.FromArgb(255, 255, 236, 0));
            }

            string[] ARGB = null;

            if (Directory.Exists(UBConfig))
            {
                ARGB = File.ReadAllLines(addonFile);
            }

            if (ARGB != null)
            {
                int[] argb = new int[ARGB.Length];
                for (var i = 0; i < ARGB.Length; i++)
                {
                    var x = ARGB[i];
                    int.TryParse(x, out argb[i]);
                }
                color = Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + " - " + Extension.AddonName + "] Color Loaded!");
                Console.ResetColor();

            }
            return color;
        }
        public static void Save(Color CurrentValue)
        {
            var UBConfig = EloBuddy.Sandbox.SandboxConfig.DataDirectory + @"\UBSeries\";
            var addonFile = UBConfig + @"\" + Extension.AddonName + ".txt";

            if (!Directory.Exists(UBConfig))
            {
                Directory.CreateDirectory(UBConfig);
            }

            var A = CurrentValue.A.ToString();
            var R = CurrentValue.R.ToString();
            var G = CurrentValue.G.ToString();
            var B = CurrentValue.B.ToString();

            string[] data = { A, R, G, B };

            File.WriteAllLines(addonFile, data);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + " - " + Extension.AddonName + "] Color Saved!");
            Console.ResetColor();
        }
    }
}

