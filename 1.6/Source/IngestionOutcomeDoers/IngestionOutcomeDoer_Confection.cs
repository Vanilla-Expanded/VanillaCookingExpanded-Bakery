using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace VanillaCookingExpandedBakery
{
    public class IngestionOutcomeDoer_Confection: IngestionOutcomeDoer
    {
        public MealType mealType;

        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            if (pawn.RaceProps?.Humanlike==true && pawn.health?.hediffSet?.HasHediff(InternalDefOf.VCE_ConsumedConfection)==false &&pawn.needs?.mood?.thoughts?.memories!=null)
            {
                int moodMax = 0;

                switch (mealType){

                    case MealType.Simple:
                        moodMax = -VanillaCookingExpandedBakery_Settings.simpleMealMood;
                        break;
                    case MealType.Fine:
                        moodMax = -VanillaCookingExpandedBakery_Settings.fineMealMood;
                        break;
                    case MealType.Lavish:
                        moodMax = -VanillaCookingExpandedBakery_Settings.lavishMealMood;
                        break;
                    case MealType.Gourmet:
                        moodMax = -VanillaCookingExpandedBakery_Settings.gourmetMealMood;
                        break;

                }
                if (moodMax != 0)
                {
                    Thought_Memory chosenMemory = pawn.needs.mood.thoughts.memories.Memories.Where(x => x.moodOffset < 0 && x.moodOffset >= moodMax)?.RandomElement();
                    if (chosenMemory != null) {
                        pawn.needs.mood.thoughts.memories.RemoveMemory(chosenMemory);
                    }

                }

                


            }
        }

    }
}
