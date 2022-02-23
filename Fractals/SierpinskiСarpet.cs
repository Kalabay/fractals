using System;
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

namespace Fractals
{
    /// <summary>
    /// Класс для ковра Серпинского.
    /// </summary>
    class SierpinskiСarpet : Fractal
    {
        /// <summary>
        /// Конструктор для присваивания значений.
        /// </summary>
        /// <param name="placeToDraw">форма, где надо рисовать.</param>
        /// <param name="depth">глубина фрактала.</param>
        public SierpinskiСarpet(MainWindow placeToDraw, int depth) : base(placeToDraw, depth) { }

        /// <summary>
        /// Метод для проверки корректности глубины.
        /// </summary>
        /// <returns>некорректна ли глубина.</returns>
        public override bool DepthCheck()
        {
            return depth > 4;
        }

        /// <summary>
        /// Нарисовать один квадрат.
        /// </summary>
        /// <param name="pointFirst">левая верхняя вершина.</param>
        /// <param name="pointSecond">правая нижняя вершина.</param>
        private void DrawSquare(Point pointFirst, Point pointSecond)
        {
            Polygon polygonDraw = new Polygon();
            polygonDraw.Points.Add(pointFirst);
            polygonDraw.Points.Add(new Point(pointSecond.X, pointFirst.Y));
            polygonDraw.Points.Add(pointSecond);
            polygonDraw.Points.Add(new Point(pointFirst.X, pointSecond.Y));
            polygonDraw.Stroke = Brushes.DeepPink;
            polygonDraw.Fill = Brushes.DeepPink;
            placeToDraw.canvas.Children.Add(polygonDraw);
        }

        /// <summary>
        /// Рекурсивный метод, который рисует 
        /// для ковра одну итерацию и вызывает следующие.
        /// </summary>
        /// <param name="pointFirst">левая верхняя вершина.</param>
        /// <param name="pointSecond">правая нижняя вершина.</param>
        /// <param name="depthNow">глубина данной итерации.</param>
        private void DrawСarpetIter(Point pointFirst, Point pointSecond, int depthNow)
        {
            if (depth == depthNow)
            {
                DrawSquare(pointFirst, pointSecond);
            }
            Point[] points = new Point[14];
            // Получение точек внутри и на границе квадрата.
            points[0] = pointFirst;
            points[1] = new Point(pointFirst.X + (pointSecond.X - pointFirst.X) / 3, pointFirst.Y);
            points[2] = new Point(pointFirst.X + 2 * (pointSecond.X - pointFirst.X) / 3, pointFirst.Y);
            points[3] = new Point(pointFirst.X, pointFirst.Y + (pointSecond.Y - pointFirst.Y) / 3);
            points[4] = new Point(pointFirst.X + (pointSecond.X - pointFirst.X) / 3,
                pointFirst.Y + (pointSecond.Y - pointFirst.Y) / 3);
            points[5] = new Point(pointFirst.X + 2 * (pointSecond.X - pointFirst.X) / 3,
                pointFirst.Y + (pointSecond.Y - pointFirst.Y) / 3);
            points[6] = new Point(pointSecond.X, pointFirst.Y + (pointSecond.Y - pointFirst.Y) / 3);
            points[7] = new Point(pointFirst.X, pointFirst.Y + 2 * (pointSecond.Y - pointFirst.Y) / 3);
            points[8] = new Point(pointFirst.X + (pointSecond.X - pointFirst.X) / 3,
                pointFirst.Y + 2 * (pointSecond.Y - pointFirst.Y) / 3);
            points[9] = new Point(pointFirst.X + 2 * (pointSecond.X - pointFirst.X) / 3,
                pointFirst.Y + 2 * (pointSecond.Y - pointFirst.Y) / 3);
            points[10] = new Point(pointSecond.X, pointFirst.Y + 2 * (pointSecond.Y - pointFirst.Y) / 3);
            points[11] = new Point(pointFirst.X + (pointSecond.X - pointFirst.X) / 3, pointSecond.Y);
            points[12] = new Point(pointFirst.X + 2 * (pointSecond.X - pointFirst.X) / 3, pointSecond.Y);
            points[13] = new Point(pointSecond.X, pointSecond.Y);
            if (depth != depthNow)
            {
                // Вызов более мелких квадратов.
                DrawСarpetIter(points[0], points[4], depthNow + 1);
                DrawСarpetIter(points[1], points[5], depthNow + 1);
                DrawСarpetIter(points[2], points[6], depthNow + 1);
                DrawСarpetIter(points[3], points[8], depthNow + 1);
                DrawСarpetIter(points[5], points[10], depthNow + 1);
                DrawСarpetIter(points[7], points[11], depthNow + 1);
                DrawСarpetIter(points[8], points[12], depthNow + 1);
                DrawСarpetIter(points[9], points[13], depthNow + 1);
            }
        }

        /// <summary>
        /// Метод для отображения фрактала.
        /// </summary>
        public override void Draw()
        {
            double midX = placeToDraw.canvas.ActualWidth / 2;
            double midY = placeToDraw.canvas.ActualHeight / 2;
            double leftX = midX - 150;
            double rightX = midX + 150;
            double upY = midY - 150;
            double downY = midY + 150;
            DrawСarpetIter(new Point(leftX, upY), new Point(rightX, downY), 0);
        }
    }
}