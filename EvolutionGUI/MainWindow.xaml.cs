using Evolution;
using Evolution.CreatureParts;
using Evolution.Timing;
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
        private Time _time;
        private bool _simulationEnabled = true;

        public MainWindow()
        {
            InitializeComponent();

            KeyDown += Refresh;

            canvas.Width = Constants.WindowSize.X;
            canvas.Height = Constants.WindowSize.Y;

            _creatures = new List<Creature>();

            canvas.Loaded += InitCanvas;

            _creatures.Add(CreateCreature());

            CompositionTarget.Rendering += Render;
            _time = new Time();
            _time.Start();
        }

        void Refresh(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R)
            {
                _creatures = new List<Creature> { CreateCreature() };
            }
            else if (e.Key == Key.S)
            {
                _simulationEnabled = !_simulationEnabled;
                _time.Toggle();
            }
        }

        private Creature CreateCreature()
        {
            var Joint1 = new Joint(new Point(-350, 200));
            var Joint2 = new Joint(new Point(100, 350));
            var Joint3 = new Joint(new Point(0, 0));
            Joint1.Direction = new Vector(0, 0);
            Joint2.Direction = new Vector(0, 0);
            var Muscle1 = new Muscle(new Tuple<Joint, Joint>(Joint1, Joint2));
            var Muscle2 = new Muscle(new Tuple<Joint, Joint>(Joint1, Joint3));
            var Muscle3 = new Muscle(new Tuple<Joint, Joint>(Joint3, Joint2));
            var creature = new Creature(new[] { Joint1, Joint2, Joint3 }, new[] { Muscle1, Muscle2, Muscle3 });
            return creature;
        }

        private void InitCanvas(object sender, EventArgs args)
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

        private void RenderJoints(Creature creature, Canvas canvas)
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

        private void RenderMuscles(Creature creature, Canvas canvas)
        {
            foreach (var muscle in creature.Muscles)
            {
                var line = new Line();
                line.Stroke = Brushes.OrangeRed;
                line.StrokeThickness = 10;

                var length = muscle.Joints.Item1.Position.DistanceFrom(muscle.Joints.Item2.Position);

                line.Width = length;

                line.X1 = muscle.Joints.Item1.Position.X;
                line.X2 = muscle.Joints.Item2.Position.X;
                line.Y1 = muscle.Joints.Item1.Position.Y;
                line.Y2 = muscle.Joints.Item2.Position.Y;

                Canvas.SetLeft(line, muscle.Joints.Item1.Diameter / 2);
                Canvas.SetTop(line, muscle.Joints.Item1.Diameter / 2);

                canvas.Children.Add(line);
            }
        }

        private void Render(object sender, EventArgs args)
        {
            canvas.Children.Clear();

            PaintBackground(canvas);
            var sim = new Simulation();
            foreach (var creature in _creatures)
            {
                RenderMuscles(creature, canvas);
                RenderJoints(creature, canvas);
            }

            RenderFpsCounter();

            if (_simulationEnabled)
            {
                foreach (var creature in _creatures)
                {
                    sim.Simulate(creature, _time);
                }
            }
        }

        private void RenderFpsCounter()
        {
            var tb = new TextBlock();
            tb.Foreground = Brushes.Black;
            tb.Background = Brushes.Wheat;
            var st = new ScaleTransform();
            st.ScaleY = -1;
            tb.RenderTransform = st;
            tb.Text = $"Elapsed time: {_time.ElapsedMilliseconds}";

            Canvas.SetTop(tb, Constants.WindowSize.Y / 2);
            Canvas.SetLeft(tb, - Constants.WindowSize.X / 2);

            canvas.Children.Add(tb);
        }
    }
}
