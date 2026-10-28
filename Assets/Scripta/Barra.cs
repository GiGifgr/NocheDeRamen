using System.Collections.Generic;
using UnityEngine;

public class Barra : Estado
{
    public override void Entrar(string anterior)
    {
        Debug.Log("En barra. No hay trabajo en curso.");
    }

    public override string AlRecibir(string trigger, int valor)
    {
        if (trigger == "ordenar")
        {
            Dictionary<int, Platillo> catalogo = Ctx.ObtenerOCrear<Dictionary<int, Platillo>>("catalogo");

            if (catalogo.ContainsKey(valor) == false)
            {
                Debug.Log("No existe ese id.");
                return null;
            }

            int numero = Ctx.Obtener<int>("consecutivo") + 1;
            Tazon renglon = catalogo[valor].Crear(numero);

            if (renglon == null)
            {
                Debug.Log("Producto agotado.");
                return null;
            }

            List<Tazon> orden = Ctx.ObtenerOCrear<List<Tazon>>("orden");
            orden.Add(renglon);
            Ctx.Fijar("consecutivo", numero);
            Debug.Log("Agregado: " + renglon.Describir());
            return "Cocinando";
        }

        if (trigger == "cerrar")
        {
            if (Ctx.Obtener<int>("atendidos") == 0)
            {
                Debug.Log("No se puede cerrar sin haber atendido.");
                return null;
            }

            return "Cierre";
        }

        return null;
    }
}