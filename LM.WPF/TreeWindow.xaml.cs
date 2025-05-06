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
using System.Windows.Shapes;

namespace Lemonui_wpf_test
{
    /// <summary>
    /// TreeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TreeWindow : Window
    {
        public TreeWindow()
        {
            InitializeComponent();

            MyList = new ListLeagueList()
            {
                new League {
                    Name="LeagueA",
                    Divisions=new List<Division>() {
                        new Division()
                        {
                            Name="DivisionA",
                            Teams=new List<Team>() {
                                new Team{Name="TeamA"},
                                new Team{Name="TeamB"},
                                new Team{Name="TeamC"}
                            }
                        },new Division()
                        {
                            Name="DivisionB",
                            Teams=new List<Team>() {
                                new Team{Name="TeamA"},
                                new Team{Name="TeamB"},
                                new Team{Name="TeamC"}
                            }
                        },

                    }
                },
                new League {
                    Name="LeagueB",
                    Divisions=new List<Division>() {
                        new Division()
                        {
                            Name="DivisionA",
                            Teams=new List<Team>() {
                                new Team{Name="TeamA"},
                                new Team{Name="TeamB"},
                                new Team{Name="TeamC"}
                            }
                        },new Division()
                        {
                            Name="DivisionB",
                            Teams=new List<Team>() {
                                new Team{Name="TeamA"},
                                new Team{Name="TeamB"},
                                new Team{Name="TeamC"}
                            }
                        },

                    }
                },

            };

            this.DataContext = this;
            TreeViewItem item = new TreeViewItem();
            //item.Header
        }

        public ListLeagueList MyList { get; set; }
    }




    public class Team
    {
        public string Name { get; set; }
    }
    public class Division
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
    }
    public class League
    {
        public string Name { get; set; }
        public List<Division> Divisions { get; set; }

    }
    public class ListLeagueList : List<League>
    {
    }
}
