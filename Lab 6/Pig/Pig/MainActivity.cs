using Android.App;
using Android.Widget;
using Android.OS;
using PigClasses;
using System.Collections.Generic;
using System.Linq;


using System;
using System.Text;

using Android.Content;
using Android.Runtime;
using Android.Views;

using Android.Content.PM;

namespace Pig
{
    [Activity(Label = "Pig", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        bool activegame;
        int playerTurn;
        List<Player> player;
        List<TextView> gameScore = new List<TextView>();
        Die d;
        const int MAXSCORE = 100;
        string p1Name;
        string p2Name;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var rollDieButton = FindViewById<Button>(Resource.Id.rollDieButton);
            var endTurnButton = FindViewById<Button>(Resource.Id.endTurnButton);
            var endGameButton = FindViewById<Button>(Resource.Id.endGameButton);
            var turnPointsLabel = FindViewById< TextView > (Resource.Id.turnPointsValue);
            var turnLabel = FindViewById<TextView>(Resource.Id.turnLabel);

            bool endTurn;
            gameScore.Add(FindViewById<TextView>(Resource.Id.Player1ScoreValue));
            gameScore.Add(FindViewById<TextView>(Resource.Id.Player2ScoreValue));

            d = StartGame(8, out player);

            playerTurn = 0;
            //roll the dice
            rollDieButton.Click += delegate
            {
                if (activegame == true)
                {
                    int roll = player[playerTurn].Roll(d, out endTurn);
                    UpdateDie(roll);
                    UpdateTurnScore(player[playerTurn].TurnScore);

                    //if the turn ended on the roll, rolled 8
                    if (endTurn == true)
                    {
                        if (!CheckWinner(player))
                        {
                            UpdateDisplayScores(player);
                            playerTurn = NextPlayer(playerTurn, player);
                        }
                    }
                }
            };

            //end the turn
            endTurnButton.Click += delegate
            {
                if (activegame == true)
                {
                    gameScore[playerTurn].Text = player[playerTurn].UpdateGameScore().ToString();
                    if (!CheckWinner(player))
                    {
                        playerTurn = NextPlayer(playerTurn, player);
                    }
                }
            };


            //end the game and start a new one
            endGameButton.Click += delegate
            {
                d = StartGame(8, out player);
            };



            //get data from splash fragment
            var startButton = FindViewById<Button>(Resource.Id.StartButton);
            var p1Data = FindViewById<EditText>(Resource.Id.p1Value);
            var p2Data = FindViewById<EditText>(Resource.Id.p2Value);

            if (startButton != null)
            {
                startButton.Click += delegate
                {


                    if (p1Data.Text == null || p1Data.Length() < 1) p1Name = "Player 1";
                    else p1Name = p1Data.Text;

                    if (p2Data.Text == null || p2Data.Length() < 1) p2Name = "Player 2";
                    else p2Name = p2Data.Text;

                    var p1Label = FindViewById<TextView>(Resource.Id.playerLabel1);
                    var p2Label = FindViewById<TextView>(Resource.Id.playerLabel2);


                    p1Label.Text = p1Name;
                    p2Label.Text = p2Name;



                };
            }


        }

        #region methods to reset game and update widgets

        /// <summary>
        /// Starts a new game
        /// </summary>
        /// <param name="diceSides">Number of sides on the Dice</param>
        /// <param name="player">Player Array to be Reset</param>
        /// <returns>Game Die</returns>
        public Die StartGame(int diceSides, out List<Player> player)
        {
            var turnLabel = FindViewById<TextView>(Resource.Id.turnLabel);
            turnLabel.Text = "Player 1\'s Turn";

            player = new List<Player>();
            player.Add(new Player());
            player.Add(new Player());
            Die d = new Die(diceSides);
            activegame = true;
            UpdateDisplayScores(player);
            return d;
        }


        public int NextPlayer(int current, List<Player> player)
        {
            var turnLabel = FindViewById<TextView>(Resource.Id.turnLabel);

            playerTurn++; if (playerTurn > player.Count - 1) playerTurn = 0;
            turnLabel.Text = "Player " + playerTurn + 1 + "\'s Turn";
            int turn = playerTurn + 1;
            turnLabel.Text = "Player " + turn + "\'s Turn";
            UpdateTurnScore(0);
            return playerTurn;
        }

        public bool CheckWinner(List<Player> player)
        {
            if (playerTurn == player.Count - 1 && player.Any(z => z.GameScore >= MAXSCORE)) {
                AnnounceWinner(player);
                return true;
            }
            else return false;
        }

        public void AnnounceWinner(List<Player> player)
        {
            string winText = "Player 1 Won!";
            int topScore = 0;
           int winner;
            for (int i = 0; i < player.Count - 1; i++)
            {
                if (player[i].GameScore > topScore)
                {
                    topScore = player[i].GameScore;
                    winner = i + 1;
                    winText = "Player " + winner + " Won!";
                }
            }

            //announce winner
            var turnLabel = FindViewById<TextView>(Resource.Id.turnLabel);
            turnLabel.Text = winText;
            playerTurn = 0;
            FindViewById<TextView>(Resource.Id.turnPointsValue).Text = "0";
            UpdateDisplayScores(player);

            activegame = false;
        }

        public void UpdateTurnScore(int score)
        {
            FindViewById<TextView>(Resource.Id.turnPointsValue).Text = score.ToString();
        }



        public void UpdateDisplayScore(Player p, int index)
        {
            gameScore[index].Text = p.GameScore.ToString();
        }

        public void UpdateDisplayScores(List<Player> player)
        {
          for (int i = 0;i<player.Count;i++)
            {
                UpdateDisplayScore(player[i],i);
            }
        }

        public void UpdateDie(int side)
        {
            var dieImage = FindViewById<ImageView>(Resource.Id.diceRoll);
           switch(side)
            {
                case 1:
                    dieImage.SetImageResource(Resource.Drawable.Die8Side1);
                    break;
                case 2:
                    dieImage.SetImageResource(Resource.Drawable.Die8Side2);
                    break;
                case 3:
                    dieImage.SetImageResource(Resource.Drawable.Die8Side3);
                    break;
                case 4:
                    dieImage.SetImageResource(Resource.Drawable.Die8Side4);
                    break;
                case 5:
                    dieImage.SetImageResource(Resource.Drawable.Die8Side5);
                    break;
                case 6:
                    dieImage.SetImageResource(Resource.Drawable.Die8Side6);
                    break;
                case 7:
                    dieImage.SetImageResource(Resource.Drawable.Die8Side7);
                    break;
                case 8:
                    dieImage.SetImageResource(Resource.Drawable.Die8Side8);
                    break;
            }



        }
        #endregion
    }
}

