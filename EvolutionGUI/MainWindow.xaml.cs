using Evolution.CreatureParts;
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

namespace EvolutionGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var canvas = (Canvas)FindName("canvas");

            canvas.ForceCursor = true;
            canvas.Cursor = Cursors.Cross;
            canvas.Background = new SolidColorBrush(Colors.Red);
            
            var Joint1 = new Joint(new Point(200, 200));
            /*var Joint2 = new Joint(new Point(200, 400));
            var Muscle1 = new Muscle(new Tuple<Joint, Joint>(Joint1, Joint2));
            var creature = new Creature(new[] { Joint1, Joint2 }, new[] { Muscle1 });

            foreach(var joint in creature.Joints)
            {
                var orb = new Ellipse();
                orb.Height = joint.Diameter;
                orb.Width = joint.Diameter;

                Canvas.SetTop(orb, joint.Position.Y);
                Canvas.SetLeft(orb, joint.Position.X);
            }*/
        }
    }
}
