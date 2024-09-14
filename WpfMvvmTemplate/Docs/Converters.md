# Converter Documentation

This documentation provides an overview of the custom value converters available in the project. Converters are an essential part of WPF applications, allowing you to convert data from one type to another during data binding. This guide covers each converter's purpose, implementation details, and examples of how to use them in your project.


## Table of Contents
1. Introduction
2. Converters
 - InverseBoolConverter
 - NumberToBrushConverter
 - NullToVisibilityConverter
 - StringToVisibilityConverter
 - EnumToBoolConverter
 - DateTimeToStringConverter
 - MultiBooleanToVisibilityConverter
 - StringFormatConverter
 - BooleanToOpacityConverter
 - RangeToBooleanConverter
 - NullToBoolConverter
 - StringIsNullOrEmptyConverter
 - MultiBooleanToBooleanConverter
 - ValueToGradientBrushConverter
 - RangeToColorConverter
 - NumberToColorConverter
3. Integration into Your Project 

## Introduction
In WPF, converters implement the `IValueConverter` or `IMultiValueConverter` interfaces to provide custom logic for converting bound data. They are especially useful when the data source and target properties are of different types or when the data needs to be transformed before display.

## Converters
### InverseBoolConverter
#### Purpose

Inverts a boolean value. Useful when you need the opposite of a boolean property for binding.

#### Usage
Declaration in XAML:

```xml	
<converters:InverseBoolConverter x:Key="InverseBoolConverter"/>
```	
#### Example Usage:

Binding the IsEnabled property of a button to the inverse of a boolean property IsProcessing:

```xml	
<Button Content="Submit" IsEnabled="{Binding IsProcessing, Converter={StaticResource InverseBoolConverter}}"/>
```	


### NumberToBrushConverter
#### Purpose

Converts a numeric value to a `Brush` based on predefined thresholds. Useful for indicating levels (e.g., low, medium, high) with different colors.
#### Usage
Declaration in XAML:

```xml	
<converters:NumberToBrushConverter x:Key="NumberToBrushConverter"
                                   LowThreshold="30"
                                   HighThreshold="70"
                                   LowBrush="LightGreen"
                                   MediumBrush="Orange"
                                   HighBrush="Red"/>
```	
#### Example Usage:
Applying a background color to a progress bar based on its value:
```xml	
<ProgressBar Value="{Binding ProgressValue}"
             Minimum="0" Maximum="100"
             Foreground="{Binding ProgressValue, Converter={StaticResource NumberToBrushConverter}}"/>
```	



### NullToVisibilityConverter
#### Purpose

Converts a `null` value to a `Visibility` value. Typically used to show or hide UI elements based on whether a bound property is `null`.
#### Usage
 Declaration in XAML:

```xml	
<converters:NullToVisibilityConverter x:Key="NullToVisibilityConverter"/>
```	
#### Example Usage:
Applying a background color to a progress bar based on its value:
```xml	
<TextBlock Text="No data available"
           Visibility="{Binding DataItem, Converter={StaticResource NullToVisibilityConverter}}"/>
```	


### StringToVisibilityConverter
#### Purpose

Converts a string to a `Visibility` value based on whether the string is empty or not.

#### Usage
Declaration in XAML:

```xml	
<converters:StringToVisibilityConverter x:Key="StringToVisibilityConverter"/>
```	
#### Example Usage:
Showing a placeholder text when the input is empty:
```xml	
<TextBlock Text="Enter your name"
           Visibility="{Binding Name, Converter={StaticResource StringToVisibilityConverter}}"/>
```	


### EnumToBoolConverter
#### Purpose

Converts an `enum` value to a `bool`, indicating whether the value matches the specified parameter. Useful for binding enums to radio buttons.
#### Usage
 Declaration in XAML:

```xml	
<converters:EnumToBoolConverter x:Key="EnumToBoolConverter"/>
```	
#### Example Usage:
Binding an enum `UserType` to radio buttons:
```xml	
<StackPanel>
    <RadioButton Content="Admin"
                 IsChecked="{Binding UserType, Converter={StaticResource EnumToBoolConverter}, ConverterParameter={x:Static local:UserType.Admin}}"/>
    <RadioButton Content="User"
                 IsChecked="{Binding UserType, Converter={StaticResource EnumToBoolConverter}, ConverterParameter={x:Static local:UserType.User}}"/>
</StackPanel>
```	


### DateTimeToStringConverter
#### Purpose

Formats a `DateTime` object into a string using a specified format.
#### Usage
Declaration in XAML:

```xml
<converters:DateTimeToStringConverter x:Key="DateTimeToStringConverter"/>
```	
#### Example Usage:
Displaying a formatted date:
```xml	
<TextBlock Text="{Binding BirthDate, Converter={StaticResource DateTimeToStringConverter}, ConverterParameter='MMMM dd, yyyy'}"/>
```	



### MultiBooleanToVisibilityConverter
#### Purpose

Converts multiple boolean values to a single `Visibility` value using logical `AND` or `OR` operations.
#### Usage
Declaration in XAML:

```xml	
<converters:MultiBooleanToVisibilityConverter x:Key="MultiBooleanToVisibilityConverter"/>
```	
#### Example Usage:
Displaying a formatted date:
```xml	
<StackPanel.Visibility>
    <MultiBinding Converter="{StaticResource MultiBooleanToVisibilityConverter}" ConverterParameter="AND">
        <Binding Path="IsLoggedIn"/>
        <Binding Path="IsAdmin"/>
    </MultiBinding>
</StackPanel.Visibility>
```	



### StringFormatConverter
#### Purpose

Formats a string using a format string provided as a parameter.

#### Usage
Declaration in XAML:

```xml	
<converters:StringFormatConverter x:Key="StringFormatConverter"/>
```	
#### Example Usage:
Displaying a formatted date:
```xml	
<TextBlock Text="{Binding Amount, Converter={StaticResource StringFormatConverter}, ConverterParameter='Total: {0:C}'}"/>
```	



### BooleanToOpacityConverter
#### Purpose

Converts a boolean value to an opacity level (e.g., `1.0` for true, `0.5` for false).

#### Usage
Declaration in XAML:

```xml	
<converters:BooleanToOpacityConverter x:Key="BooleanToOpacityConverter" FalseOpacity="0.3"/>
```	
#### Example Usage:
Displaying a formatted date:
```xml	
<Button Content="Submit"
        Opacity="{Binding IsFormValid, Converter={StaticResource BooleanToOpacityConverter}}"/>
```	
  

### RangeToBooleanConverter
#### Purpose

Checks if a numeric value falls within a specified range, returning `true` or `false`.

#### Usage
Declaration in XAML:

```xml	
<converters:RangeToBooleanConverter x:Key="RangeToBooleanConverter" Minimum="0" Maximum="50"/>
```	
#### Example Usage:
Enabling a button only when a value is within a range:
```xml	
<Button Content="Proceed"
        IsEnabled="{Binding Score, Converter={StaticResource RangeToBooleanConverter}}"/>
```	
 

### NullToBoolConverter
#### Purpose

Converts `null` to `false` and `non-null` to `true`, or vice versa if `Invert` is set.

#### Usage
Declaration in XAML:

```xml	
<converters:NullToBoolConverter x:Key="NullToBoolConverter"/>
```	
#### Example Usage:
Enabling a button only if a selection is made:
```xml	
<Button Content="Delete"
        IsEnabled="{Binding SelectedItem, Converter={StaticResource NullToBoolConverter}, Invert=True}"/>
```	

### StringIsNullOrEmptyConverter
#### Purpose

Checks if a string is `null` or empty, returning a boolean.

#### Usage
Declaration in XAML:

```xml	
<converters:StringIsNullOrEmptyConverter x:Key="StringIsNullOrEmptyConverter"/>
```	
#### Example Usage:
Disabling a button when input is empty:
```xml	
<Button Content="Submit"
        IsEnabled="{Binding InputText, Converter={StaticResource StringIsNullOrEmptyConverter}, Invert=True}"/>
```	

### MultiBooleanToBooleanConverter
#### Purpose

Performs logical `AND` or `OR` operations on multiple boolean values, returning a single boolean result.

#### Usage
 Declaration in XAML:

```xml	
<converters:MultiBooleanToBooleanConverter x:Key="MultiBooleanToBooleanConverter"/>
```	
#### Example Usage:
Enabling a button only when multiple conditions are true:
```xml	
<Button Content="Register">
    <Button.IsEnabled>
        <MultiBinding Converter="{StaticResource MultiBooleanToBooleanConverter}">
            <Binding Path="IsUsernameValid"/>
            <Binding Path="IsPasswordValid"/>
            <Binding Path="IsEmailValid"/>
        </MultiBinding>
    </Button.IsEnabled>
</Button>
```	


### ValueToGradientBrushConverter
#### Purpose

Converts a numeric value to a `SolidColorBrush` that represents a gradient between two colors based on the value, providing a smooth transition.

#### Usage
 Declaration in XAML:

```xml	
<converters:ValueToGradientBrushConverter x:Key="ValueToGradientBrushConverter"
                                          StartColor="Green"
                                          EndColor="Red"
                                          Minimum="0"
                                          Maximum="100"/>
```	
#### Example Usage:
Changing the color of a progress bar as it fills:
```xml	
<ProgressBar Minimum="0" Maximum="100" Value="{Binding Progress}">
    <ProgressBar.Foreground>
        <Binding Path="Progress" Converter="{StaticResource ValueToGradientBrushConverter}"/>
    </ProgressBar.Foreground>
</ProgressBar>
```	


### RangeToColorConverter
#### Purpose

Maps specific numeric ranges to specific colors, offering granular control over color assignments based on value ranges.

#### Usage
Declaration in XAML:

```xml	
<converters:RangeToColorConverter x:Key="RangeToColorConverter">
    <converters:RangeToColorConverter.Ranges>
        <converters:RangeColor Minimum="0" Maximum="20" Color="Green"/>
        <converters:RangeColor Minimum="21" Maximum="40" Color="YellowGreen"/>
        <converters:RangeColor Minimum="41" Maximum="60" Color="Yellow"/>
        <converters:RangeColor Minimum="61" Maximum="80" Color="Orange"/>
        <converters:RangeColor Minimum="81" Maximum="100" Color="Red"/>
    </converters:RangeToColorConverter.Ranges>
</converters:RangeToColorConverter>
```	
#### Example Usage:
Changing text color based on a score:
```xml	
<TextBlock Text="{Binding Score}"
           Foreground="{Binding Score, Converter={StaticResource RangeToColorConverter}}"/>
```	



### NumberToColorConverter
#### Purpose

Converts a numeric value to a `SolidColorBrush` by interpolating between two colors across a specified range.
#### Usage
Declaration in XAML:

```xml	
<converters:NumberToColorConverter x:Key="NumberToColorConverter"
                                   StartColor="Blue"
                                   EndColor="Red"
                                   Minimum="0"
                                   Maximum="100"/>
```	
#### Example Usage:
Changing the fill color of an ellipse based on temperature:
```xml	
<Ellipse Width="100" Height="100">
    <Ellipse.Fill>
        <Binding Path="Temperature" Converter="{StaticResource NumberToColorConverter}"/>
    </Ellipse.Fill>
</Ellipse>
```	


## Integration into Your Project
1. Add Converter Classes: 
 - Place all converter classes in your project's `Converters` folder.
2. Update Namespace: 
 - Ensure the namespace in each converter class matches your project's structure.
3. Declare Converters in XAML:
 - In your `Converters.xaml` resource dictionary (or directly in your XAML file), declare each converter with a unique `x:Key`.
```xml	
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:converters="clr-namespace:WpfMvvmTemplate.Converters">
    <!-- Declare converters here -->
    <converters:InverseBoolConverter x:Key="InverseBoolConverter"/>
    <!-- ... other converters ... -->
</ResourceDictionary> 
```	 
4. Add Converter Classes: 
 - In `App.xaml` or your main resource dictionary, merge the converters resource dictionary.
 ```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="Converters/Converters.xaml"/>
            <!-- Other resource dictionaries -->
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```
4. Use Converters in XAML Bindings:
 - Reference the converters in your XAML files as shown in the usage examples.

