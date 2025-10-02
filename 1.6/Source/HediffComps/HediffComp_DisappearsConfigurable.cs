
using RimWorld;
using System;
using UnityEngine;
using Verse;
using Verse.Noise;
namespace VanillaCookingExpandedBakery
{
    public class HediffComp_DisappearsConfigurable : HediffComp
    {
        public int ticksToDisappear;

       

        public int seed;

        private const float NoiseScale = 0.25f;

        private const float NoiseWiggliness = 9f;

        public HediffCompProperties_DisappearsConfigurable Props => (HediffCompProperties_DisappearsConfigurable)props;

        public override bool CompShouldRemove
        {
            get
            {
                if (!base.CompShouldRemove && ticksToDisappear > 0)
                {
                    if (Props.requiredMentalState != null)
                    {
                        return Pawn.MentalStateDef != Props.requiredMentalState;
                    }
                    return false;
                }
                return true;
            }
        }

        public float Progress => 1f - (float)ticksToDisappear / (float)Math.Max(1, VanillaCookingExpandedBakery_Settings.ticksForEffect);

        public int EffectiveTicksToDisappear => ticksToDisappear / TicksLostPerTick;

        public float NoisyProgress => AddNoiseToProgress(Progress, seed);

        public virtual int TicksLostPerTick => 1;

        public override string CompLabelInBracketsExtra
        {
            get
            {
                if (Props.showRemainingTime)
                {
                    if (EffectiveTicksToDisappear < 2500)
                    {
                        return EffectiveTicksToDisappear.ToStringSecondsFromTicks("F0");
                    }
                    return EffectiveTicksToDisappear.ToStringTicksToPeriod(allowSeconds: true, shortForm: true, canUseDecimals: true, allowYears: true, Props.canUseDecimalsShortForm);
                }
                return base.CompLabelInBracketsExtra;
            }
        }

        private static float AddNoiseToProgress(float progress, int seed)
        {
            float num = (float)Perlin.GetValue(progress, 0.0, 0.0, 9.0, seed);
            float num2 = 0.25f * (1f - progress);
            return Mathf.Clamp01(progress + num2 * num);
        }

        public override void CompPostMake()
        {
            base.CompPostMake();
            
            seed = Rand.Int;
            ticksToDisappear = VanillaCookingExpandedBakery_Settings.ticksForEffect;
        }

        public void ResetElapsedTicks()
        {
            ticksToDisappear = VanillaCookingExpandedBakery_Settings.ticksForEffect;
        }

        public void SetDuration(int ticks)
        {
           
            ticksToDisappear = ticks;
        }

        public override void CompPostPostRemoved()
        {
            if (!Props.leaveFreshWounds)
            {
                foreach (BodyPartRecord partAndAllChildPart in parent.Part.GetPartAndAllChildParts())
                {
                    Hediff_MissingPart hediff_MissingPart = Pawn.health.hediffSet.GetMissingPartFor(partAndAllChildPart) as Hediff_MissingPart;
                    if (hediff_MissingPart != null)
                    {
                        hediff_MissingPart.IsFresh = false;
                    }
                }
            }
            if (CompShouldRemove && !Props.messageOnDisappear.NullOrEmpty() && PawnUtility.ShouldSendNotificationAbout(Pawn))
            {
                Messages.Message(Props.messageOnDisappear.Formatted(Pawn.Named("PAWN")), Pawn, MessageTypeDefOf.PositiveEvent);
            }
            if (!Props.letterTextOnDisappear.NullOrEmpty() && !Props.letterLabelOnDisappear.NullOrEmpty() && PawnUtility.ShouldSendNotificationAbout(Pawn) && (!Pawn.Dead || Props.sendLetterOnDisappearIfDead))
            {
                Find.LetterStack.ReceiveLetter(Props.letterLabelOnDisappear.Formatted(Pawn.Named("PAWN")), Props.letterTextOnDisappear.Formatted(Pawn.Named("PAWN")), LetterDefOf.PositiveEvent, Pawn);
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            ticksToDisappear -= TicksLostPerTick;
        }

        public override void CompPostMerged(Hediff other)
        {
            base.CompPostMerged(other);
            HediffComp_Disappears hediffComp_Disappears = other.TryGetComp<HediffComp_Disappears>();
            if (hediffComp_Disappears != null && hediffComp_Disappears.ticksToDisappear > ticksToDisappear)
            {
                ticksToDisappear = hediffComp_Disappears.ticksToDisappear;
            }
        }

        public override void CompExposeData()
        {
            Scribe_Values.Look(ref ticksToDisappear, "ticksToDisappear", 0);
           
            Scribe_Values.Look(ref seed, "seed", 0);
        }

        public override string CompDebugString()
        {
            return "ticksToDisappear: " + ticksToDisappear.ToString();
        }

        public override void CopyFrom(HediffComp other)
        {
            HediffComp_Disappears hediffComp_Disappears = other as HediffComp_Disappears;
            if (hediffComp_Disappears != null)
            {
                ticksToDisappear = hediffComp_Disappears.ticksToDisappear;
               
            }
        }
    }
}