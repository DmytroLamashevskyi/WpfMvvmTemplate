# Command Documentation

This documentation provides an overview of the custom command classes implemented in the project, including their purposes, usage examples, and implementation details.	

## Table of Contents
- RelayCommand
- AsyncRelayCommand
- CompositeCommand
- ProgressAsyncRelayCommand
- OpenFileCommand

## 1. RelayCommand
### Overview
The `RelayCommand` class is a basic implementation of the `ICommand` interface that allows for simple command binding in WPF applications. It is designed to relay its functionality to other objects by invoking delegates.

### Usage

In ViewModel:

```csharp	
public ICommand SubmitCommand { get; }

public MainViewModel()
{
    SubmitCommand = new RelayCommand(Submit, CanSubmit);
}

private void Submit()
{
    // Implementation of the submit action
}

private bool CanSubmit()
{
    // Determine if the command can execute
    return !string.IsNullOrWhiteSpace(InputData);
}
```
In XAML:
```xml	
<Button Content="Submit" Command="{Binding SubmitCommand}" />
```
### Notes
- Purpose: Handles synchronous operations without parameters.
- CanExecuteChanged: Utilizes `CommandManager.RequerySuggested` for automatic updates.
- Use Case: Suitable for simple commands like button clicks where no parameters are needed.

## 2. AsyncRelayCommand
### Overview
The `AsyncRelayCommand` class extends the command pattern to support asynchronous operations using `Task`. It prevents UI blocking by running the execution logic asynchronously.
### Usage

In ViewModel:

```csharp	
public ICommand LoadDataCommand { get; }

public MainViewModel()
{
    LoadDataCommand = new AsyncRelayCommand(LoadDataAsync, CanLoadData);
}

private async Task LoadDataAsync()
{
    IsLoading = true;
    try
    {
        // Asynchronous data loading logic
        await Task.Delay(2000); // Simulate delay
        Data = await dataService.GetDataAsync();
    }
    finally
    {
        IsLoading = false;
    }
}

private bool CanLoadData()
{
    return !IsLoading;
}
```
In XAML:
```xml	
<Button Content="Load Data" Command="{Binding LoadDataCommand}" />
<ProgressBar IsIndeterminate="True" Visibility="{Binding IsLoading, Converter={StaticResource BoolToVisibilityConverter}}" />
```
### Notes
- Purpose: Handles asynchronous operations without parameters.
- Execution State: Manages the `_isExecuting` flag to prevent multiple simultaneous executions.
- Use Case: Suitable for operations like data loading, file I/O, or network requests.

## 3. CompositeCommand
### Overview
The `CompositeCommand` class allows multiple commands to be executed as a single unit. It's useful when a single UI action should trigger multiple commands.

### Usage

In ViewModel:

```csharp	
public CompositeCommand SaveAllCommand { get; } = new CompositeCommand();

public MainViewModel()
{
    SaveAllCommand.RegisterCommand(SaveCommand);
    SaveAllCommand.RegisterCommand(SaveSettingsCommand);
}

public ICommand SaveCommand { get; }
public ICommand SaveSettingsCommand { get; }
```
In XAML:
```xml	
<Button Content="Save All" Command="{Binding SaveAllCommand}" />
```
### Notes
- Purpose: Aggregates multiple `ICommand` instances to execute them together.
- CanExecute Logic: Only allows execution if all registered commands can execute.
- Use Case: Ideal for toolbar buttons or menu items that should perform multiple actions.


## 4. ProgressAsyncRelayCommand
### Overview
The `ProgressAsyncRelayCommand` class handles asynchronous operations that report progress. It allows updating the UI with progress information during the execution of the command.

### Usage

In ViewModel:

```csharp	
public ICommand DownloadCommand { get; }

public MainViewModel()
{
    DownloadCommand = new ProgressAsyncRelayCommand(DownloadFileAsync);
}

private async Task DownloadFileAsync(IProgress<int> progress)
{
    for (int i = 0; i <= 100; i++)
    {
        await Task.Delay(50); // Simulate work
        progress.Report(i);
    }
}
```
In XAML:
```xml	
<ProgressBar Minimum="0" Maximum="100" Value="{Binding DownloadCommand.ProgressValue}" />
<Button Content="Download" Command="{Binding DownloadCommand}" />
```
### Notes
- Purpose: Executes an asynchronous operation while reporting progress.
- Progress Reporting: Uses `IProgress<int>` to report progress to the UI.
- Use Case: Ideal for file downloads, data processing tasks, or any operation where progress indication enhances user experience.



## 5. OpenFileCommand
### Overview
The `OpenFileCommand` simplifies the process of opening files via a dialog and passing the selected file path to the ViewModel.

### Usage

In ViewModel:

```csharp	
public ICommand OpenFileCommand { get; }

public MainViewModel()
{
    OpenFileCommand = new OpenFileCommand(OpenFile);
}

private void OpenFile(string filePath)
{
    // Logic to handle the opened file
    FileContent = File.ReadAllText(filePath);
}
```
In XAML:
```xml	
<Button Content="Open File" Command="{Binding OpenFileCommand}" />
```
### Notes
- Purpose: Facilitates file selection and handling within the MVVM pattern.
- File Dialog: Uses `OpenFileDialog` from Microsoft.Win32.
- Use Case: When an application needs to allow users to select and open files, such as importing data or loading configurations.



































