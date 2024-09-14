# Localization Guide

This guide provides an overview of how localization is implemented in the project and instructions on how to work with it. It covers adding new languages, managing resource files, accessing localized strings, and handling culture changes at runtime.

## Table of Contents
- Overview
- Localization Implementation
-- Resource Files
-- LocalizedStrings Class
-- LocalizationService
- Adding a New Language
- Accessing Localized Strings
-- In XAML
-- In Code
- Handling Culture Changes at Runtime
- Dynamic Language Selection
- Best Practices
- Troubleshooting
- Additional Resources


## Overview

Localization in this project enables the application to support multiple languages. It allows users to switch languages at runtime without restarting the application. The localization is based on resource files ( `.resx `), which store localized strings for different cultures.

## Localization Implementation

Resource Files
Location:  `Resources\Localization` folder.
Base Resource File: `Strings.resx` (default language).
Localized Resource Files: Named with culture codes, e.g., `Strings.en.resx`, `Strings.fr.resx`.

Each resource file contains key-value pairs:

| Name | Value |
| ------ | ------ |
| Greeting | Welcome to MVVM! |
| ChangeButton | Change Greeting | 


## LocalizedStrings Class

- Purpose: Acts as a wrapper to access localized strings from resource files.
- Location: `Resources/Localization/LocalizedStrings.cs`.
- Key Features:
--  Implements `INotifyPropertyChanged` for data binding.
--  Provides an indexer to access strings by key.
--  Listens to culture changes to update bindings.

### Example: 

```csharp	
    public class LocalizedStrings : INotifyPropertyChanged
    {
        private static ResourceManager _resourceManager = new ResourceManager("WpfMvvmTemplate.Resources.Localization.Strings", typeof(LocalizedStrings).Assembly);

        public event PropertyChangedEventHandler PropertyChanged;

        public string this[string key]
        {
            get { return _resourceManager.GetString(key, CultureInfo.CurrentUICulture); }
        }

        public LocalizedStrings()
        {
            LocalizationService.Instance.PropertyChanged += (s, e) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
            };
        }
    }
```

## Localization Service
 
- Purpose: Manages culture changes and notifies the application to update bindings.
- Location: Services/LocalizationService.cs.
- Key Features:
 - Singleton instance for global access.
 - Implements INotifyPropertyChanged to notify about culture changes.
 - Provides ChangeCulture method to switch cultures at runtime.


```csharp	
    public class LocalizationService : INotifyPropertyChanged
    {
        private static LocalizationService _instance;
        public static LocalizationService Instance => _instance ??= new LocalizationService();

        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            OnPropertyChanged(string.Empty); 
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
```

## Adding a New Language

To add support for a new language:

1. Create a Resource File for the New Language:
 - Copy Strings.resx and rename it to include the culture code, e.g., Strings.es.resx for Spanish.
 - The culture code should follow the format languagecode2-country/regioncode2 (e.g., en-US, fr-FR).
2. Translate the Strings:
 - Open the new .resx file.
 - Translate the Value of each key to the target language.
3.  Ensure the Build Action is Correct:
 - Right-click the new .resx file in Solution Explorer.
 - Select Properties.
 - Verify that Build Action is set to Embedded Resource.
5. Optional: Add the new culture to the language selection UI if you're maintaining a manual list.

## Accessing Localized Strings
### In XAML

To use localized strings in XAML:

1. Reference the LocalizedStrings Resource:
 - The LocalizedStrings instance is added to App.xaml:
```xml	
<Application.Resources>
    <local:LocalizedStrings x:Key="LocalizedStrings"/> 
</Application.Resources>
```

2. Bind to the LocalizedStrings in XAML:
```xml	
<TextBlock Text="{Binding Source={StaticResource LocalizedStrings}, Path=Greeting}" />
<Button Content="{Binding Source={StaticResource LocalizedStrings}, Path=ChangeButton}" />
```
 
###  In Code

To access localized strings in code-behind or ViewModels:

1. Instantiate LocalizedStrings:
```csharp
using WpfMvvmTemplate.Resources;
var localizedStrings = new LocalizedStrings();
```
2. Access Strings by Key:
```csharp
string greeting = localizedStrings["Greeting"];
```
## Handling Culture Changes at Runtime

To change the application culture during runtime:

1. Use the LocalizationService:
```csharp
LocalizationService.Instance.ChangeCulture("fr-FR"); // Switch to French
```
2. Refresh Bindings: 
 - The LocalizedStrings class listens to `LocalizationService` and raises `PropertyChanged` events to update bindings.

3. Update UI Elements:
 - Ensure that any UI elements not bound to LocalizedStrings are updated accordingly.

 
## Dynamic Language Selection

The application includes functionality for users to select their preferred language at runtime.

1. Languages Collection:
 - The MainViewModel maintains an ObservableCollection<CultureInfo> called Languages.
 - This collection is populated with available cultures.
1. SelectedLanguage Property:
 - When SelectedLanguage changes, it calls LocalizationService.Instance.ChangeCulture.

Example in ViewModel:

```csharp	 
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<CultureInfo> Languages { get; }

        private CultureInfo _selectedLanguage;
        public CultureInfo SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                    LocalizationService.Instance.ChangeCulture(value.Name);
                }
            }
        }

        public MainViewModel()
        {
            Languages = new ObservableCollection<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ru-RU"),
                // Add other supported cultures here
            };
            SelectedLanguage = Languages[0]; // Set default language
        }
    }
```

Example in XAML:

```xml	
<StackPanel Orientation="Horizontal">
    <TextBlock Text="{Binding Source={StaticResource LocalizedStrings}, Path=SelectLanguage}" />
    <ComboBox ItemsSource="{Binding Languages}"
              SelectedItem="{Binding SelectedLanguage}"
              DisplayMemberPath="NativeName" />
</StackPanel>
```

## Troubleshooting

- Missing Resources:
 - Issue: Localized strings are not displayed.
 - Solution: Verify that the resource files are correctly named and set as Embedded Resource.
- Culture Not Changing:
Issue: Changing the culture does not update the UI.
 - Solution: Ensure that PropertyChanged is raised in LocalizationService.
 - Verify that bindings are correctly set and use INotifyPropertyChanged.
- Resource Key Not Found:
 - Issue: A key returns null or empty.
 - Solution: Check that the key exists in the resource file and is correctly spelled.