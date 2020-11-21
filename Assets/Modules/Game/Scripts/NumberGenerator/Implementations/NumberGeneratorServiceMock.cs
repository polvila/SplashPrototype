﻿using Zenject;

namespace Modules.Game
{
	public class NumberGeneratorServiceMock : INumberGeneratorService
	{
		protected const int MaxRange = 13;
		protected const int MinRange = 1;

		public int GetMaxRange => MaxRange;
		public int GetMinRange => MinRange;

		private RandomGenerator _generator;
		private DiContainer _container;

		private CardGeneratorMode _defaultGeneratorMode;
		private CardGeneratorMode _generatorMode;

		public CardGeneratorMode GeneratorMode
		{
			get { return _generatorMode; }
			set
			{
				_generatorMode = value;
				switch (_generatorMode)
				{
					case CardGeneratorMode.Random:
						_generator = _container.Resolve<RandomGenerator>();
						break;
					case CardGeneratorMode.RandomExcluding:
						_generator = _container.Resolve<RandomExcludingGenerator>();
						break;
					case CardGeneratorMode.FTUE:
						_generator = _container.Resolve<FTUEGenerator>();
						break;
				}
				_generator.Reset();
			}
		}

		protected NumberGeneratorServiceMock(CardGeneratorMode generatorMode, DiContainer container)
		{
			_container = container;
			_defaultGeneratorMode = generatorMode;
			GeneratorMode = _defaultGeneratorMode;
		}

		public int GetNumber()
		{
			return _generator.GenerateNumber(MinRange, MaxRange);
		}

		public void SetDefaultGeneratorMode()
		{
			GeneratorMode = _defaultGeneratorMode;
		}
	}
}