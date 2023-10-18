using System;
using System.ComponentModel;

namespace BMICalcWinUI.Common
{
    public class PropertyChangedEventListener : IDisposable
    {
        INotifyPropertyChanged Source;
        PropertyChangedEventHandler Handler;

        public PropertyChangedEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            Source = source;
            Handler = handler;
            Source.PropertyChanged += Handler;
        }

        public void Dispose()
        {
            if (Source != null && Handler != null)
                Source.PropertyChanged -= Handler;
        }
    }
}
