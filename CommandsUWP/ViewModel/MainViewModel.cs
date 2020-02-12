using CommandsUWP.ViewModel.AppUIBasics.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommandsUWP.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private int _max;
        private int _number;
        private Random _random;
        public MainViewModel()
        {
            _random = new Random();
            Roll = new RelayCommand(
                () => { Number = _random.Next(Max); },
                () => { return (_max > 0 && _max <= 6); }
            );

            SetMax = new ParametrizedRelayCommand(
                (parameter) =>
                {
                    if (Int32.TryParse(parameter as string, out int result))
                    {
                        Max = result;
                    }
                    if (Roll.CanExecute(null)) Roll.Execute(null);
                },
                () => { return true; }
                );
            Max = 6;
            Number = _random.Next(Max);
        }
        public int Number { get => _number+1; set { _number = value; NotifyPropertyChanged(); } }
        public int Max { get => _max; set { _max = value; Roll.RaiseCanExecuteChanged(); NotifyPropertyChanged(); } }
        public RelayCommand Roll { get; set; }
        public ParametrizedRelayCommand SetMax { get; set; }
        public event PropertyChangingEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }


    }
}
