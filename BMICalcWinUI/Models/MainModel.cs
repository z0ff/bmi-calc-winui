using System.IO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;

namespace BMICalcWinUI.Models
{
    internal class MainModel : INotifyPropertyChanged
    {
        private string _filePath = string.Empty;
        private bool _modified = false;
        private readonly ObservableCollection<BMI> _bmiList;

        public string FilePath
        {
            get { return _filePath; }
            private set
            {
                _filePath = value;
                RaisePropertyCanged(nameof(FilePath));
            }
        }

        public bool Modified {
            get { return _modified; }
            private set
            {
                _modified = value;
                RaisePropertyCanged(nameof(Modified));
            }
        }

        public ObservableCollection<BMI> BmiList { get { return _bmiList; } }

        public MainModel()
        {
            FilePath = string.Empty;
            Modified = false;
            _bmiList = new ObservableCollection<BMI>();
            /*
            if (File.Exists(filepath))
            {
                LoadFile();
            }
            */
        }

        public void AddBMIData(string name, double height, double weight)
        {
            Modified = true;
            var bmi = BMI.Init(name, height, weight);
            _bmiList.Add(bmi);
        }

        public void NewFile()
        {
            Modified = false;
            FilePath = string.Empty;
            _bmiList.Clear();
        }

        public void LoadFile(string filePath)
        {
            Modified = false;
            FilePath = filePath;
            _bmiList.Clear();
            string[] readText = File.ReadAllText(FilePath).Trim().Split('\n');
            Debug.WriteLine("readText[0]: " + readText[0]);
            foreach (string s in readText)
            {
                Debug.WriteLine("s: " + s);
                _bmiList.Add(BMI.Parse(s));
            }
        }

        public void SaveFile(string filePath)
        {
            Modified = false;
            FilePath = filePath;
            var saveFileBuf = "";

            foreach (BMI bmi in _bmiList)
            {
                saveFileBuf += bmi.ToString();
            }

            File.WriteAllText(FilePath, saveFileBuf);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyCanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
