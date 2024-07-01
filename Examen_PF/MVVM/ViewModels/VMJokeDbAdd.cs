using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Examen_PF.Data;
using Examen_PF.DTOs;
using Examen_PF.MVVM.Models;
using Examen_PF.Services;
using Examen_PF.Utility;
using Microsoft.EntityFrameworkCore;

namespace Examen_PF.MVVM.ViewModels;

public partial class VMJokeDbAdd: ObservableObject, IQueryAttributable
{
    private readonly AppDbContext _dbContext;
    private APIService _apiService;
    
    [ObservableProperty]
    private JokesDTO jokeDto = new JokesDTO();
    
    private int _idJoke;
    
    [ObservableProperty]
    private string pageTitle;
    
    [ObservableProperty]
    private bool loadingVisible = false;
    
    public VMJokeDbAdd(AppDbContext context)
    {
        _dbContext = context;
        _apiService = new APIService();
    }
    
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = int.Parse(query["id"].ToString());
        _idJoke = id;

        if (_idJoke == 0)
        {
            pageTitle = "Nuevo Chiste";
        }
        else
        {
            pageTitle = "Editar Chiste";
            loadingVisible = true;
            await Task.Run(async () =>
            {
                var found = await _dbContext.Jokes.FirstAsync(e => e.IdJoke == _idJoke);
                JokeDto.IdJoke = found.IdJoke;
                JokeDto.Categories = found.Categories;
                JokeDto.CreatedAt = found.CreatedAt;
                JokeDto.IconUrl = found.IconUrl;
                JokeDto.Id = found.Id;
                JokeDto.UpdatedAt = found.UpdatedAt;
                JokeDto.Url = found.Url;
                JokeDto.Value = found.Value;

                MainThread.BeginInvokeOnMainThread(() => { loadingVisible = false; });
            });
        }
    }

    [RelayCommand]
    public async Task FetchJoke()
    {
        
    }

    [RelayCommand]
    private async Task Save()
    {
        LoadingVisible = true;
        JokeMessage Message = new JokeMessage();
        
        await Task.Run(async () =>
        {
            if (_idJoke == 0)
            {
                var tbjoke = new ModelJoke
                {
                    Categories = JokeDto.Categories,
                    CreatedAt = JokeDto.CreatedAt,
                    IconUrl = JokeDto.IconUrl,
                    Id = JokeDto.Id,
                    UpdatedAt = JokeDto.UpdatedAt,
                    Url = JokeDto.Url,
                    Value = JokeDto.Value
                };
                _dbContext.Jokes.Add(tbjoke);
                await _dbContext.SaveChangesAsync();
                
                JokeDto.IdJoke = tbjoke.IdJoke;
                Message = new JokeMessage()
                {
                    IsCreate = true,
                    JokeDto = JokeDto
                };
            }
            else
            {
                var found = await _dbContext.Jokes.FirstAsync(e => e.IdJoke == _idJoke);
                found.Categories = JokeDto.Categories;
                found.CreatedAt = JokeDto.CreatedAt;
                found.IconUrl = JokeDto.IconUrl;
                found.Id = JokeDto.Id;
                found.UpdatedAt = JokeDto.UpdatedAt;
                found.Url = JokeDto.Url;
                found.Value = JokeDto.Value;
                
                await _dbContext.SaveChangesAsync();
                Message = new JokeMessage()
                {
                    IsCreate = false,
                    JokeDto = JokeDto
                };
            }
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                LoadingVisible = false;
                WeakReferenceMessenger.Default.Send(new JokeMessaging(Message));
                await Shell.Current.Navigation.PopAsync();
            });
        });
    }
}