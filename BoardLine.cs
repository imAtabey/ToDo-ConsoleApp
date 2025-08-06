public class BoardLine
{
    private BoardLineType _lineType;
    private List<Card> _cards;

    public BoardLineType LineType { get => _lineType; set => _lineType = value; }
    public List<Card> Cards { get => _cards; set => _cards = value; }

    public BoardLine(BoardLineType lineType)
    {
        _lineType = lineType;
        _cards = new List<Card>();
    }
}