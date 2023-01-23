namespace HotelReservationService.Application.Helper;

public static class GenerateConfirmationNumber
{
    public static string Generate()
    {
        Random rdm = new Random();
    
        string[] characters = { "A", "B", "C", "D","E","F","G","H" };
    
        int firstChar, secondChar, thirdChar,firstNumber,secondNumber,thirdNumber;

        firstChar = rdm.Next(0, characters.Length);
        secondChar = rdm.Next(0, characters.Length);
        thirdChar = rdm.Next(0, characters.Length);
        firstNumber = rdm.Next(100, 999);
        secondNumber =rdm.Next(10, 99);
        thirdNumber =rdm.Next(10, 99);

        return $"{firstChar}{firstNumber}{secondChar}{secondNumber}{thirdChar}{thirdNumber}";
    }
}