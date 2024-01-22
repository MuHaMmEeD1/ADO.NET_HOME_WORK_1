using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Azure.Messaging;
using Microsoft.Data.SqlClient;

namespace ADO.NET_HOME_WORK_1
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        

        public SqlConnection sql { get; set; } = new SqlConnection("Data Source=DESKTOP-RV3QOTA\\SQLEXPRESS;Initial Catalog=\"Library \";Integrated Security=True;Trust Server Certificate=True;");
        public List<string> CT { get; set; } = new List<string>();
        public List<int> CT_index { get; set; } = new();
        public List<int> BK_index { get; set; } = new();
        public List<string> BK { get; set; } = new List<string>();
        public ObservableCollection<string> BK_2 { get => bK_2; set { bK_2 = value; OnPrCh(nameof(BK_2)); } }
        string ct_s;
        private ObservableCollection<string> bK_2 = new ObservableCollection<string>();



        public RealCommand QueriyCommand { get; set; }

        public string QueriString { get; set; }

        /// /////////////////////////////////////////////////////////////
     
        public event PropertyChangedEventHandler? PropertyChanged;

         public void OnPrCh([CallerMemberName] string str = null)
        {
            PropertyChanged!.Invoke(this, new PropertyChangedEventArgs(str));
        }

        /// ///////////////////////////////////////////////////////////////

        public string CT_String { get { return ct_s; } set { ct_s = value; BooksuTeinEt(); OnPrCh(); } }

        //public RealCommand C_Command { get; set; }
        //public RealCommand B_Command { get; set; }


        public MainWindowViewModel()
        {
            

            sql!.Open();

            QueriyCommand = new RealCommand(QueriyCommandCode);
            DataBaseniYenile();
        }

        ~MainWindowViewModel() { sql.Close(); }

        public void DataBaseniYenile() { 
        
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Categories SELECT * FROM Books", sql);
            SqlDataReader? sqlDataReader = sqlCommand.ExecuteReader();


            //while (sqlDataReader.Read())
            //{
            //    CT.Add(sqlDataReader[0].ToString() + " " + sqlDataReader[1].ToString());
            //    CT_index.Add(int.Parse(sqlDataReader[0].ToString()));
            //}

            //sqlDataReader.NextResult();


            //while (sqlDataReader.Read())
            //{
            //    BK.Add(sqlDataReader[0].ToString() + " " + sqlDataReader[1].ToString());
            //    BK_index.Add(int.Parse(sqlDataReader["Id_Category"].ToString()));
            //}
            int say = 1;
            do
            {
               
                while (sqlDataReader.Read())
                {
                    if (say == 1) {
                        CT.Add(sqlDataReader[0].ToString() + " " + sqlDataReader[1].ToString());
                        CT_index.Add(int.Parse(sqlDataReader[0].ToString()));
                    }
                    else if (say == 2)
                    {
                        BK.Add(sqlDataReader[0].ToString() + " " + sqlDataReader[1].ToString());
                        BK_index.Add(int.Parse(sqlDataReader["Id_Category"].ToString()));
                    }

                }
                say++;
            }
            while (sqlDataReader.NextResult());




        }

        public void BooksuTeinEt()
        {
            int index = -1;
            BK_2 = new ObservableCollection<string>();

            for (int i = 0; i < CT.Count; i++)
            {
                if(CT_String == CT[i]) { index = i; break; }
            }
            if (index != -1) {
                for (int i = 0; i < BK_index.Count; i++) {

                    if (CT_index[index] == BK_index[i])
                    {
                        BK_2.Add(BK[i]);
                    }
                }
            }
            //OnPrCh(nameof(BK_2));
           
        }


        public void QueriyCommandCode(object? par)
        {
            SqlCommand cmd = new SqlCommand(QueriString, sql);
            int a = cmd.ExecuteNonQuery();

        }








    }
}






