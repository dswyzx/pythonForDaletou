using Pomelo.AspNetCore.TimedJob;

namespace DotNetCoreTest.Jobs
{
    public class AutoGetMovieListJob : Job
    {

        [Invoke(Begin = "2017-08-24 00:30", Interval = 1000 * 3600 * 3, SkipWhileExecuting = true)]
        public void Run()
        {
            //LogHelper.Info("Start crawling");
            // LatestMovieInfo.CrawlLatestMovieInfo(2);
            // HotMovieInfo.CrawlHotMovie();
            //Btdytt520HotClickHelper.CrawlHotClickMovieInfo();
            // LogHelper.Info("Finish crawling");
        }


    }
}
