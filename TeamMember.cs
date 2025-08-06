public class TeamMember
{
    private int _id;            // Takımdaki kişinin id'si. Unique number gibi
    private string? _fullName;   // Takımdaki kişinin ad ve soyadı. Aynı olsa bile id ile farklı kişiler olduğu belli
    // Encapsulation uygulayarak field'lar ile ilgili işlemler özelleştirilebilir.
    public int Id { get => _id; set => _id = value; }
    public string FullName { get => _fullName!; set => _fullName = value; }
}