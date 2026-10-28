using UnityEngine;

public class Platillo
{
    public string nombre;
    public int costo;
    public float duracion;
    public int existencias;

    public Platillo(string nombre, int costo, float duracion, int existencias)
    {
        this.nombre = nombre;
        this.costo = costo;
        this.duracion = duracion;
        this.existencias = existencias;
    }

    public Tazon Crear(int numero)
    {
        if (existencias <= 0)
        {
            return null;
        }

        existencias = existencias - 1;
        return new Tazon(numero, nombre, costo, duracion);
    }
}
