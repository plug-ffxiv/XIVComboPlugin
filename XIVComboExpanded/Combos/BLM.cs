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

    internal class BlackFireFeature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackFireFeature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Fire };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Fire)
            {
                var gauge = GetJobGauge<BLMGauge>();
                
                if (level >= BLM.Levels.Paradox && gauge.InUmbralIce && HasEffect(BLM.Buffs.Firestarter))
                    return BLM.Transpose;
                    
                if (level >= BLM.Levels.Fire3 && (!gauge.InAstralFire || HasEffect(BLM.Buffs.Firestarter)))
                    return BLM.Fire3;

                // Paradox
                return OriginalHook(BLM.Fire);
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
    
    internal class BlackFire4Feature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackFire4Feature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Fire4 };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Fire4)
            {
                if (LocalPlayer?.CurrentMp < 800)
                    return BLM.Manafont;
                    
                if (level >= BLM.Levels.Despair && LocalPlayer?.CurrentMp < 2400)
                    return BLM.Despair;
            }

            return actionID;
        }
    }
    
    internal class BlackThunderFeature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackThunderFeature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Thunder, BLM.Thunder2, BLM.Thunder3, BLM.Thunder4 };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Thunder || actionID == BLM.Thunder2 || actionID == BLM.Thunder3 || actionID == BLM.Thunder4)
            {
                if (!HasEffect(BLM.Buffs.Sharpcast) && level >= BLM.Levels.Sharpcast)
                {
                    var cooldownData = GetCooldown(BLM.Sharpcast);

                    if (!cooldownData.IsCooldown)
                        return BLM.Sharpcast;
                    
                    if (level >= BLM.Levels.EnhancedSharpcast2 && cooldownData.CooldownElapsed >= 30)
                        return BLM.Sharpcast;
                }
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
                        
                    if (LocalPlayer?.CurrentMp >= 800)
                        return BLM.Despair;
                }  
            }

            return actionID;
        }
    }
    
    internal class BlackFlareFeature : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackFlareFeature;

        protected internal override uint[] ActionIDs { get; } = new[] { BLM.Flare };

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Flare)
            {
                var gauge = GetJobGauge<BLMGauge>();
                
                var ampCooldown = GetCooldown(BLM.Amplifier);
                
                var manafontCooldown = GetCooldown(BLM.Manafont);
                
                if (gauge.InUmbralIce)
                {
                    if (!HasEffect(BLM.Buffs.Sharpcast))
                        return BLM.Sharpcast;
                    
                    if (gauge.UmbralHearts < 3)
                        return BLM.Freeze;
                        
                    if (lastComboMove == BLM.Freeze)
                        return BLM.Thunder4;
                        
                    return BLM.HighFire2;
                }
                
                if (gauge.InAstralFire)
                {
                    if (gauge.PolyglotStacks == 2)
                        return BLM.Foul;
                        
                    if (!ampCooldown.IsCooldown)
                        return BLM.Amplifier;
                    
                    if (LocalPlayer?.CurrentMp < 800)
                    {
                        if (!manafontCooldown.IsCooldown)
                            return BLM.Manafont;
                            
                        return BLM.HighBlizzard2;
                    }
                    
                    if (gauge.UmbralHearts > 1)
                        return BLM.HighFire2;
                    
                    return BLM.Flare;
                }
            
                return BLM.HighBlizzard2;
            }
        }
    }
}
