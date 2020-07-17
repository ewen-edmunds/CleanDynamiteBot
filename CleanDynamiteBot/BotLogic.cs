﻿using BotInterface.Bot;
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
                //Random
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
            Move move;
            //todo: Fixed decisions
            //todo: predicted response
            //todo: predicted patterns

            if (DrawLogic.WasDrawLastRound(gamestate))
            {
                return DrawLogic.GetMoveGivenDraw(gamestate);
            }
                
            //Predicted play
            if (gamestate.GetRounds().Length > 50)
            {
                var dict = DictionaryLogic.GetFrequencyInRounds(gamestate.GetRounds());
                Move predictedEnemyMove = MoveClass.GetWeightedPickFrom(dict);
                move = MoveClass.GetOppositeMove(predictedEnemyMove);
                return move;
            }

            return MoveClass.GetRandomMove();
        }

        public bool IsValidMove(Move move)
        {
            return !((move == Move.D && MyDynamiteRemaining <= 0) ||
                     (move == Move.W && (EnemyDynamiteRemaining <= 0 || RoundsSinceSeenEnemyDynamite >= ROUNDS_TO_IGNORE_ENEMY_DYNAMITE_AFTER)));
        }
    }
}