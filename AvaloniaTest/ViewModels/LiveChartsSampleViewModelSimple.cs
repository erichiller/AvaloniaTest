// using System;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;
// using LiveChartsCore;
// using LiveChartsCore.Defaults;
// using LiveChartsCore.SkiaSharpView;
// using LiveChartsCore.SkiaSharpView.Painting;
// using LiveChartsCore.SkiaSharpView.Painting.Effects;
// using SkiaSharp;

// namespace AvaloniaTest.ViewModels;

// public class LiveChartsSampleViewModelSimple : ViewModelBase
// {

//     public LiveChartsSampleViewModelSimple()
//     {
        
//         Console.WriteLine("LiveChartsSampleViewModelSimple constructed");
//     }
//     // public ISeries[] Series { get; set; } = new ISeries[]
//     // {
//     //     new LineSeries<double>
//     //     {
//     //         Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
//     //         Fill   = null,
//     //                 
//     //     }
//     // };

//     // we have to let the chart know that the X axis in days.
//     public Axis[] XAxes {
//         get
//         {
            
//             Console.WriteLine("ARRGGGGGGG");
//             return new Axis[]
//             {
//                 new ()
//                 {
//                     LabelsRotation = 15,
//                     Labeler        = value => new DateTime((long)value).ToString("yyyy MMM dd"),
//                     // set the unit width of the axis to "days"
//                     // since our X axis is of type date time and 
//                     // the interval between our points is in days
//                     UnitWidth = TimeSpan.FromDays(1).Ticks
//                 }
//             };
//         }
//     }


//     public RectangularSection[] Sections { get; set; }
//         = new[]
//         {
//             new RectangularSection
//             {
//                 Yi = 8,
//                 Yj = 8,
//                 Stroke = new SolidColorPaint
//                 {
//                     Color = SKColors.Red,
//                     StrokeThickness = 3,
//                     PathEffect = new DashEffect(new float[] { 6, 6 })
//                 }
//             },
//         };

//     public ISeries[] Series { get; set; }
//         = new[]
//         {
//             new ScatterSeries<ObservablePoint>
//             {
//                 GeometrySize = 10,
//                 Stroke = new SolidColorPaint { Color = SKColors.Blue, StrokeThickness = 1 },
//                 Fill = null,
//                 Values = new ObservablePoint[]
//                 {
//                     new(2.2, 5.4), new(4.5, 2.5), new(4.2, 7.4),
//                     new(6.4, 9.9), new(4.2, 9.2), new(5.8, 3.5),
//                     new(7.3, 5.8), new(8.9, 3.9), new(6.1, 4.6),
//                     new(9.4, 7.7), new(8.4, 8.5), new(3.6, 9.6),
//                     new(4.4, 6.3), new(5.8, 4.8), new(6.9, 3.4),
//                     new(7.6, 1.8), new(8.3, 8.3), new(9.9, 5.2),
//                     new(8.1, 4.7), new(7.4, 3.9), new(6.8, 2.3)
//                 }
//             }
//         };

//         public IEnumerable<ISeries> Series
//     {
//         get
//         {
//             Console.WriteLine("CHHHHH");
//             return new List<ISeries>
//             {
//                 new CandlesticksSeries<FinancialPoint>
//                 {
//                     Values = new List<FinancialPoint>
//                     {
//                         //               date,      high, open, close, low
//                         new (new DateTime(2021, 1, 1), 523, 500, 450, 400),
//                         new (new DateTime(2021, 1, 2), 500, 450, 425, 400),
//                         new (new DateTime(2021, 1, 3), 490, 425, 400, 380),
//                         new (new DateTime(2021, 1, 4), 420, 400, 420, 380),
//                         new (new DateTime(2021, 1, 5), 520, 420, 490, 400),
//                         new (new DateTime(2021, 1, 6), 580, 490, 560, 440),
//                         new (new DateTime(2021, 1, 7), 570, 560, 350, 340),
//                         new (new DateTime(2021, 1, 8), 380, 350, 380, 330),
//                         new (new DateTime(2021, 1, 9), 440, 380, 420, 350),
//                         new (new DateTime(2021, 1, 10), 490, 420, 460, 400),
//                         new (new DateTime(2021, 1, 11), 520, 460, 510, 460),
//                         new (new DateTime(2021, 1, 12), 580, 510, 560, 500),
//                         new (new DateTime(2021, 1, 13), 600, 560, 540, 510),
//                         new (new DateTime(2021, 1, 14), 580, 540, 520, 500),
//                         new (new DateTime(2021, 1, 15), 580, 520, 560, 520),
//                         new (new DateTime(2021, 1, 16), 590, 560, 580, 520),
//                         new (new DateTime(2021, 1, 17), 650, 580, 630, 550),
//                         new (new DateTime(2021, 1, 18), 680, 630, 650, 600),
//                         new (new DateTime(2021, 1, 19), 670, 650, 600, 570),
//                         new (new DateTime(2021, 1, 20), 640, 600, 610, 560),
//                         new (new DateTime(2021, 1, 21), 630, 610, 630, 590),
//                     }

//                 }
//             };
//         }
//     }
// }   