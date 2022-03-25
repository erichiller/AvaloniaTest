using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace AvaloniaTest.ViewModels;

public class LiveChartsSampleViewModel : ViewModelBase
{
    private const int _tasks = 10;

    private const int _delay = 100;

    private const int _updateLoops = 60_000 / _delay;

    // public ISeries[] Series { get; set; } = new ISeries[]
    // {
    //     new LineSeries<double>
    //     {
    //         Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
    //         Fill   = null,
    //                 
    //     }
    // };


    #region Financial Candlestick

    private          Random                               _rgen          = new Random();
    private          DateTime                             _dtBase        = new DateTime(2021, 10, 01);
    private static   int                                  _minuteCounter = 0;
    private readonly ObservableCollection<FinancialPoint> _values;
    // private static   FinancialPoint                       s_current;


    public Axis[] XAxes { get; set; } = new[] {
        // set the UnitWidth to "days" to support date time scaled points.
        new Axis {
            UnitWidth      = TimeSpan.FromMinutes(1).Ticks,
            LabelsRotation = 80,
            Labeler        = p => new DateTime((long)p).ToShortTimeString(),
            MinStep = TimeSpan.FromMinutes(1).Ticks
            // MinStep        = TimeSpan.FromMinutes(1).Ticks,
            // Padding = new Padding(50)
            // Padding =  new Padding { Bottom = 80, Left = 80, Right = 80, Top = 80 }
        }
    };

    private FinancialPoint CreateFinancialPoint() {
        var high = _rgen.Next(75, 150);
        var low  = _rgen.Next(20, 75);
        return new FinancialPoint(
            _dtBase.AddMinutes(_minuteCounter++),
            high,
            _rgen.Next(low, high),
            _rgen.Next(low, high),
            low
        );
    }


    public ObservableCollection<RectangularSection> Sections { get; set; }
        = new ObservableCollection<RectangularSection> {
            new RectangularSection {
                Xi = 25,
                Xj = 25,
                Stroke = new SolidColorPaint {
                    Color           = SKColors.DarkGray,
                    StrokeThickness = 3,
                    PathEffect      = new DashEffect(new float[] { 6, 6 })
                }
            },
        };

    public ISeries[] Series { get; set; }

    private readonly Action<Action> _uiThreadInvoker;

    public LiveChartsSampleViewModel(Action<Action> uiThreadInvoker) {
        Console.WriteLine("Constructed.");
        _values = new ObservableCollection<FinancialPoint>();
        for (int i = 0; i < 100; i++) {
            _values.Add(CreateFinancialPoint());
            // Console.WriteLine($"{_values[^1].Date}\t{_values[^1].Open}\t{_values[^1].High}\t{_values[^1].Low}\t{_values[^1].Close}");
        }

        var                             defaultSeries = new CandlesticksSeries<FinancialPoint>();
        if (LiveCharts.CurrentSettings.GetTheme<SkiaSharpDrawingContext>().SeriesDefaultsResolver is { } resolver) {
            resolver(new LvcColor[] { new LvcColor() }, defaultSeries, false);
        }

        SolidColorPaint upStroke = new SolidColorPaint((defaultSeries.UpStroke as SolidColorPaint)?.Color ?? SKColors.Green); 
        // {
            // StrokeThickness = -5
        // };
        // SolidColorPaint upStroke = new SolidColorPaint(SKColors.Orange);
        // {
            // StrokeMiter = 0,
            // StrokeThickness = -5
        // };
        // SolidColorPaint? upStroke = null;
        SolidColorPaint downStroke = new SolidColorPaint((defaultSeries.DownStroke as SolidColorPaint)?.Color ?? SKColors.Red);
        SolidColorPaint upFill     = new SolidColorPaint((defaultSeries.UpFill as SolidColorPaint)?.Color     ?? SKColors.Green);
        // {
        // SolidColorPaint upFill = new SolidColorPaint(SKColors.Purple) {
        // StrokeThickness = -5,
        // };
        SolidColorPaint downFill   = new SolidColorPaint((defaultSeries.DownFill as SolidColorPaint)?.Color   ?? SKColors.Red);

        Series = new ISeries[] {
            new CandlesticksSeries<FinancialPoint> {
                Values      = _values,
                MaxBarWidth = 10000,
                // DataPadding = new LvcPoint(0.75f, 0),
                // MaxBarWidth = 
                // UpStroke   = new SolidColorPaint(new SKColor(255, 250, 250, 250)),
                UpStroke = upStroke,
                // DownStroke =  new SolidColorPaint(new SKColor(255, 250, 250, 250)),
                DownStroke = downStroke,
                UpFill     = upFill,
                DownFill   = downFill,
                BarMargin = 0.75f,
                // AnimationsSpeed = null
                // EasingFunction = null,
                // AnimationsSpeed = TimeSpan.FromSeconds(1),
                // MaxBarWidth = TimeSpan.FromMinutes(1).Ticks * 0.25
            }
        };

        _uiThreadInvoker = uiThreadInvoker;
        // _delay           = 1;
        // var readTasks = 10;

        return; // KILL
        // create {readTasks} parallel tasks that will add a point every {_delay} milliseconds
        for (var i = 0; i < _tasks; i++) {
            ReadData();
        }
    }

    public object Sync { get; } = new object();

    private async void ReadData() {
        await Task.Delay(1000);
        int count = 0;

        while (true) {
            await Task.Delay(_delay);

            // force the change to happen in the UI thread.
            _uiThreadInvoker(() => {
                // if (count++ > _updateLoops)
                // {
                _values.Add(CreateFinancialPoint());
                _values.RemoveAt(0);
                // count = 0;
                // }
                // else
                // {
                // Console.WriteLine($"was {_values[^1].High}");
                // _values[^1].High = _values[^1].High + 1;
                // Console.WriteLine($"updated to {_values[^1].High}");
                // }
            });
        }
    }

    #endregion
}

/*

public class LiveChartsSampleViewModel : ViewModelBase
{
    
    // public ISeries[] Series { get; set; } = new ISeries[]
    // {
    //     new LineSeries<double>
    //     {
    //         Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
    //         Fill   = null,
    //                 
    //     }
    // };
    
    
    
    #region Financial Candlestick

    private Random   rgen          = new Random();
    private DateTime dtBase        = new DateTime(2021, 10, 01);
    private int      minuteCounter = 0;
    
    
    public Axis[] XAxes { get; set; } = new[]
    {
        // set the UnitWidth to "days" to support date time scaled points.
        new Axis
        {
            UnitWidth      = TimeSpan.FromMinutes(1).Ticks,
            LabelsRotation = 80,
            Labeler        = p => new DateTime((long)p).ToShortTimeString(),
            // MinStep        = TimeSpan.FromMinutes(1).Ticks
        }
    };

    private FinancialPoint createFinancialPoint() => new FinancialPoint(
        dtBase.AddMinutes(minuteCounter++),
        rgen.Next(75, 150),
        rgen.Next(20, 150),
        rgen.Next(20, 74),
        rgen.Next(20, 150)
    );

    public ISeries[] Series { get; set; }

    public LiveChartsSampleViewModel() : base()
    {
        List<FinancialPoint> values = new List<FinancialPoint>();
        for (int i =0; i < 100; i++)
        {
            values.Add(createFinancialPoint());
            Console.WriteLine($"{values[^1].Date}\t{values[^1].Open}\t{values[^1].High}\t{values[^1].Low}\t{values[^1].Close}");
        }
        Series = new ISeries[]
        {
            new CandlesticksSeries<FinancialPoint>
            {
                Values = values,
            }
        };
    }
    #endregion
}

*/