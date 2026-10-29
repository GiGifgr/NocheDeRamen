using System.Collections.Generic;
using UnityEngine;

public class Cierre : Estado
{
    public override void Entrar(string anterior)
    {
        Dictionary<string, int> vendidos = Ctx.ObtenerOCrear<Dictionary<string, int>>("vendidos");

        foreach (KeyValuePair<string, int> producto in vendidos)
        {
            Debug.Log(producto.Key + ": " + producto.Value);
        }

        Debug.Log("Atendidos: " + Ctx.Obtener<int>("atendidos"));
        Debug.Log("Recaudado: " + Ctx.Obtener<int>("recaudado") + " pesos.");
        Debug.Log("Cancelados: " + Ctx.Obtener<int>("cancelados"));
        Debug.Log("Incidentes: " + Ctx.Obtener<int>("incidentes"));
    }
}