using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace UBTaliyah
{
    class Obj_Manager
    {
        public static GameObject E_Object;
        public static void Obj_GeneralParticleEmitter_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name.Contains("Taliyah_Base_E_MineRumbleSound.troy"))
                E_Object = sender;
            if (sender.Name.Contains("Taliyah_Base_Q_aoe_bright.troy"))
                Extension.In_Q_Side = true;
        }
        public static void Obj_GeneralParticleEmitter_OnDelete(GameObject sender, EventArgs args)
        {
            if (sender.Name.Contains("Taliyah_Base_E_Timeout.troy"))
                E_Object = null;
            if (sender.Name.Contains("Taliyah_Base_Q_aoe_bright.troy"))
                Extension.In_Q_Side = false;

        }
    }
}
