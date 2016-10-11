using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace UBActivator
{
    class Potions
    {
        public static void OnTick()
        {
            if (!Config.Potions["ePotions"].Cast<CheckBox>().CurrentValue) return;
            if (Player.Instance.IsRecalling() && Config.Potions["preHPrecall"].Cast<CheckBox>().CurrentValue) return;
            if (Player.Instance.IsInShopRange() && Config.Potions["inshopHP"].Cast<CheckBox>().CurrentValue) return;
            if (Player.Instance.HasBuff("RegenerationPotion")
                || Player.Instance.HasBuff("ItemMiniRegenPotion")
                || Player.Instance.HasBuff("ItemCrystalFlask")
                || Player.Instance.HasBuff("ItemDarkCrystalFlask")
                || Player.Instance.HasBuff("ItemCrystalFlaskJungle"))
                return;
            var Health = Config.Potions["predHP"].Cast<CheckBox>().CurrentValue ? 
                Prediction.Health.GetPrediction(Player.Instance, 1000) / Player.Instance.MaxHealth * 100 :
                Player.Instance.HealthPercent;
            var Mana1 = Config.Potions["MPCP"].Cast<CheckBox>().CurrentValue ?
                Player.Instance.Mana :
                0;
            var Mana2 = Config.Potions["MPHTP"].Cast<CheckBox>().CurrentValue?
                Player.Instance.Mana :
                0;

            if (Config.Potions["HP"].Cast<CheckBox>().CurrentValue
            && Health <= Config.Potions["HPH"].Cast<Slider>().CurrentValue
            && Items.HealthPotion.IsOwned()
            && Items.HealthPotion.IsReady())
            {
                Items.HealthPotion.Cast();
            }
            if (Config.Potions["Biscuit"].Cast<CheckBox>().CurrentValue
            && Health <= Config.Potions["BiscuitH"].Cast<Slider>().CurrentValue
            && Items.Total_Biscuit_of_Rejuvenation.IsOwned()
            && Items.Total_Biscuit_of_Rejuvenation.IsReady())
            {
                Items.Total_Biscuit_of_Rejuvenation.Cast();
            }
            if (Config.Potions["RP"].Cast<CheckBox>().CurrentValue
            && Health <= Config.Potions["RPH"].Cast<Slider>().CurrentValue
            && Items.Refillable_Potion.IsOwned()
            && Items.Refillable_Potion.IsReady())
            {
                Items.Refillable_Potion.Cast();
            }
            if (Config.Potions["CP"].Cast<CheckBox>().CurrentValue
            && Health <= Config.Potions["CPH"].Cast<Slider>().CurrentValue
            && (Mana1 + 75 <= Player.Instance.MaxMana || Extensions.ChampNoMana)
            && Items.Corrupting_Potion.IsOwned()
            && Items.Corrupting_Potion.IsReady())
            {
                Items.Corrupting_Potion.Cast();
            }
            if (Config.Potions["HTP"].Cast<CheckBox>().CurrentValue
            && Health <= Config.Potions["HTPH"].Cast<Slider>().CurrentValue
            && (Mana2 + 35 <= Player.Instance.MaxMana || Extensions.ChampNoMana)
            && Items.Hunter_s_Potion.IsOwned()
            && Items.Hunter_s_Potion.IsReady())
            {
                Items.Hunter_s_Potion.Cast();
            }
        }
    }
}
