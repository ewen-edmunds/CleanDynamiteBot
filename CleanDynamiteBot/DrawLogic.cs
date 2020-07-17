using System.Collections.Generic;
using System.Linq;
using BotInterface.Game;

namespace CleanDynamiteBot
{
    public static class DrawLogic
    {
        public static Move GetMoveGivenDraw(Gamestate gamestate)
        {
            int numberDraws = GetNumberOfDraws(gamestate);
            int drawStreak = GetDrawStreak(gamestate);

            var allResponses = DictionaryLogic.GetResponsesToConditionalRounds(gamestate.GetRounds(),
                WasDrawOnRound);

            //See if the opponent has a strategy for this many points
            Move myLastMove = gamestate.GetRounds().Last().GetP1();
            var streakResponses = DictionaryLogic.GetResponsesToConditionalRounds(
                GetRoundsAfterDrawStreak(gamestate, drawStreak), round => round.GetP1() == myLastMove);

            Move? sigMove = MoveClass.GetSignificantPickFrom(streakResponses);
            if (sigMove != null)
            {
                return MoveClass.GetFinisherMove((Move) sigMove);
            }
            
            Move move = MoveClass.GetWeightedPickFrom(allResponses);
            
            return MoveClass.GetFinisherMove(move);
        }
        
        public static bool WasDrawLastRound(Gamestate gamestate)
        {
            if (gamestate.GetRounds().Length < 2)
            {
                return false;
            }
            
            Round round = gamestate.GetRounds().Last();
            return round.GetP1() == round.GetP2();
        }

        public static bool WasDrawOnRound(Round round)
        {
            return round.GetP1() == round.GetP2();
        }

        public static int GetNumberOfDraws(Gamestate gamestate)
        {
            int count = 0;
            foreach (var round in gamestate.GetRounds())
            {
                if (WasDrawOnRound(round)){count += 1;}
            }

            return count;
        }

        public static int GetDrawStreak(Gamestate gamestate)
        {
            int streak = 0;

            foreach (var round in gamestate.GetRounds().Reverse())
            {
                if (WasDrawOnRound(round))
                {
                    streak += 1;
                }
            }

            return streak;
        }

        public static Round[] GetRoundsAfterDrawStreak(Gamestate gamestate, int streak)
        {
            int currentStreak = 0;
            List<Round> rounds = new List<Round>();

            foreach (Round round in gamestate.GetRounds())
            {
                if (currentStreak >= streak)
                {
                    rounds.Add(round);
                }
                
                if (WasDrawOnRound(round))
                {
                    currentStreak += 1;
                }
                else
                {
                    currentStreak = 0;
                }
            }

            return rounds.ToArray();
        }
    }
}