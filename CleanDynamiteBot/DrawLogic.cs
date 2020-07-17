using System.Linq;
using BotInterface.Game;

namespace CleanDynamiteBot
{
    public static class DrawLogic
    {
        public static Move GetMoveGivenDraw(Gamestate gamestate)
        {
            int numberDraws = GetNumberOfDraws(gamestate);

            var allResponses = DictionaryLogic.GetResponsesToConditionalRounds(gamestate.GetRounds(),
                WasDrawOnRound);

            

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
    }
}