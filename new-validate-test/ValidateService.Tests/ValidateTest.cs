namespace ValidateService.Tests;

public class ValidateTest
{
    [Fact]
    
    
   public void IsEmail_Correct_returnTrue()
   {

      var primeService = new ValidateService();
      bool result = primeService.ValidateEmail("jay@gmail.com");

      Assert.False(result);
   }
    
}