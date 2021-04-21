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
using Enterprice_incidents.Ef;
using static Enterprice_incidents.Ef.DataClass;
using Enterprice_incidents.Windows;

namespace Enterprice_incidents.Windows
{
    /// <summary>
    /// Логика взаимодействия для CreationOfNewIncident.xaml
    /// </summary>
    public partial class CreationOfNewIncident : Window
    {
        Incident selectIncident = new Incident();

        Worker selectWorker = new Worker();
        public CreationOfNewIncident()
        {
            InitializeComponent();

            selectWorkerListView.ItemsSource = DataClass.Context.Worker.ToList();

            this.Width = 400;

            List<Incident> incidentsList = DataClass.Context.Incident.ToList();
            chooseIncident_Cmb.ItemsSource = incidentsList;
            chooseIncident_Cmb.DisplayMemberPath = "IncidentName";
            chooseIncident_Cmb.SelectedIndex = 100;


            List<Incident_Type> incidentTypesList = DataClass.Context.Incident_Type.ToList();
            chooseIncidentType_Cmb.ItemsSource = incidentTypesList;
            chooseIncidentType_Cmb.DisplayMemberPath = "ImportanceOfIncident";
            chooseIncidentType_Cmb.SelectedIndex = 100;
        }

        private void chooseIncident_Cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void chooseIncidentType_Cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void search_Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterWorker();
        }

        private void FilterWorker()
        {
            var searchListWorker = Context.Worker.ToList();
            selectWorkerListView.ItemsSource = searchListWorker.Where(i => i.GetFIO.ToLower().Contains(search_Box.Text.ToLower()));
        }

        private void choosePerson_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Width = 1100;
        }

        private void cleanWorker_Btn_Click(object sender, RoutedEventArgs e)
        {
            responsibleWorker_Box.Text = null;
        }

        private void saveIncident_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (responsibleWorker_Box.Text == null)
            {
                MessageBox.Show("Выберите сотрудника!", "Сотрудник не выбран", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (chooseIncident_Cmb.SelectedItem == null)
            {
                MessageBox.Show("Укажите инцидент!", "Инцидент не выбран", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (chooseIncidentType_Cmb.SelectedItem == null)
            {
                MessageBox.Show("Укажите тип инцидента!", "Тип инцидента не выбран", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (incidentDate_Picker.SelectedDate == default)
            {
                MessageBox.Show("Укажите дату!", "Дата не выбрана", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataClass.Context.Incidents_History.Add(new Incidents_History()
            {
                IdIncident = chooseIncidentType_Cmb.SelectedIndex + 1,  //selectIncident.Id,
                IdWorker = selectWorker.Id,
                DateOfIncident = incidentDate_Picker.SelectedDate.Value.Date,
                IncidentName = chooseIncident_Cmb.Text,
                ImportanceOfIncident = chooseIncidentType_Cmb.Text,
                Description = description_Box.Text
            });

            try
            {
                DataClass.Context.SaveChanges();
                MessageBox.Show("Запись об инциденте создана.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            } 
        }

        private void cancelIncident_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void selectWorker_Ok_Btn_Click(object sender, RoutedEventArgs e)
        {
            selectWorker = selectWorkerListView.SelectedItem as Worker;

            if (selectWorker == null)
            {
                MessageBox.Show("Сотрудник не выбран.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (selectWorker == selectWorkerListView.SelectedItem as Worker)
            {
                responsibleWorker_Box.Text = selectWorker.FIO;

                this.Width = 400;
            } 
        }

        private void selectWorker_Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Width = 400;
        }
    }
}
