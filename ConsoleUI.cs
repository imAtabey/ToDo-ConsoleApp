public class ConsoleUI
{
    private BoardService _boardService;
    private TeamMemberService _teamMemberService;

    public ConsoleUI(BoardService boardService, TeamMemberService teamMemberService)
    {
        _boardService = boardService;
        _teamMemberService = teamMemberService;
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListBoard();
                    break;
                case "2":
                    AddCard();
                    break;
                case "3":
                    RemoveCard();
                    break;
                case "4":
                    MoveCard();
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                    break;
            }
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz");
        Console.WriteLine("*****************************************");
        Console.WriteLine("(1) Board Listelemek");
        Console.WriteLine("(2) Board'a Kart Eklemek");
        Console.WriteLine("(3) Board'dan Kart Silmek");
        Console.WriteLine("(4) Kart Taşımak");
    }

    private void ListBoard()
    {
        foreach (BoardLine line in _boardService.GetBoardLines())
        {
            Console.WriteLine(line.LineType + " Line");
            Console.WriteLine("************************");

            if (line.Cards.Count == 0)
            {
                Console.WriteLine("~ BOŞ ~");
            }
            else
            {
                foreach (Card card in line.Cards)
                {
                    TeamMember member = _teamMemberService.GetMemberById(card.AssignedMemberId);
                    string memberName = member == null ? "Bilinmiyor" : member.FullName;

                    Console.WriteLine("Başlık      : " + card.Title);
                    Console.WriteLine("İçerik      : " + card.Content);
                    Console.WriteLine("Atanan Kişi : " + memberName);
                    Console.WriteLine("Büyüklük    : " + card.Size);
                    Console.WriteLine("-");
                }
            }
            Console.WriteLine();
        }
    }
    // Kullanıcının kart ekleme metodu için inputları ilgili metot'a gönderme
    private void AddCard()
    {
        Console.Write("Başlık Giriniz: ");
        string title = Console.ReadLine();

        Console.Write("İçerik Giriniz: ");
        string content = Console.ReadLine();

        Console.WriteLine("Büyüklük Seçiniz -> XS(1), S(2), M(3), L(4), XL(5): ");
        int sizeInput = int.Parse(Console.ReadLine());
        CardSize size = (CardSize)sizeInput;

        Console.WriteLine("Kişi Seçiniz: ");
        int memberId = int.Parse(Console.ReadLine());

        // İlgili kişi var mı kontrolü
        if (!_teamMemberService.IsMemberExists(memberId))
        {
            Console.WriteLine("Hatalı girişler yaptınız!");
            return;
        }

        Console.WriteLine("Line Seçiniz -> TODO(1), IN_PROGRESS(2), DONE(3): ");
        int lineInput = int.Parse(Console.ReadLine());
        BoardLineType lineType = (BoardLineType)lineInput;

        Card card = new Card();
        card.Title = title;
        card.Content = content;
        card.AssignedMemberId = memberId;
        card.Size = size;
        card.Line = lineType;

        _boardService.AddCard(card, lineType);

        Console.WriteLine("Kart eklendi.");
    }

    // Kullanıcı tarafından silinecek kartı için parametlerin input alınıp ilgili metot'a iletilmesi
    private void RemoveCard()
    {
        Console.Write("Silmek istediğiniz kartın başlığını yazınız: ");
        string title = Console.ReadLine();

        // Kartın silinip silinmediğini kontrol ettiğimiz bool değişken
        bool isRemoved = _boardService.RemoveCard(title);

        if (isRemoved)
        {
            Console.WriteLine("Kart başarıyla silindi.");
        }
        else
        {
            Console.WriteLine("Aradığınız kriterlere uygun kart board'da bulunamadı.");
            Console.WriteLine("* Silmeyi sonlandırmak için : (1)");
            Console.WriteLine("* Yeniden denemek için : (2)");

            string secim = Console.ReadLine();
            switch (secim)
            {
                case "1":
                    return;
                case "2":
                    RemoveCard(); // İşlemi tekrarlamak için
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        }
    }

    private void MoveCard()
    {
        Console.Write("Taşımak istediğiniz kartın başlığını yazınız: ");
        string title = Console.ReadLine();

        // Kartın olup olmadığı kontrol edilir
        Card foundCard = null;
        BoardLine sourceLine = null;

        foreach (var line in _boardService.GetBoardLines())
        {
            foreach (var card in line.Cards)
            {
                if (card.Title == title)
                {
                    foundCard = card;
                    sourceLine = line;
                    break;
                }
            }
            if (foundCard != null)
                break;
        }
        // Kart bulunmazsa
        if (foundCard == null)
        {
            Console.WriteLine("Hatalı başlık girdiniz. Kart bulunamadı, işlem yapılmadı.");
            return;
        }

        // Kart bulunduysa taşınacak line'ı sor:
        Console.WriteLine("Bulunan Kart Bilgileri:");
        Console.WriteLine($"Başlık      : {foundCard.Title}");
        Console.WriteLine($"İçerik      : {foundCard.Content}");
        Console.WriteLine($"Atanan Kişi : {_teamMemberService.GetMemberById(foundCard.AssignedMemberId)?.FullName ?? "Bilinmiyor"}");
        Console.WriteLine($"Büyüklük    : {foundCard.Size}");
        Console.WriteLine($"Line        : {foundCard.Line}");
        Console.WriteLine();

        Console.WriteLine("Lütfen taşımak istediğiniz Line'ı seçiniz -> TODO(1), IN_PROGRESS(2), DONE(3): ");
        int toInput = int.Parse(Console.ReadLine());

        // Yanlış bir line seçimi olmaması için sınırlar belirtiliyor
        if (toInput < 1 || toInput > 3)
        {
            Console.WriteLine("Hatalı bir seçim yaptınız! İşlem sonlandırıldı.");
            return;
        }

        BoardLineType to = (BoardLineType)toInput;

        // Taşıma işlemini yap:
        _boardService.MoveCard(title, to);

        Console.WriteLine("Kart başarıyla taşındı.");
    }
}
