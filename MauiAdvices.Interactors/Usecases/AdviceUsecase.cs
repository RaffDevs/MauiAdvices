using MauiAdvices.Core.Entities;
using MauiAdvices.Core.Repositories;
using MauiAdvices.Infrastructure.Models;
using MauiAdvices.Infrastructure.Services;
using MauiAdvices.Interactors.Models;

namespace MauiAdvices.Interactors.Usecases;

public class AdviceUsecase
{
    private readonly AdviceService _adviceService;
    private readonly IAdviceRepository _adviceRepository;

    public AdviceUsecase(AdviceService adviceService, IAdviceRepository adviceRepository)
    {
        _adviceService = adviceService;
        _adviceRepository = adviceRepository;
    }

    public async Task<RandomAdviceDTO> GetRandomAdvice()
    {
        try
        {
            var advice = await _adviceService.GetRandomAdvice();
            return new RandomAdviceDTO
            {
                Code = advice.Data.SlipId,
                Text = advice.Data.Advice
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get advice: {ex.Message}");
        }
    }

    public async Task SaveAdvice(RandomAdviceDTO data)
    {
        try
        {
            var advice = new Advice
            {
                Text = data.Text,
                Code = data.Code.ToString(),
                CreatedAt = DateTime.Now
            };

            await _adviceRepository.Create(advice);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<List<AdviceDTO>> GetAllAdvices()
    {
        try
        {
            var advices = await _adviceRepository.Get();
            return advices.Select(adv => new AdviceDTO
            {
                Id = adv.Id,
                Text = adv.Text,
                Author = adv.Author,
                TranslatedText = adv.TranslatedText
            }).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteAdvice(int id)
    {
        try 
        {
            await _adviceRepository.Delete(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }
    }
}