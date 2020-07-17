using System;
using System.Collections.Generic;
using BotInterface.Game;

namespace CleanDynamiteBot
{
    public static class DictionaryLogic
    {
        //Returns a dictionary of the opponent's <Specific Move, Frequency> for a given set of rounds 
        public static Dictionary<Move, int> GetFrequencyInRounds(Round[] rounds)
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
        
        //Returns a dictionary of the opponent's <Specific Move on next turn, Frequency> for a given set of rounds 
            //only considers responses to rounds where the condition is met
        public static Dictionary<Move, int> GetResponsesToConditionalRounds(Round[] rounds, Func<Round, bool> condition)
        {
            Dictionary<Move, int> newDict = new Dictionary<Move, int>();
            newDict.Add(Move.R, 0);
            newDict.Add(Move.P, 0);
            newDict.Add(Move.S, 0);
            newDict.Add(Move.D, 0);
            newDict.Add(Move.W, 0);

            for (int i = 0; i < rounds.Length-1; i++)
            {
                var round = rounds[i];
                if (condition(round))
                {
                    //Then we care about the opponent's response to this round
                    var nextRound = rounds[i + 1];
                    newDict[nextRound.GetP2()] = newDict[nextRound.GetP2()] + 1;
                }
            }

            return newDict;
        }
    }
}