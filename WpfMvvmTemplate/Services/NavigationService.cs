using System;
using System.Collections.Generic;
using System.Windows.Controls; 

namespace WpfMvvmTemplate.Services
{
    public interface INavigationService
    {
        void NavigateTo(string viewName);
        void NavigateTo(string viewName, object parameter);
        void NavigateTo<TViewModel>(object parameter = null);
        void GoBack();
        void Configure(string key, Uri pageUri);
        void Configure<TViewModel, TView>() where TView : Page;
        void SetFrame(Frame frame);
    }

    public class NavigationService : INavigationService
    {
        private static NavigationService _instance;
        public static NavigationService Instance => _instance ?? (_instance = new NavigationService());

        private readonly Dictionary<string, Uri> _pagesByKey;
        private readonly Dictionary<Type, Type> _pagesByViewModel;
        private Frame _mainFrame;

        private NavigationService()
        {
            _pagesByKey = new Dictionary<string, Uri>();
            _pagesByViewModel = new Dictionary<Type, Type>();
        }

        public void SetFrame(Frame frame)
        {
            _mainFrame = frame;
        }

        public void Configure(string key, Uri pageUri)
        {
            if(!_pagesByKey.ContainsKey(key))
            {
                _pagesByKey.Add(key, pageUri);
            }
        }

        public void Configure<TViewModel, TView>() where TView : Page
        {
            var viewModelType = typeof(TViewModel);
            var viewType = typeof(TView);

            if(!_pagesByViewModel.ContainsKey(viewModelType))
            {
                _pagesByViewModel.Add(viewModelType, viewType);
            }
        }

        public void NavigateTo(string viewName)
        {
            NavigateTo(viewName, null);
        }

        public void NavigateTo(string viewName, object parameter)
        {
            if(_pagesByKey.ContainsKey(viewName))
            {
                _mainFrame.Navigate(_pagesByKey[viewName], parameter);
            }
            else
            {
                throw new ArgumentException($"Page with name {viewName} not found.");
            }
        }

        public void NavigateTo<TViewModel>(object parameter = null)
        {
            var viewModelType = typeof(TViewModel);
            if(_pagesByViewModel.ContainsKey(viewModelType))
            {
                var viewType = _pagesByViewModel[viewModelType];
                var page = Activator.CreateInstance(viewType) as Page;
                var viewModel = parameter != null
                    ? Activator.CreateInstance(viewModelType, parameter)
                    : Activator.CreateInstance(viewModelType);
                page.DataContext = viewModel;
                _mainFrame.Navigate(page);
            }
            else
            {
                throw new ArgumentException($"Page for ViewModel {viewModelType} not found.");
            }
        }


        public void GoBack()
        {
            if(_mainFrame != null && _mainFrame.CanGoBack)
            {
                _mainFrame.GoBack();
            }
        }
    }
}
