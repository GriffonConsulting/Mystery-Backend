using Microsoft.Extensions.Diagnostics.HealthChecks;
using Stripe;

namespace Payment
{
    public class StripeHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var service = new CustomerService();
                var customers = await service.ListAsync(new CustomerListOptions { Limit = 1 }, cancellationToken: cancellationToken);

                return HealthCheckResult.Healthy("Stripe API is reachable.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Stripe API is unreachable.", ex);
            }
        }
    }
}