using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;

namespace AvaloniaTest.ViewModels;

public class LiveChartsSampleViewModel : ViewModelBase {

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
    private        static  int                                  _minuteCounter = 0;
    private readonly ObservableCollection<FinancialPoint> _values;
    // private static   FinancialPoint                       s_current;


    public Axis[] XAxes { get; set; } = new[]
    {
        // set the UnitWidth to "days" to support date time scaled points.
        new Axis
        {
            UnitWidth = TimeSpan.FromMinutes(1).Ticks, LabelsRotation = 80, Labeler = p => new DateTime((long)p).ToShortTimeString(),
            // MinStep        = TimeSpan.FromMinutes(1).Ticks
        }
    };

    private FinancialPoint CreateFinancialPoint() => new FinancialPoint(
        _dtBase.AddMinutes(_minuteCounter++),
        _rgen.Next(75, 150),
        _rgen.Next(20, 150),
        _rgen.Next(20, 74),
        _rgen.Next(20, 150)
    );

    public ISeries[] Series { get; set; }

    private readonly Action<Action> _uiThreadInvoker;
    public LiveChartsSampleViewModel(Action<Action> uiThreadInvoker)
    {
        
        Console.WriteLine("Constructed.");
        _values = new ObservableCollection<FinancialPoint>();
        for (int i = 0; i < 100; i++)
        {
            _values.Add(CreateFinancialPoint());
            Console.WriteLine($"{_values[^1].Date}\t{_values[^1].Open}\t{_values[^1].High}\t{_values[^1].Low}\t{_values[^1].Close}");
        }

        Series = new ISeries[]
        {
            new CandlesticksSeries<FinancialPoint>
            {
                Values = _values,
                MaxBarWidth = 2,
                // EasingFunction = null,
                // AnimationsSpeed = TimeSpan.FromSeconds(1),
                // MaxBarWidth = TimeSpan.FromMinutes(1).Ticks * 0.25
            }
        };

        _uiThreadInvoker = uiThreadInvoker;
        // _delay           = 1;
        // var readTasks = 10;


        // create {readTasks} parallel tasks that will add a point every {_delay} milliseconds
        for (var i = 0; i < _tasks; i++)
        {
            ReadData();
        }
    }

    public object Sync { get; } = new object();

    private async void ReadData()
    {
        await Task.Delay(1000);
        int count = 0;

        while (true)
        {
            await Task.Delay(_delay);
            
            // force the change to happen in the UI thread.
            _uiThreadInvoker(() =>
            {
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