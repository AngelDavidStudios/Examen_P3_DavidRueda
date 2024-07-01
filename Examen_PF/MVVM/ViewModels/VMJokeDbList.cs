using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Examen_PF.Data;
using Examen_PF.DTOs;
using Examen_PF.MVVM.Views;
using Examen_PF.Utility;
using Microsoft.EntityFrameworkCore;

namespace Examen_PF.MVVM.ViewModels;

public partial class VMJokeDbList: ObservableObject
{
    private readonly AppDbContext _dbContext;
    [ObservableProperty]
    private ObservableCollection<JokesDTO> jokeList = new ObservableCollection<JokesDTO>();
    
    public VMJokeDbList(AppDbContext context)
    {
        _dbContext = context;
        MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));
        WeakReferenceMessenger.Default.Register<JokeMessaging>(this, (r, m) =>
        {
            JokesMessageRecived(m.Value);
        });
    }
    
    public async Task Obtener()
    {
        var list = await _dbContext.Jokes.ToListAsync();
        if(list.Any())
        {
            foreach(var item in list)
            {
                var jokeDto = new JokesDTO
                {
                    IdJoke = item.IdJoke,
                    Categories = item.Categories,
                    CreatedAt = item.CreatedAt,
                    IconUrl = item.IconUrl,
                    Id = item.Id,
                    UpdatedAt = item.UpdatedAt,
                    Url = item.Url,
                    Value = item.Value
                };
                jokeList.Add(jokeDto);
            }
        }
    }
    
    public void JokesMessageRecived(JokeMessage jokeMessage)
    {
        var jokeDto = jokeMessage.JokeDto;

        if (jokeMessage.IsCreate)
        {
            JokeList.Add(jokeDto);
        }
        else
        {
            var joke = JokeList.FirstOrDefault(x => x.IdJoke == jokeDto.IdJoke);
            if (joke != null)
            {
                joke.Categories = jokeDto.Categories;
                joke.CreatedAt = jokeDto.CreatedAt;
                joke.IconUrl = jokeDto.IconUrl;
                joke.Id = jokeDto.Id;
                joke.UpdatedAt = jokeDto.UpdatedAt;
                joke.Url = jokeDto.Url;
                joke.Value = jokeDto.Value;
            }
        }
    }

    [RelayCommand]
    private async Task Create()
    {
        var uri= $"{nameof(VJokeDbAdd)}";
        await Shell.Current.GoToAsync(uri);
    }
    
    [RelayCommand]
    private async Task Edit(JokesDTO jokeDto)
    {
        var uri = $"{nameof(VJokeDbEdit)}?id={jokeDto.IdJoke}";
        await Shell.Current.GoToAsync(uri);
    }

    [RelayCommand]
    private async Task Delete(JokesDTO jokeDto)
    {
        bool answer = await App.Current.MainPage.DisplayAlert("Delete", "Are you sure?", "Yes", "No");
        if (answer)
        {
            var found = await _dbContext.Jokes.FirstAsync(x => x.IdJoke == jokeDto.IdJoke);
            if (found != null)
            {
                _dbContext.Jokes.Remove(found);
                await _dbContext.SaveChangesAsync();
                JokeList.Remove(jokeDto);
            }
        }
    }
}