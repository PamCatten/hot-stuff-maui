using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace HotStuff.ViewModel
{
    public class MainPageViewModel
    {
        public IEnumerable<ISeries> Series { get; set; } =
            new[] { 2, 4, 1, 4, 3 }.AsPieSeries((value, series) =>
            {
                series.InnerRadius = 50;
            });

        public MainPageViewModel()
        {
        }
    }
}
