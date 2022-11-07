using System;
using Signals;
using Zenject;

public class AdsManager: IInitializable,IDisposable
{
    private const string APP_KEY = "171d3899d";
    private const string RELIVE_PLACEMENT_NAME = "ResumeRun";
    
    private readonly SignalBus _signalBus;

    private AdsManager(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    public void Initialize()
    {
        IronSource.Agent.shouldTrackNetworkState(true);
        IronSource.Agent.init(APP_KEY, IronSourceAdUnits.REWARDED_VIDEO);
        Subscribe();
    }

    public void Dispose()
    {
       UnSubscribe();
    }

    private void Subscribe()
    {
        IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardVideoEnd;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += OnRewardVideoFailed;
        _signalBus.Subscribe<RewardShowSignal>(ShowRewardVideo);
    }
   
    private void UnSubscribe()
    {
        IronSourceRewardedVideoEvents.onAdRewardedEvent -= OnRewardVideoEnd;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent -= OnRewardVideoFailed;
        _signalBus.Unsubscribe<RewardShowSignal>(ShowRewardVideo);
    }
    

    private void OnRewardVideoEnd(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        if (placement.getPlacementName()==RELIVE_PLACEMENT_NAME)
        {
            _signalBus.Fire<ReliveSignal>();
        }
    }

    private void OnRewardVideoFailed(IronSourceError error, IronSourceAdInfo adInfo)
    {
        
    }
    
    private void ShowRewardVideo(RewardShowSignal signal)
    {
        IronSource.Agent.showRewardedVideo(signal.PlacementName);
        
#if UNITY_EDITOR
        
        if (signal.PlacementName == RELIVE_PLACEMENT_NAME)
        {
            _signalBus.Fire<ReliveSignal>();
        }
        
#endif
        
    }
}