using System;
using System.Security.Cryptography.X509Certificates;

namespace Prim
{
    //  Clase Vertice
    public class Vertice
    {
        String nombre;
        int numVertice;
        public Vertice(String x)
        {
            nombre = x;
            numVertice = - 1;
        }

        /* RETORNA EL NOMBRE DEL VERTICE */
        public String nomVertice()
        {
            return nombre;
        }

        /* COMPARA EL VERTICE ACTUAL CON EL RECIBIDO EN EL PARAMETRO */
        public Boolean equals(Vertice n)
        {
            return nombre.Equals(n.nombre);
        }

        /* ASIGNA AL NUMERO DE VERTICE EL VALOR QUE RECIBE EL PARAMETRO */

        public void asigVert(int n)
        {
            numVertice = n;
        }
    }

    //  Clase GrafMatPeso
    public class GrafMatPeso
    {
        public static int INFINITO = 0xFFFF;
        private int[][] matPeso;
        private Vertice[] verts;
        private int numVerts;

        public GrafMatPeso(int mx)
        {
            matPeso = new int[mx][]; //[mx][mx]
            verts = new Vertice[mx];
            for (int i = 0; i < mx; i++)
            {
                for(int j = 0; j < mx; j++)
                {
                    matPeso[i][j] = INFINITO;
                }
                numVerts = 0;
            }
        }

        /* CREA UN NUEVO VERTICE RECIBIENDO SU NOMBRE DE PARAMETRO */
        public void nuevoVertice(String nom)
        {
            Boolean esta = numVertice(nom) >= 0;
            if (!esta)
            {
                Vertice v = new Vertice(nom);
                v.asigVert(numVerts);
                verts[numVerts++] = v;
            }
        }

        /* RETORNA EL VALOR DEL PESO ENTRE LAS ARISTAS INGRESADOS */
        public int pesoArco(String a, String b)
        {
            int va, vb;
            va = numVertice(a);
            vb = numVertice(b);
            return matPeso[va][vb];
        }

        /* RETORNA EL NUMERO DE VERTICES */
        public int numeroDeVertices()
        {
            return numVerts;
        }

        /* RETORNA EL ARRAY DE VERTICES */
        public Vertice[] vertices()
        {
            return verts;
        }

        /* CREA UN NUEVO ARCO ENTRE DOS VERTICES CON UN PESO ESPECIFICADO */
        public void nuevoArco(String a, String b, int Peso)
        {
            int va, vb;
            va = numVertice(a);
            vb = numVertice(b);
            matPeso[va][vb] = Peso;
        }

        /* VALIDA QUE EXISTA EL VERTICE CON EL NOMBRE QUE RECIBE EL PARAMETRO */
        public int numVertice(String vs)
        {
            Vertice v = new Vertice(vs);
            Boolean encontrado = false;
            int i = 0;
            for(; (i<numVerts) && !encontrado;)
            {
                encontrado = verts[i].equals(v);
                if (!encontrado) i++;
            }
            return (i < numVerts) ? i : -1;
        }

        /* RETORNA L AMTRIZ DE PESOS */
        public int[][] getMatPeso()
        {
            return matPeso;
        }
    }

    //  Clase Algoritmo Prim
    public class AlgoritmoPrim
    {
        private int[][] Pesos;
        private int n;  // Vertices origen y numero de vertices
        private Vertice[] vertices;
        int cont;


        public AlgoritmoPrim(GrafMatPeso gp, Vertice[] verts)
        {
            n = gp.numeroDeVertices();
            Pesos = gp.getMatPeso();
            vertices = verts;
            cont = 0;
        }

        public int arbolExpansionPrim()
        {
            int longMin, menor;
            int z;
            int[] coste = new int[n];
            int[] masCerca = new int[n];
            Boolean[] w = new Boolean[n];
            for(int i = 0; i < n; i++)
            {
                w[i] = false; // conjunto vacio
            }
            longMin = 0;
            w[0] = true; // se parte del vertice 0

            /* inicialmente, coste[i] es la arista (0, i) */
            for(int i = 1; i < n; i++)
            {
                coste[i] = Pesos[0][i];
                masCerca[i] = 0;
            }
            
            for(int i = 1; i < n; i++)
            {/* se busca vertice z de V-W mas cercano, de menor longitud de arista, a algun vertice de W */
                menor = coste[1];
                z = 1;

                for (int j = 2; j < n; j++)
                {
                    if (coste[j] < menor)
                    {
                        menor = coste[j];
                        z = j;
                    }
                }
                longMin += menor;
                Console.WriteLine((++cont) + " Pasada: Vertice: " + vertices[masCerca[z]].nomVertice() + " -> " + "Vertices: " + vertices[z].nomVertice() + "    Peso: " + coste[z]);
                w[z] = true;
                coste[z] = GrafMatPeso.INFINITO;
                /* debido a la incorporacion de z, se ajusta coste[] para el resto de vertices */
                for(int j=1; j < n; j++)
                {
                    if ((Pesos[z][j] < coste[j] && !w[j]))
                    {
                        coste[j] = Pesos[z][j];
                        masCerca[j] = z;
                    }
                }
                
            }
            return longMin;
        }
    }

    

/* CLASE MAIN */

    class Main_Prim
    {
        static void Main(string[] args)
        {
            /* NUMERO DE VERTICES */
            int n = 7;

            /* INSTANCIAMOS EL OBJETO QUE VA A CONTENER EL GRAFO */
            GrafMatPeso gra = new GrafMatPeso(n);

            /* NOMBRES DE LAS ARISTAS */
            String a = "A";
            String b = "B";
            String c = "C";
            String d = "D";
            String e = "E";
            String f = "F";
            String g = "g";

            /* CREAMOS LOS VERTICES */
            gra.nuevoVertice(a);
            gra.nuevoVertice(b);
            gra.nuevoVertice(c);
            gra.nuevoVertice(d);
            gra.nuevoVertice(e);
            gra.nuevoVertice(f);
            gra.nuevoVertice(g);

            /* REALIZAMOS LOS ENLACES(NODO_INICAL, NODO_FINAL, PESO) */
            gra.nuevoArco(a, b, 7);
            gra.nuevoArco(b, a, 7);
            gra.nuevoArco(a, d, 5);
            gra.nuevoArco(d, a, 5);
            gra.nuevoArco(b, c, 8);
            gra.nuevoArco(c, b, 8);
            gra.nuevoArco(b, d, 9);
            gra.nuevoArco(d, b, 9);
            gra.nuevoArco(b, e, 7);
            gra.nuevoArco(e, b, 7);
            gra.nuevoArco(e, c, 5);
            gra.nuevoArco(c, e, 5);
            gra.nuevoArco(e, d, 15);
            gra.nuevoArco(d, e, 15);
            gra.nuevoArco(d, f, 6);
            gra.nuevoArco(f, d, 6);
            gra.nuevoArco(e, f, 8);
            gra.nuevoArco(f, e, 8);
            gra.nuevoArco(e, g, 9);
            gra.nuevoArco(g, e, 9);
            gra.nuevoArco(f, g, 11);
            gra.nuevoArco(g, f, 11);

            /* IMPRIMIR LOS VERTICES EXISTENTES EN EL GRAFO */
            Console.WriteLine("Vertices del grafo");
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine(gra.vertices()[i].nomVertice() + " ");
            }
            Console.WriteLine();

            /* INSTANCIAMOS EL OBJETO ArbolExpansionMinimo */
            AlgoritmoPrim arbol = new AlgoritmoPrim(gra, gra.vertices());
            Console.WriteLine("Costo del arbol minimo: " + arbol.arbolExpansionPrim());
        }
    }
}