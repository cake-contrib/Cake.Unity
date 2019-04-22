namespace Cake.Unity.Arguments
{
    public class AssetServerUpdate
    {
        public string IP { get; }
        public int? Port { get; }
        public string ProjectName { get; }
        public string Username { get; }
        public string Password { get; }
        public string Revision { get; }

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
