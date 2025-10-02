
using Verse;
namespace VanillaCookingExpandedBakery
{
    public class HediffCompProperties_DisappearsConfigurable : HediffCompProperties
    {


        public bool showRemainingTime;

        public bool canUseDecimalsShortForm;

        public MentalStateDef requiredMentalState;

        [MustTranslate]
        public string messageOnDisappear;

        [MustTranslate]
        public string letterTextOnDisappear;

        [MustTranslate]
        public string letterLabelOnDisappear;

        public bool sendLetterOnDisappearIfDead = true;

        public bool leaveFreshWounds = true;

        public HediffCompProperties_DisappearsConfigurable()
        {
            compClass = typeof(HediffComp_DisappearsConfigurable);
        }
    }
}
