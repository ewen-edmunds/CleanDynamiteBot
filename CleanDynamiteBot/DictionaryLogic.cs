using System.Collections.Generic;
using BotInterface.Game;

namespace CleanDynamiteBot
{
    public static class DictionaryLogic
    {
        public static Dictionary<Move, int> GetResponsesToRounds(Round[] rounds)
        {
            Dictionary<Move, int> newDict = new Dictionary<Move, int>();
            newDict.Add(Move.R, 0);
            newDict.Add(Move.P, 0);
            newDict.Add(Move.S, 0);
            newDict.Add(Move.D, 0);
            newDict.Add(Move.W, 0);
            
            foreach (var round in rounds)
            {
                newDict[round.GetP2()] = newDict[round.GetP2()] + 1;
            }

            return newDict;
        }
    }
}