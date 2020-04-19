using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using Microsoft.Win32;
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

namespace TestUtilitatsGrafico
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabItem_KeyDown(object sender, KeyEventArgs e)
        {
            BitmapAnimated bmpAnimated;
           
            OpenFileDialog opnFile = new OpenFileDialog();
            opnFile.Multiselect = true;
            if (opnFile.ShowDialog().Value)
            {
                bmpAnimated = new BitmapAnimated(opnFile.FileNames.Select((pathBmp) => new System.Drawing.Bitmap(pathBmp)).ToArray(), 500);
                bmpAnimated.NumeroDeRepeticionesFijas = 2;
                bmpAnimated.FrameASaltarAnimacionCiclica = 0;
                bmpAnimated.SaltarFramePrimerCiclo = false;
                bmpAnimated.FrameAlAcabar = 0;
                bmpAnimated.FrameChanged += (s, m)=>{
                    Action act;
                    act = () => img.SetImage(m);
                    Dispatcher.BeginInvoke(act);
                 };
                bmpAnimated.Start();
            }
        }
    }
}
