using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Qyz.UI.Base
{
    public class DialogWindow : Window
    {
        public DialogWindow(string title)
        {
            Grid grid = new Grid();
            this.Content = grid; 
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40, GridUnitType.Star) });

            grid.Children.Add(createTitlePanel(title));
            grid.Children.Add(createBottomPanel());
        }

        private Grid createTitlePanel(string title)
        {
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Qyz.UI.Themes;component/defaultTheme/image/Dialog_Top.jpg"));
            b.Stretch = Stretch.Fill;
            Grid gridTitle = new Grid(); 
            gridTitle.Background = b; 
            Grid.SetRow(gridTitle, 0);

            TextBlock tb = new TextBlock();
            tb.Text = title;
            tb.SetResourceReference(TextBlock.StyleProperty, "titleTextStyle");
            gridTitle.Children.Add(tb);

            Button closeBt = new Button();
            closeBt.SetResourceReference(Button.StyleProperty, "closeBtnStyle");
            closeBt.Click += closeBt_Click;
            gridTitle.Children.Add(closeBt);
            return gridTitle;
        }

        protected virtual void closeBt_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("1111");
        }
        private Grid createBottomPanel()
        {
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Qyz.UI.Themes;component/defaultTheme/image/Dialog_footer.jpg"));
            b.Stretch = Stretch.Fill;
            Grid gridTitle = new Grid();
            gridTitle.Background = b;

            Grid.SetRow(gridTitle, 1);
            return gridTitle;
        }
        
    }
}
