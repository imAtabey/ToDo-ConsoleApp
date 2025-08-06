public class Board
{
    private List<BoardLine> _lines;

    public List<BoardLine> Lines => _lines;

    public Board()
    {
        _lines = new List<BoardLine>
        {
            new BoardLine(BoardLineType.TODO),
            new BoardLine(BoardLineType.INPROGRESS),
            new BoardLine(BoardLineType.DONE)
        };
    }
}