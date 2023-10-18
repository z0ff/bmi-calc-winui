using BMICalcWinUI.Views;
using BMICalcWinUI.Common;
using BMICalcWinUI.Models;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace BMICalcWinUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly MainModel _model;
        private readonly IDisposable _listener;

        private string _filePath = string.Empty;
        private bool _modified = false;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                RaisePropertyCanged(nameof(FilePath));
            }
        }
        public bool Modified
        {
            get => _modified;
            set
            {
                _modified = value;
                RaisePropertyCanged(nameof(Modified));
            }
        }

        public string Name { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }

        public ObservableCollection<BMI> BmiList { get { return _model.BmiList; } }

        public ICommand AddBMIDataCommand { get; set; }
        public ICommand NewFileCommand { get; set; }
        public ICommand LoadFileCommand { get; set; }
        public ICommand SaveFileCommand { get; set; }

        public MainWindowViewModel()
        {
            _model = new MainModel();

            _listener = new PropertyChangedEventListener(_model,
                (sender, e) =>
                {
                    if (e.PropertyName == nameof(_model.FilePath))
                    {
                        FilePath = _model.FilePath;
                    }
                    else if (e.PropertyName == nameof(_model.Modified))
                    {
                        Modified = _model.Modified;
                    }
                });

            Name = string.Empty;
            Height = 0;
            Weight = 0;

            AddBMIDataCommand = new RelayCommand(AddBMIData);
            NewFileCommand = new RelayCommand(NewFile);
            LoadFileCommand = new RelayCommand(LoadFile);
            SaveFileCommand = new RelayCommand(SaveFile);
        }

        private void AddBMIData()
        {
            Debug.WriteLine("{0}, {1}, {2}", Name, Height, Weight);
            _model.AddBMIData(Name, Height, Weight);
        }

        private async void NewFile()
        {
            if (Modified)
            {
                ContentDialog msgBox = new ContentDialog
                {
                    XamlRoot = MainWindow.Handle.Content.XamlRoot,
                    Title = "データは保存されていません",
                    Content = "変更済みのデータを保存しますか?",
                    PrimaryButtonText = "はい",
                    SecondaryButtonText = "いいえ",
                    CloseButtonText = "キャンセル"
                };
                ContentDialogResult result = await msgBox.ShowAsync();
                if (result == ContentDialogResult.Primary) SaveFile();
                else if (result == ContentDialogResult.None) return;
            }

            _model.NewFile();
        }

        private async void LoadFile()
        {
            if (Modified)
            {
                ContentDialog msgBox = new ContentDialog
                {
                    XamlRoot = MainWindow.Handle.Content.XamlRoot,
                    Title = "データは保存されていません",
                    Content = "変更済みのデータを保存しますか?",
                    PrimaryButtonText = "はい",
                    SecondaryButtonText = "いいえ",
                    CloseButtonText = "キャンセル"
                };
                ContentDialogResult result = await msgBox.ShowAsync();
                if (result == ContentDialogResult.Primary) SaveFile();
                else if (result == ContentDialogResult.None) return;
            }

            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".csv");
            InitializeWithWindow.Initialize(picker,
            WindowNative.GetWindowHandle(MainWindow.Handle));
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                _model.LoadFile(file.Path.ToString());
            }
        }

        private async void SaveFile()
        {
            var picker = new FileSavePicker();
            picker.FileTypeChoices.Add("CSV File", new List<string>() { ".csv" });
            InitializeWithWindow.Initialize(picker,
            WindowNative.GetWindowHandle(MainWindow.Handle));
            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                _model.SaveFile(file.Path.ToString());
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyCanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _listener?.Dispose();
        }
    }
}
