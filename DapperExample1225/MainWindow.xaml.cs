using Dapper;
using DapperExample1225.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace DapperExample1225
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //GetAllCaller();


            var player = GetById(1);
            myDataGrid.ItemsSource = new List<Player> { player };
        }

        public async void GetAllCaller()
        {
           var players = await GetAllAsync();
           myDataGrid.ItemsSource = players;


        }

        public async Task<List<Player>> GetAllAsync()
        {
            List<Player> players = new List<Player>();
            var conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;

            using (var connection=new SqlConnection(conn))
            {
                var data = await connection.QueryAsync<Player>("SELECT Id,Name,Score,IsStar FROM Players");
                players = data.ToList();
            }
            return players;
        }

        public Player GetById(int id)
        {
            var conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
            using (var connection=new SqlConnection(conn))
            {
                var player = connection
                    .QueryFirstOrDefault<Player>("SELECT * FROM Players WHERE Id=@PId",new {PId=id });
                return player;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
