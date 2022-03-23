using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using AvaloniaTest.ViewModels;

namespace AvaloniaTest.Views;

public partial class LiveChartsSampleView : UserControl
{
    public LiveChartsSampleView()
    {
        Console.WriteLine("hello");
        InitializeComponent();
            
        var viewModel = new LiveChartsSampleViewModel(InvokeOnUIThread);
        DataContext = viewModel;
    }
        
        
// this method takes another function as an argument.
// the idea is that we are invoking the passed action in the UI thread
// but the UI framework will let the view model how to do this.
// we will pass the InvokeOnUIThread method to our view model so the view model knows how
// to invoke an action in the UI thread.
    private void InvokeOnUIThread(Action action)
    {
        Dispatcher.UIThread.Post(action);
    }
    
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    
}