using System.Net;
using FakePaymentService.Application.DTOs;
using FakePaymentService.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared.ResponceDto;
using UnitTest.FakePaymentServiceTest.TestSetup;
using UnitTest.Helpers;

namespace UnitTest.FakePaymentServiceTest.ControllerTest;

public class CreditCardsControllerTest : BaseControllerTest
{
    private readonly TestApiServer<FakePaymentService.Api.Program, FakePaymentDbContex> _factory;

    public CreditCardsControllerTest(TestApiServer<FakePaymentService.Api.Program, FakePaymentDbContex> factory) :
        base(factory)
    {
        _factory = factory;
        var scope = _factory.Services.CreateScope();
        Database.FakePaymentDb = scope.ServiceProvider.GetRequiredService<FakePaymentTestContext>();
        Database.FakePaymentDb.AddDbContext(_factory.Services);
    }

    [Fact]
    public async Task should_success_get_credit_card()
    {
        // act
        var response = await _client.GetAsync($"api/CreditCards/5555112548076309");

        var creditCard =
            JsonConvert.DeserializeObject<CustomResponseDto<CreditCardResponce>>(
                await response.Content.ReadAsStringAsync());

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(creditCard.Data);
    }
}