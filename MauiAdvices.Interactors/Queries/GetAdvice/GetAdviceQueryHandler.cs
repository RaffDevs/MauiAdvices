using MauiAdvices.Infrastructure.Models;
using MauiAdvices.Infrastructure.Services;

namespace MauiAdvices.Interactors.Queries.GetAdvice;

public class GetAdviceQueryHandler
{
    private readonly AdviceService _adviceService;

    public GetAdviceQueryHandler(AdviceService adviceService)
    {
        _adviceService = adviceService;
    }

    public async Task<AdviceResponseDTO> Execute(GetAdviceQuery? query)
    {
        try
        {
            var advice = await _adviceService.GetRandomAdvice();
            return advice;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get advice: {ex.Message}");
        }
    }
}