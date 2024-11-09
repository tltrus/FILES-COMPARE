using System.Collections.ObjectModel;
using Helpers;
using Model;


namespace ViewModel
{
    internal class ApplicationViewModel : ViewModelBase
    {

        private CompareFile compareFile;

        private ConsoleModel consoleModel;
        public ConsoleModel ConsoleModel
        {
            get { return consoleModel; }
            set
            {
                consoleModel = value;
                OnPropertyChanged("ConsoleModel");
            }
        }

        public ObservableCollection<string> FilesList { get; set; }


        public ApplicationViewModel()
        {
            compareFile = new CompareFile();
            FilesList = new ObservableCollection<string>();

            ConsoleModel = new ConsoleModel()
            {
                Text = "Hello, this is Console"
            };
        }

        #region RelayCommand

        private RelayCommand startCommand;
        public RelayCommand StartCommand
        {
            get
            {
                return startCommand ??
                  (startCommand = new RelayCommand(async obj =>
                  {
                      if (FilesList.Count != 2)
                      {
                          ConsoleModel.Text = "> Wrong files count";
                          return;
                      }
                      var result = await compareFile.StartAsync(FilesList[0], FilesList[1]);
                      ConsoleModel.Text = string.Join(Environment.NewLine, result);
                  }));
            }
        }

        private RelayCommand clearCommand;
        public RelayCommand ClearCommand
        {
            get
            {
                return clearCommand ??
                  (clearCommand = new RelayCommand(obj =>
                  {
                      FilesList.Clear();
                      ConsoleModel.Text = "";
                  }));
            }
        }

        #endregion

        public void AddFileToList(string file)
        {
            if (FilesList.Count >= 2) return;

            FilesList.Add(file);
        }
    }
}
