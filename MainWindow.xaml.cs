using Infragistics.Controls.DataSource;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SQLiteVirtualDataSource_Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private SQLiteVirtualDataSource _dataSource;
        public SQLiteVirtualDataSource DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                OnPropertyChanged("DataSource");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var connection = new SQLiteAsyncConnection("chinook.db");

            var dataSource = new SQLiteVirtualDataSource();
            dataSource.Connection = connection;
            dataSource.TableExpression = "tracks left outer join albums on tracks.AlbumId = albums.AlbumId";
            dataSource.ProjectionType = typeof(Track);
            DataSource = dataSource;
        }
    }

    [Table("tracks")]
    public class Track
    {
        [PrimaryKey, AutoIncrement]
        public int TrackId { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public int AlbumId { get; set; }

        [Column("Title")]
        public string AlbumTitle { get; set; }

        public int MediaTypeId { get; set; }

        public int GenreId { get; set; }

        [MaxLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int Bytes { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
