using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace HotStuff.ViewModel
{
    public class MainPageViewModel
    {
        public ISeries[] Series { get; set; } =
        {
            new ColumnSeries<int>
            {
                Values = new[] { 6, 3, 5, 7, 3, 4, 6, 3 },
                Stroke = null,
                Fill = new SolidColorPaint(SKColor.Parse("FC5D52")),
                MaxBarWidth = double.MaxValue,
                IgnoresBarPosition = true
            },
        };
        public IEnumerable<ISeries> PieSeries { get; set; } =
        new[] { 2, 4, 1, 4, 3 }.AsPieSeries((value, series) =>
        {
            series.InnerRadius = 0;
        });

        public MainPageViewModel()
        {
            
        }

    }
}
