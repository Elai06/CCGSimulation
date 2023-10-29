using System;
using Gameplay.Enums;

namespace Gameplay.Cards.Parameters
{
    [Serializable]
    public class CardParametersData
    {
        public ECardParametersType ParametersType;
        public int Value;
    }
}