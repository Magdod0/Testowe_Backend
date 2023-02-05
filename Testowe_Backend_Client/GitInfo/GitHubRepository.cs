namespace Testowe_Backend_Client.GitInfo
{
    public class GitHubRepository
    {
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public int stargazers_count { get; set; }
        public override string ToString()
        {
            return $"Project name: {name}, Stars: {stargazers_count}, Date of creation: {created_at}";
        }
    }

}
