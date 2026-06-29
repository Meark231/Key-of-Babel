using UnityEngine;

public class ProcessSystem : Singleton<ProcessSystem>
{
    public bool SuSheP = false;
    public bool WeiShengP = false;
    public bool BanGongP = false;
    public bool DianLiP = false;
    public bool YanJiuP = false;
    public bool XinBiaoP = false;

    public bool ifGetLowCard = false;
    public bool ifGetMidCard = false;
    public bool ifGetHighCard = false;

    public void Start()
    {
        EventCenter.Instance.AddEventListener("KaiSuSheDian", OpenSuShePower);
        EventCenter.Instance.AddEventListener("GuanSuSheDian", CloseSuShePower);

        EventCenter.Instance.AddEventListener("KaiWeiShengDian", OpenWeiShengPower);
        EventCenter.Instance.AddEventListener("GuanWeiShengDian", CloseWeiShengPower);

        EventCenter.Instance.AddEventListener("KaiBanGongDian", OpenBanGongPower);
        EventCenter.Instance.AddEventListener("GuanBanGongDian", CloseBanGongPower);

        EventCenter.Instance.AddEventListener("KaiDianLiDian", OpenDianLiPower);
        EventCenter.Instance.AddEventListener("GuanDianLiDian", CloseDianLiPower);

        EventCenter.Instance.AddEventListener("KaiYanJiuDian", OpenYanJiuPower);
        EventCenter.Instance.AddEventListener("GuanYanJiuDian", CloseYanJiuPower);

        EventCenter.Instance.AddEventListener("KaiXinBiaoDian", OpenXinBiaoPower);
        EventCenter.Instance.AddEventListener("GuanXinBiaoDian", CloseXinBiaoPower);
    }



    public void OpenSuShePower()
    {
        SuSheP = true;
    }

    public void CloseSuShePower()
    {
        SuSheP = false;
    }

    public void OpenWeiShengPower()
    {
        WeiShengP = true;
    }

    public void CloseWeiShengPower()
    {
        WeiShengP = false;
    }

    public void OpenBanGongPower()
    {
        BanGongP = true;
    }

    public void CloseBanGongPower()
    {
        BanGongP = false;
    }

    public void OpenDianLiPower()
    {
        DianLiP = true;
    }

    public void CloseDianLiPower()
    {
        DianLiP = false;
    }

    public void OpenYanJiuPower()
    {
        YanJiuP = true;
    }

    public void CloseYanJiuPower()
    {
        YanJiuP = false;
    }

    public void OpenXinBiaoPower()
    {
        XinBiaoP = true;
    }

    public void CloseXinBiaoPower()
    {
        XinBiaoP = false;
    }
}