﻿namespace EnergyTubo.Models
{
    public class Bank
    {
      public List<Result> Result { get; }
      public string ErrorMessage { get; }
      
      public bool HasError { get;} = false;
    }
}
