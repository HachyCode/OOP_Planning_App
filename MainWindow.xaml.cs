using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OOP_Planning_App
{
    public partial class MainWindow : Window
    {
        #region Data Members

        private bool isLeftMouseButtonDownOnWindow = false;

        private bool isDraggingSelectionRect = false;

        private Point origMouseDownPoint;

        private static readonly double DragThreshold = 5;

        private bool isLeftMouseDownOnRectangle = false;

        private bool isLeftMouseAndControlDownOnRectangle = false;

        private bool isDraggingRectangle = false;

        #endregion Data Members

        static public int rectangelIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private ViewModel ViewModel
        {
            get
            {
                return (ViewModel)this.DataContext;
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            var rectangle = (FrameworkElement)sender;
            var rectangleViewModel = (RectangleViewModel)rectangle.DataContext;

            isLeftMouseDownOnRectangle = true;

            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
               
                isLeftMouseAndControlDownOnRectangle = true;
            }
            else
            {
                
                isLeftMouseAndControlDownOnRectangle = false;

                if (this.listBox.SelectedItems.Count == 0)
                {
                    
                    this.listBox.SelectedItems.Add(rectangleViewModel);
                }
                else if (this.listBox.SelectedItems.Contains(rectangleViewModel))
                {
                    
                }
                else
                {
                    
                    this.listBox.SelectedItems.Clear();
                    this.listBox.SelectedItems.Add(rectangleViewModel);
                }
            }

            rectangle.CaptureMouse();
            origMouseDownPoint = e.GetPosition(this);

            e.Handled = true;
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isLeftMouseDownOnRectangle)
            {
                var rectangle = (FrameworkElement)sender;
                var rectangleViewModel = (RectangleViewModel)rectangle.DataContext;

                if (!isDraggingRectangle)
                {
                    
                    if (isLeftMouseAndControlDownOnRectangle)
                    {
                        
                        if (this.listBox.SelectedItems.Contains(rectangleViewModel))
                        {
                            
                            this.listBox.SelectedItems.Remove(rectangleViewModel);
                        }
                        else
                        {
                            
                            this.listBox.SelectedItems.Add(rectangleViewModel);
                        }
                    }
                    else
                    {
                        
                        if (this.listBox.SelectedItems.Count == 1 &&
                            this.listBox.SelectedItem == rectangleViewModel)
                        {
                            
                        }
                        else
                        {
                            
                            this.listBox.SelectedItems.Clear();
                            this.listBox.SelectedItems.Add(rectangleViewModel);
                        }
                    }
                }

                rectangle.ReleaseMouseCapture();
                isLeftMouseDownOnRectangle = false;
                isLeftMouseAndControlDownOnRectangle = false;

                e.Handled = true;
            }

            isDraggingRectangle = false;
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingRectangle)
            {
                
                Point curMouseDownPoint = e.GetPosition(this);
                var dragDelta = curMouseDownPoint - origMouseDownPoint;

                origMouseDownPoint = curMouseDownPoint;

                foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
                {
                    rectangle.X += dragDelta.X;
                    rectangle.Y += dragDelta.Y;
                }
            }
            else if (isLeftMouseDownOnRectangle)
            {
               
                Point curMouseDownPoint = e.GetPosition(this);
                var dragDelta = curMouseDownPoint - origMouseDownPoint;
                double dragDistance = Math.Abs(dragDelta.Length);
                if (dragDistance > DragThreshold)
                {
                    
                    isDraggingRectangle = true;
                }

                e.Handled = true;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                listBox.SelectedItems.Clear();

                isLeftMouseButtonDownOnWindow = true;
                origMouseDownPoint = e.GetPosition(this);

                this.CaptureMouse();

                e.Handled = true;
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                bool wasDragSelectionApplied = false;

                if (isDraggingSelectionRect)
                {
                    isDraggingSelectionRect = false;
                    ApplyDragSelectionRect();

                    e.Handled = true;
                    wasDragSelectionApplied = true;
                }

                if (isLeftMouseButtonDownOnWindow)
                {
                    isLeftMouseButtonDownOnWindow = false;
                    this.ReleaseMouseCapture();

                    e.Handled = true;
                }

                if (!wasDragSelectionApplied)
                {
                    listBox.SelectedItems.Clear();
                }
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingSelectionRect)
            {
                Point curMouseDownPoint = e.GetPosition(this);
                UpdateDragSelectionRect(origMouseDownPoint, curMouseDownPoint);

                e.Handled = true;
            }
            else if (isLeftMouseButtonDownOnWindow)
            {
                Point curMouseDownPoint = e.GetPosition(this);
                var dragDelta = curMouseDownPoint - origMouseDownPoint;
                double dragDistance = Math.Abs(dragDelta.Length);
                if (dragDistance > DragThreshold)
                {
 
                    isDraggingSelectionRect = true;
                    InitDragSelectionRect(origMouseDownPoint, curMouseDownPoint);
                }

                e.Handled = true;
            }
        }

        private void InitDragSelectionRect(Point pt1, Point pt2)
        {
            UpdateDragSelectionRect(pt1, pt2);

            dragSelectionCanvas.Visibility = Visibility.Visible;
        }

        private void UpdateDragSelectionRect(Point pt1, Point pt2)
        {
            double x, y, width, height;

            if (pt2.X < pt1.X)
            {
                x = pt2.X;
                width = pt1.X - pt2.X;
            }
            else
            {
                x = pt1.X;
                width = pt2.X - pt1.X;
            }

            if (pt2.Y < pt1.Y)
            {
                y = pt2.Y;
                height = pt1.Y - pt2.Y;
            }
            else
            {
                y = pt1.Y;
                height = pt2.Y - pt1.Y;
            }

            Canvas.SetLeft(dragSelectionBorder, x);
            Canvas.SetTop(dragSelectionBorder, y);
            dragSelectionBorder.Width = width;
            dragSelectionBorder.Height = height;
        }

        private void ApplyDragSelectionRect()
        {
            dragSelectionCanvas.Visibility = Visibility.Collapsed;

            double x = Canvas.GetLeft(dragSelectionBorder);
            double y = Canvas.GetTop(dragSelectionBorder);
            double width = dragSelectionBorder.Width;
            double height = dragSelectionBorder.Height;
            Rect dragRect = new Rect(x, y, width, height);

            dragRect.Inflate(width / 10, height / 10);

            listBox.SelectedItems.Clear();

            foreach (RectangleViewModel rectangleViewModel in this.ViewModel.Rectangles)
            {
                Rect itemRect = new Rect(rectangleViewModel.X, rectangleViewModel.Y, rectangleViewModel.Width, rectangleViewModel.Height);
                if (dragRect.Contains(itemRect))
                {
                    listBox.SelectedItems.Add(rectangleViewModel);
                }
            }
        }

        private void Add_Class(object sender, RoutedEventArgs e)
        {
            ViewModel.AddClassBox(250.0, Convert.ToDouble(250), Convert.ToDouble(50), Convert.ToDouble(40), Colors.Crimson, rectangelIndex);
            rectangelIndex++;
        }

        private void Button_Delet_Class_Box(object sender, RoutedEventArgs e)
        {
            int inttype = Convert.ToInt32(((OOP_Planning_App.RectangleViewModel)((System.Windows.FrameworkElement)sender).DataContext).Index);
            ViewModel.rectangles.Remove(ViewModel.rectangles[inttype]);
        }

        private void Button_Add_Parameter(object sender, RoutedEventArgs e)
        {
            StackPanel stack = new StackPanel();
            Label lab = new Label();
            lab.Content = "New Par";

            stack.Children.Add(lab);
            stack.SetValue(Grid.RowProperty, 2);
            stack.SetValue(Grid.ColumnProperty, 2);

            ((Grid) ((Button) sender).Parent).Children.Add(stack);
        }

        private void Button_Add_Method(object sender, RoutedEventArgs e)
        {

        }
    }
}
