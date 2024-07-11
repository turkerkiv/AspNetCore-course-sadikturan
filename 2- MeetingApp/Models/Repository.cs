namespace MeetingApp.Models
{
    public static class Repository
    {
        private static List<UserInfo> _users = new();

        public static List<UserInfo> Users => _users;

        public static void AddUser(UserInfo user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
        }

        public static UserInfo? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }
}