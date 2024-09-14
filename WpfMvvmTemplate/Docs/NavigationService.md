# NavigationService Documentation

This documentation provides a comprehensive guide on how to use the `NavigationService` in your WPF MVVM application. The `NavigationService` facilitates navigation between views (pages) while adhering to the MVVM pattern, ensuring a clean separation between the UI and business logic.

## Table of Contents
1. Introduction
2. Implementation Overview
3. Setting Up NavigationService
 - Initializing NavigationService
 - Configuring Views and ViewModels
4. Using NavigationService in ViewModels
 - Navigating to a View
 - Navigating with Parameters
5. Setting Up Views
 - Creating a View
 - Assigning DataContext
6. Passing Parameters Between Views
7. Handling Back Navigation
8. Complete Example

## Introduction

The `NavigationService` is a singleton service designed to manage navigation between different pages (views) in a WPF application following the MVVM pattern. It decouples the navigation logic from the views and view models, allowing for a clean and testable codebase.

Key Features:
- Navigate to views by their name or associated `ViewModel`.
- Pass parameters during navigation.
- Maintain navigation history with the ability to navigate back.

## Setting Up NavigationService

### Initializing NavigationService
To use the `NavigationService`, you need to initialize it and set the Frame control that will host your pages.

In `MainWindow.xaml`:
```xml	
<Window x:Class="WpfMvvmTemplate.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        <Frame x:Name="MainFrame" NavigationUIVisibility="Hidden"/>
    </Grid>
</Window>
```

In `MainWindow.xaml.cs`:
```csharp	
    public MainWindow()
    {
        InitializeComponent();

        // Initialize NavigationService and set the Frame
        var navigationService = NavigationService.Instance;
        navigationService.SetFrame(MainFrame);

        // Configure views and ViewModels
        navigationService.Configure<HomeViewModel, Views.HomePage>();
        navigationService.Configure<SettingsViewModel, Views.SettingsPage>();

        // Navigate to the initial view
         navigationService.NavigateTo<HomeViewModel>();
    } 
```	

### Configuring Views and ViewModels
Use the Configure methods to map view names and `ViewModels` to their corresponding pages.

```csharp	
// Mapping view name to page URI
navigationService.Configure("HomePage", new Uri("Views/HomePage.xaml", UriKind.Relative));

// Mapping ViewModel to View
navigationService.Configure<HomeViewModel, Views.HomePage>();
```	

## Using NavigationService in ViewModels
To navigate between views from your `ViewModels`, you can use the `NavigationService` singleton instance.
 
### Navigating to a View

In `HomeViewModel.cs`:
```csharp	
    public class HomeViewModel
    {
        public ICommand NavigateCommand { get; }

        public HomeViewModel()
        {
            NavigateCommand = new RelayCommand(NavigateToSettings);
        }

        private void NavigateToSettings()
        {
            NavigationService.Instance.NavigateTo<SettingsViewModel>();
        }
    }
```	
### Navigating with Parameters
You can pass parameters during navigation.

```csharp	
private void NavigateToDetails()
{
    var parameter = new { Id = 42 };
    NavigationService.Instance.NavigateTo<DetailsViewModel>(parameter);
} 
```	

## Setting Up Views
### Creating a View
Create a page (view) for your application.
In `Views/HomePage.xaml`: 
```xml	
<Page x:Class="WpfMvvmTemplate.Views.HomePage"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      Title="HomePage">
    <Grid>
        <Button Content="Go to Settings"
                Command="{Binding NavigateCommand}"
                Width="200" Height="50"
                HorizontalAlignment="Center" VerticalAlignment="Center"/>
    </Grid>
</Page>
 
```	
### Assigning DataContext
Set the `DataContext` of the page to its corresponding `ViewModel`.
In `Views/HomePage.xaml.cs`: 
```csharp	
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }
    } 
```	

## Passing Parameters Between Views

To receive parameters in the target view or `ViewModel`, you can override the `OnNavigatedTo` method in your page.

In `Views/DetailsPage.xaml.cs`: 
```csharp	  
namespace WpfMvvmTemplate.Views
{
    public partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = e.ExtraData;
            // Use the parameter as needed
        }
    }
} 
```	
Alternatively, you can modify your `ViewModel` to accept parameters.

In `DetailsViewModel.cs`:
```csharp	  
namespace WpfMvvmTemplate.ViewModels
{
    public class DetailsViewModel
    {
        public int ItemId { get; }

        public DetailsViewModel(object parameter)
        {
            if (parameter is int id)
            {
                ItemId = id;
            }
            // Load data based on ItemId
        }
    }
} 
```	 
 
## Handling Back Navigation
To navigate back to the previous page, use the GoBack method.

In your `ViewModel`:
```csharp	  
public class SettingsViewModel
{
    public ICommand GoBackCommand { get; }

    public SettingsViewModel()
    {
        GoBackCommand = new RelayCommand(GoBack);
    }

    private void GoBack()
    {
        NavigationService.Instance.GoBack();
    }
}
```	 
In `SettingsPage.xaml`:
```xml	  
<Button Content="Go Back"
        Command="{Binding GoBackCommand}" />
```	 

## Complete Example
Putting it all together, here's how you might set up navigation in your application.
1. Initialize `NavigationService` in `MainWindow`:
```csharp	  
var navigationService = NavigationService.Instance;
navigationService.SetFrame(MainFrame);
navigationService.Configure<HomeViewModel, Views.HomePage>();
navigationService.Configure<SettingsViewModel, Views.SettingsPage>();
navigationService.NavigateTo<HomeViewModel>();
```	
2. Define `HomeViewModel` with navigation command:
```csharp	  
public class HomeViewModel
{
    public ICommand NavigateCommand { get; }

    public HomeViewModel()
    {
        NavigateCommand = new RelayCommand(NavigateToSettings);
    }

    private void NavigateToSettings()
    {
        NavigationService.Instance.NavigateTo<SettingsViewModel>();
    }
}
```	
3. Create `HomePage` and set `DataContext`:
```csharp	  
public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
        DataContext = new HomeViewModel();
    }
}
```	
4. Implement `SettingsViewModel` with back navigation:
```csharp	  
public class SettingsViewModel
{
    public ICommand GoBackCommand { get; }

    public SettingsViewModel()
    {
        GoBackCommand = new RelayCommand(GoBack);
    }

    private void GoBack()
    {
        NavigationService.Instance.GoBack();
    }
} 
```	 
5. Create `SettingsPage` and set `DataContext`: 
```csharp	  
public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
        DataContext = new SettingsViewModel();
    }
} 
```	   
6. Set up the UI with buttons bound to commands:
In `HomePage.xaml`:
```xml	  
<Button Content="Go to Settings"
        Command="{Binding NavigateCommand}"
        Width="200" Height="50"
        HorizontalAlignment="Center" VerticalAlignment="Center"/>
```	 
In `HomePage.xaml`:
```xml	  
<Button Content="Go Back"
        Command="{Binding GoBackCommand}"
        Width="100" Height="30"
        HorizontalAlignment="Left" VerticalAlignment="Top" Margin="10"/>
```	 
  