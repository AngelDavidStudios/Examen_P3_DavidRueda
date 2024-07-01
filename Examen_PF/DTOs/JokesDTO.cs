using CommunityToolkit.Mvvm.ComponentModel;

namespace Examen_PF.DTOs;

public partial class JokesDTO: ObservableObject
{
    [ObservableProperty] public int IdJoke;
    [ObservableProperty] public List<string> Categories;
    [ObservableProperty] public string CreatedAt;
    [ObservableProperty] public string IconUrl;
    [ObservableProperty] public string Id;
    [ObservableProperty] public string UpdatedAt;
    [ObservableProperty] public string Url;
    [ObservableProperty] public string Value;
}