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
    /// Базовый класс для всех фракталов.
    /// </summary>
    abstract class Fractal
    {
        /// <summary>
        /// Глубина фрактала.
        /// </summary>
        protected int depth;
        /// <summary>
        /// Форма, где надо рисовать.
        /// </summary>
        protected MainWindow placeToDraw;

        /// <summary>
        /// Конструктор для присваивания значений.
        /// </summary>
        /// <param name="newPlaceToDraw">форма, где надо рисовать.</param>
        /// <param name="newDepth">глубина фрактала.</param>
        public Fractal(MainWindow newPlaceToDraw, int newDepth)
        {
            placeToDraw = newPlaceToDraw;
            depth = newDepth;
        }

        /// <summary>
        /// Метод для поворота линии на угол.
        /// </summary>
        /// <param name="line">линия для поворота.</param>
        /// <param name="angle">угол поворота в градусах.</param>
        /// <returns>новая линия после поворота.</returns>
        protected Line Rotation(Line line, double angle)
        {
            Line lineNew = new Line();
            angle = Math.PI / 180 * angle;
            lineNew.X1 = line.X1;
            lineNew.Y1 = line.Y1;
            // Вычисления при помощи матрицы поворота.
            lineNew.X2 = line.X1 + (line.X2 - line.X1) * Math.Cos(angle)
                - (line.Y2 - line.Y1) * Math.Sin(angle);
            lineNew.Y2 = line.Y1 + (line.X2 - line.X1) * Math.Sin(angle)
                + (line.Y2 - line.Y1) * Math.Cos(angle);
            return lineNew;
        }

        /// <summary>
        /// Абстрактный метод для проверки корректности глубины.
        /// </summary>
        /// <returns>некорректна ли глубина.</returns>
        public abstract bool DepthCheck();

        /// <summary>
        /// Абстрактный метод для отображения фрактала.
        /// </summary>
        public abstract void Draw();
    }
}