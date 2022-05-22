using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sem7Jaramillo.Models;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace sem7Jaramillo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
       
        private SQLiteAsyncConnection con;
        IEnumerable<Estudiante> Rupdate;
        IEnumerable<Estudiante> Rdelete;

        public int idSeleccionado;
        public string nombreSeleccionado;
        public string usuarioSeleccionado;
        public string contrasenaSeleccionado;

        public Elemento(int id, string nombre, string usuario, string contrasena)
        {
            InitializeComponent();
            idSeleccionado = id;
            nombreSeleccionado = nombre;
            usuarioSeleccionado = usuario;
            contrasenaSeleccionado = contrasena;
            con = DependencyService.Get<Database>().GetConnection();
            txtNombre.Text = nombre;
            txtUsuario.Text = usuario;
            txtContrasena.Text = contrasena;
        }

        public static IEnumerable<Estudiante> Delete(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("DELETE FROM Estudiante WHERE Id = ?", id);
        }

        public static IEnumerable<Estudiante> Update(SQLiteConnection db, string nombre, string usuario, string contrasena, int id)
        {
            return db.Query<Estudiante>("UPDATE Estudiante SET Nombre = ?, Usuario = ?, Contrasena = ? WHERE Id = ?", nombre, usuario, contrasena, id);
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                Rupdate = Update(db, txtNombre.Text, txtUsuario.Text, txtContrasena.Text, idSeleccionado);
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                Rdelete = Delete(db, idSeleccionado);
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}