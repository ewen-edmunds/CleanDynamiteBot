using BotInterface.Bot;
using BotInterface.Game;

namespace CleanDynamiteBot
{
    //todo: rename classes to make sense
    public class BotLogic : IBot
    {
        public int MyDynamiteRemaining = 100;
        public int EnemyDynamiteRemaining = 100;
        public int RoundsSinceSeenEnemyDynamite = 50;
        public const int ROUNDS_TO_IGNORE_ENEMY_DYNAMITE_AFTER = 50;
        
        public Move MakeMove(Gamestate gamestate)
        {
            Move move = GetMove(gamestate);

            while (!IsValidMove(move))
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

        public bool IsValidMove(Move move)
        {
            return !((move == Move.D && MyDynamiteRemaining <= 0) ||
                     (move == Move.W && (EnemyDynamiteRemaining <= 0 || RoundsSinceSeenEnemyDynamite >= ROUNDS_TO_IGNORE_ENEMY_DYNAMITE_AFTER)));
        }
    }
}