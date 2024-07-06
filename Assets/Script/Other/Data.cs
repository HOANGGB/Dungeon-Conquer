using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static string Gem = "Gem";
    public static string Gold = "Gold";
    //CHARACTER 
    public static string Heal = "Heal";
    public static string Mana = "Heal";

    public static string AttackLevel = "AttackLV";
    public static string SkillLevel = "SkillLV";
    public static string UltiLevel = "UltiLV";
    public static string UnlockSkill = "UnlockSkill";
    public static string UnlockUlti = "UnlockUlti";
    //ITEM SHOP IN BAR
    public static string HealPotionNum = "HealPotionNum";
    public static string ManaPotionNum = "ManaPotionNum";

    public static string TeleportGateGame1 = "TeleportGateGame1";
    public static string TeleportGateGame2 = "TeleportGateGame2";
    public static string TeleportGateGame3 = "TeleportGateGame3";
    public static string TeleportGateHome = "TeleportGateHome";

    public static string TeleportMap = "TeleportMap";
    public static string TeleportX = "TeleportX";
    public static string TeleportY = "TeleportY";
    public static string TeleportZ = "TeleportZ";






    public static Dictionary<string, int> defaultValues = new Dictionary<string, int>()
    {
        { "Gem", 0 },
        { "Gold", 0 },
        { "Heal", 100},
        { "Mana", 100},
        { "AttackLV", 1 },
        { "SkillLV", 1 },
        { "UltiLV", 1 },
        { "UnlockSkill", 0},
        { "UnlockUlti", 0},
        { "HealPotionNum", 0},


        
    };
    
    public static void CheckAndSetDefaultValues()
    {
        foreach (var kvp in defaultValues)
        {
            if (!PlayerPrefs.HasKey(kvp.Key))
            {
                PlayerPrefs.SetInt(kvp.Key, kvp.Value);
                // Debug.Log(kvp.Key +" = " + kvp.Value);
            }
        }
        PlayerPrefs.Save();
    }
    public static void SetDefaultValues()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        foreach (var kvp in defaultValues)
        {
            PlayerPrefs.SetInt(kvp.Key, kvp.Value);
            Debug.Log(kvp.Key +" = " + kvp.Value);

        }
        PlayerPrefs.Save();
    }

}
