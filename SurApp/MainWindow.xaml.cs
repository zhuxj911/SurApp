using Microsoft.Win32;
using SurApp.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SurApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainWindowVM vm;
		public MainWindow()
		{
			InitializeComponent();

			vm = new MainWindowVM(figureCanvas);
			this.DataContext = vm;
			this.figureCanvas.Tag = vm.GeoPointList;
		}

		private void MenuItem_CalculateAzimuth_Click(object sender, RoutedEventArgs e)
		{
			AzimuthWindow win = new AzimuthWindow();
			win.ShowDialog();
		}

		private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
		{
			vm.Open();
		}

		private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
		{
			vm.Save();
		}

		private void MenuItem_OutToBmp_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.DefaultExt = ".jpg'";
			dlg.Filter = "jpg图形|*.jpg|bmp图像|*.bmp|png图像|*.png|All File(*.*)|*.*";
			if(dlg.ShowDialog() != true)
				return;


			this.figureCanvas.OutToBmp(dlg.FileName);
		}

		private void MenuItem_SaveAs_Click(object sender, RoutedEventArgs e)
		{
			vm.SaveAs();
		}

		private void MenuItem_BLtoXY_Click(object sender, RoutedEventArgs e)
		{
			vm.BLtoXY();
		}

		private void MenuItem_XYtoBL_Click(object sender, RoutedEventArgs e)
		{
			vm.XYtoBL();
		}

		private void MenuItem_ClearBL_Click(object sender, RoutedEventArgs e)
		{
			vm.ClearBL();
		}

		private void MenuItem_ClearXY_Click(object sender, RoutedEventArgs e)
		{
			vm.ClearXY();
		}

		private void MenuItem_New_Click(object sender, RoutedEventArgs e)
		{
			vm.New();
		}
	}
}