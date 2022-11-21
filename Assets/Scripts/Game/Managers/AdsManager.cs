using Zenject;

public class AdsManager: IInitializable
{
    public const string RELIVE_REWRD = "ReliveReward";
    public const string COINS_REWARD = "CoinsReward";

    private const string APP_ANDROID_KEY = "171d3899d";

    public void Initialize()
    {
        IronSource.Agent.shouldTrackNetworkState(true);
        IronSource.Agent.setConsent(false);
        IronSource.Agent.init(APP_ANDROID_KEY, IronSourceAdUnits.REWARDED_VIDEO);
        
    }
    
}