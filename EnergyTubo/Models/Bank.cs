namespace EnergyTubo.Models
{
    public class Bank
    {
      public List<Result> Result { get; set; }
      public string ErrorMessage { get; set; }
      public string[] ErrorMessages { get; set; }

     public bool HasError { get; set; } = false;
     public string TimeGenerated { get; set; }
    }
}



