using UnitTest.FakePaymentServiceTest.TestSetup;
using UnitTest.HotelReservationServiceTest.TestContext;
using UnitTest.IdentityServerTest.TestContext;

namespace UnitTest.Helpers;

public static class Database
{
    public static IdentityTestContext FakeIdentityDb { get; set; }
    public static FakePaymentTestContext FakePaymentDb { get; set; }
    public static HotelReservationTestContext FakeReservationDb { get; set; }
    
}