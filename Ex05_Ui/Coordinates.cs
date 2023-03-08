namespace Ex05_Ui
{
    public struct Coordinates
    {
        private int m_RowCoordinateToPlay;
        private int m_ColumnCoordinateToPlay;

        public int RowCoordinateToPlay
        {
            get
            {
                return m_RowCoordinateToPlay;
            }

            set
            {
                m_RowCoordinateToPlay = value;
            }
        }

        public int ColumnCoordinateToPlay
        {
            get
            {
                return m_ColumnCoordinateToPlay;
            }

            set
            {
                m_ColumnCoordinateToPlay = value;
            }
        }
    }
}
