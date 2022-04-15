using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingBasketApi.Objects.Responses;

namespace ShoppingBasketApi.Services.Abstract
{
    public interface IPriceConverterService
    {
        Task<double> GetConversionRate(string fromCurrency);
    }
}
