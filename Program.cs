using System;

class Program
{
    static void Main(string[] args)
    {
        //İlgili sınıflardan nesneler oluşturulur ve default 3 card eklenir
        var teamMemberService = new TeamMemberService();
        var board = new Board();
        var boardService = new BoardService(board);
        var card1 = new Card
        {
            Title = "Görev 1",
            Content = "İlk görev açıklaması",
            AssignedMemberId = 1,
            Size = CardSize.M,
            Line = BoardLineType.TODO
        };

        var card2 = new Card
        {
            Title = "Görev 2",
            Content = "İkinci görev açıklaması",
            AssignedMemberId = 2,
            Size = CardSize.L,
            Line = BoardLineType.INPROGRESS
        };

        var card3 = new Card
        {
            Title = "Görev 3",
            Content = "Üçüncü görev açıklaması",
            AssignedMemberId = 3,
            Size = CardSize.S,
            Line = BoardLineType.DONE
        };

        boardService.AddCard(card1, card1.Line);
        boardService.AddCard(card2, card2.Line);
        boardService.AddCard(card3, card3.Line);
        var consoleUI = new ConsoleUI(boardService, teamMemberService);

        consoleUI.Run();
    }
}
