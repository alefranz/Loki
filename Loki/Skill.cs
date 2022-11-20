using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using JetBrains.Annotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace Loki
{
    public class Skill : INotifyPropertyChanged
    {
        private float _level;
        private float _accumulator;
		private double _xp;
        private Dictionary<SkillType, double> _increments = new Dictionary<SkillType, double>
        {
            [SkillType.Unarmed] = 16.27228143,
            [SkillType.Clubs] = 87.80272379,
            [SkillType.Blocking] = 29.13766185,
            [SkillType.Axes] = 114.7614143,
            [SkillType.Run] = 816.5026443,
            [SkillType.WoodCutting] = 1087.85807,
            [SkillType.Bows] = 170.6985529,
            [SkillType.Sneak] = 65.44530885,
            [SkillType.Jump] = 364.4433961,
            [SkillType.Swim] = 17.71874028,
            [SkillType.Spears] = 1124.998332,
            [SkillType.Knives] = 0,
            [SkillType.Pickaxes] = 584.1558452,
            [SkillType.Polearms] = 0,
            [SkillType.Swords] = 0,
        };

	public Skill(SkillType type, float level, float accumulator)
        {
            Type = type;
            SkillName = GetSkillName(type);
            Level = level;
            Accumulator = accumulator;
            for (var i = 0; i <= ((int)level) - 1; i++)
            {
                _xp += Math.Pow(i + 1, 1.5) * 0.5 + 0.5;
            }
            var rest = level - (int)level;
			var mmm = Math.Pow((int)level + 1, 1.5) * 0.5 + 0.5;
            _xp += rest * mmm;
            _xp += accumulator;


            var increment = _increments[type];
            Debug.Write(increment);
            while (increment > 0)
            {
                var xp = Math.Pow(Level + 1, 1.5) * 0.5 + 0.5;
                if (increment > xp)
                {
                    increment -= xp;
                    Level++;
                }
                else
                {
                    Level += (float)(increment / xp);
                    increment = 0;
                }
			}
			Debug.Write(Level);
		}

		private static string GetSkillName(SkillType type)
            =>
                type switch
                {
                    SkillType.None => Properties.Resources.None,
                    SkillType.Swords => Properties.Resources.Swords,
                    SkillType.Knives => Properties.Resources.Knives,
                    SkillType.Clubs => Properties.Resources.Clubs,
                    SkillType.Polearms => Properties.Resources.Polearms,
                    SkillType.Spears => Properties.Resources.Spears,
                    SkillType.Blocking => Properties.Resources.Blocking,
                    SkillType.Axes => Properties.Resources.Axes,
                    SkillType.Bows => Properties.Resources.Bows,
                    SkillType.FireMagic => Properties.Resources.FireMagic,
                    SkillType.FrostMagic => Properties.Resources.FrostMagic,
                    SkillType.Unarmed => Properties.Resources.Unarmed,
                    SkillType.Pickaxes => Properties.Resources.Pickaxes,
                    SkillType.WoodCutting => Properties.Resources.Wood_Cutting,
                    SkillType.Jump => Properties.Resources.Jump,
                    SkillType.Sneak => Properties.Resources.Sneak,
                    SkillType.Run => Properties.Resources.Run,
                    SkillType.Swim => Properties.Resources.Swim,
                    SkillType.Ride => Properties.Resources.Riding,
                    SkillType.VL_Discipline => Properties.Resources.VL_Discipline,
                    SkillType.VL_Abjuration => Properties.Resources.VL_Abjuration,
                    SkillType.VL_Alteration => Properties.Resources.VL_Alteration,
                    SkillType.VL_Conjuration => Properties.Resources.VL_Conjuration,
                    SkillType.VL_Evocation => Properties.Resources.VL_Evocation,
                    SkillType.VL_Illusion => Properties.Resources.VL_Illusion,
                    SkillType.PP_Alchemy => Properties.Resources.PP_Alchemy,
                    SkillType.All => Properties.Resources.All,
                    //_ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised skill type"),
                    _ => Properties.Resources.Unknown,
                };

        public SkillType Type { get; }
        public string SkillName { get; }

        public float Level
        {
            get => _level;
            set
            {
                if (value.Equals(_level)) return;
                if (value <= 0f) value = 0f;
                if (value >= 100f) value = 100f;
                _level = value;
                OnPropertyChanged();
            }
        }

        public float Accumulator
        {
            get => _accumulator;
            set
            {
                if (value.Equals(_accumulator)) return;
                _accumulator = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
