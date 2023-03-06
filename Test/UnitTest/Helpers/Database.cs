using UnitTest.FakePaymentServiceTest.TestSetup;
using UnitTest.IdentityServerTest.TestContext;

namespace UnitTest.Helpers;

public static class Database
{
    public static IdentityTestContext IdentityDb { get; set; }
    public static FakePaymentTestContext FakePaymentDb { get; set; }
    
}