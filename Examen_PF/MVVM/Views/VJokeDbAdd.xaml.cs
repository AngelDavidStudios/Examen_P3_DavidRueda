using Examen_PF.MVVM.ViewModels;

namespace Examen_PF.MVVM.Views;

public partial class VJokeDbAdd : ContentPage
{
    public VJokeDbAdd(VMJokeDbAdd viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}