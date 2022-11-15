using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdManager : MonoBehaviour
{
    private static AdManager _instant;
    public static AdManager instant => _instant;

    #region BANNER
    string bannerID = "ca-app-pub-4840630438414270/7732235576";
    private BannerView bannerView;
    [SerializeField] bool isBannerShow = false;
    float reloadBannerTime = 1;
    #endregion
    
    #region INTER
    private string interID = "ca-app-pub-4840630438414270/8701416904";
    private InterstitialAd interstitialAd;
    private float interTime = 1;
    private float timerDelay = 0;
    #endregion

    private void Awake() {
        _instant = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => {
            RequestBanner();
            RequestInter();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isBannerShow){
                this.bannerView.Hide();
                isBannerShow = false;
            }else{
                this.bannerView.Show();
                isBannerShow = true;
            }
        }
        if(timerDelay > 0)
            timerDelay -= Time.deltaTime;
    }

    #region BANNER HANDLE
    void RequestBanner(){
        this.bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.OnAdLoaded += bannerView_OnAdLoaded;
        this.bannerView.LoadAd(request);
        this.bannerView.OnAdFailedToLoad += bannerView_OnAdFailedToLoad;
    }
    private void bannerView_OnAdLoaded(object sender, System.EventArgs e){
        // this.bannerView.Show();
        isBannerShow = true;
        reloadBannerTime = 1;
    }
    private void bannerView_OnAdFailedToLoad(object sender, System.EventArgs e){
        Invoke("RequestBanner", reloadBannerTime);
        reloadBannerTime *= 2;
    }
    #endregion

    #region INTER HANDLE
    void RequestInter(){
        this.interstitialAd = new InterstitialAd(this.interID);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitialAd.OnAdLoaded += interstitialAd_OnAdLoaded;
        this.interstitialAd.OnAdFailedToLoad += interstitialAd_OnAdFailedToLoad;
        this.interstitialAd.OnAdFailedToShow += interstitialAd_OnAdFailedToShow;
        this.interstitialAd.OnAdClosed += interstitialAd_OnAdClosed;
        this.interstitialAd.LoadAd(request);
    }
    public void ShowInter(){
        if(timerDelay > 0)
            return;
        if(this.interstitialAd.IsLoaded()){
            this.interstitialAd.Show();
        }
    }
    private void interstitialAd_OnAdLoaded (object sender, System.EventArgs e){
        Debug.Log("Inter Loaded");
    }
    private void interstitialAd_OnAdFailedToLoad (object sender, System.EventArgs e){
        Invoke("RequestInter", interTime);
        interTime *= 2;
    }
    private void interstitialAd_OnAdFailedToShow (object sender, System.EventArgs e){
        Invoke("RequestInter", interTime);
        interTime *= 2;
    }
    private void interstitialAd_OnAdClosed (object sender, System.EventArgs e){
        RequestInter();
        timerDelay = 60;
    }
    #endregion

}
