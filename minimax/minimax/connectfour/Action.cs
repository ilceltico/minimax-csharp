using System;
namespace minimax.connectfour
{
    public class Action
    {
        private readonly int row;
        private readonly int col;

        public Action(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int Row
        {
            get { return row; }
        }

        public int Col
        {
            get { return col; }
        }

        public override string ToString()
        {
            return $"Row={row}, Col={col}";
        }
    }
}
