namespace EnergyTubo.Models
{
    public class Bank
    {
      public List<Result> Result { get; }
      public string ErrorMessage { get; }
      public string[] ErrorMessages { get;}

     public bool HasError { get; } = false;
     public string TimeGenerated { get; }
    }
}



