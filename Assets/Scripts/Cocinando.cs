using System.Collections.Generic;
using UnityEngine;

public class Cocinando : Estado
{
    private float tiempo;
    private float espera;

    public override void Entrar(string anterior)
    {
        tiempo = 0;
        espera = 0;

        List<Tazon> orden = Ctx.ObtenerOCrear<List<Tazon>>("orden");

        foreach (Tazon renglon in orden)
        {
            espera += renglon.duracion;
        }

        Debug.Log("Espera total: " + espera + " segundos.");
    }

    public override string Actualizar(float dt)
    {
        List<Tazon> orden = Ctx.ObtenerOCrear<List<Tazon>>("orden");

        if (orden.Count == 0)
        {
            return "Barra";
        }

        tiempo += dt;

        if (tiempo >= espera)
        {
            return "Cuenta";
        }

        return null;
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
            espera += renglon.duracion;

            return null;
        }

        if (trigger == "cancelar")
        {
            List<Tazon> orden = Ctx.ObtenerOCrear<List<Tazon>>("orden");

            if (orden.Count == 0)
            {
                return "Barra";
            }

            Tazon renglon = orden[orden.Count - 1];

            orden.RemoveAt(orden.Count - 1);
            espera -= renglon.duracion;

            int cancelados = Ctx.Obtener<int>("cancelados");
            Ctx.Fijar("cancelados", cancelados + 1);

            if (orden.Count == 0)
            {
                return "Barra";
            }

            return null;
        }

        if (trigger == "incendio")
        {
            return "Incendio";
        }

        return null;
    }
}