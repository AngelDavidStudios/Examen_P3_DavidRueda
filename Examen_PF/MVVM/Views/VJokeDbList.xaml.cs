using Examen_PF.MVVM.ViewModels;

namespace Examen_PF.MVVM.Views;

public partial class VJokeDbList : ContentPage
{
    public VJokeDbList(VMJokeDbList viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}