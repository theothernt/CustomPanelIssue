using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CustomPanelIssue
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            TestGridView.ItemsSource = GenerateDemoData();
        }

        private IEnumerable<TestModel> GenerateDemoData()
        {
            var data = new List<TestModel>
            {
                new TestModel() { Name = "Item 1" },
                new TestModel() { Name = "Item 2" },
                new TestModel() { Name = "Item 3" },

                new TestModel() { Name = "Item 4" },
                new TestModel() { Name = "Item 5" },
                new TestModel() { Name = "Item 6" }
            };

            return data;
        }
    }
}
