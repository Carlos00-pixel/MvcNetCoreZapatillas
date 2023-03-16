using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreZapatillas.Data;
using MvcNetCoreZapatillas.Models;

#region SQL SERVER
//VUESTRO PROCEDIMIENTO DE PAGINACION DE IMAGENES DE ZAPATILLAS
/*
    CREATE VIEW V_IMAGENES_ZAPATILLAS
    AS
	    SELECT CAST(
	    ROW_NUMBER() OVER (ORDER BY IDIMAGEN) AS INT) AS POSICION,
	    ISNULL(IDIMAGEN, 0) AS IDIMAGEN, IDPRODUCTO, IMAGEN 
	    FROM IMAGENESZAPASPRACTICA
    GO 

    CREATE PROCEDURE SP_IMAGENES_ZAPATILLAS
    (@POSICION INT, @IDPRODUCTO INT)
    AS
        SELECT POSICION,IDIMAGEN, IDPRODUCTO, IMAGEN
        FROM (SELECT CAST(
	    ROW_NUMBER() OVER (ORDER BY IDIMAGEN) AS INT) AS POSICION,
	    ISNULL(IDIMAGEN, 0) AS IDIMAGEN, IDPRODUCTO, IMAGEN
	    FROM imageneszapaspractica
        WHERE IDPRODUCTO = @IDPRODUCTO) as query
	    where POSICION >= @POSICION AND POSICION < (@POSICION + 1)
    GO
*/
#endregion

namespace MvcNetCoreZapatillas.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public List<Zapatilla> GetZapatillas()
        {
            return this.context.Zapatillas.ToList();
        }

        public Zapatilla FindZapatilla(int idZapatilla)
        {
            return this.context.Zapatillas.Where(x => x.IdProducto == idZapatilla).FirstOrDefault();
        }

        public List<ImagenZapatilla> FindImagenZapatilla(int idZapatilla)
        {
            return this.context.ImagenesZapatillas.Where(x => x.IdProducto == idZapatilla).ToList();
        }

        public int GetNumeroRegistrosVistaImagenZapatillas(int idzapatilla)
        {
            return this.context.VistaImagenZapatillas.Where(x => x.IdProducto == idzapatilla).Count();
        }

        public async Task<List<VistaImagenZapatilla>>
            GetVistaImagenZapatillasAsync(int posicion, int idZapatilla)
        {
            string sql = "SP_IMAGENES_ZAPATILLAS @POSICION, @IDPRODUCTO";
            SqlParameter pamposicion =
                new SqlParameter("@POSICION", posicion);
            SqlParameter pamzapatilla =
                new SqlParameter("@IDPRODUCTO", idZapatilla);
            var consulta =
            this.context.VistaImagenZapatillas.FromSqlRaw(sql, pamposicion, pamzapatilla);
            return await consulta.ToListAsync();

        }
    }
}
