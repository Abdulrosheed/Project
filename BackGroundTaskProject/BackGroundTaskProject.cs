using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NCrontab;
using Project.BackGroundTaskConfiguration;
using Project.Repository.Interface;

namespace Project.BackGroundTaskProject
{
    public class BackGroundTaskProject : BackgroundService
    {
        private readonly BackGroundConfig _config;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly ILogger<BackGroundTaskProject> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackGroundTaskProject(IOptions<BackGroundConfig> config, ILogger<BackGroundTaskProject> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _config = config.Value;
            _schedule = CrontabSchedule.Parse(_config.CronExpression);
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var requests = scope.ServiceProvider.GetRequiredService<ICarRepository>();
                    

                    var pendingRequests =  requests.ListAllNonAvailableCars();
                    var pendingRequests2 = requests.ListAllAvailableCars();
                    if (pendingRequests.Count != 0)
                    {
                        foreach (var item in pendingRequests)
                        {

                            if(item.DateToBeReturned.Day >= DateTime.Now.Day)
                            {
                                item.IsAvailable = true;
                                requests.UpdateCar(item);
                            }
                            
                           

                        }
                    }
                    if (pendingRequests2.Count != 0)
                    {
                        foreach (var item in pendingRequests2)
                        {

                            if(item.BorrowedDate != null && item.BorrowedDate.Day == DateTime.Now.Day)
                            {
                                item.IsAvailable = false;
                                requests.UpdateCar(item);
                            }
                            
                           

                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured reading Reminder Table in database.{ex.Message}");
                    _logger.LogError(ex, ex.Message);
                }
                _logger.LogInformation($"Background Hosted Service for {nameof(BackGroundTaskProject)} is stopping");
                var timeSpan = _nextRun - now;
                await Task.Delay(timeSpan, stoppingToken);
                _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
            }
        }

    }
}