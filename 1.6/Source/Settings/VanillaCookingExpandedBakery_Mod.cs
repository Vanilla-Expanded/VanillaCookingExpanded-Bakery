using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace VanillaCookingExpandedBakery;

public class VanillaCookingExpandedBakery_Mod : Mod
{
    public VanillaCookingExpandedBakery_Mod(ModContentPack content) : base(content)
    {
      
        settings = GetSettings<VanillaCookingExpandedBakery_Settings>();
    }

    public static VanillaCookingExpandedBakery_Settings settings;

    public override string SettingsCategory()
    {
        return "VCE - Bakery";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        settings.DoWindowContents(inRect);
    }

}
