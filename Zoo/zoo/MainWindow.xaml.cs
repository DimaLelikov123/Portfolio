﻿using System;
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

namespace zootopia
{


    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            User user = new User(GlobalVariables.proxy);
            var userlogin = login.Text;
            var userpassword = password1.Text;

            bool check = user.LoginUser(userlogin, userpassword);

            if (check)
            {
                ChooseAnAction nwc = new ChooseAnAction();
                Hide();
                nwc.Show();
                this.Close();

            }

        }

    }
}


