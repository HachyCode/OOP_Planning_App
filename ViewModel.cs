using System.Collections.ObjectModel;
using System.Windows.Media;
using System.ComponentModel;

namespace OOP_Planning_App
{
    internal class ViewModel : INotifyPropertyChanged
    {
        #region Data Members
        private ObservableCollection<RectangleViewModel> rectangles = new ObservableCollection<RectangleViewModel>();
        #endregion Data Members

        public ViewModel()
        {
            var r1 = new RectangleViewModel(10, 10, 50, 40, Colors.Blue);
            rectangles.Add(r1);
            var r2 = new RectangleViewModel(70, 60, 50, 60, Colors.Green);
            rectangles.Add(r2);
            var r3 = new RectangleViewModel(150, 130, 55, 48, Colors.Purple);
            rectangles.Add(r3);
        }

        public ObservableCollection<RectangleViewModel> Rectangles
        {
            get
            {
                return rectangles;
            }
        }
        
        #region INotifyPropertyChanged Members

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion
    }
}