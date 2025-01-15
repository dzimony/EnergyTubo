using Azure;
using EnergyTubo.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EnergyTubo.Interface
{
    public class BankService : IBankService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;
        public BankService(HttpClient HttpClient, IConfiguration Config)
        {
            httpClient = HttpClient;
            config = Config;
        }
        public async Task<Bank> GetBanks()
        {

            httpClient.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");

            

            var rs = config.GetSection("Key").Value;
            httpClient.DefaultRequestHeaders.Add(config.GetSection("KeyName").Value, config.GetSection("Key").Value);
            var url = "https://apiplayground.alat.ng/debit-wallet/api/Shared/GetAllBanks";
            
            return await httpClient.GetFromJsonAsync<Bank>(url);

        }

        
    }
}

