using System;
[Serializable]
public class SocialNetwork
{
    private string nameSocialNetwork;
    private string nameAccount;
    public SocialNetwork(string nameNet, string nameAcc)
    {
        nameSocialNetwork = nameNet;
        nameAccount = nameAcc;
    }
    public string GetNameAccount() => nameAccount;
    public string GetNameNetwork() => nameSocialNetwork;
}