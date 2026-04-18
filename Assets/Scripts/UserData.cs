[System.Serializable]
public class UserData
{
    public string username;
    public string email;

    public UserData(string username, string email)
    {
        this.username = username;
        this.email = email;
    }
}