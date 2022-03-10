using System.Collections.ObjectModel;
using System.Windows.Media;
using System.ComponentModel;

namespace OOP_Planning_App
{
    internal class ViewModel : INotifyPropertyChanged
    {
        #region Data Members
        public ObservableCollection<RectangleViewModel> rectangles = new ObservableCollection<RectangleViewModel>();
        #endregion Data Members

        public ViewModel()
        {
            AddClassBox(10, 10, 50, 40, Colors.DarkBlue, MainWindow.rectangelIndex);
            MainWindow.rectangelIndex++;
            AddClassBox(70, 60, 50, 60, Colors.DarkBlue, MainWindow.rectangelIndex);
            MainWindow.rectangelIndex++;
            AddClassBox(150, 130, 55, 48, Colors.DarkBlue, MainWindow.rectangelIndex);
        }

        public ObservableCollection<RectangleViewModel> Rectangles
        {
            get
            {
                return rectangles;
            }
        }

        public void AddClassBox(double x, double y, double width, double height, Color color, int index)
        {
            rectangles.Add(new RectangleViewModel(x, y, width, height, color, index));
        }

        public void DeletClassBox(int value)
        {
            rectangles.Remove(rectangles[value]);
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