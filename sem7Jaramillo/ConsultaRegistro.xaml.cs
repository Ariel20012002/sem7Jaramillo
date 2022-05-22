using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using sem7Jaramillo.Models;
using System.Collections.ObjectModel;

namespace sem7Jaramillo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {

        private SQLiteAsyncConnection con;
        private ObservableCollection<Estudiante> TablaEstudiantes;

        public ConsultaRegistro()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected async override void OnAppearing()
        {
            var ResulRegistros = await con.Table<Estudiante>().ToListAsync();
            TablaEstudiantes = new ObservableCollection<Estudiante>(ResulRegistros);
            listaUsuarios.ItemsSource = TablaEstudiantes;
            base.OnAppearing();

        }

        public async void get()
        {
            try
            {
                var resultado = await con.Table<Estudiante>().ToListAsync();
                TablaEstudiantes = new ObservableCollection<Estudiante>(resultado);
                listaUsuarios.ItemsSource = TablaEstudiantes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private void listaUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Estudiante)e.SelectedItem;
            var item = obj.Id.ToString();
            var ID = Convert.ToInt32(item);
            var nombreItem = obj.Nombre;
            string nombre = nombreItem.ToString();

            var usuarioItem = obj.Usuario;
            string usuario = usuarioItem.ToString();

            var contraseñaItem = obj.Contrasena;
            string contrasena = contraseñaItem.ToString();

            Navigation.PushAsync(new Elemento(ID, nombre, usuario, contrasena));
        }
    }
}