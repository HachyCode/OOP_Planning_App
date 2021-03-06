using System.ComponentModel;
using System.Windows.Media;
using System.Windows;
using System;
using System.Collections.Generic;

namespace OOP_Planning_App
{
    internal class RectangleViewModel : INotifyPropertyChanged
    {
        #region Data Members

        private double x = 0;

        private double y = 0;

        private double width = 0;

        private double height = 0;

        private Color color;

        private Point connectorHotspot;

        public double index { get; set; }

        public List<string> paremeters = new List<string>();

        #endregion Data Members
        public RectangleViewModel(double x, double y, double width, double height, Color color, double index)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
            this.index = index;

            paremeters.Add("parameter 1");
            paremeters.Add("parameter 2");
            paremeters.Add("parameter 3");
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (x == value)
                {
                    return;
                }

                x = value;

                OnPropertyChanged("X");
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (y == value)
                {
                    return;
                }

                y = value;

                OnPropertyChanged("Y");
            }
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width == value)
                {
                    return;
                }

                width = value;

                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height == value)
                {
                    return;
                }

                height = value;

                OnPropertyChanged("Height");
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color == value)
                {
                    return;
                }

                color = value;

                OnPropertyChanged("Color");
            }
        }

        public double Index
        {
            get
            {
                return index;
            }
            set
            {
                if (index == value)
                {
                    return;
                }

                index = value;

                OnPropertyChanged("Index");
            }
        }

        public Point ConnectorHotspot
        {
            get
            {
                return connectorHotspot;
            }
            set
            {
                if (connectorHotspot == value)
                {
                    return;
                }

                connectorHotspot = value;

                OnPropertyChanged("ConnectorHotspot");
            }
        }

        #region INotifyPropertyChanged Members

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void AddRectangle(int v1, int v2, int v3, int v4, object blue, double index)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}