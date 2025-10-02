using RimWorld;
using UnityEngine;
using Verse;
using System;



namespace VanillaCookingExpandedBakery
{


    public class VanillaCookingExpandedBakery_Settings : ModSettings

    {

        public const int simpleMealMoodBase = 3;
        public static int simpleMealMood = simpleMealMoodBase;

        public const int fineMealMoodBase = 5;
        public static int fineMealMood = fineMealMoodBase;

        public const int lavishMealMoodBase = 10;
        public static int lavishMealMood = lavishMealMoodBase;

        public const int gourmetMealMoodBase = 15;
        public static int gourmetMealMood = gourmetMealMoodBase;

        public const int ticksForEffectBase = 180000;
        public static int ticksForEffect = ticksForEffectBase;

        private static Vector2 scrollPosition = Vector2.zero;



        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref simpleMealMood, "simpleMealMood", simpleMealMoodBase, true);
            Scribe_Values.Look<int>(ref fineMealMood, "fineMealMood", fineMealMoodBase, true);
            Scribe_Values.Look<int>(ref lavishMealMood, "lavishMealMood", lavishMealMoodBase, true);
            Scribe_Values.Look<int>(ref gourmetMealMood, "gourmetMealMood", gourmetMealMoodBase, true);
            Scribe_Values.Look<int>(ref ticksForEffect, "ticksForEffect", ticksForEffectBase, true);

        }

        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();

            var scrollContainer = inRect.ContractedBy(10);
            scrollContainer.height -= ls.CurHeight;
            scrollContainer.y += ls.CurHeight;
            Widgets.DrawBoxSolid(scrollContainer, Color.grey);
            var innerContainer = scrollContainer.ContractedBy(1);
            Widgets.DrawBoxSolid(innerContainer, new ColorInt(42, 43, 44).ToColor);
            var frameRect = innerContainer.ContractedBy(5);
            frameRect.y += 15;
            frameRect.height -= 15;
            var contentRect = frameRect;
            contentRect.x = 0;
            contentRect.y = 0;
            contentRect.width -= 20;
            contentRect.height = 500;

            Listing_Standard ls2 = new Listing_Standard();

            Widgets.BeginScrollView(frameRect, ref scrollPosition, contentRect, true);
            ls2.Begin(contentRect.AtZero());


            var simpleLabel = ls2.LabelPlusButton("VCE_SimpleMealMood".Translate() + ": -" + simpleMealMood, "VCE_SimpleMealMoodDesc".Translate());
            simpleMealMood = (int)Math.Round(ls2.Slider(simpleMealMood, 0f, 20f), 0);

            if (ls2.Settings_Button("VCE_Reset".Translate(), new Rect(0f, simpleLabel.position.y + 35, 250f, 29f)))
            {
                simpleMealMood = simpleMealMoodBase;
            }
            var fineLabel = ls2.LabelPlusButton("VCE_FineMealMood".Translate() + ": -" + fineMealMood, "VCE_FineMealMoodDesc".Translate());
            fineMealMood = (int)Math.Round(ls2.Slider(fineMealMood, 0f, 20f), 0);

            if (ls2.Settings_Button("VCE_Reset".Translate(), new Rect(0f, fineLabel.position.y + 35, 250f, 29f)))
            {
                fineMealMood = fineMealMoodBase;
            }
            var lavishLabel = ls2.LabelPlusButton("VCE_LavishMealMood".Translate() + ": -" + lavishMealMood, "VCE_LavishMealMoodDesc".Translate());
            lavishMealMood = (int)Math.Round(ls2.Slider(lavishMealMood, 0f, 30f), 0);

            if (ls2.Settings_Button("VCE_Reset".Translate(), new Rect(0f, lavishLabel.position.y + 35, 250f, 29f)))
            {
                lavishMealMood = lavishMealMoodBase;
            }
            var gourmetLabel = ls2.LabelPlusButton("VCE_GourmetMealMood".Translate() + ": -" + gourmetMealMood, "VCE_GourmetMealMoodDesc".Translate());
            gourmetMealMood = (int)Math.Round(ls2.Slider(gourmetMealMood, 0f, 30f), 0);

            if (ls2.Settings_Button("VCE_Reset".Translate(), new Rect(0f, gourmetLabel.position.y + 35, 250f, 29f)))
            {
                gourmetMealMood = gourmetMealMoodBase;
            }

            var ticksLabel = ls2.LabelPlusButton("VCE_TicksForEffect".Translate() + ": " + ticksForEffect.ToStringTicksToDays(), "VCE_TicksForEffectDesc".Translate());
            ticksForEffect = (int)Math.Round(ls2.Slider(ticksForEffect, 0f, 600000f), 0);

            if (ls2.Settings_Button("VCE_Reset".Translate(), new Rect(0f, ticksLabel.position.y + 35, 250f, 29f)))
            {
                ticksForEffect = ticksForEffectBase;
            }


            ls2.End();
            Widgets.EndScrollView();
            Write();
        }
    }
}
