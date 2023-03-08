using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex05_Ui
{
    public class GameManager
    {
        private const int k_AmountOfPlayers = 2;
        private readonly List<Player> r_WinnersList;
        private readonly FormLogin r_LoginForm;
        private readonly Player[] r_PlayersArray;
        private Board m_Board;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;

        public GameManager()
        {
            r_WinnersList = new List<Player>();
            r_PlayersArray = new Player[k_AmountOfPlayers];
            r_LoginForm = new FormLogin();
        }
        public Board BoardOthello
        {
            get
            {
                return m_Board;
            }
        }

        public Player[] PlayersArray
        {
            get
            {
                return r_PlayersArray;
            }
        }

        public Player CreateFirstPlayer()
        {
            m_FirstPlayer = new Player();
            m_FirstPlayer.InitPlayer("Red", eMark.X);
            r_PlayersArray[0] = m_FirstPlayer;

            return m_FirstPlayer;
        }

        public Player CreateSecondPlayer(bool i_IsComputer)
        {
            m_SecondPlayer = new Player();

            if (i_IsComputer.Equals(true))
            {
                m_SecondPlayer.InitComputerPlayer();
            }
            else
            {
                m_SecondPlayer.InitPlayer("Yellow", eMark.O);
            }

            r_PlayersArray[1] = m_SecondPlayer;

            return m_SecondPlayer;
        }

        public void RunGame()
        {
            r_LoginForm.ShowDialog();

            if (r_LoginForm.IsGameStart)
            {
                m_Board = new Board(r_LoginForm.UserBoardSize);

                FormGame gameBoard = new FormGame(r_LoginForm.UserBoardSize, r_LoginForm.IsComputer, this);
                gameBoard.ShowDialog();
            }
        }

        public void SwitchTurns(ref int io_CurrentIndex)
        {
            io_CurrentIndex = io_CurrentIndex == 1 ? 0 : 1;
        }

        public void playTurn(Board i_CurrentBoard, ref int io_CurrentIndex, int i_RowInput, int i_ColInput)
        {
            i_CurrentBoard.PutMarkOnBoard(r_PlayersArray[io_CurrentIndex].PlayerMark, i_RowInput, i_ColInput);
            i_CurrentBoard.ChangeCurrentBoard(r_PlayersArray[io_CurrentIndex], i_RowInput, i_ColInput);
            i_CurrentBoard.UpdatePlayersScore(ref m_FirstPlayer, ref m_SecondPlayer);

            SwitchTurns(ref io_CurrentIndex);
        }

        public string WinnersStatus()
        {
            StringBuilder winnersAnnouncement = new StringBuilder();

            if (r_PlayersArray[0].PlayerScore == r_PlayersArray[1].PlayerScore)
            {
                winnersAnnouncement.Append("It's a Tie!");
            }
            else
            {
                Player winnerPlayer = r_PlayersArray[0].PlayerScore > r_PlayersArray[1].PlayerScore ? r_PlayersArray[0] : r_PlayersArray[1];
                Player loserPlayer = winnerPlayer.PlayerName == "Red" ? r_PlayersArray[1] : r_PlayersArray[0];

                r_WinnersList.Add(winnerPlayer);
                int countWinningGames = countWinnigGames(winnerPlayer.PlayerName);

                winnersAnnouncement.Append(winnerPlayer.PlayerName).Append(" ").Append("Won!! (").Append(winnerPlayer.PlayerScore).Append("/")
                    .Append(loserPlayer.PlayerScore).Append(") (").Append(countWinningGames).Append("/").Append(r_WinnersList.Distinct().Count()).Append(")");
            }

            winnersAnnouncement.Append(Environment.NewLine).Append("Would you like another round?");

            return winnersAnnouncement.ToString();
        }

        private int countWinnigGames(string i_WinnerPlayerName)
        {
            int countWinningGames = 0;

            foreach (Player currentWinner in r_WinnersList)
            {
                if (currentWinner.PlayerName.Equals(i_WinnerPlayerName))
                {
                    countWinningGames++;
                }
            }

            return countWinningGames;
        }
    }
}
