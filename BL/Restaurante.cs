using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Restaurante
    {

        private readonly DL.RestauranteContext _context;

        public Restaurante(DL.RestauranteContext context)
        {
            _context = context;
        }

        public ML.Result GetAllRestaurante()
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.SPGetAll
                    .FromSqlRaw("EXEC RestauranteGetAll")
                    .ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Restaurante restaurante = new ML.Restaurante();

                        restaurante.IdRestaurante = item.IdRestaurante;
                        restaurante.Nombre = item.Nombre;
                        //restaurante.Logo = item.Logo;
                        restaurante.FechaApertura = item.FechaApertura;
                        restaurante.FechaCierre = item.FechaCierre;

                        // Dirección (solo ID)
                        restaurante.Direccion = new ML.Direccion();
                        restaurante.Direccion.Calle = item.Calle;
                        restaurante.Direccion.NumeroInterior = item.NumeroInterior;
                        restaurante.Direccion.NumeroExterior = item.NumeroExterior;

                        // Colonia
                        restaurante.Direccion.Colonia = new ML.Colonia();
                        restaurante.Direccion.Colonia.Nombre = item.NombreColonia;
                        restaurante.Direccion.Colonia.CodigoPostal = item.CodigoPostal;

                        // Municipio
                        restaurante.Direccion.Colonia.Municipio = new ML.Municipio();
                        restaurante.Direccion.Colonia.Municipio.Nombre = item.NombreMunicipio;

                        // Estado
                        restaurante.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        restaurante.Direccion.Colonia.Municipio.Estado.Nombre = item.NombreEstado;

                        result.Objects.Add(restaurante);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron registros.";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result Delete(int idRestaurante)
        {
            ML.Result result = new ML.Result();

            try
            {
                int rowsAffected = _context.Database.ExecuteSqlRaw(
                    "EXEC RestauranteDelete @IdRestaurante = {0}",
                    idRestaurante);

                result.Correct = rowsAffected > 0;

                if (!result.Correct)
                {
                    result.ErrorMessage = "No se pudo eliminar el registro.";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result GetById(int idRestaurante)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.SPGetAll
                    .FromSqlRaw("EXEC RestauranteGetById @IdRestaurante = {0}", idRestaurante)
                    .AsEnumerable()
                    .FirstOrDefault();

                if (query != null)
                {
                    ML.Restaurante restaurante = new ML.Restaurante();

                    restaurante.IdRestaurante = query.IdRestaurante;
                    restaurante.Nombre = query.Nombre;
                    restaurante.FechaApertura = query.FechaApertura;
                    restaurante.FechaCierre = query.FechaCierre;

                    restaurante.Direccion = new ML.Direccion();
                    restaurante.Direccion.IdDireccion = query.IdRestaurante;
                    restaurante.Direccion.Calle = query.Calle;
                    restaurante.Direccion.NumeroExterior = query.NumeroExterior;
                    restaurante.Direccion.NumeroInterior = query.NumeroInterior;

                    restaurante.Direccion.Colonia = new ML.Colonia();
                    restaurante.Direccion.Colonia.Nombre = query.NombreColonia;

                    restaurante.Direccion.Colonia.Municipio = new ML.Municipio();
                    restaurante.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;

                    restaurante.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    restaurante.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;

                    result.Object = restaurante;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontró el restaurante.";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }



    }
}
