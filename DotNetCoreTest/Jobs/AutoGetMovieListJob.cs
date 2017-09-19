using Pomelo.AspNetCore.TimedJob;

namespace DotNetCoreTest.Jobs
{
    public class AutoGetDLTListJob : Job
    {

        [Invoke(Begin = "2017-08-24 00:30", Interval = 1000 * 3600 * 3, SkipWhileExecuting = true)]
        public void Run()
        {
            //LogHelper.Info("AutoGetDLTListJob begin!");
            // GetAllInfo.CrawlLatestDLTInfo(2);
        }


    }
}
