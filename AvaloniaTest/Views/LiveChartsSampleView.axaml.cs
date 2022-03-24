using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using AvaloniaTest.ViewModels;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace AvaloniaTest.Views;

public partial class LiveChartsSampleView : UserControl {

    // private Section SectionLineX;
    public LiveChartsSampleView()
    {
        Console.WriteLine("hello");
        InitializeComponent();
            
        var viewModel = new LiveChartsSampleViewModel(InvokeOnUIThread);
        DataContext = viewModel;
    }


    private void ChartPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        var chart = this.FindControl<CartesianChart>("chart");
        var viewModel = DataContext as LiveChartsSampleViewModel;

        Console.WriteLine( viewModel.Sections);
        Console.WriteLine( $"Sections: {viewModel.Sections.Count}" );
        Console.WriteLine($"Chart is " + (chart is null ? "null" : chart.Name));

        // gets the point in the UI coordinates.
        var p = e.GetPosition(chart);
        // var cp = e.GetCurrentPoint(chart);

        var scaledPoint = chart.ScaleUIPoint(new LvcPoint((float)p.X, (float)p.Y));

        Console.WriteLine($"Position is {p}");
        Console.WriteLine($"Position on Chart is len={scaledPoint.Length} {scaledPoint[0]}, {scaledPoint[1]}");

        // // scales the UI coordintaes to the corresponging data in the chart.
        // // ScaleUIPoint retuns an array of double
        // var scaledPoint = chart.ScaleUIPoint(new LvcPoint((float)p.X, (float)p.Y));

        // // where the X coordinate is in the first position
        // var x = scaledPoint[0];

        // // and the Y coordinate in the second position
        // var y = scaledPoint[1];

        // finally add the new point to the data in our chart.
        // viewModel.Sections[0].Xi = 148;
        // viewModel.Sections[0].Xj = 148;

        viewModel.Sections[0] = new RectangularSection
        {
            Yi = scaledPoint[1],
            Yj = scaledPoint[1],
            Stroke = new SolidColorPaint
            {
                Color = SKColors.Purple,
                StrokeThickness = 3,
                PathEffect = new DashEffect(new float[] { 6, 6 })
            }
        };

         
    }


    private void ChartPointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        var chart = this.FindControl<CartesianChart>("chart");
        var viewModel = DataContext as LiveChartsSampleViewModel;

        var p = e.GetPosition(chart);
        var scaledPoint = chart.ScaleUIPoint(new LvcPoint((float)p.X, (float)p.Y));

        Console.WriteLine($"Point Position is {p}; Position on Chart is len={scaledPoint.Length} {scaledPoint[0]}, {scaledPoint[1]}");

        viewModel.Sections[0] = new RectangularSection
        {
            Yi = scaledPoint[1],
            Yj = scaledPoint[1],
            Stroke = new SolidColorPaint
            {
                Color = SKColors.Purple,
                StrokeThickness = 3,
                PathEffect = new DashEffect(new float[] { 6, 6 })
            }
        };


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

    public void OnPointerMove( LvcPoint point ) {
        
    }
    
    
}