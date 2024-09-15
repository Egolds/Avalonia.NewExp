using Avalonia.NewExp.Core.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Avalonia.NewExp.Core.Services
{
    public interface IViewModelLocator
    {
        TVewModel Locate<TVewModel>(ObservableObject? owner = null) where TVewModel : ObservableObject;
    }

    public class ViewModelLocator : IViewModelLocator
    {
        private readonly Func<Type, ObservableObject> viewModelFactory;

        public ViewModelLocator(Func<Type, ObservableObject> viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
        }

        public TVewModel Locate<TVewModel>(ObservableObject? ownerWindowViewModel = null) where TVewModel : ObservableObject
        {
            var viewModel = (TVewModel)viewModelFactory.Invoke(typeof(TVewModel));
            if (viewModel != null && viewModel is IOwnerable ownerable)
                ownerable.OwnerWindowViewModel = ownerWindowViewModel!;

            return viewModel!;
        }
    }
}
