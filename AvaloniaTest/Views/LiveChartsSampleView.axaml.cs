using System;
using Avalonia.Controls;
using Avalonia.Input;
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

public partial class LiveChartsSampleView : UserControl
{
    // private Section SectionLineX;
    public LiveChartsSampleView() {
        Console.WriteLine("hello");
        InitializeComponent();

        var viewModel = new LiveChartsSampleViewModel(InvokeOnUIThread);
        DataContext = viewModel;
    }


    // private void ChartPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    // {
    //     var chart     = this.FindControl<CartesianChart>("OhlcChart") ?? throw new NullReferenceException();
    //     var viewModel = DataContext as LiveChartsSampleViewModel      ?? throw new NullReferenceException();
    //
    //     Console.WriteLine( viewModel.Sections);
    //     Console.WriteLine( $"Sections: {viewModel.Sections.Count}" );
    //
    //     // gets the point in the UI coordinates.
    //     var p = e.GetPosition(chart);
    //     // var cp = e.GetCurrentPoint(chart);
    //
    //     var scaledPoint = chart.ScaleUIPoint(new LvcPoint((float)p.X, (float)p.Y));
    //
    //     Console.WriteLine($"Position is {p}");
    //     Console.WriteLine($"Position on Chart is len={scaledPoint.Length} {scaledPoint[0]}, {scaledPoint[1]}");
    //
    //     viewModel.Sections[0] = new RectangularSection
    //     {
    //         Yi = scaledPoint[1],
    //         Yj = scaledPoint[1],
    //         Stroke = new SolidColorPaint
    //         {
    //             Color = SKColors.Purple,
    //             StrokeThickness = 3,
    //             PathEffect = new DashEffect(new float[] { 6, 6 })
    //         }
    //     };
    // }


    private void ChartPointerMoved(object? sender, Avalonia.Input.PointerEventArgs e) {
        var chart = this.FindControl<CartesianChart>("OhlcChart") ?? throw new NullReferenceException();
        chart.Cursor = new Cursor(StandardCursorType.Arrow);
        var viewModel = DataContext as LiveChartsSampleViewModel ?? throw new NullReferenceException();

        var p           = e.GetPosition(chart);
        var scaledPoint = chart.ScaleUIPoint(new LvcPoint((float)p.X, (float)p.Y));

        Console.WriteLine($"Point Position is {p}; Position on Chart is len={scaledPoint.Length} {scaledPoint[0]}, {scaledPoint[1]}");

        viewModel.Sections[0] = new RectangularSection {
            Xi = scaledPoint[0],
            Xj = scaledPoint[0],
            Stroke = new SolidColorPaint {
                Color           = SKColors.DarkGray,
                StrokeThickness = 3,
                PathEffect      = new DashEffect(new float[] { 6, 6 })
            }
        };
    }


// this method takes another function as an argument.
// the idea is that we are invoking the passed action in the UI thread
// but the UI framework will let the view model how to do this.
// we will pass the InvokeOnUIThread method to our view model so the view model knows how
// to invoke an action in the UI thread.
    private void InvokeOnUIThread(Action action) {
        Dispatcher.UIThread.Post(action);
    }


    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    public void OnPointerMove(LvcPoint point) { }
}