using System;
using System.Collections.Generic;

namespace Ex05_Ui
{
    public class Player
    {
        private static readonly Random sr_RandomNumber = new Random();
        private eMark m_PlayerMark;
        private string m_PlayerName;
        private int m_PlayerScore;

        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }

        public eMark PlayerMark
        {
            get
            {
                return m_PlayerMark;
            }

            set
            {
                m_PlayerMark = value;
            }
        }

        public int PlayerScore
        {
            get
            {
                return m_PlayerScore;
            }

            set
            {
                m_PlayerScore = value;
            }
        }

        public void InitPlayer(string i_PlayerName, eMark i_PlayerEMark)
        {
            m_PlayerScore = 0;
            m_PlayerName = i_PlayerName;
            m_PlayerMark = i_PlayerEMark;
        }

        public void InitComputerPlayer()
        {
            m_PlayerName = "Computer";
            m_PlayerScore = 0;
            m_PlayerMark = eMark.O;
        }

        public eMark GetOppositeMark()
        {
            eMark resultOfOpposite = eMark.X;

            if (PlayerMark == eMark.X)
            {
                resultOfOpposite = eMark.O;
            }

            return resultOfOpposite;
        }

        public Coordinates PlayComputerTurn(List<Coordinates> i_LegalStepForComputer)
        {
            int randomNumber = sr_RandomNumber.Next(i_LegalStepForComputer.Count);
            return i_LegalStepForComputer[randomNumber];
        }
    }
}
