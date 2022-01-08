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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatabaseModel;
using System.Data.Entity; //for Load()
using System.Data; //for DataException


namespace Cordea_Anamaria_Proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
        {
            New,
            Edit,
            Delete,
            Nothing
}
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource animalVSource;
        CollectionViewSource medicVSource;
        CollectionViewSource animalProgramariVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource animalViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("animalViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // animalViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource medicViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("medicViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // medicViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource animalProgramariViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("animalProgramariViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // animalViewSource.Source = [generic data source]

            animalVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("animalViewSource")));
            animalVSource.Source = ctx.Animals.Local;
            ctx.Animals.Load();

            //animalProgramariVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("animalProgramariViewSource")));
            //animalProgramariVSource.Source = ctx.Appointments.Local;
            //BindDataGrid();
            medicVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("medicViewSource")));
            medicVSource.Source = ctx.Medics.Local;
            ctx.Medics.Load();

            animalProgramariVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("animalProgramariViewSource")));
            //animalProgramariVSource.Source = ctx.Programari.Local;
            ctx.Appointments.Load();
            //ctx.Medics.Load();

            cmbAnimale.ItemsSource = ctx.Animals.Local;
            //cmbAnimale.DisplayMemberPath = "Rasa";
            cmbAnimale.SelectedValuePath = "AnimalId";

            cmbMedici.ItemsSource = ctx.Medics.Local;
            //cmbMedici.DisplayMemberPath = "Nume";
            cmbMedici.SelectedValuePath = "MedicId";

            BindDataGrid();
            System.Windows.Data.CollectionViewSource programariViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("programariViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // programariViewSource.Source = [generic data source]
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            BindingOperations.ClearBinding(problemaTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(rasaTextBox, TextBox.TextProperty);
            SetValidationBinding();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(problemaTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(rasaTextBox, TextBox.TextProperty);
            SetValidationBinding();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }
        private void btnNexta_Click(object sender, RoutedEventArgs e)
        {
            animalVSource.View.MoveCurrentToNext();
        }
        private void btnPreviousa_Click(object sender, RoutedEventArgs e)
        {
            animalVSource.View.MoveCurrentToPrevious();
        }

        private void SaveAnimale()
        {
            Animale animal = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Animale entity
                    animal = new Animale()
                    {
                        Rasa = rasaTextBox.Text.Trim(),
                        Tip = tipTextBox.Text.Trim(),
                        Varsta = varstaTextBox.Text.Trim(),
                        Problema = problemaTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Animals.Add(animal);
                    animalVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    animal = (Animale)animaleDataGrid.SelectedItem;
                    animal.Rasa = rasaTextBox.Text.Trim();
                    animal.Tip = tipTextBox.Text.Trim();
                    animal.Varsta = varstaTextBox.Text.Trim();
                    animal.Problema = problemaTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    animal = (Animale)animaleDataGrid.SelectedItem;
                    ctx.Animals.Remove(animal);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                animalVSource.View.Refresh();
            }
        }
        private void btnNextm_Click(object sender, RoutedEventArgs e)
        {
            medicVSource.View.MoveCurrentToNext();
        }
        private void btnPreviousm_Click(object sender, RoutedEventArgs e)
        {
            medicVSource.View.MoveCurrentToPrevious();
        }
        private void SaveMedici()
        {
            Medici medic = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Medici entity
                    medic = new Medici()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim(),
                        Zi = ziTextBox.Text.Trim(),
                        Ora = oraTextBox.Text.Trim(),
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Medics.Add(medic);
                    medicVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    medic = (Medici)mediciDataGrid.SelectedItem;
                    medic.Nume = numeTextBox.Text.Trim();
                    medic.Prenume = prenumeTextBox.Text.Trim();
                    medic.Zi = ziTextBox.Text.Trim();
                    medic.Ora = oraTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    medic = (Medici)mediciDataGrid.SelectedItem;
                    ctx.Medics.Remove(medic);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                medicVSource.View.Refresh();
            }
        }
        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tabCtrlDatabase.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Animale":
                    SaveAnimale();
                    break;
                case "Medici":
                    SaveMedici();
                    break;
                case "Programari":
                    SaveProgramari();
                    break;
            }
            ReInitialize();
        }
        private void SaveProgramari()
        {
            Programari programare = null;
            if (action == ActionState.New)
            {
                try
                {
                    Animale animal = (Animale)cmbAnimale.SelectedItem;
                    Medici medic = (Medici)cmbMedici.SelectedItem;
                    //instantiem Programari entity
                    programare = new Programari()
                    {

                        AnimalId = animal.AnimalId,
                        MedicId = medic.MedicId
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Appointments.Add(programare);
                    //salvam modificarile
                    ctx.SaveChanges();
                    BindDataGrid();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                dynamic selectedProgramari = programariDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedProgramari.ProgramareId;
                    var editedProgramari = ctx.Appointments.FirstOrDefault(s => s.ProgramareId == curr_id);
                    if (editedProgramari != null)
                    {
                        editedProgramari.AnimalId = Int32.Parse(cmbAnimale.SelectedValue.ToString());
                        editedProgramari.MedicId = Convert.ToInt32(cmbMedici.SelectedValue.ToString());
                        //salvam modificarile
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                // pozitionarea pe item-ul curent
                animalProgramariVSource.View.MoveCurrentTo(selectedProgramari);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedOrder = programariDataGrid.SelectedItem;
                    int curr_id = selectedOrder.ProgramareId;
                    var deletedProgramari = ctx.Appointments.FirstOrDefault(s => s.ProgramareId == curr_id);
                    if (deletedProgramari != null)
                    {
                        ctx.Appointments.Remove(deletedProgramari);
                        ctx.SaveChanges();
                        MessageBox.Show("Programari Deleted Successfully", "Message");
                        BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        
        private void BindDataGrid()
        {
            var queryOrder = from pgm in ctx.Appointments
                             join anm in ctx.Animals on pgm.AnimalId equals
                             anm.AnimalId
                             join mdc in ctx.Medics on pgm.MedicId
                 equals mdc.MedicId
                             select new
                             {
                                 pgm.ProgramareId,
                                 pgm.MedicId,
                                 pgm.AnimalId,
                                 anm.Rasa,
                                 anm.Tip,
                                 anm.Varsta,
                                 anm.Problema,
                                 mdc.Nume,
                                 mdc.Prenume,
                                 mdc.Zi,
                                 mdc.Ora
                             };
            animalProgramariVSource.Source = queryOrder.ToList();
        }
        private void SetValidationBinding()
        {
            Binding problemaValidationBinding = new Binding();
            problemaValidationBinding.Source = animalVSource;
            problemaValidationBinding.Path = new PropertyPath("Problema");
            problemaValidationBinding.NotifyOnValidationError = true;
            problemaValidationBinding.Mode = BindingMode.TwoWay;
            problemaValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //string required
            problemaValidationBinding.ValidationRules.Add(new StringNotEmpty());
            problemaTextBox.SetBinding(TextBox.TextProperty, problemaValidationBinding);

            Binding rasaValidationBinding = new Binding();
            rasaValidationBinding.Source = animalVSource;
            rasaValidationBinding.Path = new PropertyPath("Rasa");
            rasaValidationBinding.NotifyOnValidationError = true;
            rasaValidationBinding.Mode = BindingMode.TwoWay;
            rasaValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //string min length validator
            rasaValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            rasaTextBox.SetBinding(TextBox.TextProperty, rasaValidationBinding); //setare binding nou
        }
    }

}
