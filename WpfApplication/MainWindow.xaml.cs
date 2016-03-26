using Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using Theater;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            const string add = "+";
            const string subtract = "-";
            const string divide = "/";
            const string multiply = "*";

            Setting setting = new Setting();
            Play.Setting = setting;
            Play.Script = Application.ScriptProvider.Script;

            Application.CastingProvider cp = new Application.CastingProvider();

            Play.Cast = cp.Actors;
            
            List<string> operators = new List<string>() { add, subtract, divide, multiply };

            Operator.ItemsSource = operators;

            Operator.Text = add;

            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += (b, x) =>
                {
                    var actionBlock = new ActionBlock<Event>((d) =>
                    {
                        Dispatcher.Invoke(() =>
                            {
                                Log.Items.Insert(0, d.Name);
                            });

                    }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = -1 });

                    LeftHand.TextChanged += (s, d) =>
                    {
                        Play.Setting.RegisterSource("Input", new Input(Convert.ToInt32("0" + LeftHand.Text), Convert.ToInt32("0" + RightHand.Text)));
                    };

                    RightHand.TextChanged += (s, d) =>
                    {
                        Play.Setting.RegisterSource("Input", new Input(Convert.ToInt32("0" + LeftHand.Text), Convert.ToInt32("0" + RightHand.Text)));
                    };

                    Dispatcher.Invoke(() =>
                    {
                        Play.Setting.RegisterSource("Operator", Operator.Text);
                    });

                    Operator.SelectionChanged += (s, d) =>
                    {
                        if (LeftHand.Text.Length > 0 && RightHand.Text.Length > 0)
                        {
                            Play.Setting.RegisterSource("Operator", d.AddedItems[0]);
                        }
                    };

                    Evaluate.Click += (s, d) =>
                    {
                        Play.Setting.RegisterSource("Evaluate", d);
                    };

                    Play.Setting.Bind("Value", true, (p) =>
                    {
                        Value.Content = p.Value;
                    });
                };

            bw.RunWorkerAsync();

            Play.Action();
        }
    }
}
