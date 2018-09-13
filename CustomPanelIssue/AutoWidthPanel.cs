using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CustomPanelIssue
{
    public class AutoWidthItemsPanel : Panel
    {
        public static readonly DependencyProperty MinItemWidthProperty = DependencyProperty.Register(
            "MinItemWidth",
            typeof(double),
            typeof(AutoWidthItemsPanel),
            new PropertyMetadata(0));

        public static readonly DependencyProperty MinItemHeightProperty = DependencyProperty.Register(
            "MinItemHeight",
            typeof(double),
            typeof(AutoWidthItemsPanel),
            new PropertyMetadata(0));

        public double MinItemWidth
        {
            get { return (double)GetValue(MinItemWidthProperty); }
            set { SetValue(MinItemWidthProperty, value); }
        }

        public double MinItemHeight
        {
            get { return (double)GetValue(MinItemHeightProperty); }
            set { SetValue(MinItemHeightProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var numColumns = Math.Floor(availableSize.Width / MinItemWidth);
            numColumns = numColumns == 0 ? 1 : numColumns;
            var numRows = Math.Ceiling(Children.Count / numColumns);

            Debug.WriteLine($"MeasureOverride: {availableSize.Height},{availableSize.Width}");

            var itemWidth = availableSize.Width / numColumns;
            var aspectRatio = MinItemHeight / MinItemWidth;
            //var itemHeight = itemWidth * aspectRatio;
            var itemHeight = MinItemHeight;

            foreach (var child in Children)
            {
                var item = child as GridViewItem;
                child.Measure(new Size(itemWidth, itemHeight));
            }

            return new Size(itemWidth * numColumns, itemHeight * numRows);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var numColumns = Math.Floor(finalSize.Width / MinItemWidth);
            numColumns = numColumns == 0 ? 1 : numColumns;
            var numRows = Math.Ceiling(Children.Count / numColumns);

            Debug.WriteLine($"ArrangeOverride: {finalSize.Height},{finalSize.Width}");

            var itemWidth = finalSize.Width / numColumns;
            var aspectRatio = MinItemHeight / MinItemWidth;
            //var itemHeight = itemWidth * aspectRatio;
            var itemHeight = MinItemHeight;

            var row = 0;
            var column = 0;

            foreach (var child in Children)
            {
                var item = child as GridViewItem;
                child.Arrange(new Rect(column * itemWidth, row * itemHeight, itemWidth, itemHeight));

                column++;

                if (column >= numColumns)
                {
                    column = 0;
                    row++;
                }
            }

            return finalSize;
        }
    }
}
