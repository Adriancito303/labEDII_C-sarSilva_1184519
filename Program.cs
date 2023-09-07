using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace lab1EDII_CésarSilva
{
    class insertar
    {
        public insertar(string nombre, string dpis, string fechas, string direcciones)
        {
            name = nombre;
            dpi = dpis;
            datebirth = fechas;
            address = direcciones;
        }
        public string name { get; set; }
        public string dpi { get; set; }
        public string datebirth { get; set; }
        public string address { get; set; }
    }
    class serializar
    {
        public serializar(string nombre, string dpis, string fechas, string direcciones)
        {
            name = nombre;
            dpi = dpis;
            datebirth = fechas;
            address = direcciones;
        }
        public string name { get; set; }
        public string dpi { get; set; }
        public string datebirth { get; set; }
        public string address { get; set; }
    }
    public class AVLNode
    {
        public int Data { get; set; }
        public int Height { get; set; }
        public AVLNode Left { get; set; }
        public AVLNode Right { get; set; }

        public AVLNode(int data)
        {
            Data = data;
            Height = 1;
            Left = null;
            Right = null;
        }
    }

    public class AVLTree
    {
        private AVLNode root;

        public AVLTree()
        {
            root = null;        // Constructor
        }

        public void Insert(int data)
        {
            root = Insert(root, data);         // Insertar un nodo en el árbol
        }

        private AVLNode Insert(AVLNode node, int data)
        {
            if (node == null)
            {
                return new AVLNode(data);
            }

            if (data < node.Data)
            {
                node.Left = Insert(node.Left, data);
            }
            else if (data > node.Data)
            {
                node.Right = Insert(node.Right, data);
            }
            else // Duplicados no permitidos en este ejemplo
            {
                return node;
            }

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));// Actualizar la altura del nodo actual

            int balance = GetBalance(node);// Verificar el balance y realizar rotaciones si es necesario

            if (balance > 1)// Casos de rotación
            {
               // if (data < node.Left.Data)
               // {
               //     return RotateRight(node);
               // }
               // else
               // {
               //     node.Left = RotateLeft(node.Left);
               //     return RotateRight(node);
               // }
            }

            if (balance < -1)
            {
                //if (data > node.Right.Data)
                //{
                //    return RotateLeft(node);
                //}
                //else
                //{
                //    node.Right = RotateRight(node.Right);
                //    return RotateLeft(node);
                //}
            }

            return node;
        }

        public void Delete(int data)
        {
            root = Delete(root, data);// Eliminar un nodo del árbol
        }

        private AVLNode Delete(AVLNode node, int data)
        {
            return null;
        }

        private int Height(AVLNode node)// Funciones de equilibrio y rotación
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;
        }
        
        private int GetBalance(AVLNode node)
        {
            if (node == null)
            {
                return 0;
            }
            return Height(node.Left) - Height(node.Right);
        }
        //
        //private AVLNode RotateRight(AVLNode y)
        //{
        //    // Implementar rotación a la derecha aquí
        //}
        //
        //private AVLNode RotateLeft(AVLNode x)
        //{
        //    // Implementar rotación a la izquierda aquí
        //}
    }

    class lectura
    {
        public lectura(string ope, string js)
        {
            operacion = ope;
            jsond = js;
        }
        public string operacion { get; set; }
        public string jsond { get; set; }
    }
    class Program
    {
        public static List<serializar> personas = new List<serializar>();
        public static List<lectura> jtotal = new List<lectura>();
        public static List<serializar> inserte = new List<serializar>();
        public static List<serializar> delete = new List<serializar>();
        public static List<serializar> patch = new List<serializar>();
        public static List<serializar> resultados = new List<serializar>();
        public static AVLTree arbol = new AVLTree();
        static void Main(string[] args)
        {
            bool volver = true;
            string opi;
            while (volver == true)
            {
                try
                {
                    volver = false;
                    Console.Clear();
                    int opcion;
                    string ruta;
                    Console.WriteLine("----------------------------MENÚ bY CÉSAR SILVA / 1184519----------------------------");
                    Console.WriteLine("------------------------------Ingrese opcion a realizar------------------------------");
                    Console.WriteLine("1. Operaciones a travez de un nuevo CSV");
                    Console.WriteLine("2. Agregar mas operaciones a travez de un CSV");
                    Console.WriteLine("3. Buscar un dato por nombre");
                    Console.WriteLine("4. Salir");
                    opcion = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    switch (opcion)
                    {
                        case 1: //Insertar valores por medio de csv
                            inserte.Clear();
                            Console.WriteLine("Inserte ruta absoluta del archivo");
                            ruta = Console.ReadLine();
                            string[] archivo = File.ReadAllLines(ruta);
                            foreach (var linea in archivo)
                            {
                                string[] valores = linea.Split(';'); //se divide el csv en 2 partes
                                lectura guardar = new(valores[0], valores[1]); //se llama a la lista guardar con los valores separados
                                jtotal.Add(guardar);//se agrega a la lista jtotal los valores guardados
                                                    //string cosa;
                                                    //cosa = JsonConvert.SerializeObject(lectura);

                                guardar.jsond = "[" + guardar.jsond + "]";//se compone el formato json que está en guardar
                                guardar.jsond = guardar.jsond.Replace("\"\"", "\"");
                                if (guardar.operacion == "INSERT")
                                {
                                    personas = JsonConvert.DeserializeObject<List<serializar>>(guardar.jsond);//se deserializa el json
                                    inserte.AddRange(personas);//se agregan los valores a la lista inserte
                                }
                                else if (guardar.operacion == "DELETE")
                                {
                                    personas = JsonConvert.DeserializeObject<List<serializar>>(guardar.jsond);//se deserializa el json
                                                                                                              //inserte.RemoveRange(personas);
                                    delete.AddRange(personas);
                                    foreach (var item in delete)
                                    {
                                        inserte.RemoveAll(inserte =>
                                                            inserte.name == item.name &&
                                                            inserte.dpi == item.dpi &&
                                                            inserte.address == item.address &&
                                                            inserte.datebirth == item.datebirth);
                                    }
                                    delete.Clear();
                                }
                                else if (guardar.operacion == "PATCH")
                                {
                                    personas = JsonConvert.DeserializeObject<List<serializar>>(guardar.jsond);//se deserializa el json
                                    patch.AddRange(personas);
                                    foreach (var item in patch)
                                    {
                                        var reemplazo = inserte.Find(inserte =>
                                                                                inserte.name == item.name &&
                                                                                inserte.dpi == item.dpi);
                                        if (item.address != null)
                                        {
                                            //reemplazo.datebirth = item.datebirth;
                                            reemplazo.address = item.address;
                                        }
                                        if (item.datebirth != null)
                                        {
                                            reemplazo.datebirth = item.datebirth;
                                            //reemplazo.address = item.address;
                                        }
                                        //if (reemplazo != null)
                                        //{
                                        //    reemplazo.datebirth = item.datebirth;
                                        //    reemplazo.address = item.address;
                                        //}
                                    }
                                    patch.Clear();
                                }
                                //AVLTree arbol = new AVLTree();
                                foreach (var item in inserte)
                                {
                                    if (int.TryParse(item.dpi, out int value))
                                    {
                                        arbol.Insert(value);
                                    }
                                }
                            }
                            Console.Clear();
                            //foreach (var item in inserte)
                            //{
                            //    Console.WriteLine(item.name);
                            //    Console.WriteLine(item.dpi);
                            //    Console.WriteLine(item.datebirth);
                            //    Console.WriteLine(item.address);
                            //}
                            Console.WriteLine("----------------------------LISTO----------------------------");
                            Console.WriteLine("----------------------Lectura exitosa------------------------");
                            Console.WriteLine("-------------operaciones realizada correctamente-------------");
                            Console.WriteLine("-----------------Volviendo al menú principal-----------------");
                            volver = true;
                            Console.ReadLine();
                            break;
                        case 2: //AGREGAR 2DO ARCHIVO CSV
                            Console.WriteLine("Inserte ruta absoluta del archivo");
                            ruta = Console.ReadLine();
                            string[] archivo2 = File.ReadAllLines(ruta);
                            foreach (var linea in archivo2)
                            {
                                string[] valores = linea.Split(';'); //se divide el csv en 2 partes
                                lectura guardar = new(valores[0], valores[1]); //se llama a la lista guardar con los valores separados
                                jtotal.Add(guardar);//se agrega a la lista jtotal los valores guardados
                                                    //string cosa;
                                                    //cosa = JsonConvert.SerializeObject(lectura);

                                guardar.jsond = "[" + guardar.jsond + "]";//se compone el formato json que está en guardar
                                guardar.jsond = guardar.jsond.Replace("\"\"", "\"");
                                if (guardar.operacion == "INSERT")
                                {
                                    personas = JsonConvert.DeserializeObject<List<serializar>>(guardar.jsond);//se deserializa el json
                                    inserte.AddRange(personas);//se agregan los valores a la lista inserte
                                }
                                else if (guardar.operacion == "DELETE")
                                {
                                    personas = JsonConvert.DeserializeObject<List<serializar>>(guardar.jsond);//se deserializa el json
                                                                                                              //inserte.RemoveRange(personas);
                                    delete.AddRange(personas);

                                    foreach (var item in delete)
                                    {
                                        inserte.RemoveAll(inserte =>
                                                            inserte.name == item.name &&
                                                            inserte.dpi == item.dpi &&
                                                            inserte.address == item.address &&
                                                            inserte.datebirth == item.datebirth);
                                    }
                                    delete.Clear();
                                }
                                else if (guardar.operacion == "PATCH")
                                {
                                    personas = JsonConvert.DeserializeObject<List<serializar>>(guardar.jsond);//se deserializa el json
                                    patch.AddRange(personas);
                                    foreach (var item in patch)
                                    {
                                        var reemplazo = inserte.Find(inserte =>
                                                                                inserte.name == item.name &&
                                                                                inserte.dpi == item.dpi);
                                        
                                        //else //if (reemplazo != null)
                                        //{
                                        //    reemplazo.datebirth = item.datebirth;
                                        //    reemplazo.address = item.address;
                                        //}
                                    }
                                    patch.Clear();
                                }
                                //AVLTree arbol = new AVLTree();
                                foreach (var item in inserte)
                                {
                                    if (int.TryParse(item.dpi, out int value))
                                    {
                                        arbol.Insert(value);
                                    }
                                }
                            }
                            Console.Clear();
                            //foreach (var item in inserte)
                            //{
                            //    Console.WriteLine(item.name);
                            //    Console.WriteLine(item.dpi);
                            //    Console.WriteLine(item.datebirth);
                            //    Console.WriteLine(item.address);
                            //}
                            Console.WriteLine("----------------------------LISTO----------------------------");
                            Console.WriteLine("----------------------Lectura exitosa------------------------");
                            Console.WriteLine("-------------operaciones realizada correctamente-------------");
                            Console.WriteLine("-----------------Volviendo al menú principal-----------------");
                            volver = true;
                            Console.ReadLine();
                            break; //
                        case 3: //BUSQUEDA
                            resultados.Clear();
                            AVLTree arbol1;
                            Console.WriteLine("----------------------Que desea buscar----------------------");
                            string busqueda = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("----------------------Sus Resultados----------------------");
                            foreach (var item in inserte)
                            {
                                if (item.name == busqueda)
                                {
                                    arbol1 = new AVLTree();
                                    Console.WriteLine($"name: {item.name}, dpi: {item.dpi}, datebith: {item.datebirth}, address: {item.address}");
                                    resultados.Add(item);
                                }
                            }
                            Console.WriteLine("");
                            Console.ReadLine();
                            string jsonl = "resultados.jsonl";
                            using (StreamWriter escritura = new StreamWriter(jsonl))
                            {
                                foreach (var resul in resultados)//arbol)
                                {
                                    string jsons = JsonConvert.SerializeObject(resul);
                                    escritura.WriteLine(jsons);
                                }
                            }
                            Console.WriteLine($"-Se guardaron exitosamente en un archivo llamado: { jsonl}-");
                            Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("-----------------¿Desea volver al menú principal?-----------------");
                            Console.WriteLine("---------Presione 1 para volver, u otra tecla para salir.---------");
                            opi = Console.ReadLine();
                            if (opi == "1")
                            {
                                volver = true;
                                Console.Clear();
                                Console.WriteLine("-----------------Volviendo al menú principal-----------------");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("----------------------------Adios----------------------------");
                                Console.ReadLine();
                            }
                            break;
                        case 4://SALIDA
                            Console.Clear();
                            Console.WriteLine("----------------------------Adios----------------------------");
                            Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("----------------------------Opcion invalida----------------------------");
                            Console.ReadLine();
                            volver = true;
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    volver = true;
                    Console.WriteLine("---------------------------ERROR----------------------------");
                    Console.WriteLine("------------------------favor revisar-----------------------");
                    Console.ReadLine();
                }
            }
        }
    }
}
