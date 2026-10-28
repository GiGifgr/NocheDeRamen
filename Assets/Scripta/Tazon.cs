using UnityEngine;

public class Tazon
{
    public int numero;
    public string nombre;
    public int costo;
    public float duracion;

    public Tazon(int numero, string nombre, int costo, float duracion)
    {
        this.numero = numero;
        this.nombre = nombre;
        this.costo = costo;
        this.duracion = duracion;
    }

    public string Describir()
    {
        return "#" + numero + " " + nombre + " ($" + costo + ")";
    }
}
