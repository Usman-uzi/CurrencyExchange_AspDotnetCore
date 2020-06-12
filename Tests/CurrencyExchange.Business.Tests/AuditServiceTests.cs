using CurrencyExchange.Business.Interfaces;
using CurrencyExchange.Business.Services.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyExchange.Business.Tests
{
    public class AuditServiceTests
    {
        private ICurrencyService currencyService;
        private Mock<IAuditService> auditService;

        public AuditServiceTests()
        {
            auditService = new Mock<IAuditService>();
            currencyService = new CurrencyService(auditService.Object);
        }
        [Fact]
        public async Task ConvertTest()
        {
            var result = await currencyService.Convert("U", 1, "GBP", "USD");
            Assert.Equal(1.27m, result);
        }
    }
}
