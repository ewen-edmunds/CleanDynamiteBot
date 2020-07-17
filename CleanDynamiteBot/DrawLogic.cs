using System.Linq;
using BotInterface.Game;

namespace CleanDynamiteBot
{
    public static class DrawLogic
    {
        public static bool WasDrawLastRound(Gamestate gamestate)
        {
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