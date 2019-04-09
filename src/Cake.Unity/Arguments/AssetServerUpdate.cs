namespace Cake.Unity.Arguments
{
    public class AssetServerUpdate
    {
        public string IP { get; set; }
        public int? Port { get; set; }
        public string ProjectName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Revision { get; set; }

        public AssetServerUpdate(string ip, string projectName, string userName, string password)
        {
            IP = ip;
            ProjectName = projectName;
            Username = userName;
            Password = password;
        }

        public AssetServerUpdate(string ip, int port, string projectName, string userName, string password)
            : this(ip, projectName, userName, password)
        {
            Port = port;
        }

        public AssetServerUpdate(string ip, string projectName, string userName, string password, string revision)
            : this(ip, projectName, userName, password)
        {
            Revision = revision;
        }

        public AssetServerUpdate(string ip, int port, string projectName, string userName, string password, string revision)
            : this(ip, projectName, userName, password)
        {
            Port = port;
            Revision = revision;
        }
    }
}
