public class TeamMemberService
{
    private Dictionary<int, TeamMember> _teamMembers;
    // Constructor ile başlarken sisteme tanımlı takım üyeleri oluşturulur.
    public TeamMemberService()
    {
        _teamMembers = new Dictionary<int, TeamMember>
        {
            {1,new TeamMember {Id=1,FullName="Ahmet YILMAZ"}},
            {2,new TeamMember {Id=2,FullName="Ayşe KAYA"}},
            {3,new TeamMember {Id=3,FullName="Mehmet DEMİR"}}
        };
    }
    // İlgili kişinin varlığını kontrol eder.
    public bool IsMemberExists(int id)
    {
        return _teamMembers.ContainsKey(id);
    }
    // id ye göre ilgili kişiyi getirir.
    public TeamMember GetMemberById(int id)
    {
        if (_teamMembers.ContainsKey(id))
            return _teamMembers[id];
        return null!;
    }
}