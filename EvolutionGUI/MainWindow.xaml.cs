using Evolution;
using Evolution.CreatureParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private List<Creature> _creatures;

        public MainWindow()
        {
            InitializeComponent();

            canvas.Width = Constants.WindowSize.X;
            canvas.Height = Constants.WindowSize.Y;

            _creatures = new List<Creature>();

            canvas.Loaded += SetCanvas;
            
            var Joint1 = new Joint(new Point(-350, 200));
            var Joint2 = new Joint(new Point(250, 200));
            Joint1.Direction = new Vector(20, 0);
            var Muscle1 = new Muscle(new Tuple<Joint, Joint>(Joint1, Joint2));
            var creature = new Creature(new[] { Joint1, Joint2 }, new[] { Muscle1 });
            _creatures.Add(creature);
            CompositionTarget.Rendering += Render;
        }

        private void SetCanvas(object sender, EventArgs args)
        {
            canvas.ForceCursor = true;
            canvas.Cursor = Cursors.Cross;
            canvas.SetCoordinateSystem(-canvas.Width / 2, canvas.Width / 2, -canvas.Height / 2, canvas.Height / 2);
        }

        private void PaintBackground(Canvas canvas)
        {
            var sky = new Rectangle();
            sky.Width = canvas.Width;
            sky.Height = canvas.Height;
            sky.Fill = Brushes.SkyBlue;

            var ground = new Rectangle();
            ground.Width = canvas.Width;
            ground.Height = canvas.Height * Constants.GroundHeight;
            ground.Fill = Brushes.LawnGreen;

            Canvas.SetTop(sky, -canvas.Height / 2);
            Canvas.SetLeft(sky, -canvas.Width / 2);

            Canvas.SetTop(ground, Constants.GroundOffset - Constants.WindowSize.Y * Constants.GroundHeight);
            Canvas.SetLeft( ground, - canvas.Width / 2);

            canvas.Children.Add(sky);
            canvas.Children.Add(ground);
        }

        private void Render(object sender, EventArgs args)
        {
            canvas.Children.Clear();

            PaintBackground(canvas);
            var sim = new Simulation();
            foreach (var creature in _creatures)
            {
                foreach (var joint in creature.Joints)
                {
                    var orb = new Ellipse();
                    orb.Fill = Brushes.Black;
                    orb.Height = joint.Diameter;
                    orb.Width = joint.Diameter;

                    Canvas.SetLeft(orb, joint.Position.X);
                    Canvas.SetTop(orb, joint.Position.Y);

                    canvas.Children.Add(orb);
                }
            }

            foreach (var creature in _creatures)
            {
                sim.Simulate(creature);
            }
        }
    }
}
