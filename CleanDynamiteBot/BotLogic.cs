using BotInterface.Bot;
using BotInterface.Game;

namespace CleanDynamiteBot
{
    public class BotLogic : IBot
    {
        public int MyDynamiteRemaining = 100;
        public int EnemyDynamiteRemaining = 100;
        public int RoundsSinceSeenEnemyDynamite = 50;
        
        public Move MakeMove(Gamestate gamestate)
        {
            Move move = GetMove(gamestate);

            while ((move == Move.D && MyDynamiteRemaining <= 0) ||
                   (move == Move.W && (EnemyDynamiteRemaining <= 0 || RoundsSinceSeenEnemyDynamite >= 50)))
            {
                move = MoveClass.GetRandomMove();
            }
            
            if (move == Move.D)
            {
                MyDynamiteRemaining -= 1;
            }

            return move;
        }


        public Move GetMove(Gamestate gamestate)
        {
            return MoveClass.GetRandomMove();
        }
    }
}