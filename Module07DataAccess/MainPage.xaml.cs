﻿using Module07DataAccess.Services;
using MySql.Data.MySqlClient;


namespace Module07DataAccess
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly DatabaseConnectionService _dbConnectionService;

        public MainPage()
        {
            InitializeComponent();

            _dbConnectionService = new DatabaseConnectionService();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnTestConnectionClicked(object sender, EventArgs e)
        {
            var connectionString = _dbConnectionService.GetConnectionString();
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    ConnectionStatuslabel.Text = "Connection Successful";
                    ConnectionStatuslabel.TextColor = Colors.Green;
                }
            }
            catch(Exception ex) 
            {
                ConnectionStatuslabel.Text = $"Connection Failed: {ex:Message}";
                ConnectionStatuslabel.TextColor = Colors.Red;
            }
        }

        private async void OnViewPersonal(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ViewPersonal");
        }
    }

}
