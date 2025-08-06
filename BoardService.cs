// Board için yapılacak işlemleri yürüten sınıftır.
public class BoardService
{
    private Board _board;

    public BoardService(Board board)
    {
        _board = board;
    }

    //Kart oluşturulurken hem kart nesnesi hemde ilgili kolon ile oluşacağından metot'a parametre olarak istenir.
    public void AddCard(Card card, BoardLineType lineType)
    {
        BoardLine line = null!;
        foreach (var l in _board.Lines)
        {
            if (l.LineType == lineType)
            {
                line = l;
                break;
            }
        }
        card.Line = lineType;
        line.Cards.Add(card);
    }
    // Silme işleminde sadece kartın başlığına ihtiyaç var. Bu yüzden tek parametre verilir.
    public bool RemoveCard(string title)
    {
        //UI tarafında silme işlemi kontrolünün yapılıp yapılmadığıbı tutmak için Yerel Değişken tanımladım.
        bool isAnyRemoved = false;
        // Her kart ayrı bir Line (TODO-INPROGRESS-DONE) içinde olduğundan her birinin içini kontrol etmeliyiz.
        foreach (var line in _board.Lines)
        {
            // aynı kart birden fazla ise bunları listeleyip silmek için
            var cardsToRemove = new List<Card>();

            foreach (var card in line.Cards)
            {
                if (card.Title == title)
                {
                    cardsToRemove.Add(card);
                }
            }

            foreach (var card in cardsToRemove)
            {
                line.Cards.Remove(card);
                isAnyRemoved = true;
            }
        }
        //kart var ise true, yoksa false döner
        return isAnyRemoved;
    }
    // ilgili başlığa sahip kartı  nereye atayacağını parametlerle veriyoruz
    public bool MoveCard(string title, BoardLineType to)
    {
        BoardLine sourceLine = null;
        Card cardToMove = null;

        // Kartı bul
        foreach (var line in _board.Lines)
        {
            foreach (var card in line.Cards)
            {
                if (card.Title == title)
                {
                    sourceLine = line;
                    cardToMove = card;
                    break;
                }
            }
            if (cardToMove != null) break;
        }

        if (cardToMove == null)
        {
            // Kart bulunamadı
            return false;
        }

        // Kartı taşı
        sourceLine.Cards.Remove(cardToMove);
        cardToMove.Line = to;

        // Yeni Line bul
        BoardLine targetLine = null;
        foreach (var line in _board.Lines)
        {
            if (line.LineType == to)
            {
                targetLine = line;
                break;
            }
        }
        targetLine.Cards.Add(cardToMove);

        return true;
    }


    public List<BoardLine> GetBoardLines()
    {
        return _board.Lines;
    }
}
