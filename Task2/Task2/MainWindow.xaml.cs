﻿// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Task2
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using Business_Layer.Services;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IService<Polygon> figureService;
        private PointCollection clickedPoints = new PointCollection();
        public ObservableCollection<Polygon> polygons = new ObservableCollection<Polygon>();
        private List<Line> lines = new List<Line>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.figureService = new FigureService();
            polygonesList.ItemsSource = polygons;
        }

        private void NewCanvas(object sender, RoutedEventArgs e)
        {
            this.ClearCanvas();
            this.figureService.RemoveAll();
            this.Background = Brushes.White;
        }

        private void SaveCanvas(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text file (*.xml)|*.xml";
            dialog.ShowDialog();
            string path = System.IO.Path.GetFullPath(dialog.FileName);
            this.figureService.SerealizeAll(path);
        }

        private void OpenCanvas(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text file (*.xml)|*.xml";
            dialog.ShowDialog();
            string fullPath = System.IO.Path.GetFullPath(dialog.FileName);
            var polygons = this.figureService.DeserializeAll(fullPath);
            this.ClearCanvas();
            this.Background = Brushes.White;
            foreach (var polygon in polygons)
            {
                this.DrawFigure(polygon);
            }
        }

        private void MouseClick(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            this.clickedPoints.Add(p);
            if (this.clickedPoints.Count > 1)
            {
                this.DrawLine();
            }

            if (this.clickedPoints.Count == 6)
            {
                this.lines.Clear();
                Polygon polygon = this.CreatePolygon();
                this.DrawFigure(polygon);
                this.clickedPoints.Clear();
            }
        }

        private void DrawFigure(Polygon polygon)
        {
            this.polygons.Add(polygon);
            this.Main.Children.Add(polygon);
            //this.ShapesMenu.Items.Add(polygon);
        }

        private Polygon CreatePolygon()
        {
            Polygon polygon = new Polygon();
            polygon.Points = this.clickedPoints.Clone();
            this.figureService.Add(polygon);
            ChooseColorModal modal = new ChooseColorModal();
            modal.ShowDialog();
            polygon.Fill = new SolidColorBrush(modal.ChosenColor);
            polygon.StrokeThickness = 4;
            polygon.Stroke = new SolidColorBrush(Colors.Black);
            return polygon;
        }

        private void DrawLine()
        {
            int lastPointIndex = this.clickedPoints.Count - 1;
            var line = new Line();
            line.Stroke = Brushes.Black;

            line.X1 = this.clickedPoints[lastPointIndex].X;
            line.X2 = this.clickedPoints[lastPointIndex - 1].X;
            line.Y1 = this.clickedPoints[lastPointIndex].Y;
            line.Y2 = this.clickedPoints[lastPointIndex - 1].Y;
            this.lines.Add(line);
            this.Main.Children.Add(line);
        }

        private void ClearCanvas()
        {
            this.Main.Children.Clear();
            this.polygons.Clear();
            this.lines.Clear();
            this.clickedPoints.Clear();
            this.Main.Visibility = Visibility.Visible;
            this.Hint.Visibility = Visibility.Collapsed;
        }

        private void polygonesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
