using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Drawing;

namespace LinearRegression
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random rnd = new Random();
        public MainWindow()
        {

            InitializeComponent();

            double x_offset = grid.Width / 2;
            double y_offset = grid.Height / 2;

            #region Add coordinate system
            Line Yaxis = new Line();
            Yaxis.X1 = 0 + x_offset;
            Yaxis.Y1 = 0;
            Yaxis.Y2 = grid.Height;
            Yaxis.X2 = 0 + x_offset;
            Yaxis.Stroke = Brushes.Black;
            Yaxis.StrokeThickness = 1;
            grid.Children.Add(Yaxis);

            Line Xaxis = new Line();
            Xaxis.X1 = 0;
            Xaxis.Y1 = y_offset;
            Xaxis.Y2 = y_offset;
            Xaxis.X2 = grid.Width;
            Xaxis.Stroke = Brushes.Black;
            Xaxis.StrokeThickness = 1;
            grid.Children.Add(Xaxis);
            #endregion

            double slope = rnd.Next(1, 20);
            double intercept = rnd.Next(1, 20);
            List<Data> datas = File.ReadAllLines("Experience-Salary.csv").Skip(1).Select(x => new Data(x)).ToList(); // reading in dataset

            // adding data points to coordinate system
            for (int i = 0; i < datas.Count; i++)
            {
                Line l = new Line();
                l.X1 = datas[i].MonthsWorked + x_offset;
                l.Y1 = datas[i].MoneyReceived;
                l.X2 = datas[i].MonthsWorked + x_offset;
                l.Y2 = datas[i].MoneyReceived + 1;
                l.Stroke = Brushes.Black;
                l.StrokeThickness = 1;
                grid.Children.Add(l);
            }

            //DEPENDENT = y
            double sum_Money = datas.Sum(x => x.MoneyReceived);

            // INDEPENDENT = x
            double sum_monthsWorked = datas.Sum(x => x.MonthsWorked);
            double sum_MonthsTimesMoney = datas.Sum(x => (double)x.MonthsTimesMoney);
            double sum_MonthsWorkedSquared = 0.0;
            for (int i = 0; i < datas.Count; i++)
            {
                sum_MonthsWorkedSquared += Math.Pow(datas[i].MonthsWorked, 2);
            }

            // calculating the regression line
            double newSlope = (datas.Count * sum_MonthsTimesMoney - sum_monthsWorked * sum_Money) / (datas.Count * sum_MonthsWorkedSquared - Math.Pow(sum_Money, 2));
            double newIntercept = (sum_Money - newSlope * sum_monthsWorked) / datas.Count;

            // adding regression line
            for (int i = 0; i < datas.Count; i++)
            {
                datas[i].slope = newSlope;
                datas[i].intercept = newIntercept;
                datas[i].Calculate_Y();
                Line regresionLine = new Line();
                regresionLine.X1 = datas[i].MonthsWorked + x_offset;
                regresionLine.Y1 = datas[i].Predicted_Money;
                regresionLine.X2 = datas[i].MonthsWorked + x_offset;
                regresionLine.Y2 = datas[i].Predicted_Money + 1;
                regresionLine.Stroke = Brushes.Red;
                regresionLine.StrokeThickness = 1;
                grid.Children.Add(regresionLine);
            }



        }
    }
}
