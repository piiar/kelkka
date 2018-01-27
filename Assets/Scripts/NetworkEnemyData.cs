using System.Collections;

public class NetworkEnemyData
{
    public string ip;
    public string sessionId;
    public string guid;
    public string name;
    public int points = 0;

    public NetworkEnemyData(string _ip, string _sessionId, string _guid, string _name)
    {
        ip = _ip;
        sessionId = _sessionId;
        guid = _guid;
        name = _name;
    }
}
