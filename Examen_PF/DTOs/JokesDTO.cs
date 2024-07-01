using CommunityToolkit.Mvvm.ComponentModel;

namespace Examen_PF.DTOs;

public partial class JokesDTO: ObservableObject
{
    [ObservableProperty] public int idJoke;
    [ObservableProperty] public List<string> categories;
    [ObservableProperty] public string createdAt;
    [ObservableProperty] public string iconUrl;
    [ObservableProperty] public string id;
    [ObservableProperty] public string updatedAt;
    [ObservableProperty] public string url;
    [ObservableProperty] public string value;
}