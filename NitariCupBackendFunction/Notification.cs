using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NitariCupBackendFunction.Library;

namespace NitariCupBackendFunction
{
    public class Notification
    {
        private readonly ILogger _logger;

        public Notification(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Notification>();
        }

        [Function("Notification")]
        public async Task SendNotification([TimerTrigger("0 */20 * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            var data = await TaskGetter.GetTask(Environment.GetEnvironmentVariable("NITARI_CUP_BACKEND_URI") + "/api/TaskScheme/Notification");

            _logger.LogInformation($"Tasks to notify: {data}");

            if (data.IsEmpty)
            {
                _logger.LogInformation("No task to notify.");
                return;
            }

            var users = data
                .OrderBy(x => x.userId)
                .GroupBy(x => x.userId);
            _logger.LogInformation($"Users to notify: {users.Count()}");

            var channelAccessToken = Environment.GetEnvironmentVariable("LINE_CHANNEL_ACCESS_TOKEN");

            foreach (var user in users)
            {
                await Replayer.Replay(channelAccessToken, user.ToArray());
            }
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
