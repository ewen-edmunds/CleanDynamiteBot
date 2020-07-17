using System;
using System.Collections.Generic;
using BotInterface.Game;

namespace CleanDynamiteBot
{
    public static class MoveClass
    {
        public const int RANDOM_DYNAMITE_WEIGHT = 6;
        public const float SIGNIFICANCE_THRESHOLD = 0.55f;
        public static Move GetRandomMove()
        {
            Random rng = new Random();
            int randomNum = rng.Next(1000);
            
            if (randomNum < RANDOM_DYNAMITE_WEIGHT)
            {
                return Move.D;
            }

            randomNum = rng.Next(900);
            if (randomNum < 300)
            {
                return Move.P;
            }
            else if (randomNum < 600)
            {
                return Move.R;
            }
            return Move.S;
        }
        
        public static Move GetOppositeMove(Move enemyMove)
        {
            if (enemyMove == Move.S)
            {
                return Move.R;
            }
            else if (enemyMove == Move.R)
            {
                return Move.P;
            }
            else if (enemyMove == Move.P)
            {
                return Move.S;
            }

            return GetRandomMove();
        }
        
        public static Move GetFinisherMove(Move enemyMove)
        {
            if (enemyMove == Move.D)
            {
                return Move.W;
            }
            else if (enemyMove == Move.S || enemyMove == Move.R || enemyMove == Move.P)
            {
                return Move.D;
            }
            return GetRandomMove();
        }

        public static Move GetWeightedPickFrom(Dictionary<Move, int> dict)
        {
            Random rng = new Random();
            
            int sum = 0;
            foreach (var pair in dict)
            {
                sum = sum + pair.Value;
            }

            int randomValue = rng.Next(sum);
            foreach (var pair in dict)
            {
                randomValue -= pair.Value;
                if (randomValue <= 0)
                {
                    return pair.Key;
                }
            }

            return GetRandomMove();
        }
        
        public static Move? GetSignificantPickFrom(Dictionary<Move, int> dict)
        {
            int sum = 0;
            foreach (var pair in dict)
            {
                sum = sum + pair.Value;
            }
            
            foreach (var pair in dict)
            {
                if (pair.Value >= sum * SIGNIFICANCE_THRESHOLD)
                {
                    return pair.Key;
                }
            }

            return null;
        }
    }
}