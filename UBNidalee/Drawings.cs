using System;
using System.Linq;
using System.Collections.Generic;
using UBNidalee.Properties;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Colour = System.Drawing.Color;

namespace UBNidalee
{
    class Drawings
    {
        public static readonly TextureLoader TextureLoader = new TextureLoader();
        private static float XBonus { get { return Config.DrawMenu["Xbonus"].Cast<Slider>().CurrentValue; } }
        private static float YBonus { get { return Config.DrawMenu["Ybonus"].Cast<Slider>().CurrentValue; } }
        private static bool Checked { get { return Config.DrawMenu["formdraw"].Cast<CheckBox>().CurrentValue; } }
        private static Sprite Takedown_grey, Pounce_grey, Swipe_grey, R2;
        private static Sprite Javelin_Toss_grey, Bushwhack_grey, Primal_Surge_grey, Aspect_of_the_Cougar_grey;
        private static Sprite Takedown, Pounce, Swipe, R1;
        private static Sprite Javelin_Toss, Bushwhack, Primal_Surge, Aspect_of_the_Cougar;

        public static void ImgData()
        {
            //Animal Grey
            TextureLoader.Load("Takedown_grey", Resources.Takedown_grey);
            Takedown_grey = new Sprite(() => TextureLoader["Takedown_grey"]);

            TextureLoader.Load("Pounce_grey", Resources.Pounce_grey);
            Pounce_grey = new Sprite(() => TextureLoader["Pounce_grey"]);

            TextureLoader.Load("Swipe_grey", Resources.Swipe_grey);
            Swipe_grey = new Sprite(() => TextureLoader["Swipe_grey"]);

            TextureLoader.Load("R2", Resources.Nidalee_R2);
            R2 = new Sprite(() => TextureLoader["R2"]);

            //Human Grey
            TextureLoader.Load("Javelin_Toss_grey", Resources.Javelin_Toss_grey);
            Javelin_Toss_grey = new Sprite(() => TextureLoader["Javelin_Toss_grey"]);

            TextureLoader.Load("Bushwhack_grey", Resources.Bushwhack_grey);
            Bushwhack_grey = new Sprite(() => TextureLoader["Bushwhack_grey"]);

            TextureLoader.Load("Primal_Surge_grey", Resources.Primal_Surge_grey);
            Primal_Surge_grey = new Sprite(() => TextureLoader["Primal_Surge_grey"]);

            TextureLoader.Load("Aspect_of_the_Cougar_grey", Resources.Aspect_of_the_Cougar_grey);
            Aspect_of_the_Cougar_grey = new Sprite(() => TextureLoader["Aspect_of_the_Cougar_grey"]);

            //Animal
            TextureLoader.Load("Takedown", Resources.Takedown);
            Takedown = new Sprite(() => TextureLoader["Takedown"]);

            TextureLoader.Load("Pounce", Resources.Pounce);
            Pounce = new Sprite(() => TextureLoader["Pounce"]);

            TextureLoader.Load("Swipe", Resources.Swipe);
            Swipe = new Sprite(() => TextureLoader["Swipe"]);

            TextureLoader.Load("R1", Resources.Nidalee_R1);
            R1 = new Sprite(() => TextureLoader["R1"]);

            //Human
            TextureLoader.Load("Javelin_Toss", Resources.Javelin_Toss);
            Javelin_Toss = new Sprite(() => TextureLoader["Javelin_Toss"]);

            TextureLoader.Load("Bushwhack", Resources.Bushwhack);
            Bushwhack = new Sprite(() => TextureLoader["Bushwhack"]);

            TextureLoader.Load("Primal_Surge", Resources.Primal_Surge);
            Primal_Surge = new Sprite(() => TextureLoader["Primal_Surge"]);

            TextureLoader.Load("Aspect_of_the_Cougar", Resources.Aspect_of_the_Cougar);
            Aspect_of_the_Cougar = new Sprite(() => TextureLoader["Aspect_of_the_Cougar"]);

        }
        public static void Special_Draw(EventArgs args)
        {
            if (Checked && Event.Humanform)
            {
                Takedown_grey.Draw(new Vector2(275 + XBonus, 200 + YBonus));
                string status = Spells.Q.IsLearned ? Event.IsReady(Event.CD["Takedown"]) ? "Ready" : Event.CD["Takedown"].ToString() : "Not learn";
                Colour color = Spells.Q.IsLearned ? Event.IsReady(Event.CD["Takedown"]) ? Colour.Green : Colour.White : Colour.Red;
                Drawing.DrawText(275 + XBonus, 275 + YBonus, color, status);
            }
            if (Checked && Event.Humanform)
            {
                Pounce_grey.Draw(new Vector2(345 + XBonus, 200 + YBonus));
                string status = Spells.W.IsLearned ? Event.IsReady(Event.CD["Pounce"]) ? "Ready" : Event.CD["Pounce"].ToString() : "Not learn";
                Colour color = Spells.W.IsLearned ? Event.IsReady(Event.CD["Pounce"]) ? Colour.Green : Colour.White : Colour.Red;
                Drawing.DrawText(345 + XBonus, 275 + YBonus, color, status);
            }
            if (Checked && Event.Humanform)
            {
                Swipe_grey.Draw(new Vector2(415 + XBonus, 200 + YBonus));
                string status = Spells.E.IsLearned ? Event.IsReady(Event.CD["Swipe"]) ? "Ready" : Event.CD["Swipe"].ToString() : "Not learn";
                Colour color = Spells.E.IsLearned ? Event.IsReady(Event.CD["Swipe"]) ? Colour.Green : Colour.White : Colour.Red;
                Drawing.DrawText(415 + XBonus, 275 + YBonus, color, status);
            }
            //--------------------------------------------------------------------------//
            if (Checked && !Event.Humanform)
            {
                Javelin_Toss_grey.Draw(new Vector2(275 + XBonus, 200 + YBonus));
                string status = Spells.Q.IsLearned ? Event.IsReady(Event.CD["Javelintoss"]) ? "Ready" : Event.CD["Javelintoss"].ToString() : "Not learn";
                Colour color = Spells.Q.IsLearned ? Event.IsReady(Event.CD["Javelintoss"]) ? Colour.Green : Colour.White : Colour.Red;
                Drawing.DrawText(275 + XBonus, 275 + YBonus, color, status);
            }
            if (Checked && !Event.Humanform)
            {
                Bushwhack_grey.Draw(new Vector2(345 + XBonus, 200 + YBonus));
                string status = Spells.W.IsLearned ? Event.IsReady(Event.CD["Bushwhack"]) ? "Ready" : Event.CD["Bushwhack"].ToString() : "Not learn";
                Colour color = Spells.W.IsLearned ? Event.IsReady(Event.CD["Bushwhack"]) ? Colour.Green : Colour.White : Colour.Red;
                Drawing.DrawText(345 + XBonus, 275 + YBonus, color, status);
            }
            if (Checked && !Event.Humanform)
            {
                Primal_Surge_grey.Draw(new Vector2(415 + XBonus, 200 + YBonus));
                string status = Spells.E.IsLearned ? Event.IsReady(Event.CD["Primalsurge"]) ? "Ready" : Event.CD["Primalsurge"].ToString() : "Not learn";
                Colour color = Spells.E.IsLearned ? Event.IsReady(Event.CD["Primalsurge"]) ? Colour.Green : Colour.White : Colour.Red;
                Drawing.DrawText(415 + XBonus, 275 + YBonus, color, status);
            }
            //---------------------------------------------------------------------------//
            if (Checked && Event.Humanform)
            {
                R1.Draw(new Vector2(485 + XBonus, 200 + YBonus));
                string status = Spells.R.IsLearned ? Event.IsReady(Event.CD["Aspect"]) ? "Ready" : Event.CD["Aspect"].ToString() : "Not learn";
                Colour color = Spells.R.IsLearned ? Event.IsReady(Event.CD["Aspect"]) ? Colour.Green : Colour.White : Colour.Red;
                Drawing.DrawText(485 + XBonus, 275 + YBonus, color, status);
            }
            if (Checked && !Event.Humanform)
            {
                Aspect_of_the_Cougar_grey.Draw(new Vector2(485 + XBonus, 200 + YBonus));
                string status = Spells.R.IsLearned ? Event.IsReady(Event.CD["Aspect"]) ? "Ready" : Event.CD["Aspect"].ToString() : "Not learn";
                Colour color = Spells.R.IsLearned ? Event.IsReady(Event.CD["Aspect"]) ? Colour.Green : Colour.White : Colour.Red;
                Drawing.DrawText(485 + XBonus, 275 + YBonus, color, status);
            }
        }
        //public static void Special_Draw_2(EventArgs agrs)
        //{
        //    if (Checked && Event.Humanform && Spells.Q.IsLearned && Event.IsReady(Event.CD["Takedown"]))
        //    {
        //        Takedown.Draw(new Vector2(275 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(275 + XBonus, 275 + YBonus, Colour.Green, "Ready");
        //    }
        //    if (Checked && Event.Humanform && Spells.W.IsLearned && Event.IsReady(Event.CD["Pounce"]))
        //    {
        //        Pounce.Draw(new Vector2(345 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(345 + XBonus, 275 + YBonus, Colour.Green, "Ready");
        //    }
        //    if (Checked && Event.Humanform && Spells.E.IsLearned && Event.IsReady(Event.CD["Swipe"]))
        //    {
        //        Swipe.Draw(new Vector2(415 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(415 + XBonus, 275 + YBonus, Colour.Green, "Ready");
        //    }
        //    if (Checked && Event.Humanform && Event.IsReady(Event.CD["Aspect"]))
        //    {
        //        R1.Draw(new Vector2(485 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(485 + XBonus, 275 + YBonus, Colour.Green, "Ready");
        //    }
        //    //-----------------------------------------------------------------------------//
        //    if (Checked && !Event.Humanform && Spells.Q2.IsLearned && Event.IsReady(Event.CD["Javelintoss"]))
        //    {
        //        Javelin_Toss.Draw(new Vector2(275 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(275 + XBonus, 275 + YBonus, Colour.Green, "Ready");
        //    }
        //    if (Checked && !Event.Humanform && Spells.W2.IsLearned && Event.IsReady(Event.CD["Bushwhack"]))
        //    {
        //        Bushwhack.Draw(new Vector2(345 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(345 + XBonus, 275 + YBonus, Colour.Green, "Ready");
        //    }
        //    if (Checked && !Event.Humanform && Spells.E2.IsLearned && Event.IsReady(Event.CD["Primalsurge"]))
        //    {
        //        Primal_Surge.Draw(new Vector2(415 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(415 + XBonus, 275 + YBonus, Colour.Green, "Ready");
        //    }
        //    if (Checked && !Event.Humanform && Event.IsReady(Event.CD["Aspect"]))
        //    {
        //        Aspect_of_the_Cougar.Draw(new Vector2(485 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(485 + XBonus, 275 + YBonus, Colour.Green, "Ready");
        //    }
        //}
        //public static void Special_Draw_3(EventArgs agrs)
        //{
        //    if (Checked && Event.Humanform && Spells.Q.IsLearned && !Event.IsReady(Event.CD["Takedown"]))
        //    {
        //        Takedown_grey.Draw(new Vector2(275 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(275 + XBonus, 275 + YBonus, Colour.White, "" + Event.CD["Takedown"]);
        //    }
        //    if (Checked && Event.Humanform && Spells.W.IsLearned && !Event.IsReady(Event.CD["Pounce"]))
        //    {
        //        Pounce_grey.Draw(new Vector2(345 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(345 + XBonus, 275 + YBonus, Colour.White, "" + Event.CD["Pounce"]);
        //    }
        //    if (Checked && Event.Humanform && Spells.E.IsLearned && !Event.IsReady(Event.CD["Swipe"]))
        //    {
        //        Swipe_grey.Draw(new Vector2(415 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(415 + XBonus, 275 + YBonus, Colour.White, "" + Event.CD["Swipe"]);
        //    }
        //    if (Checked && Event.Humanform && !Event.IsReady(Event.CD["Aspect"]))
        //    {
        //        R2.Draw(new Vector2(485 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(485 + XBonus, 275 + YBonus, Colour.White, "" + Event.CD["Aspect"]);
        //    }
        //    //--------------------------------------------------------------------------//
        //    if (Checked && !Event.Humanform && Spells.Q2.IsLearned && !Event.IsReady(Event.CD["Javelintoss"]))
        //    {
        //        Javelin_Toss_grey.Draw(new Vector2(275 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(275 + XBonus, 275 + YBonus, Colour.White, "" + Event.CD["Javelintoss"]);
        //    }
        //    if (Checked && !Event.Humanform && Spells.W2.IsLearned && !Event.IsReady(Event.CD["Bushwhack"]))
        //    {
        //        Bushwhack_grey.Draw(new Vector2(345 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(345 + XBonus, 275 + YBonus, Colour.White, "" + Event.CD["Bushwhack"]);
        //    }
        //    if (Checked && !Event.Humanform && Spells.E2.IsLearned && !Event.IsReady(Event.CD["Primalsurge"]))
        //    {
        //        Primal_Surge_grey.Draw(new Vector2(415 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(415 + XBonus, 275 + YBonus, Colour.White, "" + Event.CD["Primalsurge"]);
        //    }
        //    if (Checked && !Event.Humanform && !Event.IsReady(Event.CD["Aspect"]))
        //    {
        //        Aspect_of_the_Cougar_grey.Draw(new Vector2(485 + XBonus, 200 + YBonus));
        //        Drawing.DrawText(485 + XBonus, 275 + YBonus, Colour.White, "" + Event.CD["Aspect"]);
        //    }
        //}
    }
}
