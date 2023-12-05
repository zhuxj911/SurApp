using Microsoft.Win32;
using SurApp.ViewModels;
using System.Windows;

namespace SurApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

		}

		//private void MenuItem_OutToBmp_Click(object sender, RoutedEventArgs e)
		//{
		//	SaveFileDialog dlg = new SaveFileDialog();
		//	dlg.DefaultExt = ".jpg'";
		//	dlg.Filter = "jpg图形|*.jpg|bmp图像|*.bmp|png图像|*.png|All File(*.*)|*.*";
		//	if(dlg.ShowDialog() != true)
		//		return;

		//	this.figureCanvas.OutToBmp(dlg.FileName);
		//}
	}
}