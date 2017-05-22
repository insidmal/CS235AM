using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PigClasses
{
    public class Player
    {
        int gameScore;
        int turnScore;

        public int GameScore { get { return gameScore; } }
        public int TurnScore { get { return turnScore; } }

        public Player()
        {
            gameScore = 0;
            turnScore = 0;
        }

        public int AddTurnScore(int diceRoll)
        {
            turnScore += diceRoll;
            return turnScore;
        }

        public int UpdateGameScore()
        {
            gameScore += turnScore;
            turnScore = 0;
            return gameScore;
        }

        public void ResetTurnScore()
        {
            turnScore = 0;
        }

        /// <summary>
        /// Roll Die and Update Score
        /// </summary>
        /// <param name="die">Die Object.</param>
        /// <param name="endGame">True if turn is over.</param>
        /// <returns>Dice Roll.</returns>
        public int Roll(Die die, out bool endGame)
        {
            int roll = die.Roll();
            endGame = false;
            if (roll == 8) { endGame = true;
                ResetTurnScore();
                 }
            else {

                turnScore = AddTurnScore(roll);
            }
            return roll;

        }



    }


}