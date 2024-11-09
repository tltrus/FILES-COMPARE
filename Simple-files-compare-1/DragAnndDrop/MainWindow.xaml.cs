using System.Windows;
using ViewModel;


namespace WpfApp
{
    public partial class MainWindow : Window
    {
        ApplicationViewModel AppViewModel = new ApplicationViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = AppViewModel;
        }


        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (System.IO.File.Exists(file))
                {
                    AppViewModel.AddFileToList(file); // Should be changed on MVVM solution
                }
            }
        }
    }
}