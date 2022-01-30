using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class BLM
    {
        public const byte ClassID = 7;
        public const byte JobID = 25;

        public const uint
            Fire = 141,
            Blizzard = 142,
            Thunder = 144,
            Fire2 = 147,
            Thunder2 = 148,
            Transpose = 149,
            Fire3 = 152,
            Thunder3 = 153,
            Blizzard3 = 154,
            Scathe = 156,
            Manafont = 158,
            Freeze = 159,
            Flare = 162,
            LeyLines = 3573,
            Sharpcast = 3574,
            Blizzard4 = 3576,
            Fire4 = 3577,
            BetweenTheLines = 7419,
            Thunder4 = 7420,
            Foul = 7422,
            Despair = 16505,
            UmbralSoul = 16506,
            Xenoglossy = 16507,
            Blizzard2 = 25793,
            HighFire2 = 25794,
            HighBlizzard2 = 25795,
            Amplifier = 25796,
            Paradox = 25797;

        public static class Buffs
        {
            public const ushort
                Thundercloud = 164,
                LeyLines = 737,
                Firestarter = 165,
                Sharpcast = 867;
        }

        public static class Debuffs
        {
            public const ushort
                Thunder = 161,
                Thunder3 = 163;
        }

        public static class Levels
        {
            public const byte
                Fire3 = 35,
                Blizzard3 = 35,
                Freeze = 40,
                Thunder3 = 45,
                Flare = 50,
                Sharpcast = 54,
                Blizzard4 = 58,
                Fire4 = 60,
                BetweenTheLines = 62,
                Despair = 72,
                UmbralSoul = 76,
                Xenoglossy = 80,
                HighFire2 = 82,
                HighBlizzard2 = 82,
                Amplifier = 86,
                EnhancedSharpcast2 = 88,
                Paradox = 90;
        }
    }

    internal class BlackManaFeature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackManaFeature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.UmbralSoul };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.UmbralSoul)
            {
                var gauge = GetJobGauge<BLMGauge>();

                if (gauge.InAstralFire || level < BLM.Levels.UmbralSoul)
                    return BLM.Transpose;
            }

            return actionID;
        }
    }

    internal class BlackLeyLinesFeature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackLeyLinesFeature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.LeyLines };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.LeyLines)
            {
                if (level >= BLM.Levels.BetweenTheLines && HasEffect(BLM.Buffs.LeyLines))
                    return BLM.BetweenTheLines;
            }

            return actionID;
        }
    }
    
    internal class BlackFire3Feature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackFire3Feature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Fire3 };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Fire3)
            {
                var gauge = GetJobGauge<BLMGauge>();
                
                if (gauge.InAstralFire)
                {
                    if (HasEffect(BLM.Buffs.Firestarter))
                        return BLM.Fire3;
                    
                    if (LocalPlayer?.CurrentMp < 1600)
                        return BLM.Blizzard3;
                        
                    if (gauge.IsParadoxActive)
                        return OriginalHook(BLM.Fire);
                        
                    return OriginalHook(BLM.Fire);
                }
                
                if (gauge.InUmbralIce)
                {
                    if (gauge.IsParadoxActive)
                        return OriginalHook(BLM.Blizzard);
                        
                    if (gauge.UmbralHearts < 3 && gauge.ElementTimeRemaining > 4000)
                        return BLM.Blizzard4;
                        
                    return BLM.Fire3;
                }
                
                return BLM.Fire3;
            }

            return actionID;
        }
    }


    internal class BlackFireFeature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackFireFeature;
        
        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Fire };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Fire)
            {
                var gauge = GetJobGauge<BLMGauge>();

                if (gauge.InAstralFire)
                {
                    if (gauge.AstralFireStacks < 3 && HasEffect(BLM.Buffs.Firestarter))
                        return BLM.Fire3;
                        
                    if (lastComboMove == BLM.Despair || LocalPlayer?.CurrentMp < 800)
                        return BLM.Blizzard3;
                        
                    if (!gauge.IsParadoxActive || LocalPlayer?.CurrentMp < 2400)
                        return BLM.Despair;
                        
                    return OriginalHook(BLM.Fire);
                }
                
                if (gauge.InUmbralIce)
                {
                    if (gauge.IsParadoxActive)
                        return OriginalHook(BLM.Blizzard);
                    
                    if (gauge.UmbralHearts < 3)
                        return BLM.Blizzard4;
                        
                    return BLM.Fire3;
                }

                if (LocalPlayer?.CurrentMp < 2000)
                    return BLM.Blizzard3;
                    
                return BLM.Fire3;
            }

            return actionID;
        }
    }

    internal class BlackBlizzardFeature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackBlizzardFeature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Blizzard };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Blizzard)
            {
                var gauge = GetJobGauge<BLMGauge>();

                if (level >= BLM.Levels.Blizzard3 && !gauge.InUmbralIce)
                    return BLM.Blizzard3;

                // Paradox
                return OriginalHook(BLM.Blizzard);
            }

            return actionID;
        }
    }

    internal class BlackFire2Feature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackFire2Feature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Fire2, BLM.HighFire2 };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Fire2 || actionID == BLM.HighFire2)
            {
                var gauge = GetJobGauge<BLMGauge>();
                
                if (gauge.InAstralFire)
                {
                    if (level >= BLM.Levels.Flare && LocalPlayer?.CurrentMp < 800)
                        return BLM.Manafont;

                    if (level >= BLM.Levels.Flare && gauge.InAstralFire && gauge.UmbralHearts <= 1)
                        return BLM.Flare;
                }
            }

            return actionID;
        }
    }

    internal class BlackBlizzard2Feature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackBlizzard2Feature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Blizzard2, BLM.HighBlizzard2 };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Blizzard2 || actionID == BLM.HighBlizzard2)
            {
                var gauge = GetJobGauge<BLMGauge>();

                if (level >= BLM.Levels.Freeze && gauge.InUmbralIce)
                    return BLM.Freeze;
            }

            return actionID;
        }
    }
    
    internal class BlackXenoglossyFeature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackXenoglossyFeature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Xenoglossy };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Xenoglossy)
            {
                var gauge = GetJobGauge<BLMGauge>();

                if (level >= BLM.Levels.Paradox && gauge.InUmbralIce && gauge.IsParadoxActive && gauge.PolyglotStacks < 2)
                    return OriginalHook(BLM.Blizzard);
            }

            return actionID;
        }
    }
    
    internal class BlackAmplifierFeature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackAmplifierFeature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Foul, BLM.Xenoglossy };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Foul || actionID == BLM.Xenoglossy)
            {
                var gauge = GetJobGauge<BLMGauge>();

                if (level >= BLM.Levels.Amplifier && gauge.PolyglotStacks == 0)
                    return BLM.Amplifier;
            }

            return actionID;
        }
    }
    
    internal class BlackBlizzard4Feature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackBlizzard4Feature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Blizzard4 };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Blizzard4)
            {                    
                var gauge = GetJobGauge<BLMGauge>();
                                                
                if (gauge.InAstralFire)
                    return BLM.Fire4;
                    
                if (gauge.InUmbralIce)
                {
                    if (gauge.UmbralHearts < 3)
                        return BLM.Blizzard4;
                        
                    return BLM.Fire4;
                }
                
                return BLM.Fire4;
            }

            return actionID;
        }
    }
    
    internal class BlackSharpcastFeature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackSharpcastFeature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Sharpcast };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Sharpcast)
            {          
                var thunder3 = FindTargetEffect(BLM.Debuffs.Thunder3);
                
                var cooldownData = GetCooldown(BLM.Sharpcast);
                
                if (((thunder3 is null) || (thunder3?.RemainingTime < 4)) && (HasEffect(BLM.Sharpcast)))
                    return BLM.Thunder3;
                    
                if ((cooldownData.IsCooldown && (cooldownData.CooldownElapsed < 30))
                    return BLM.Thunder3;
                    
                return BLM.Sharpcast;
            }

            return actionID;
        }
    }

    
    internal class BlackBlizzard3Feature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackBlizzard3Feature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Blizzard3 };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Blizzard3 && level >= BLM.Levels.Paradox)
            {
                var gauge = GetJobGauge<BLMGauge>();
                
                if (gauge.InUmbralIce)
                {                
                    if (gauge.UmbralHearts != 3)
                        return BLM.Blizzard4;
                        
                    if (gauge.IsParadoxActive)
                        return OriginalHook(BLM.Blizzard);
                        
                    if (gauge.PolyglotStacks == 2)
                        return BLM.Xenoglossy;
                        
                    if (LocalPlayer?.CurrentMp >= 2400)    
                        return BLM.Transpose;
                        
                    if (gauge.PolyglotStacks == 0)
                        return BLM.Amplifier;
                    
                    return BLM.Xenoglossy;
                }
                
                if (gauge.InAstralFire)
                {                
                    if (gauge.IsParadoxActive)
                        return OriginalHook(BLM.Fire);
                        
                    if (HasEffect(BLM.Buffs.Firestarter))
                        return BLM.Fire3;
                        
                    if (LocalPlayer?.CurrentMp >= 800)
                        return BLM.Despair;
                }  
            }

            return actionID;
        }
    }
}
