using SampleFive.PresentaionLayer;

namespace SampleFive.ServiceLayer.Interfaces
{
    public interface ISettingService
    {
        string GetSiteName();
        string GetSiteName2();
        string GetSiteName3();
        string GetSiteWelcome(string title = "my Kingdom");
        SiteCopyRightConfig GetSiteCopyRightConfig();
    }
}