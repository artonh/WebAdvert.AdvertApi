using AdvertApi.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
//using Microsoft.Extensions.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

//https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1#prerequisites

namespace AdvertApi.HealthChecks
{
    /// <summary>
    /// To check the health of the service
    /// </summary>
    public class StorageHealthCheck : IHealthCheck
    {
        private readonly IAdvertStorageService _storageService;
        public StorageHealthCheck(IAdvertStorageService storageService)
        {
            _storageService = storageService;
        }

        //public async ValueTask<IHealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        //{
        //    var isStorageOK = await _storageService.CheckHealthAsync();

        //    return HealthCheckResult.FromStatus(isStorageOK ? CheckStatus.Healthy : CheckStatus.Unhealthy, "");
        //}

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthCheckResultHealthy = await _storageService.CheckHealthAsync();

            if (healthCheckResultHealthy)
            {
                return await Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result."));
            }

            return await Task.FromResult(
                HealthCheckResult.Unhealthy("An unhealthy result."));
        }
    }
}
