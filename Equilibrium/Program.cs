using System;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////
    //                                    My Classes                                       //
    /////////////////////////////////////////////////////////////////////////////////////////
    public class Speciement //класс химических элементов
    {
        public string Name, AtomicSF, Phase, string1, string2, string3, string4; //имя, формула и фаза элемента
        public double LowT, HighT, CommonT, molecular_mass; // нижняя, верхняя температуры, температура точки сшивки и молекулярная масса
        public bool Using = true;
    }
    public class Atom //класс атомов
    {
        public string Name; //имя атома
        public double Weihgt; // атомный вес
        public bool ChemUse; //выводить ли в chem.inp
    }

    public class EquilOut //параметроы из файла equil.out
    {
        public double[] molarInlet = new double[0];
        public double[] molarOutlet = new double[0];
        public double[] massInlet = new double[0];
        public double[] massOutlet = new double[0];
    }


}
