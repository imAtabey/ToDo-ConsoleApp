public class Card
{
    private string? _title;          //Başlık
    private string? _content;        //İçerik
    private int _assignedMemberId;  //Atanan Kişi Id
    private CardSize _size;         //Büyüklük - enum olacak.
    private BoardLineType _line;    //İlgili kart hangi kolonda
    // Encapsulation yapılarak field için güvenli veri atamaları sağlanmışştır.
    public string Title { get => _title!; set => _title = value; }
    public string Content { get => _content!; set => _content = value; }
    public int AssignedMemberId { get => _assignedMemberId; set => _assignedMemberId = value; }
    public CardSize Size { get => _size; set => _size = value; }
    public BoardLineType Line { get => _line; set => _line = value; }
}